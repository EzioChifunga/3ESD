using System.Text;
using EstudoC.Core.Compilacao;
using EstudoC.Core.Exemplos;
using EstudoC.Core.Execucao;
using EstudoC.Core.Projetos;

namespace EstudoC.Core.Tests;

/// <summary>Garante que todo exemplo de exemplos/ se comporta como o exemplo.json promete.</summary>
public class ExemplosTests
{
    private static readonly CatalogoExemplos Catalogo =
        new(CatalogoExemplos.Localizar() ?? throw new InvalidOperationException("exemplos/ não encontrado"));

    private static readonly Toolchain.Toolchain Toolchain =
        Core.Toolchain.Toolchain.Localizar() ?? throw new InvalidOperationException("Toolchain não encontrado");

    public static TheoryData<string> Ids()
    {
        var dados = new TheoryData<string>();
        foreach (var e in Catalogo.Todos)
            dados.Add(e.Id);
        return dados;
    }

    [Fact]
    public void TemExemplosDeCadaModo()
    {
        Assert.True(Catalogo.Todos.Count >= 15);
        foreach (var modo in Enum.GetValues<ModoExemplo>())
            Assert.Contains(Catalogo.Todos, e => e.Modo == modo);
        Assert.All(Catalogo.Todos, e => Assert.True(File.Exists(e.Readme), e.Id));
    }

    [Theory]
    [MemberData(nameof(Ids))]
    public async Task ExemploFuncionaComoDescrito(string id)
    {
        var exemplo = Catalogo.Todos.Single(e => e.Id == id);
        var pasta = Path.Combine(Path.GetTempPath(), "estudoc-exemplos", Guid.NewGuid().ToString("N")[..8]);
        try
        {
            var projeto = new Workspace(pasta).CriarDeExemplo(exemplo);
            var r = await new CompiladorGcc(Toolchain).CompilarAsync(projeto);

            if (!exemplo.Compila)
            {
                Assert.False(r.Sucesso);
                Assert.True(r.Erros >= 3, $"esperava vários erros, veio {r.Erros}");
                Assert.All(r.Diagnosticos.Where(d => d.Severidade == Severidade.Erro), d => Assert.NotNull(d.Dica));
                return;
            }

            Assert.True(r.Sucesso, r.SaidaBruta);
            Assert.Equal(0, r.Avisos);

            using var exec = new ExecutorPrograma();
            var saida = new StringBuilder();
            var fim = new TaskCompletionSource<ResultadoExecucao>();
            exec.SaidaRecebida += (t, _) => { lock (saida) saida.Append(t); };
            exec.Terminou += x => fim.TrySetResult(x);
            exec.Iniciar(projeto.Executavel, projeto.Pasta, Toolchain);
            foreach (var linha in (exemplo.Entrada ?? "").Split('\n', StringSplitOptions.RemoveEmptyEntries))
                exec.EnviarLinha(linha);
            exec.FecharEntrada();

            var resultado = await fim.Task.WaitAsync(TimeSpan.FromSeconds(20));
            if (exemplo.TerminaComErro)
                Assert.True(resultado.Falhou, "deveria terminar com erro de execução");
            else
                Assert.True(resultado.CodigoSaida == 0, $"código {resultado.CodigoSaida}; saída:\n{saida}");
        }
        finally
        {
            try { Directory.Delete(pasta, true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
        }
    }
}
