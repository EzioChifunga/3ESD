<#
.SYNOPSIS
  Gera o instalador do Estudo C (um comando, tudo incluído).

.DESCRIPTION
  1. Garante o .NET SDK 10 (instala localmente em vendor/dotnet se faltar — sem admin).
  2. Baixa e confere o compilador w64devkit (GCC + GDB) em vendor/ (só na primeira vez).
  3. Roda os testes.
  4. Publica o app self-contained (não precisa de .NET instalado no destino).
  5. Embute o compilador (sem partes que não são de C) e a pasta material/.
  6. Testa o compilador empacotado (compila e roda um programa).
  7. Gera dist/release/EstudoC-win-Setup.exe (+ versão portátil .zip) com Velopack.

.EXAMPLE
  powershell -ExecutionPolicy Bypass -File build\build.ps1
  powershell -ExecutionPolicy Bypass -File build\build.ps1 -SemTestes -Versao 1.0.1
#>
param(
    [string]$Versao,
    [switch]$SemTestes
)

$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'   # Invoke-WebRequest fica muito mais rápido

$Raiz      = Split-Path $PSScriptRoot -Parent
$Vendor    = Join-Path $Raiz 'vendor'
$Dist      = Join-Path $Raiz 'dist'
$Publicado = Join-Path $Dist 'publish'
$Release   = Join-Path $Dist 'release'
$Projeto   = Join-Path $Raiz 'app\src\EstudoC.App\EstudoC.App.csproj'
$Testes    = Join-Path $Raiz 'app\tests\EstudoC.Core.Tests\EstudoC.Core.Tests.csproj'

function Etapa($texto) { Write-Host "`n==> $texto" -ForegroundColor Cyan }
function Executar([string]$exe, [string[]]$argumentos) {
    & $exe @argumentos
    if ($LASTEXITCODE -ne 0) { throw "Falhou ($LASTEXITCODE): $exe $($argumentos -join ' ')" }
}

New-Item -ItemType Directory -Force $Vendor | Out-Null

# ---------------------------------------------------------------- .NET SDK
Etapa '.NET SDK 10'
$dotnet = Join-Path $Vendor 'dotnet\dotnet.exe'
if (-not (Test-Path $dotnet)) {
    $sistema = Get-Command dotnet -ErrorAction SilentlyContinue
    if ($sistema -and ((& $sistema.Source --list-sdks) -match '^10\.')) {
        $dotnet = $sistema.Source
    } else {
        Write-Host 'Instalando o .NET SDK 10 em vendor\dotnet (local, sem admin)...'
        $instalador = Join-Path $Vendor 'dotnet-install.ps1'
        Invoke-WebRequest 'https://dot.net/v1/dotnet-install.ps1' -OutFile $instalador
        & $instalador -Channel 10.0 -InstallDir (Join-Path $Vendor 'dotnet') -NoPath
    }
}
$env:DOTNET_ROOT = Split-Path $dotnet -Parent
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_NOLOGO = '1'
Write-Host "Usando $dotnet"

if (-not $Versao) {
    $Versao = ([xml](Get-Content (Join-Path $Raiz 'app\Directory.Build.props'))).Project.PropertyGroup.Version
}
Write-Host "Versão: $Versao"

