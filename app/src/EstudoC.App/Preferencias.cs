using System.Text.Json;
using Avalonia;
using Avalonia.Styling;
using EstudoC.Core;

namespace EstudoC.App;

public enum Tema { Sistema, Claro, Escuro }

/// <summary>Preferências do usuário, salvas em %LocalAppData%\EstudoC\preferencias.json.</summary>
public sealed class Preferencias
{
    public Tema Tema { get; set; } = Tema.Sistema;

    private static string Arquivo => Path.Combine(Path.GetDirectoryName(Caminhos.DadosLocais)!, "preferencias.json");

    public static Preferencias Atual { get; private set; } = Carregar();

    private static Preferencias Carregar()
    {
        try
        {
            if (File.Exists(Arquivo))
                return JsonSerializer.Deserialize<Preferencias>(File.ReadAllText(Arquivo)) ?? new();
        }
        catch (Exception e) when (e is IOException or JsonException or UnauthorizedAccessException) { }
        return new();
    }

    public void Salvar()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Arquivo)!);
            File.WriteAllText(Arquivo, JsonSerializer.Serialize(this));
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException) { }
    }

    /// <summary>Aplica o tema na aplicação inteira (editores trocam as cores sozinhos).</summary>
    public void AplicarTema()
    {
        if (Application.Current is { } app)
            app.RequestedThemeVariant = Tema switch
            {
                Tema.Claro => ThemeVariant.Light,
                Tema.Escuro => ThemeVariant.Dark,
                _ => ThemeVariant.Default,
            };
    }

    public string TemaTexto => Tema switch
    {
        Tema.Claro => "☀ Claro",
        Tema.Escuro => "🌙 Escuro",
        _ => "🖥 Tema do sistema",
    };

    /// <summary>Sistema → Claro → Escuro → Sistema…</summary>
    public void ProximoTema()
    {
        Tema = Tema switch { Tema.Sistema => Tema.Claro, Tema.Claro => Tema.Escuro, _ => Tema.Sistema };
        AplicarTema();
        Salvar();
    }
}
