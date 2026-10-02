using System.Collections.Concurrent;
using System.Text;
using EstudoC.Core.Compilacao;
using EstudoC.Core.Depuracao;
using EstudoC.Core.Execucao;
using EstudoC.Core.Projetos;

namespace EstudoC.Core.Tests;

/// <summary>Compila, executa e depura de verdade com o GCC/GDB embutidos (vendor/w64devkit).</summary>
public class IntegracaoToolchainTests : IDisposable
{
    private readonly Toolchain.Toolchain _toolchain =
        Toolchain.Toolchain.Localizar() ?? throw new InvalidOperationException("Toolchain não encontrado: rode build/build.ps1 uma vez.");

    private readonly string _pasta = Path.Combine(Path.GetTempPath(), "estudoc-testes", Guid.NewGuid().ToString("N")[..8]);

    private Projeto CriarProjeto(string codigo)
    {
        Directory.CreateDirectory(_pasta);
        File.WriteAllText(Path.Combine(_pasta, "main.c"), codigo);
        return new Projeto(_pasta);
    }

    [Fact]
    public async Task CompilaEExecutaComEntrada()
    {
        var projeto = CriarProjeto("""
            #include <stdio.h>
            int main(void) {
                int a, b;
                printf("Digite dois números: ");
                scanf("%d %d", &a, &b);
                printf("soma=%d\n", a + b);
                return 0;
            }
            """);
        var r = await new CompiladorGcc(_toolchain).CompilarAsync(projeto);
        Assert.True(r.Sucesso, r.SaidaBruta);

        var (saida, resultado) = await ExecutarAsync(projeto, entrada: "2 3", esperarPrompt: "números: ");

        Assert.Contains("Digite dois números: ", saida);
        Assert.Contains("soma=5", saida);
        Assert.Equal(0, resultado.CodigoSaida);
    }

    [Fact]
    public async Task ErroDeSintaxeNaLinhaCerta()
    {
        var projeto = CriarProjeto("#include <stdio.h>\nint main(void) {\n    int x = 1\n    printf(\"%d\", x);\n    return 0;\n}\n");

        var r = await new CompiladorGcc(_toolchain).CompilarAsync(projeto);

        Assert.False(r.Sucesso);
        var erro = r.Diagnosticos.First(d => d.Severidade == Severidade.Erro);
        Assert.Equal(4, erro.Linha);
        Assert.Equal(Path.Combine(_pasta, "main.c"), erro.Arquivo, ignoreCase: true);
        Assert.Contains("ponto e vírgula", erro.Dica);
    }

    [Fact]
    public async Task VerificacaoUsaTextoNaoSalvo()
    {
        var projeto = CriarProjeto("int main(void) { return 0; }\n");
        var arquivo = Path.Combine(_pasta, "main.c");
        var emEdicao = new Dictionary<string, string> { [arquivo] = "int main(void) { return x; }\n" };

        var d = await new CompiladorGcc(_toolchain).VerificarAsync(projeto, emEdicao);

        var erro = Assert.Single(d, x => x.Severidade == Severidade.Erro);
        Assert.Equal(arquivo, erro.Arquivo);
        Assert.Equal(1, erro.Linha);
    }

    [Fact]
    public async Task CompilaProjetoComTad()
    {
        Directory.CreateDirectory(_pasta);
        var projeto = new Workspace(_pasta).Criar("tad", ModeloProjeto.Tad);

        var r = await new CompiladorGcc(_toolchain).CompilarAsync(projeto);
        Assert.True(r.Sucesso, r.SaidaBruta);

        var (saida, _) = await ExecutarAsync(projeto);
        Assert.Contains("Ponto: (1.0, 2.0)", saida);
    }

    [Fact]
    public async Task ExplicaFalhaDeSegmentacao()
    {
        var projeto = CriarProjeto("int main(void) { int *p = 0; *p = 1; return 0; }\n");
        Assert.True((await new CompiladorGcc(_toolchain).CompilarAsync(projeto)).Sucesso);

        var (_, resultado) = await ExecutarAsync(projeto);

        Assert.True(resultado.Falhou);
        Assert.Contains("segmentação", resultado.Explicacao);
    }

