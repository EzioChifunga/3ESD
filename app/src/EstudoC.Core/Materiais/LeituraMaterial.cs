using System.Text;
using System.Text.RegularExpressions;

namespace EstudoC.Core.Materiais;

/// <summary>Uma página pronta para leitura (Markdown). Numero = página do PDF original (null = cabeçalho/sem páginas).</summary>
public sealed record PaginaLeitura(int? Numero, string Markdown);

/// <summary>
/// Transforma o texto extraído dos PDFs (layout cru) em Markdown legível, sem alterar o conteúdo:
/// separa páginas, transforma marcadores (●, –, ») em listas, junta espaços de alinhamento
/// e coloca trechos de código / layout em colunas em blocos monoespaçados.
/// Arquivos que já são Markdown de verdade (vindos de DOCX) passam direto.
/// </summary>
public static partial class LeituraMaterial
{
    [GeneratedRegex(@"^<!--\s*página\s+(\d+)\s*-->$")]
    private static partial Regex RegexPagina();

    [GeneratedRegex(@"^>\s*Fonte:\s*`([^`]+)`")]
    private static partial Regex RegexFonte();

    // Palavras e construções típicas de código C
    [GeneratedRegex(@"^(#\s*(include|define|ifndef|ifdef|endif|else)\b|typedef\b|struct\s+\w+\s*\{?\s*$|(static\s+|const\s+|unsigned\s+)*(int|float|double|char|void|long|short|bool|size_t|FILE)\b[\s\*]+\**\w+.*[;{(,=]|return\b.*;|if\s*\(|else\b|for\s*\(|while\s*\(|do\s*\{|switch\s*\(|case\b.*:|default\s*:|break\s*;|continue\s*;|[{}]\s*;?\s*$|\}\s*\w*\s*;|//|/\*|\*/)")]
    private static partial Regex RegexCodigo();

    // linha termina com ; e tem cara de comando (chamada, atribuição, acesso a campo)
    [GeneratedRegex(@"(\w\s*\(.*\)|[\w\]\)]\s*[-+*/]?=|->|\w\.\w).*;\s*(//.*|/\*.*)?$")]
    private static partial Regex RegexComando();

    // "1.2 Título .......... 14" (sumário)
    [GeneratedRegex(@"^(.+?)\s{3,}(\d{1,3})$")]
    private static partial Regex RegexSumario();

    [GeneratedRegex(@"^\d{1,3}(\s*/\s*\d{1,3})?$")]
    private static partial Regex RegexSoNumero();

    [GeneratedRegex(@"\S\s{4,}\S")]
    private static partial Regex RegexColunas();

    [GeneratedRegex(@"^([●•▪■◦○➔➜✔✓»–—\-o])\s+")]
    private static partial Regex RegexMarcador();

    [GeneratedRegex(@"^[-]\s*")]
    private static partial Regex RegexMarcadorPrivado(); // símbolos de fontes Wingdings do PDF

    /// <summary>true se o arquivo veio de um PDF/TXT (texto cru) e precisa de conversão para leitura.</summary>
    public static bool PrecisaConverter(string texto)
    {
        var fonte = texto.Split('\n').Take(6).Select(l => RegexFonte().Match(l.Trim())).FirstOrDefault(m => m.Success);
        if (fonte is null)
            return false;
        var ext = Path.GetExtension(fonte.Groups[1].Value).ToLowerInvariant();
        return ext is ".pdf" or ".txt";
    }

