using System.Text.Json;
using System.Text.Json.Serialization;

namespace EstudoC.Core.Exemplos;

/// <summary>Como o exemplo deve ser usado.</summary>
public enum ModoExemplo { Executar, Depurar, CorrigirErros }

/// <summary>
/// Um exemplo pronto: pasta em exemplos/ com exemplo.json, README.md e os arquivos .c/.h.
/// Abrir um exemplo sempre cria uma CÓPIA (projeto ou sandbox) — o original não muda.
/// </summary>
public sealed class Exemplo
{
    public required string Pasta { get; init; }
    public required string Titulo { get; init; }
    /// <summary>"00" (primeiros passos) ou o número do módulo da trilha ("01"…"12").</summary>
    public required string Modulo { get; init; }
    public required ModoExemplo Modo { get; init; }
    public required string Descricao { get; init; }
    /// <summary>Entrada usada no teste automático (o que seria digitado no terminal).</summary>
    public string? Entrada { get; init; }
    /// <summary>false para exemplos com erros de compilação de propósito.</summary>
    public bool Compila { get; init; } = true;
    /// <summary>true para exemplos que quebram de propósito (falha de segmentação…).</summary>
    public bool TerminaComErro { get; init; }

    public string Id => Path.GetFileName(Pasta);
    public string Readme => Path.Combine(Pasta, "README.md");

    public IReadOnlyList<string> Arquivos() =>
        Directory.GetFiles(Pasta).Where(f => f.EndsWith(".c") || f.EndsWith(".h")).Order(StringComparer.OrdinalIgnoreCase).ToList();

    public string ModoTexto => Modo switch
    {
        ModoExemplo.Depurar => "🐞 Depurar",
        ModoExemplo.CorrigirErros => "🩹 Corrigir erros",
        _ => "▶ Executar",
    };
}

public sealed class CatalogoExemplos
{
    private sealed record Metadados(
        string Titulo, string Modulo, ModoExemplo Modo, string Descricao,
        string? Entrada, bool Compila = true, bool TerminaComErro = false);

    private static readonly JsonSerializerOptions Opcoes = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() },
    };

    public string Raiz { get; }
    public IReadOnlyList<Exemplo> Todos { get; }

    public CatalogoExemplos(string raiz)
    {
        Raiz = Path.GetFullPath(raiz);
        var lista = new List<Exemplo>();
        foreach (var pasta in Directory.GetDirectories(Raiz).Order(StringComparer.OrdinalIgnoreCase))
        {
            var json = Path.Combine(pasta, "exemplo.json");
            if (!File.Exists(json))
                continue;
            var m = JsonSerializer.Deserialize<Metadados>(File.ReadAllText(json), Opcoes)
                    ?? throw new InvalidDataException($"exemplo.json inválido em {pasta}");
            lista.Add(new Exemplo
            {
                Pasta = pasta,
                Titulo = m.Titulo,
                Modulo = m.Modulo,
                Modo = m.Modo,
                Descricao = m.Descricao,
                Entrada = m.Entrada,
                Compila = m.Compila,
                TerminaComErro = m.TerminaComErro,
            });
        }
        Todos = lista;
    }

    public IEnumerable<Exemplo> DoModulo(string modulo) => Todos.Where(e => e.Modulo == modulo);

    /// <summary>exemplos/ ao lado do executável (instalado) ou na raiz do repositório (desenvolvimento).</summary>
    public static string? Localizar(string? pastaBase = null)
    {
        foreach (var pasta in Caminhos.Ancestrais(pastaBase ?? AppContext.BaseDirectory))
        {
            var candidato = Path.Combine(pasta, "exemplos");
            if (Directory.Exists(candidato) && Directory.EnumerateFiles(candidato, "exemplo.json", SearchOption.AllDirectories).Any())
                return candidato;
        }
        return null;
    }
}
