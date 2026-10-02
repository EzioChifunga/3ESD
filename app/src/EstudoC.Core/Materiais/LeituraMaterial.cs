using System.Text;
using System.Text.RegularExpressions;

namespace EstudoC.Core.Materiais;

/// <summary>Uma página pronta para leitura (Markdown). Numero = página do PDF original (null = cabeçalho/sem páginas).</summary>
public sealed record PaginaLeitura(int? Numero, string Markdown);

/// <summary>
/// Transforma o texto extraído dos PDFs (layout cru) em Markdown estruturado, sem mudar o conteúdo:
/// <list type="bullet">
/// <item>títulos: seções numeradas (1.1 …), capítulos, questões, título do slide, linhas em CAIXA ALTA;</item>
/// <item>parágrafos: junta as linhas que o PDF quebrou no meio da frase;</item>
/// <item>listas: marcadores (●, •, –, ◦…) com níveis, itens numerados, alternativas a) b) e I, II;</item>
/// <item>negrito: rótulos ("Definição:", "Exemplo:"…), números de itens e o termo buscado;</item>
/// <item>caixas: blocos de código C, caixas de destaque ("Atenção", "Dica", "Erro comum"…)
///   e código em linha (int, malloc, p-&gt;prox, v[i], arquivo.h);</item>
/// <item>limpeza: números de slide, cabeçalhos/rodapés repetidos, separadores ----.</item>
/// </list>
/// Arquivos que já são Markdown de verdade (vindos de DOCX) passam direto.
/// </summary>
public static partial class LeituraMaterial
{
    // ---------------------------------------------------------------- padrões

    [GeneratedRegex(@"^<!--\s*página\s+(\d+)\s*-->$")]
    private static partial Regex RegexPagina();

    [GeneratedRegex(@"^>\s*Fonte:\s*`([^`]+)`")]
    private static partial Regex RegexFonte();

    // construções típicas de código C
    [GeneratedRegex(@"^(#\s*(include|define|ifndef|ifdef|endif|else)\b|typedef\b|struct\s+\w+\s*\{?\s*$|(static\s+|const\s+|unsigned\s+)*(int|float|double|char|void|long|short|bool|size_t|FILE)\b[\s\*]+\**\w+.*[;{(,=]|return\b.*;|if\s*\(|else\b|for\s*\(|while\s*\(|do\s*\{|switch\s*\(|case\b.*:|default\s*:|break\s*;|continue\s*;|[{}]\s*;?\s*$|\}\s*\w*\s*;|//|/\*|\*/|\*(\s|$))")]
    private static partial Regex RegexCodigo();

    // desenhos feitos com caracteres de caixa/setas (listas, pilhas, memória)
    [GeneratedRegex(@"[│─┌┐└┘├┤┬┴┼▼▲◄═║╔╗╚╝↓↑]|─→|←─|\[[^\]]*\|[^\]]*\]")]
    private static partial Regex RegexDesenho();

    // termina com ; e tem cara de comando (chamada, atribuição, acesso a campo)
    [GeneratedRegex(@"(\w\s*\(.*\)|[\w\]\)]\s*[-+*/]?=|->|\w\.\w).*;\s*(//.*|/\*.*)?$")]
    private static partial Regex RegexComando();

    // "1.2 Título .......... 14" (sumário)
    [GeneratedRegex(@"^(.*[A-Za-zÀ-ú].*?)\s{3,}(\d{1,3})$")]
    private static partial Regex RegexSumario();

    [GeneratedRegex(@"^([\d\s/]{1,40}|[•●▪■◦○·])$")]
    private static partial Regex RegexLixo(); // número de slide solto, marcador sozinho

    [GeneratedRegex(@"\S\s{4,}\S")]
    private static partial Regex RegexColunas();

    [GeneratedRegex(@"^[-_=─—]{5,}$")]
    private static partial Regex RegexSeparador();

    // marcadores: nível 0 e nível 1 (com ou sem espaço depois)
    [GeneratedRegex(@"^([●•▪■➔➜➢✔✓❖►▶]|[-])\s*")]
    private static partial Regex RegexMarcador0();

