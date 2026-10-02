# 3ESD — Estudo de Estrutura de Dados em C

**Estudo C** é um aplicativo para Windows, 100% offline, para estudar C: traz a trilha de materiais da disciplina e um ambiente para editar, compilar, executar e depurar programas. O compilador GCC e o depurador GDB já vêm dentro do aplicativo.

| Pasta | O que tem |
|---|---|
| [`material/`](material/README.md) | Trilha de estudo em Markdown (12 módulos, do início até a prova). É o conteúdo embutido no app. |
| [`app/`](app/) | Código do aplicativo (C# .NET 10 + Avalonia). |
| [`build/`](build/) | `build.ps1` (gera o instalador) e `toolchain.json` (versão fixa do GCC/GDB). |
| [`ferramentas/`](ferramentas/) | Scripts Python que converteram os PDFs/DOCX originais em `material/`. |
| `base/`, `convertido/`, `vendor/`, `dist/` | Fora do Git (originais, intermediários, dependências baixadas e saída do build). |

## Gerar o instalador (um comando)

```powershell
powershell -ExecutionPolicy Bypass -File build\build.ps1
```

Na primeira execução o script baixa o que falta para a pasta `vendor/` (não é preciso ser administrador):
- .NET SDK 10, se não estiver instalado;
- w64devkit 2.10 (GCC 16 + GDB 17), com o SHA256 conferido.

Depois ele roda os testes, publica o app de forma self-contained, embute o compilador e os materiais, testa o compilador empacotado e gera:

| Arquivo | Para quê |
|---|---|
| `dist\release\EstudoC-win-Setup.exe` (~145 MB) | **Instalador para compartilhar.** Instala para o usuário atual, sem pedir administrador, e cria atalhos. |
| `dist\release\EstudoC-win-Portable.zip` | Versão portátil: basta descompactar e abrir `Estudo C.exe`. |

Opções: `-SemTestes` pula os testes; `-Versao 1.0.1` define a versão (o padrão vem de `app/Directory.Build.props`).

No computador de destino não é preciso ter nada instalado: nem .NET, nem compilador, nem internet.

## O que o aplicativo faz

- **Trilha de estudo:** os módulos 01 a 12 ficam na barra lateral e abrem em abas. Os guias têm links clicáveis.
- **Busca em todos os materiais** (Ctrl+Shift+F): ignora acentos e maiúsculas e mostra módulo, arquivo e página do PDF original. Um clique abre o material na linha encontrada. Dentro de um texto, Ctrl+F procura só nele.
- **Projetos em C:** ficam em `Documentos\EstudoC\projetos`, com os modelos "programa simples" e "TAD (.h + .c + main)". O botão *Abrir como projeto* nos códigos do módulo 09 copia o TAD junto com as dependências que ele inclui. Por exemplo, `instante.h` traz `data` e `horario`.
- **Erros enquanto digita:** o código é verificado pelo GCC e os erros aparecem sublinhados, com explicação em português (ex.: "faltou ponto e vírgula na linha anterior", "faltou `#include <stdlib.h>`").
- **Executar** (Ctrl+F5): o programa roda no terminal embutido e a entrada funciona (`scanf`). Os prompts aparecem antes da leitura. Erros como falha de segmentação ou estouro de pilha são explicados.
- **Depurar** (F5): ponto de parada (clique na margem ou F9), Continuar (F5), Pausar (F6, inclusive em laço infinito), Próxima linha (F10), Entrar (F11), Sair (Shift+F11), variáveis com tipos, pilha de chamadas e expressões observadas (`*p`, `v[0]@5`, `lista->prox`). Em uma falha de segmentação, a execução para na linha exata.
  - Durante a depuração, a entrada e a saída do programa ficam numa janela de console separada. É uma limitação do GDB no Windows.

## Desenvolvimento

```powershell
# testes (unitários + integração real com GCC/GDB)
vendor\dotnet\dotnet.exe test app\tests\EstudoC.Core.Tests

# rodar o app em modo debug (uma pasta como argumento abre como projeto)
$env:DOTNET_ROOT = "$PWD\vendor\dotnet"
vendor\dotnet\dotnet.exe run --project app\src\EstudoC.App -- C:\caminho\do\projeto
```

O código é dividido assim:
- **`EstudoC.Core`**, sem interface: compilação (`ICompilador`), execução, depuração GDB/MI (`IDepurador`), materiais/busca e projetos.
- **`EstudoC.App`**: a interface em Avalonia.

Para trocar o compilador ou adicionar outra linguagem, basta criar uma nova implementação dessas interfaces.
