using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace EstudoC.Core.Materiais;

/// <summary>Uma ocorrência do texto buscado. Linha começa em 1; Pagina = página do PDF original (se houver).</summary>
public sealed record ResultadoBusca(ItemMaterial Item, int Linha, int? Pagina, string Trecho, int InicioDestaque, int TamanhoDestaque)
{
    public string Local => Item.Modulo.Titulo + "  ›  " + Item.Titulo + (Pagina is { } p ? $"  ›  pág. {p}" : "");
}

/// <summary>Busca de texto em todos os materiais. Ignora maiúsculas e acentos ("alocacao" acha "Alocação").</summary>
public sealed partial class BuscaMateriais
{
    private sealed record Linha(ItemMaterial Item, int Numero, int? Pagina, string Original, string Normalizada);

    private readonly List<Linha> _linhas = [];

    [GeneratedRegex(@"<!--\s*página\s+(\d+)\s*-->")]
    private static partial Regex RegexPagina();

    public BuscaMateriais(CatalogoMateriais catalogo)
    {
        foreach (var item in catalogo.TodosItens())
        {
            int? pagina = null;
            var numero = 0;
            foreach (var texto in File.ReadLines(item.Caminho))
            {
                numero++;
                var m = RegexPagina().Match(texto);
                if (m.Success)
                {
                    pagina = int.Parse(m.Groups[1].Value);
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(texto))
                    _linhas.Add(new Linha(item, numero, pagina, texto, Normalizar(texto)));
            }
        }
    }

    public IReadOnlyList<ResultadoBusca> Buscar(string consulta, int limite = 500)
    {
        var termo = Normalizar(consulta.Trim());
        if (termo.Length < 2)
            return [];

        var resultados = new List<ResultadoBusca>();
        foreach (var linha in _linhas)
        {
            var pos = linha.Normalizada.IndexOf(termo, StringComparison.Ordinal);
            if (pos < 0)
                continue;
            resultados.Add(CriarResultado(linha, pos, termo.Length));
            if (resultados.Count >= limite)
                break;
        }
        return resultados;
    }

    /// <summary>Recorta a linha em volta da ocorrência (sem espaços de alinhamento do PDF).</summary>
    private static ResultadoBusca CriarResultado(Linha linha, int pos, int tamanho)
    {
        const int Contexto = 70;
        var original = linha.Original;
        var inicio = Math.Max(0, pos - Contexto);
        var fim = Math.Min(original.Length, pos + tamanho + Contexto);
        var antes = original[inicio..pos];
        var trechoInicio = antes.TrimStart();
        var prefixo = (inicio > 0 ? "…" : "") + EspacosSimples(trechoInicio);
        var destaque = original.Substring(pos, tamanho);
        var depois = EspacosSimples(original[(pos + tamanho)..fim]) + (fim < original.Length ? "…" : "");
        return new ResultadoBusca(linha.Item, linha.Numero, linha.Pagina, prefixo + destaque + depois, prefixo.Length, tamanho);
    }

    private static string EspacosSimples(string s) => Regex.Replace(s, @"\s{2,}", " ");

    /// <summary>
    /// Minúsculas e sem acento, mantendo o MESMO comprimento do texto original
    /// (cada caractere vira exatamente um) para que as posições coincidam.
    /// </summary>
    public static string Normalizar(string texto)
    {
        var sb = new StringBuilder(texto.Length);
        foreach (var c in texto)
        {
            if (char.IsSurrogate(c) || c < 128)
            {
                sb.Append(char.ToLowerInvariant(c)); // emoji (metade de par) ou ASCII: nada a remover
                continue;
            }
            var decomposto = c.ToString().Normalize(NormalizationForm.FormD);
            var basico = decomposto.Length > 0 && CharUnicodeInfo.GetUnicodeCategory(decomposto[0]) != UnicodeCategory.NonSpacingMark
                ? decomposto[0]
                : c;
            sb.Append(char.ToLowerInvariant(basico));
        }
        return sb.ToString();
    }
}