    [GeneratedRegex(@"^([◦○»–—]\s*|o\s+|-\s+)")]
    private static partial Regex RegexMarcador1();

    [GeneratedRegex(@"^\(?([a-z])\)\s+")]
    private static partial Regex RegexLetra();

    [GeneratedRegex(@"^(I{1,3}|IV|V|VI{1,3}|IX|X)\s*[-.)–]\s+")]
    private static partial Regex RegexRomano();

    [GeneratedRegex(@"^(\d{1,2}[A-Z]?)([.)])\s+(.*)$")]
    private static partial Regex RegexNumerado();

    [GeneratedRegex(@"^(\d{1,2}(\.\d{1,2})+)\.?\s+(\S.{1,90})$")]
    private static partial Regex RegexSecao();

    [GeneratedRegex(@"^(Cap[íi]tulo\s+\d+|CAP[ÍI]TULO\s+\d+|Quest[ãa]o\s+\d+|QUEST[ÃA]O\s+\d+|Exerc[íi]cio\s+\d+|M[ÓO]DULO\s+\d+|M[óo]dulo\s+\d+|T[óo]pico\s|Parte\s+\d+|Caso\s+[A-Z]\b)")]
    private static partial Regex RegexTituloForte();

    // caixas de destaque (apostilas)
    [GeneratedRegex(@"^(Atenção( conceitual)?|Dica|Erro comum|Boa prática|Grave isso|Importante|Observação|Obs\.?|Lembre(-se)?|Cuidado|Quiz relâmpago|Exercício prático|Para discutir em sala|Meta de domínio|Checkpoint|Resumo|Conclusão|Nota)\s*:?\s*(.*)$")]
    private static partial Regex RegexCaixa();

    // rótulos em negrito no início da linha
    [GeneratedRegex(@"^((Definição|Defini[çc][ãa]o|Exemplo|Ex|Obs|Observação|Objetivo|Entrada|Saída|Tarefa|Contexto|Enunciado(\s+Técnico)?|Regras de Negócio|Interface|Estratégia|Quando utilizar|Tipos abstratos|Problema|Solução|Resposta|Retorno|Parâmetros?|Pré-condição|Pós-condição|Lembrando)\s*\.?\s*:)\s*")]
    private static partial Regex RegexRotulo();

    // código em linha no meio do texto
    [GeneratedRegex(@"#include\s*<[^>]+>|<\w+\.h>|\b[A-Za-z_]\w*(?:->[A-Za-z_]\w*)+|\b[A-Za-z_]\w*\[[^\]\s]{0,12}\](?:\[[^\]\s]{0,12}\])?|\b[A-Za-z_]\w*\(\)|\b[A-Za-z_]\w*\.[ch]\b|(?<![\w.])\.[ch]\b|\b(?:int|float|double|char|void|struct|typedef|sizeof|malloc|calloc|realloc|free|printf|scanf|fgets|fprintf|puts|strcpy|strncpy|strcmp|strlen|strcat|NULL|main|union|enum|unsigned|memcpy|memmove)\b")]
    private static partial Regex RegexCodigoEmLinha();

    // ---------------------------------------------------------------- API

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

        var repetidas = CabecalhosRepetidos(paginas.Skip(1).Select(p => p.Linhas).ToList());
        var jaVistas = new HashSet<string>();
        var ctx = new Contexto(destaque);

