namespace EstudoC.Core.Toolchain;

/// <summary>Compilador e depurador embutidos (w64devkit: GCC + GDB).</summary>
public sealed class Toolchain
{
    public string Raiz { get; }
    public string PastaBin => Path.Combine(Raiz, "bin");
    public string Gcc => Path.Combine(PastaBin, "gcc.exe");
    public string Gdb => Path.Combine(PastaBin, "gdb.exe");

    public Toolchain(string raiz) => Raiz = raiz;

    public bool Valido => File.Exists(Gcc) && File.Exists(Gdb);

    /// <summary>
    /// Procura o toolchain: primeiro em "toolchain/" ao lado do executável (instalado),
    /// depois em "vendor/w64devkit" subindo a partir do executável (desenvolvimento).
    /// </summary>
    public static Toolchain? Localizar(string? pastaBase = null)
    {
        var inicio = pastaBase ?? AppContext.BaseDirectory;
        var instalado = new Toolchain(Path.Combine(inicio, "toolchain"));
        if (instalado.Valido)
            return instalado;

        foreach (var pasta in Caminhos.Ancestrais(inicio))
        {
            var dev = new Toolchain(Path.Combine(pasta, "vendor", "w64devkit"));
            if (dev.Valido)
                return dev;
        }
        return null;
    }

    /// <summary>Ambiente para processos do toolchain: bin do GCC na frente do PATH.</summary>
    public void PrepararAmbiente(System.Diagnostics.ProcessStartInfo info)
    {
        var path = info.Environment.TryGetValue("PATH", out var atual) ? atual : Environment.GetEnvironmentVariable("PATH");
        info.Environment["PATH"] = PastaBin + Path.PathSeparator + path;
        // mensagens do gcc sempre em inglês e sem cor: o parser e as dicas dependem disso
        info.Environment["LC_ALL"] = "C";
        info.Environment["LANG"] = "C";
        info.Environment["GCC_COLORS"] = "";
    }
}
