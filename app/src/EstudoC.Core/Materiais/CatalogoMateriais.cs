using System.Text.RegularExpressions;

namespace EstudoC.Core.Materiais;

public enum TipoMaterial { Guia, Texto, Codigo }

/// <summary>Um arquivo de material (README do módulo, texto convertido ou código .c/.h).</summary>
public sealed record ItemMaterial(string Caminho, string Titulo, TipoMaterial Tipo, Modulo Modulo)
{
    public string NomeArquivo => Path.GetFileName(Caminho);
}

public sealed class Modulo
{
    public required string Pasta { get; init; }
    public required string Titulo { get; init; }
    public List<ItemMaterial> Itens { get; } = [];
    public ItemMaterial? Guia => Itens.FirstOrDefault(i => i.Tipo == TipoMaterial.Guia);
}

/// <summary>Lê a pasta material/: módulos (01-…, 02-…) e seus arquivos, com títulos tirados dos README.</summary>
public sealed partial class CatalogoMateriais
{
    public string Raiz { get; }
    public ItemMaterial Inicio { get; }
    public IReadOnlyList<Modulo> Modulos { get; }

    [GeneratedRegex(@"\[([^\]]+)\]\(([^)#\s]+)\)")]
    private static partial Regex RegexLink();

    [GeneratedRegex(@"^\d+-")]
    private static partial Regex RegexPrefixo();

    public CatalogoMateriais(string raiz)
    {
        Raiz = Path.GetFullPath(raiz);
        var raizModulo = new Modulo { Pasta = Raiz, Titulo = "Trilha de estudo" };
        Inicio = new ItemMaterial(Path.Combine(Raiz, "README.md"), "Início — Trilha de estudo", TipoMaterial.Guia, raizModulo);

        var modulos = new List<Modulo>();
        foreach (var pasta in Directory.GetDirectories(Raiz).OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
        {
            var readme = Path.Combine(pasta, "README.md");
            if (!File.Exists(readme))
                continue;
            var textoReadme = File.ReadAllText(readme);
            var modulo = new Modulo { Pasta = pasta, Titulo = TituloDoMarkdown(textoReadme) ?? Path.GetFileName(pasta) };
            var titulosLinks = TitulosDosLinks(textoReadme);

            modulo.Itens.Add(new ItemMaterial(readme, "Guia do módulo", TipoMaterial.Guia, modulo));
            foreach (var arquivo in Directory.GetFiles(pasta, "*", SearchOption.AllDirectories)
                         .Where(a => !a.Equals(readme, StringComparison.OrdinalIgnoreCase))
                         .OrderBy(a => Path.GetRelativePath(pasta, a), StringComparer.OrdinalIgnoreCase))
            {
                var ext = Path.GetExtension(arquivo).ToLowerInvariant();
                var tipo = ext switch
                {
                    ".md" => TipoMaterial.Texto,
                    ".c" or ".h" => TipoMaterial.Codigo,
                    _ => (TipoMaterial?)null,
                };
                if (tipo is null)
                    continue;
                var relativo = Path.GetRelativePath(pasta, arquivo).Replace('\\', '/');
                var titulo = titulosLinks.GetValueOrDefault(relativo) ?? TituloDoNome(arquivo, tipo.Value);
                modulo.Itens.Add(new ItemMaterial(arquivo, titulo, tipo.Value, modulo));
            }
            modulos.Add(modulo);
        }
        Modulos = modulos;
    }

    public IEnumerable<ItemMaterial> TodosItens() => Modulos.SelectMany(m => m.Itens).Prepend(Inicio);

    public ItemMaterial? Encontrar(string caminho)
    {
        var completo = Path.GetFullPath(caminho);
        return TodosItens().FirstOrDefault(i => i.Caminho.Equals(completo, StringComparison.OrdinalIgnoreCase));
    }

    private static string? TituloDoMarkdown(string texto) =>
        texto.Split('\n').Select(l => l.Trim()).FirstOrDefault(l => l.StartsWith("# "))?[2..].Replace("`", "").Trim();

    private static Dictionary<string, string> TitulosDosLinks(string readme)
    {
        var titulos = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (Match m in RegexLink().Matches(readme))
        {
            var alvo = Uri.UnescapeDataString(m.Groups[2].Value);
            if (!alvo.StartsWith("..") && !alvo.EndsWith("README.md"))
                titulos.TryAdd(alvo, m.Groups[1].Value);
        }
        return titulos;
    }

    private static string TituloDoNome(string arquivo, TipoMaterial tipo)
    {
        if (tipo == TipoMaterial.Codigo)
            return Path.GetFileName(arquivo);
        var nome = RegexPrefixo().Replace(Path.GetFileNameWithoutExtension(arquivo), "").Replace('-', ' ');
        return nome.Length > 0 ? char.ToUpper(nome[0]) + nome[1..] : nome;
    }
}
