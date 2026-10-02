namespace EstudoC.Core.Projetos;

/// <summary>Um projeto C = uma pasta com arquivos .c e .h. Todos os .c são compilados juntos.</summary>
public sealed class Projeto
{
    public string Pasta { get; }
    public string Nome => Path.GetFileName(Pasta.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
    public string PastaBuild => Path.Combine(Pasta, "build");
    public string Executavel => Path.Combine(PastaBuild, Nome + ".exe");

    public Projeto(string pasta) => Pasta = Path.GetFullPath(pasta);

    public IReadOnlyList<string> ArquivosFonte() => Listar("*.c");

    public IReadOnlyList<string> Arquivos() =>
        Listar("*.c").Concat(Listar("*.h")).OrderBy(OrdemArquivo).ToList();

    private List<string> Listar(string padrao) =>
        Directory.Exists(Pasta)
            ? Directory.GetFiles(Pasta, padrao, SearchOption.TopDirectoryOnly).OrderBy(f => f, StringComparer.OrdinalIgnoreCase).ToList()
            : [];

    /// <summary>main.c primeiro, depois .h, depois os outros .c.</summary>
    private static string OrdemArquivo(string caminho)
    {
        var nome = Path.GetFileName(caminho).ToLowerInvariant();
        var grupo = nome == "main.c" ? "0" : nome.EndsWith(".h") ? "1" : "2";
        return grupo + nome;
    }
}