    [Fact]
    public async Task DepuraComPontoDeParadaPassosEVariaveis()
    {
        var projeto = CriarProjeto("""
            #include <stdio.h>
            int soma(int a, int b) { int r = a + b; return r; }
            int main(void)
            {
                int v[3] = {4, 5, 6};
                int s = soma(v[0], v[1]);
                printf("%d\n", s);
                return 0;
            }
            """);
        Assert.True((await new CompiladorGcc(_toolchain).CompilarAsync(projeto)).Sucesso);

        await using var dbg = new SessaoDepuracao(_toolchain);
        var paradas = new BlockingCollection<InfoParada>();
        var fim = new TaskCompletionSource<int?>();
        dbg.Parou += paradas.Add;
        dbg.Terminou += c => fim.TrySetResult(c);

        await dbg.IniciarAsync(projeto.Executavel, projeto.Pasta, [new PontoParada(Path.Combine(_pasta, "main.c"), 6)]);

        var p1 = Proxima(paradas);
        Assert.Equal(6, p1.Linha);
        var vars = await dbg.VariaveisAsync(0);
        Assert.Contains(vars, v => v.Nome == "v" && v.Valor == "{4, 5, 6}" && v.Tipo == "int [3]");

        await dbg.PassoDentroAsync();
        var p2 = Proxima(paradas);
        Assert.Equal("soma", p2.Funcao);
        Assert.Equal("9", await dbg.AvaliarAsync("a + b", 0));
        Assert.Equal(2, (await dbg.PilhaAsync()).Count);

        await dbg.PassoForaAsync();
        Assert.Equal("main", Proxima(paradas).Funcao);

        await dbg.ContinuarAsync();
        Assert.Equal(0, await fim.Task.WaitAsync(TimeSpan.FromSeconds(20)));
    }

    [Fact]
    public async Task DepuracaoParaNaLinhaDoSegfault()
    {
        var projeto = CriarProjeto("int main(void)\n{\n    int *p = 0;\n    *p = 1;\n    return 0;\n}\n");
        Assert.True((await new CompiladorGcc(_toolchain).CompilarAsync(projeto)).Sucesso);

        await using var dbg = new SessaoDepuracao(_toolchain);
        var paradas = new BlockingCollection<InfoParada>();
        dbg.Parou += paradas.Add;
        await dbg.IniciarAsync(projeto.Executavel, projeto.Pasta, []);

        var p = Proxima(paradas);
        Assert.True(p.ErroFatal);
        Assert.Equal(4, p.Linha);
        await dbg.PararAsync();
    }

    [Fact]
    public async Task PausaProgramaEmLacoInfinito()
    {
        var projeto = CriarProjeto("int main(void)\n{\n    volatile int i = 0;\n    while (1)\n        i++;\n    return 0;\n}\n");
        Assert.True((await new CompiladorGcc(_toolchain).CompilarAsync(projeto)).Sucesso);

        await using var dbg = new SessaoDepuracao(_toolchain);
        var paradas = new BlockingCollection<InfoParada>();
        dbg.Parou += paradas.Add;
        await dbg.IniciarAsync(projeto.Executavel, projeto.Pasta, []);
        await Task.Delay(1000);

        await dbg.PausarAsync();
        var p = Proxima(paradas);
        Assert.False(p.ErroFatal);
        Assert.Equal("main", p.Funcao);
        Assert.InRange(p.Linha, 4, 5);
        await dbg.PararAsync();
    }

    private static InfoParada Proxima(BlockingCollection<InfoParada> paradas) =>
        paradas.TryTake(out var p, TimeSpan.FromSeconds(20)) ? p : throw new TimeoutException("o depurador não parou");

    private async Task<(string Saida, ResultadoExecucao Resultado)> ExecutarAsync(Projeto projeto, string? entrada = null, string? esperarPrompt = null)
    {
        using var exec = new ExecutorPrograma();
        var saida = new StringBuilder();
        var fim = new TaskCompletionSource<ResultadoExecucao>();
        var prompt = new TaskCompletionSource();
        exec.SaidaRecebida += (t, _) =>
        {
            lock (saida)
            {
                saida.Append(t);
                if (esperarPrompt is not null && saida.ToString().Contains(esperarPrompt))
                    prompt.TrySetResult();
            }
        };
        exec.Terminou += r => fim.TrySetResult(r);
        exec.Iniciar(projeto.Executavel, projeto.Pasta, _toolchain);

        if (esperarPrompt is not null)
            await prompt.Task.WaitAsync(TimeSpan.FromSeconds(10)); // prova que o prompt chega ANTES da entrada
        if (entrada is not null)
            exec.EnviarLinha(entrada);

        var resultado = await fim.Task.WaitAsync(TimeSpan.FromSeconds(20));
        lock (saida)
            return (saida.ToString(), resultado);
    }

    public void Dispose()
    {
        try { Directory.Delete(_pasta, true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
    }
}