    public static IReadOnlyList<PaginaLeitura> Converter(string texto, string? destaque = null)
    {
        var linhas = texto.Replace("\r\n", "\n").Split('\n');
        if (!PrecisaConverter(texto))
            return [new PaginaLeitura(null, Destacar(texto, destaque))];

        // separa em páginas
        var paginas = new List<(int? Numero, List<string> Linhas)> { (null, []) };
        foreach (var linha in linhas)
        {
            var m = RegexPagina().Match(linha.Trim());
            if (m.Success)
                paginas.Add((int.Parse(m.Groups[1].Value), []));
            else
                paginas[^1].Linhas.Add(linha);
        }

        var resultado = new List<PaginaLeitura>();
        foreach (var (numero, conteudo) in paginas)
        {
            if (numero is null)
            {
                // cabeçalho ("# título" e "> Fonte", já em Markdown) + texto antes da 1ª página (arquivos .txt não têm páginas)
                var fimCabecalho = conteudo.FindIndex(l => l.StartsWith("> Fonte")) + 1;
                var cabecalho = string.Join("\n", conteudo.Take(fimCabecalho)).Trim();
                var resto = ConverterBloco(conteudo.Skip(fimCabecalho).ToList(), destaque);
                resultado.Add(new PaginaLeitura(null, (cabecalho + "\n\n" + resto).Trim()));
                continue;
            }
            var corpo = ConverterBloco(conteudo, destaque);
            if (corpo.Length > 0)
                resultado.Add(new PaginaLeitura(numero, $"---\n\n###### Página {numero}\n\n{corpo}"));
        }
        return resultado;
    }

    private enum Tipo { Vazia, Texto, Codigo }

    private static string ConverterBloco(List<string> linhas, string? destaque)
    {
        var tipos = linhas.Select(Classificar).ToArray();

        // linha curta "com cara de código" (termina em ; , { } ( )) entre duas linhas de código também é código
        for (var i = 0; i < tipos.Length; i++)
        {
            if (tipos[i] != Tipo.Texto)
                continue;
            var antes = Array.FindLastIndex(tipos, Math.Max(0, i - 1), t => t != Tipo.Vazia);
            var depois = i + 1 < tipos.Length ? Array.FindIndex(tipos, i + 1, t => t != Tipo.Vazia) : -1;
            var t = linhas[i].Trim();
            if (antes >= 0 && depois >= 0 && antes < i && tipos[antes] == Tipo.Codigo && tipos[depois] == Tipo.Codigo
                && (t.Length <= 25 || t[^1] is ';' or ',' or '{' or '}' or '(' or ')'))
                tipos[i] = Tipo.Codigo;
        }

        // linhas em branco entre duas linhas de código ficam dentro do bloco
        for (var i = 0; i < tipos.Length; i++)
        {
            if (tipos[i] != Tipo.Vazia)
                continue;
            var antes = Array.FindLastIndex(tipos, i, t => t != Tipo.Vazia);
            var depois = Array.FindIndex(tipos, i, t => t != Tipo.Vazia);
            if (antes >= 0 && depois >= 0 && tipos[antes] == Tipo.Codigo && tipos[depois] == Tipo.Codigo)
                tipos[i] = Tipo.Codigo;
        }

        var sb = new StringBuilder();
        var i2 = 0;
        while (i2 < linhas.Count)
        {
            switch (tipos[i2])
            {
                case Tipo.Codigo:
                {
                    var bloco = new List<string>();
                    while (i2 < linhas.Count && tipos[i2] == Tipo.Codigo)
                        bloco.Add(linhas[i2++].TrimEnd());
                    var recuo = bloco.Where(l => l.Trim().Length > 0).Select(l => l.Length - l.TrimStart().Length).DefaultIfEmpty(0).Min();
                    sb.Append("\n```\n");
                    foreach (var l in bloco)
                        sb.Append(l.Length >= recuo ? l[recuo..] : l.TrimStart()).Append('\n');
                    sb.Append("```\n\n");
                    break;
                }
                case Tipo.Vazia:
                    sb.Append('\n');
                    i2++;
                    break;
                default:
                    sb.Append(LinhaTexto(linhas[i2++], destaque)).Append("  \n"); // dois espaços = quebra de linha
                    break;
            }
        }
        return Regex.Replace(sb.ToString(), @"\n{3,}", "\n\n").Trim();
    }

    private static Tipo Classificar(string linha)
    {
        var t = linha.Trim();
        if (t.Length == 0 || RegexSoNumero().IsMatch(t))
            return Tipo.Vazia; // números de slide soltos não são conteúdo
        if (RegexSumario().IsMatch(t) && !RegexCodigo().IsMatch(t))
            return Tipo.Texto;
        if (RegexCodigo().IsMatch(t) || RegexComando().IsMatch(t) || RegexColunas().IsMatch(t))
            return Tipo.Codigo;
        return Tipo.Texto;
    }