# ---------------------------------------------------------------- toolchain
Etapa 'Compilador embutido (w64devkit: GCC + GDB)'
$cfg = Get-Content (Join-Path $PSScriptRoot 'toolchain.json') -Raw | ConvertFrom-Json
$pastaToolchain = Join-Path $Vendor 'w64devkit'
$marcaVersao = Join-Path $pastaToolchain '.versao-estudoc'
if (-not ((Test-Path $marcaVersao) -and ((Get-Content $marcaVersao) -eq $cfg.versao))) {
    $pacote = Join-Path $Vendor "w64devkit-$($cfg.versao).7z.exe"
    if (-not (Test-Path $pacote)) {
        Write-Host "Baixando $($cfg.url)"
        Invoke-WebRequest $cfg.url -OutFile $pacote
    }
    $hash = (Get-FileHash $pacote -Algorithm SHA256).Hash.ToLower()
    if ($hash -ne $cfg.sha256) {
        Remove-Item $pacote
        throw "SHA256 não confere para $pacote (esperado $($cfg.sha256), obtido $hash)."
    }
    if (Test-Path $pastaToolchain) { Remove-Item $pastaToolchain -Recurse -Force }
    Write-Host 'Extraindo...'
    $p = Start-Process $pacote -ArgumentList "-o`"$Vendor`"", '-y' -Wait -PassThru -WindowStyle Hidden
    if ($p.ExitCode -ne 0) { throw "Falha ao extrair o w64devkit ($($p.ExitCode))." }
    Set-Content $marcaVersao $cfg.versao
}
Write-Host "OK: $pastaToolchain"

# ---------------------------------------------------------------- testes
if (-not $SemTestes) {
    Etapa 'Testes'
    Executar $dotnet @('test', $Testes, '-c', 'Release', '--nologo', '-v', 'q')
}

# ---------------------------------------------------------------- publicação
Etapa 'Publicando o aplicativo (self-contained, win-x64)'
if (Test-Path $Dist) { Remove-Item $Dist -Recurse -Force }
Executar $dotnet @('publish', $Projeto, '-c', 'Release', '-r', 'win-x64', '--self-contained', 'true',
                   '-p:PublishSingleFile=false', "-p:Version=$Versao", '-o', $Publicado, '--nologo', '-v', 'q')

Etapa 'Embutindo compilador e materiais'
$destinoToolchain = Join-Path $Publicado 'toolchain'
Copy-Item $pastaToolchain $destinoToolchain -Recurse
foreach ($padrao in $cfg.remover) {
    Get-Item (Join-Path $destinoToolchain $padrao) -ErrorAction SilentlyContinue |
        ForEach-Object { Remove-Item $_.FullName -Recurse -Force }
}
Remove-Item (Join-Path $destinoToolchain '.versao-estudoc') -ErrorAction SilentlyContinue
Copy-Item (Join-Path $Raiz 'material') (Join-Path $Publicado 'material') -Recurse

$tamanho = (Get-ChildItem $Publicado -Recurse -File | Measure-Object Length -Sum).Sum / 1MB
Write-Host ("Tamanho instalado: {0:N0} MB" -f $tamanho)

# ---------------------------------------------------------------- teste de fumaça
Etapa 'Testando o compilador empacotado'
$tmp = Join-Path ([IO.Path]::GetTempPath()) "estudoc-fumaca-$([guid]::NewGuid().ToString('N').Substring(0,8))"
New-Item -ItemType Directory $tmp | Out-Null
try {
    Set-Content (Join-Path $tmp 'ola.c') "#include <stdio.h>`n#include <stdlib.h>`n#include <math.h>`nint main(void){ int *v = malloc(3*sizeof(int)); v[0]=2; printf(`"ok %.0f\n`", pow(v[0], 3)); free(v); return 0; }`n"
    $envPath = $env:PATH
    $env:PATH = (Join-Path $destinoToolchain 'bin') + ';' + $env:PATH
    try {
        Executar (Join-Path $destinoToolchain 'bin\gcc.exe') @('-g', '-O0', '-Wall', (Join-Path $tmp 'ola.c'), '-o', (Join-Path $tmp 'ola.exe'), '-lm')
        $saida = & (Join-Path $tmp 'ola.exe')
        if ($saida -ne 'ok 8') { throw "Programa de teste devolveu '$saida'." }
        $gdb = & (Join-Path $destinoToolchain 'bin\gdb.exe') --version | Select-Object -First 1
        Write-Host "gcc OK ($saida) · $gdb"
    } finally { $env:PATH = $envPath }
} finally {
    Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
}

# ---------------------------------------------------------------- instalador
Etapa 'Gerando instalador (Velopack)'
$pastaFerramentas = Join-Path $Vendor 'tools'
$vpk = Join-Path $pastaFerramentas 'vpk.exe'
if (-not (Test-Path $vpk)) {
    Executar $dotnet @('tool', 'install', 'vpk', '--version', '1.2.161', '--tool-path', $pastaFerramentas)
}
Executar $vpk @('pack', '--packId', 'EstudoC', '--packVersion', $Versao, '--packDir', $Publicado,
                '--mainExe', 'EstudoC.exe', '--runtime', 'win-x64', '--packTitle', 'Estudo C', '--packAuthors', 'Estudo C',
                '--outputDir', $Release, '--delta', 'None')

Etapa 'Pronto'
Get-ChildItem $Release -File | Where-Object { $_.Name -match 'Setup|Portable' } |
    ForEach-Object { Write-Host ("{0,-40} {1,8:N0} MB" -f $_.Name, ($_.Length / 1MB)) }
Write-Host "`nCompartilhe: $(Join-Path $Release 'EstudoC-win-Setup.exe')" -ForegroundColor Green
