using Avalonia;
using Velopack;

namespace EstudoC.App;

internal static class Program
{
    /// <summary>Argumentos da linha de comando: uma pasta abre como projeto.</summary>
    public static string[] Argumentos { get; private set; } = [];

    [STAThread]
    public static void Main(string[] args)
    {
        Argumentos = args;
        // Instalação/atualização/desinstalação via Velopack (não faz nada em execução normal).
        VelopackApp.Build().Run();

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