    private static string LinhaTexto(string linha, string? destaque)
    {
        var t = Regex.Replace(linha.Trim(), @"\s{2,}", " ");
        var sumario = RegexSumario().Match(linha.Trim());
        if (sumario.Success)
            t = $"{Regex.Replace(sumario.Groups[1].Value, @"\s{2,}", " ")} — p. {sumario.Groups[2].Value}";

        var prefixo = "";
        var marcador = RegexMarcador().Match(t);
        if (marcador.Success)
        {
            prefixo = marcador.Groups[1].Value is "◦" or "○" or "o" or "»" ? "  - " : "- ";
            t = t[marcador.Length..];
        }
        else if (RegexMarcadorPrivado().Match(t) is { Success: true } privado)
        {
            prefixo = "- ";
            t = t[privado.Length..];
        }
        else if (t.Length > 1 && t[0] is '●' or '•' or '▪' or '■')
        {
            prefixo = "- ";
            t = t[1..].TrimStart();
        }

        return prefixo + EscaparComDestaque(t, destaque, inicioDeLinha: prefixo.Length == 0);
    }

    /// <summary>Escapa caracteres do Markdown e põe em negrito as ocorrências do termo buscado.</summary>
    private static string EscaparComDestaque(string t, string? destaque, bool inicioDeLinha)
    {
        if (string.IsNullOrWhiteSpace(destaque))
            return Escapar(t, inicioDeLinha);

        var termo = BuscaMateriais.Normalizar(destaque.Trim());
        var normal = BuscaMateriais.Normalizar(t);
        var sb = new StringBuilder();
        var pos = 0;
        while (true)
        {
            var achou = normal.IndexOf(termo, pos, StringComparison.Ordinal);
            if (achou < 0)
                break;
            sb.Append(Escapar(t[pos..achou], inicioDeLinha && pos == 0));
            sb.Append("**").Append(Escapar(t.Substring(achou, termo.Length), false)).Append("**");
            pos = achou + termo.Length;
        }
        sb.Append(Escapar(t[pos..], inicioDeLinha && pos == 0));
        return sb.ToString();
    }

    /// <summary>
    /// Escapa só o que o Markdown.Avalonia interpreta dentro de uma linha (ênfase e código).
    /// Outros escapes (\[, \#…) apareceriam com a barra na tela.
    /// </summary>
    private static string Escapar(string t, bool inicioDeLinha)
    {
        var sb = new StringBuilder(t.Length + 8);
        foreach (var c in t)
        {
            if (c is '*' or '_' or '`')
                sb.Append('\\');
            sb.Append(c);
        }
        var r = sb.ToString();
        // '#' ou '>' no início virariam título/citação: um espaço invisível (U+200B) evita isso
        if (inicioDeLinha && r.Length > 0 && r[0] is '#' or '>')
            r = "​" + r;
        return r;
    }

    /// <summary>Destaque simples para Markdown verdadeiro (guias/DOCX): negrito fora de blocos de código.</summary>
    private static string Destacar(string texto, string? destaque)
    {
        if (string.IsNullOrWhiteSpace(destaque))
            return texto;
        var termo = BuscaMateriais.Normalizar(destaque.Trim());
        var sb = new StringBuilder();
        var emCodigo = false;
        foreach (var linha in texto.Replace("\r\n", "\n").Split('\n'))
        {
            if (linha.TrimStart().StartsWith("```"))
                emCodigo = !emCodigo;
            var normal = BuscaMateriais.Normalizar(linha);
            var pos = emCodigo || linha.TrimStart().StartsWith('|') ? -1 : normal.IndexOf(termo, StringComparison.Ordinal);
            sb.Append(pos < 0 ? linha : linha[..pos] + "**" + linha.Substring(pos, termo.Length) + "**" + linha[(pos + termo.Length)..]).Append('\n');
        }
        return sb.ToString();
    }
}
