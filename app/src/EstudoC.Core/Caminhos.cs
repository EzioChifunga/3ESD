using System.Reflection;

namespace EstudoC.Core;

/// <summary>Pastas usadas pela aplicação.</summary>
public static class Caminhos
{
    /// <summary>Onde ficam os projetos do aluno (Documentos\EstudoC\projetos).</summary>
    public static string Projetos =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EstudoC", "projetos");

    /// <summary>Área livre de testes (Documentos\EstudoC\sandbox).</summary>
    public static string Sandbox =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EstudoC", "sandbox");

    /// <summary>Dados internos (runtime, verificação de sintaxe).</summary>
    public static string DadosLocais =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EstudoC", "dados");

    /// <summary>
    /// Pasta de materiais: "material/" ao lado do executável (instalado) ou na raiz do repositório (desenvolvimento).
    /// </summary>
    public static string? LocalizarMateriais(string? pastaBase = null)
    {
        foreach (var pasta in Ancestrais(pastaBase ?? AppContext.BaseDirectory))
        {
            var candidato = Path.Combine(pasta, "material");
            if (File.Exists(Path.Combine(candidato, "README.md")))
                return candidato;
        }
        return null;
    }

    public static IEnumerable<string> Ancestrais(string inicio)
    {
        for (var dir = new DirectoryInfo(inicio); dir != null; dir = dir.Parent)
            yield return dir.FullName;
    }

    /// <summary>Extrai um recurso embutido para DadosLocais (se mudou) e devolve o caminho.</summary>
    public static string ExtrairRecurso(string nome)
    {
        Directory.CreateDirectory(DadosLocais);
        var destino = Path.Combine(DadosLocais, nome);
        using var fluxo = Assembly.GetExecutingAssembly().GetManifestResourceStream("EstudoC.Recursos." + nome)
            ?? throw new InvalidOperationException($"Recurso não encontrado: {nome}");
        using var leitor = new StreamReader(fluxo);
        var conteudo = leitor.ReadToEnd();
        if (!File.Exists(destino) || File.ReadAllText(destino) != conteudo)
            File.WriteAllText(destino, conteudo);
        return destino;
    }
}