        var resultado = new List<PaginaLeitura>();
        foreach (var (numero, conteudo) in paginas)
        {
            // remove cabeçalhos/rodapés repetidos (mantém a primeira ocorrência)
            var limpas = conteudo.Where(l =>
            {
                var chave = ChaveRepeticao(l);
                return !repetidas.Contains(chave) || (!EhRodape(chave) && jaVistas.Add(chave));
            }).ToList();

            if (numero is null)
            {
                // cabeçalho ("# título" e "> Fonte", já em Markdown) + texto antes da 1ª página (.txt não tem páginas)
                var fimCabecalho = limpas.FindIndex(l => l.StartsWith("> Fonte")) + 1;
                var cabecalho = string.Join("\n", limpas.Take(fimCabecalho)).Trim();
                var resto = ConverterBloco(limpas.Skip(fimCabecalho).ToList(), ctx, primeiraLinhaETitulo: false);
                resultado.Add(new PaginaLeitura(null, (cabecalho + "\n\n" + resto).Trim()));
                continue;
            }
            var corpo = ConverterBloco(limpas, ctx, primeiraLinhaETitulo: true);
            if (corpo.Length > 0)
                resultado.Add(new PaginaLeitura(numero, $"---\n\n###### Página {numero}\n\n{corpo}"));
        }
        return resultado;
    }

    // ---------------------------------------------------------------- cabeçalhos/rodapés

    private static string ChaveRepeticao(string linha) =>
        Regex.Replace(Regex.Replace(linha.Trim(), @"\s+", " "), @"\d+", "#");

    /// <summary>Linhas com número/•/| que se repetem em várias páginas (ex.: "Apostila • Página 3").</summary>
    private static HashSet<string> CabecalhosRepetidos(List<List<string>> paginas)
    {
        var contagem = new Dictionary<string, int>();
        foreach (var pagina in paginas)
            foreach (var chave in pagina.Select(ChaveRepeticao).Where(c => c.Length >= 6).Distinct())
                contagem[chave] = contagem.GetValueOrDefault(chave) + 1;
        var minimo = Math.Max(3, paginas.Count / 4);
        return contagem.Where(c =>
                (c.Value >= minimo && (c.Key.Contains('#') || c.Key.Contains('•') || c.Key.Contains('|')))
                // cabeçalho corrido sem número ("Apostila: Listas Lineares em C") em metade das páginas ou mais
                || (c.Value >= Math.Max(3, paginas.Count / 2) && c.Key.Length >= 15))
            .Select(c => c.Key).ToHashSet();
    }

    /// <summary>Rodapé de paginação ("… • Página 3"): some em todas as páginas, inclusive a primeira.</summary>
    private static bool EhRodape(string chave) =>
        chave.Contains("Página", StringComparison.OrdinalIgnoreCase) || chave.Contains('•') || chave.Contains('|');

    // ---------------------------------------------------------------- estrutura

    private enum Tipo { Vazia, Texto, Codigo, Separador }

    private enum TipoBloco { Paragrafo, Titulo, Item, Numerado, Alternativa, Caixa, Codigo, Separador }

    private sealed class Bloco
    {
        public TipoBloco Tipo;
        public int Nivel;              // título: 2..4; item: 0..1
        public string Rotulo = "";     // número, letra, romano ou nome da caixa
        public readonly List<string> Linhas = []; // texto (já sem marcador); código: linhas cruas
        public bool Aberto = true;     // aceita linhas de continuação
    }

    private sealed class Contexto(string? destaque)
    {
        public string? Destaque { get; } = destaque;
    }

    private static string ConverterBloco(List<string> linhas, Contexto ctx, bool primeiraLinhaETitulo)
    {
        var tipos = Classificar(linhas);
        var blocos = new List<Bloco>();
        Bloco? atual = null;
        var primeiroTexto = primeiraLinhaETitulo;
        var anteriorVazia = true;

        for (var i = 0; i < linhas.Count; i++)
        {
            var bruta = linhas[i];
            switch (tipos[i])
            {
                case Tipo.Vazia:
                    if (atual is not null)
                        atual.Aberto = false;
                    anteriorVazia = true;
                    continue;

                case Tipo.Separador:
                    blocos.Add(atual = new Bloco { Tipo = TipoBloco.Separador, Aberto = false });
                    anteriorVazia = true;
                    continue;

                case Tipo.Codigo:
                    if (atual is not { Tipo: TipoBloco.Codigo })
                        blocos.Add(atual = new Bloco { Tipo = TipoBloco.Codigo });
                    atual.Linhas.Add(bruta.TrimEnd());
                    anteriorVazia = false;
                    continue;
            }

            var t = Regex.Replace(bruta.Trim(), @"\s{2,}", " ");
            var proxima = ProximaTexto(linhas, tipos, i);
            var novo = Reconhecer(t, bruta, atual, primeiroTexto, anteriorVazia, proxima);
            primeiroTexto = false;
            anteriorVazia = false;

            if (novo is null && atual is { Aberto: true } && atual.Tipo is not (TipoBloco.Titulo or TipoBloco.Codigo or TipoBloco.Separador))
            {
                atual.Linhas.Add(t); // continuação do parágrafo/item
                continue;
            }
            novo ??= new Bloco { Tipo = TipoBloco.Paragrafo, Linhas = { t } };
            blocos.Add(novo);
            atual = novo;
        }
        return Renderizar(blocos, ctx);
    }

    private static string? ProximaTexto(List<string> linhas, Tipo[] tipos, int i)
    {
        for (var j = i + 1; j < linhas.Count; j++)
        {
            if (tipos[j] == Tipo.Vazia)
                return null;
            return tipos[j] == Tipo.Texto ? linhas[j].Trim() : null;
        }
        return null;
    }

    /// <summary>Decide se a linha começa um bloco novo (título, item, caixa…). null = continuação/parágrafo.</summary>
    private static Bloco? Reconhecer(string t, string bruta, Bloco? atual, bool primeiraDaPagina, bool anteriorVazia, string? proxima)
    {
        Match m;

        // sumário: "1.2 Título .... 14"
        if ((m = RegexSumario().Match(bruta.Trim())).Success && !RegexCodigo().IsMatch(t))
            return new Bloco { Tipo = TipoBloco.Paragrafo, Linhas = { $"{Regex.Replace(m.Groups[1].Value, @"\s{2,}", " ")} — p. {m.Groups[2].Value}" }, Aberto = false };

        // marcadores
        if ((m = RegexMarcador0().Match(t)).Success && t.Length > m.Length)
            return Item(0, t[m.Length..]);
        if ((m = RegexMarcador1().Match(t)).Success && t.Length > m.Length)
        {
            var simbolo = m.Groups[1].Value.Trim();
            var dentroDeLista = atual is { Tipo: TipoBloco.Item or TipoBloco.Alternativa or TipoBloco.Numerado };
            // frase anterior ainda não terminou ("… um item de dados / - o último inserido"): é continuação
            var anteriorAberta = atual is { Aberto: true } && !anteriorVazia && atual.Linhas.Count > 0
                                 && atual.Linhas[^1].Length >= 40 && !TerminaFrase(atual.Linhas[^1]);
            var marcadorForte = simbolo is "◦" or "○" or "»";
            // "o " e "- " só são marcadores dentro de uma lista
            if (marcadorForte || (!anteriorAberta && (simbolo is "o" or "-" ? dentroDeLista : true)))
                return Item(dentroDeLista || marcadorForte ? 1 : 0, t[m.Length..]);
        }

        // alternativas a) b) / I - II -
        if ((m = RegexLetra().Match(t)).Success)
            return new Bloco { Tipo = TipoBloco.Alternativa, Rotulo = m.Groups[1].Value + ")", Linhas = { t[m.Length..] } };
        if ((m = RegexRomano().Match(t)).Success && t.Length > m.Length + 3)
            return new Bloco { Tipo = TipoBloco.Alternativa, Rotulo = m.Groups[1].Value + ".", Linhas = { t[m.Length..] } };

        // caixas de destaque: "Atenção", "Dica: ..." (rótulo sozinho na linha ou seguido de dois-pontos)
        if ((m = RegexCaixa().Match(t)).Success && (anteriorVazia || primeiraDaPagina) && (m.Groups[4].Value.Length == 0 || t.Contains(':')))
        {
            var caixa = new Bloco { Tipo = TipoBloco.Caixa, Rotulo = m.Groups[1].Value };
            if (m.Groups[4].Value.Length > 0)
                caixa.Linhas.Add(m.Groups[4].Value);
            return caixa;
        }

        // títulos fortes: Capítulo N, Questão N, Exercício N, MÓDULO N…
        if (RegexTituloForte().IsMatch(t) && t.Length <= 120 && !FraseIncompleta(t))
            return Titulo(t.StartsWith("Cap", StringComparison.OrdinalIgnoreCase) || t.StartsWith("M", StringComparison.Ordinal) ? 2 : 3, t);

        // seção numerada: "1.1 Tipos primitivos" / "3.2.1 ..."
        if ((m = RegexSecao().Match(t)).Success && ComecaMaiuscula(m.Groups[3].Value) && !TerminaPontuacao(t) && !FraseIncompleta(t))
            return Titulo(m.Groups[1].Value.Count(c => c == '.') >= 2 ? 4 : 3, t);

        // item numerado "1. ..." — ou título de seção "1. Antes do TAD: ..." (isolado e curto)
        if ((m = RegexNumerado().Match(t)).Success)
        {
            var isolado = (anteriorVazia || t.Length <= 40) && (proxima is null || !RegexNumerado().IsMatch(proxima));
            // só "N." vira título; "N)" é pergunta/item numerado
            if (isolado && m.Groups[2].Value == "." && t.Length <= 80 && ComecaMaiuscula(m.Groups[3].Value) && !t.EndsWith('.')
                && !FraseIncompleta(t) && atual is not { Tipo: TipoBloco.Numerado })
                return Titulo(2, t);
            return new Bloco { Tipo = TipoBloco.Numerado, Rotulo = m.Groups[1].Value + m.Groups[2].Value, Linhas = { m.Groups[3].Value } };
        }

        // CAIXA ALTA curta → título
        if (EhCaixaAlta(t) && t.Length <= 80)
            return Titulo(3, t);

        // título do slide: primeira linha da página, curta, sem pontuação final
        if (primeiraDaPagina && t.Length <= 70 && ComecaMaiuscula(t) && !TerminaPontuacao(t) && !FraseIncompleta(t))
            return Titulo(3, t);

        // rótulo curto isolado seguido de texto ("Como usar esta apostila") → subtítulo em negrito
        if (anteriorVazia && proxima is not null && t.Length <= 50 && ComecaMaiuscula(t) && !TerminaPontuacao(t) && !FraseIncompleta(t)
            && !RegexMarcador0().IsMatch(proxima) && t.Split(' ').Length <= 7)
            return Titulo(4, t);

        // continuação de bloco aberto, ou parágrafo novo depois de linha vazia
        return atual is { Aberto: true } && !anteriorVazia ? null : new Bloco { Tipo = TipoBloco.Paragrafo, Linhas = { t } };

        static Bloco Item(int nivel, string texto) => new() { Tipo = TipoBloco.Item, Nivel = nivel, Linhas = { texto.Trim() } };
        static Bloco Titulo(int nivel, string texto) => new() { Tipo = TipoBloco.Titulo, Nivel = nivel, Linhas = { texto.TrimEnd(':').Trim() }, Aberto = false };
    }

    private static bool ComecaMaiuscula(string s) => s.Length > 0 && (char.IsUpper(s[0]) || char.IsDigit(s[0]) || s[0] is '“' or '"' or '(');

    /// <summary>Pontuação que impede uma linha de ser título (interrogação é permitida: "Por que usar TAD?").</summary>
    private static bool TerminaPontuacao(string s) => s.Length > 0 && s[^1] is '.' or ',' or ';' or ':' or '!';

    private static readonly HashSet<string> Ligacao = new(StringComparer.OrdinalIgnoreCase)
    {
        "a", "o", "as", "os", "e", "ou", "de", "da", "do", "das", "dos", "com", "em", "no", "na", "nos", "nas",
        "para", "por", "que", "um", "uma", "ao", "à", "se", "como", "entre", "sobre", "pelo", "pela",
    };

    /// <summary>Linha que termina em "de", "com", "para"… é uma frase que continua na linha seguinte.</summary>
    private static bool FraseIncompleta(string s)
    {
        var ultima = s.TrimEnd().Split(' ').LastOrDefault() ?? "";
        return Ligacao.Contains(ultima);
    }

    private static bool TerminaFrase(string s) => s.Length > 0 && s[^1] is '.' or ':' or ';' or '!' or '?';

    private static bool EhCaixaAlta(string s)
    {
        var letras = s.Where(char.IsLetter).ToList();
        return letras.Count >= 4 && letras.All(char.IsUpper) && !RegexCodigo().IsMatch(s);
    }

    private static Tipo[] Classificar(List<string> linhas)
    {
        var tipos = linhas.Select(ClassificarLinha).ToArray();

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

        // rótulo curto logo acima de um desenho ("prim" sobre "│ ▼ [10|●]") vai junto para o bloco
        for (var i = 0; i + 1 < tipos.Length; i++)
            if (tipos[i] == Tipo.Texto && tipos[i + 1] == Tipo.Codigo && linhas[i].Trim().Length <= 12
                && !linhas[i].Trim().Contains(' ') && RegexDesenho().IsMatch(linhas[i + 1]))
                tipos[i] = Tipo.Codigo;

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
        return tipos;
    }

    private static Tipo ClassificarLinha(string linha)
    {
        var t = linha.Trim();
        if (t.Length == 0 || RegexLixo().IsMatch(t))
            return Tipo.Vazia;
        if (RegexSeparador().IsMatch(t))
            return Tipo.Separador;
        if (RegexSumario().IsMatch(t) && !RegexCodigo().IsMatch(t))
            return Tipo.Texto;
        var ehItem = RegexLetra().IsMatch(t) || RegexRomano().IsMatch(t) || RegexNumerado().IsMatch(t)
                     || RegexMarcador0().IsMatch(t);
        if (RegexCodigo().IsMatch(t) || RegexComando().IsMatch(t) || RegexDesenho().IsMatch(t))
            return Tipo.Codigo;
        if (!ehItem && RegexColunas().IsMatch(t))
            return Tipo.Codigo; // layout em colunas: preserva em bloco monoespaçado
        return Tipo.Texto;
    }

    // ---------------------------------------------------------------- saída

    private static string Renderizar(List<Bloco> blocos, Contexto ctx)
    {
        var sb = new StringBuilder();
        TipoBloco? anterior = null;
        foreach (var b in blocos)
        {
            var emLista = b.Tipo is TipoBloco.Item or TipoBloco.Alternativa;
            var anteriorLista = anterior is TipoBloco.Item or TipoBloco.Alternativa;
            if (!(emLista && anteriorLista))
                sb.Append('\n'); // listas ficam "coladas"; o resto separado por linha em branco

            switch (b.Tipo)
            {
                case TipoBloco.Titulo:
                    sb.Append(new string('#', b.Nivel)).Append(' ').Append(Inline(b.Linhas[0], ctx, inicioDeLinha: false)).Append('\n');
                    break;

                case TipoBloco.Paragrafo:
                    sb.Append(Inline(Juntar(b.Linhas), ctx, inicioDeLinha: true)).Append('\n');
                    break;

                case TipoBloco.Item:
                    // sub-item só existe dentro de uma lista; no início vira item normal (recuo solto = bloco de código)
                    sb.Append(b.Nivel > 0 && anteriorLista ? "    - " : "- ").Append(Inline(Juntar(b.Linhas), ctx, inicioDeLinha: false)).Append('\n');
                    break;

                case TipoBloco.Alternativa:
                    sb.Append("- **").Append(b.Rotulo).Append("** ").Append(Inline(Juntar(b.Linhas), ctx, inicioDeLinha: false)).Append('\n');
                    break;

                case TipoBloco.Numerado:
                    sb.Append("**").Append(b.Rotulo).Append("** ").Append(Inline(Juntar(b.Linhas), ctx, inicioDeLinha: false)).Append('\n');
                    break;

                case TipoBloco.Caixa:
                    sb.Append("> **").Append(b.Rotulo).Append("**");
                    if (b.Linhas.Count > 0)
                        sb.Append("  \n> ").Append(Inline(Juntar(b.Linhas), ctx, inicioDeLinha: false).Replace("  \n", "  \n> "));
                    sb.Append('\n');
                    break;

                case TipoBloco.Codigo:
                {
                    var recuo = b.Linhas.Where(l => l.Trim().Length > 0).Select(l => l.Length - l.TrimStart().Length).DefaultIfEmpty(0).Min();
                    // espaçamento duplo do PDF (linha em branco entre todas as linhas): compacta
                    var cheias = b.Linhas.Count(l => l.Trim().Length > 0);
                    var linhasBloco = cheias >= 3 && b.Linhas.Count - cheias >= cheias - 1
                        ? b.Linhas.Where(l => l.Trim().Length > 0).ToList()
                        : b.Linhas;
                    sb.Append("```\n");
                    foreach (var l in linhasBloco)
                        sb.Append(l.Length >= recuo ? l[recuo..] : l.TrimStart()).Append('\n');
                    sb.Append("```\n");
                    break;
                }

                case TipoBloco.Separador:
                    sb.Append("---\n");
                    break;
            }
            anterior = b.Tipo;
        }
        return Regex.Replace(sb.ToString(), @"\n{3,}", "\n\n").Trim();
    }

    /// <summary>
    /// Junta as linhas que o PDF quebrou: frase sem pontuação final + próxima linha → mesmo parágrafo.
    /// Linhas curtas que terminam a ideia continuam em linha própria (quebra de linha).
    /// </summary>
    private static string Juntar(List<string> linhas)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < linhas.Count; i++)
        {
            var l = linhas[i];
            if (i == 0)
            {
                sb.Append(l);
                continue;
            }
            var anterior = linhas[i - 1];
            var continua = !anterior.EndsWith('.') && !anterior.EndsWith(':') && !anterior.EndsWith('!') && !anterior.EndsWith('?') && !anterior.EndsWith(';')
                           && (anterior.Length >= 45 || (l.Length > 0 && char.IsLower(l[0])));
            if (continua && anterior.Length > 1 && anterior[^1] == '-' && char.IsLetter(anterior[^2]) && l.Length > 0 && char.IsLower(l[0]))
                sb.Append(l); // palavra hifenizada na quebra
            else
                sb.Append(continua ? " " : "  \n").Append(l);
        }
        return sb.ToString();
    }

    // ---------------------------------------------------------------- formatação em linha

    /// <summary>Código em linha (`malloc`, `p->prox`), rótulos em negrito, termo buscado em negrito e escapes.</summary>
    private static string Inline(string texto, Contexto ctx, bool inicioDeLinha)
    {
        var sb = new StringBuilder();

        var rotulo = RegexRotulo().Match(texto);
        if (rotulo.Success)
        {
            sb.Append("**").Append(rotulo.Groups[1].Value).Append("** ");
            texto = texto[rotulo.Length..];
            inicioDeLinha = false;
        }

        var pos = 0;
        foreach (Match m in RegexCodigoEmLinha().Matches(texto))
        {
            if (DentroDeUrl(texto, m.Index))
                continue;
            sb.Append(TextoSimples(texto[pos..m.Index], ctx, inicioDeLinha && pos == 0));
            sb.Append('`').Append(m.Value).Append('`');
            pos = m.Index + m.Length;
        }
        sb.Append(TextoSimples(texto[pos..], ctx, inicioDeLinha && pos == 0));
        return sb.ToString();
    }

    private static bool DentroDeUrl(string texto, int indice)
    {
        var inicio = texto.LastIndexOf(' ', Math.Max(0, indice - 1)) + 1;
        var palavra = texto[inicio..Math.Min(texto.Length, indice + 1)];
        return palavra.Contains("http") || palavra.Contains("www.");
    }

    /// <summary>Escapa caracteres do Markdown e põe em negrito as ocorrências do termo buscado.</summary>
    private static string TextoSimples(string t, Contexto ctx, bool inicioDeLinha)
    {
        if (string.IsNullOrWhiteSpace(ctx.Destaque))
            return Escapar(t, inicioDeLinha);

        var termo = BuscaMateriais.Normalizar(ctx.Destaque.Trim());
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
        // '#', '>' ou '-' no início virariam título/citação/lista: um espaço invisível (U+200B) evita isso
        if (inicioDeLinha && r.Length > 0 && r[0] is '#' or '>' or '-' or '+')
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
