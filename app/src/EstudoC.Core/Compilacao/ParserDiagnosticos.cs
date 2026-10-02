using System.Text.RegularExpressions;

namespace EstudoC.Core.Compilacao;

/// <summary>Converte a saída de texto do gcc/ld em diagnósticos estruturados.</summary>
public static partial class ParserDiagnosticos
{
    // C:\pasta\arquivo.c:4:5: error: expected ',' or ';' before 'printf'
    [GeneratedRegex(@"^(?<arq>(?:[A-Za-z]:)?[^:]+?):(?<lin>\d+):(?:(?<col>\d+):)?\s*(?<sev>fatal error|error|warning|note):\s*(?<msg>.*)$")]
    private static partial Regex RegexGcc();

    // ld.exe: C:\...\cc.o:link.c:(.text+0x13): undefined reference to `f'
    [GeneratedRegex(@"^.*?ld(?:\.exe)?:\s*(?:.*?\.o:)?(?:(?<arq>[^:()]+\.c):)?(?:\([^)]*\):)?\s*(?<msg>.+)$")]
    private static partial Regex RegexLigador();

    public static List<Diagnostico> Analisar(string saida)
    {
        var lista = new List<Diagnostico>();
        foreach (var linhaBruta in saida.Split('\n'))
        {
            var linha = linhaBruta.TrimEnd('\r');
            var m = RegexGcc().Match(linha);
            if (m.Success)
            {
                var sev = m.Groups["sev"].Value switch
                {
                    "warning" => Severidade.Aviso,
                    "note" => Severidade.Nota,
                    _ => Severidade.Erro,
                };
                var msg = NormalizarAspas(m.Groups["msg"].Value.Trim());
                lista.Add(new Diagnostico(
                    m.Groups["arq"].Value,
                    int.Parse(m.Groups["lin"].Value),
                    m.Groups["col"].Success ? int.Parse(m.Groups["col"].Value) : 1,
                    sev, msg, DicasErros.Para(msg)));
                continue;
            }

            if (linha.StartsWith("collect2", StringComparison.OrdinalIgnoreCase))
                continue; // "ld returned 1 exit status" só repete o erro do ligador

            var l = RegexLigador().Match(linha);
            if (l.Success && (linha.Contains("ld.exe:") || linha.Contains("/ld:") || linha.Contains("\\ld:")))
            {
                var msg = NormalizarAspas(l.Groups["msg"].Value.Trim());
                var arq = l.Groups["arq"].Success ? l.Groups["arq"].Value : null;
                lista.Add(new Diagnostico(arq, 0, 0, Severidade.Erro, msg, DicasErros.Para(msg)));
            }
        }
        return lista;
    }

    /// <summary>gcc pode usar ‘ ’ ou ` ' — padroniza para aspas simples.</summary>
    private static string NormalizarAspas(string s) =>
        s.Replace('‘', '\'').Replace('’', '\'').Replace('`', '\'');
}
