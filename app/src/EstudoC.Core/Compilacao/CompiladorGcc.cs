using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using EstudoC.Core.Projetos;

namespace EstudoC.Core.Compilacao;

public sealed class CompiladorGcc(Toolchain.Toolchain toolchain) : ICompilador
{
    private static readonly string[] OpcoesComuns =
    [
        "-std=gnu17", "-Wall",
        "-fdiagnostics-color=never", "-fno-diagnostics-show-caret", "-fno-diagnostics-show-option",
    ];

    public async Task<ResultadoCompilacao> CompilarAsync(Projeto projeto, CancellationToken ct = default)
    {
        var fontes = projeto.ArquivosFonte();
        if (fontes.Count == 0)
            return new ResultadoCompilacao(false,
                [new Diagnostico(null, 0, 0, Severidade.Erro, "O projeto não tem nenhum arquivo .c.", null)], "", null);

        Directory.CreateDirectory(projeto.PastaBuild);
        var runtime = Caminhos.ExtrairRecurso("estudoc_runtime.c");

        var args = new List<string>(OpcoesComuns) { "-g", "-O0", "-I", projeto.Pasta };
        args.AddRange(fontes);
        args.Add(runtime);
        args.AddRange(["-o", projeto.Executavel]);

        var (codigo, saida) = await ExecutarAsync(args, projeto.Pasta, ct);
        var diagnosticos = ParserDiagnosticos.Analisar(saida);

        if (codigo != 0 && !diagnosticos.Any(d => d.Severidade == Severidade.Erro))
        {
            var msg = saida.Contains("Permission denied", StringComparison.OrdinalIgnoreCase)
                ? "Não foi possível gravar o executável: o programa ainda está rodando? Pare a execução e tente de novo."
                : "A compilação falhou. Veja a saída do compilador.";
            diagnosticos.Add(new Diagnostico(null, 0, 0, Severidade.Erro, msg, null));
        }

        return new ResultadoCompilacao(codigo == 0, diagnosticos, saida, codigo == 0 ? projeto.Executavel : null);
    }

    public async Task<IReadOnlyList<Diagnostico>> VerificarAsync(
        Projeto projeto, IReadOnlyDictionary<string, string> conteudos, CancellationToken ct = default)
    {
        // Copia o projeto (com o texto ainda não salvo) para uma pasta-sombra e verifica lá.
        var hash = Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(projeto.Pasta.ToLowerInvariant())))[..12];
        var sombra = Path.Combine(Caminhos.DadosLocais, "verificacao", hash);
        if (Directory.Exists(sombra))
            Directory.Delete(sombra, recursive: true);
        Directory.CreateDirectory(sombra);

        var originalPorNome = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var arquivo in projeto.Arquivos())
        {
            var nome = Path.GetFileName(arquivo);
            originalPorNome[nome] = arquivo;
            var texto = conteudos.TryGetValue(arquivo, out var emEdicao) ? emEdicao : await File.ReadAllTextAsync(arquivo, ct);
            await File.WriteAllTextAsync(Path.Combine(sombra, nome), texto, ct);
        }

        var fontes = Directory.GetFiles(sombra, "*.c");
        if (fontes.Length == 0)
            return [];

        var args = new List<string>(OpcoesComuns) { "-fsyntax-only", "-I", sombra };
        args.AddRange(fontes);
        var (_, saida) = await ExecutarAsync(args, sombra, ct);

        return ParserDiagnosticos.Analisar(saida)
            .Select(d => d.Arquivo is not null && originalPorNome.TryGetValue(Path.GetFileName(d.Arquivo), out var original)
                ? d with { Arquivo = original }
                : d)
            .ToList();
    }

    private async Task<(int Codigo, string Saida)> ExecutarAsync(List<string> args, string pasta, CancellationToken ct)
    {
        var info = new ProcessStartInfo(toolchain.Gcc)
        {
            WorkingDirectory = pasta,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8,
        };
        foreach (var a in args)
            info.ArgumentList.Add(a);
        toolchain.PrepararAmbiente(info);

        using var processo = Process.Start(info) ?? throw new InvalidOperationException("Não foi possível iniciar o gcc.");
        await using var _ = ct.Register(() => { try { processo.Kill(true); } catch { /* já terminou */ } });
        var saida = processo.StandardOutput.ReadToEndAsync(ct);
        var erro = processo.StandardError.ReadToEndAsync(ct);
        await processo.WaitForExitAsync(ct);
        return (processo.ExitCode, (await saida) + (await erro));
    }
}
