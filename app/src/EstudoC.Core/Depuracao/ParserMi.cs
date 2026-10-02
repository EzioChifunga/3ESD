using System.Text;

namespace EstudoC.Core.Depuracao;

/// <summary>Valor do protocolo GDB/MI: string, tupla {a=..} ou lista [..].</summary>
public abstract record MiValor
{
    public virtual MiValor? this[string chave] => null;
    public string? Texto => (this as MiTexto)?.Valor;
    public IReadOnlyList<MiValor> Itens => (this as MiLista)?.Valores ?? [];
}

public sealed record MiTexto(string Valor) : MiValor;

public sealed record MiTupla(IReadOnlyList<KeyValuePair<string, MiValor>> Campos) : MiValor
{
    public override MiValor? this[string chave] => Campos.FirstOrDefault(c => c.Key == chave).Value;
    public static readonly MiTupla Vazia = new([]);
}

/// <summary>Lista: de valores [..] ou de resultados [frame={..},frame={..}] (os nomes são descartados).</summary>
public sealed record MiLista(IReadOnlyList<MiValor> Valores) : MiValor;

public enum TipoRegistroMi { Resultado, Assincrono, Console, Alvo, Log, Prompt, Desconhecido }

/// <summary>Uma linha de saída do GDB/MI já interpretada.</summary>
public sealed record RegistroMi(TipoRegistroMi Tipo, int? Token, string Classe, MiTupla Dados, string? TextoStream)
{
    /// <summary>Para registros assíncronos: '*' (exec), '+' (status), '=' (notificação).</summary>
    public char Prefixo { get; init; }
}

public static class ParserMi
{
    public static RegistroMi AnalisarLinha(string linha)
    {
        linha = linha.TrimEnd('\r');
        if (linha.StartsWith("(gdb)"))
            return new RegistroMi(TipoRegistroMi.Prompt, null, "", MiTupla.Vazia, null);

        var pos = 0;
        while (pos < linha.Length && char.IsDigit(linha[pos]))
            pos++;
        int? token = pos > 0 ? int.Parse(linha[..pos]) : null;
        if (pos >= linha.Length)
            return Desconhecido(linha);

        var prefixo = linha[pos];
        switch (prefixo)
        {
            case '~' or '@' or '&':
            {
                var i = pos + 1;
                var texto = i < linha.Length && linha[i] == '"' ? LerString(linha, ref i) : linha[(pos + 1)..];
                var tipo = prefixo switch { '~' => TipoRegistroMi.Console, '@' => TipoRegistroMi.Alvo, _ => TipoRegistroMi.Log };
                return new RegistroMi(tipo, token, "", MiTupla.Vazia, texto);
            }
            case '^' or '*' or '+' or '=':
            {
                var i = pos + 1;
                var inicio = i;
                while (i < linha.Length && linha[i] != ',')
                    i++;
                var classe = linha[inicio..i];
                var campos = new List<KeyValuePair<string, MiValor>>();
                while (i < linha.Length && linha[i] == ',')
                {
                    i++;
                    campos.Add(LerResultado(linha, ref i));
                }
                var tipo = prefixo == '^' ? TipoRegistroMi.Resultado : TipoRegistroMi.Assincrono;
                return new RegistroMi(tipo, token, classe, new MiTupla(campos), null) { Prefixo = prefixo };
            }
            default:
                return Desconhecido(linha);
        }
    }

    private static RegistroMi Desconhecido(string linha) => new(TipoRegistroMi.Desconhecido, null, "", MiTupla.Vazia, linha);

    private static KeyValuePair<string, MiValor> LerResultado(string s, ref int i)
    {
        var inicio = i;
        while (i < s.Length && s[i] != '=')
            i++;
        var nome = s[inicio..i];
        i++; // '='
        return new(nome, LerValor(s, ref i));
    }

    private static MiValor LerValor(string s, ref int i)
    {
        if (i >= s.Length)
            return new MiTexto("");
        switch (s[i])
        {
            case '"':
                return new MiTexto(LerString(s, ref i));
            case '{':
            {
                i++;
                var campos = new List<KeyValuePair<string, MiValor>>();
                while (i < s.Length && s[i] != '}')
                {
                    campos.Add(LerResultado(s, ref i));
                    if (i < s.Length && s[i] == ',')
                        i++;
                }
                i++; // '}'
                return new MiTupla(campos);
            }
            case '[':
            {
                i++;
                var valores = new List<MiValor>();
                while (i < s.Length && s[i] != ']')
                {
                    // item pode ser valor ou resultado (nome=valor)
                    if (s[i] is '"' or '{' or '[')
                        valores.Add(LerValor(s, ref i));
                    else
                        valores.Add(LerResultado(s, ref i).Value);
                    if (i < s.Length && s[i] == ',')
                        i++;
                }
                i++; // ']'
                return new MiLista(valores);
            }
            default:
            {
                var inicio = i;
                while (i < s.Length && s[i] is not (',' or '}' or ']'))
                    i++;
                return new MiTexto(s[inicio..i]);
            }
        }
    }

    /// <summary>Lê uma c-string do MI (com escapes \n, \t, \", \\, \ooo octal em UTF-8).</summary>
    private static string LerString(string s, ref int i)
    {
        i++; // abre aspas
        var bytes = new List<byte>();
        void Adicionar(string t) => bytes.AddRange(Encoding.UTF8.GetBytes(t));

        while (i < s.Length && s[i] != '"')
        {
            var c = s[i];
            if (c == '\\' && i + 1 < s.Length)
            {
                var e = s[i + 1];
                if (e is >= '0' and <= '7')
                {
                    var j = i + 1;
                    var valor = 0;
                    while (j < s.Length && j < i + 4 && s[j] is >= '0' and <= '7')
                        valor = valor * 8 + (s[j++] - '0');
                    bytes.Add((byte)valor);
                    i = j;
                    continue;
                }
                Adicionar(e switch
                {
                    'n' => "\n",
                    't' => "\t",
                    'r' => "\r",
                    'a' => "\a",
                    _ => e.ToString(),
                });
                i += 2;
                continue;
            }
            Adicionar(c.ToString());
            i++;
        }
        i++; // fecha aspas
        return Encoding.UTF8.GetString(bytes.ToArray());
    }
}
