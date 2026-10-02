using System.Diagnostics;
using System.Text;

namespace EstudoC.Core.Execucao;

/// <summary>
/// Executa o programa do aluno com entrada/saída redirecionadas para o terminal embutido.
/// A saída é repassada assim que chega (sem esperar '\n'), para prompts antes de scanf aparecerem.
/// </summary>
public sealed class ExecutorPrograma : IDisposable
{
    private Process? _processo;
    private readonly Stopwatch _cronometro = new();

    /// <summary>Texto vindo do programa. bool = veio de stderr.</summary>
    public event Action<string, bool>? SaidaRecebida;
    public event Action<ResultadoExecucao>? Terminou;

    public bool Rodando => _processo is { HasExited: false };

    public void Iniciar(string executavel, string pastaTrabalho, Toolchain.Toolchain? toolchain = null)
    {
        if (Rodando)
            throw new InvalidOperationException("O programa já está em execução.");

        var info = new ProcessStartInfo(executavel)
        {
            WorkingDirectory = pastaTrabalho,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        toolchain?.PrepararAmbiente(info); // DLLs de runtime do gcc, se houver

        var processo = new Process { StartInfo = info, EnableRaisingEvents = true };
        processo.Start();
        _processo = processo;
        _cronometro.Restart();

        var leituraSaida = LerAsync(processo.StandardOutput.BaseStream, false);
        var leituraErro = LerAsync(processo.StandardError.BaseStream, true);

        _ = Task.Run(async () =>
        {
            await processo.WaitForExitAsync();
            await Task.WhenAll(leituraSaida, leituraErro); // garante que toda a saída foi entregue antes do fim
            _cronometro.Stop();
            Terminou?.Invoke(new ResultadoExecucao(processo.ExitCode, _cronometro.Elapsed, ExplicacaoSaida.Para(processo.ExitCode)));
        });
    }

    /// <summary>Envia uma linha para o stdin do programa (como se o aluno digitasse e apertasse Enter).</summary>
    public void EnviarLinha(string linha)
    {
        if (!Rodando)
            return;
        try
        {
            var bytes = Encoding.UTF8.GetBytes(linha + "\n");
            _processo!.StandardInput.BaseStream.Write(bytes);
            _processo.StandardInput.BaseStream.Flush();
        }
        catch (IOException)
        {
            // o programa fechou a entrada / terminou
        }
    }

    /// <summary>Sinaliza fim de entrada (Ctrl+Z), útil para laços com while(scanf(...) != EOF).</summary>
    public void FecharEntrada()
    {
        if (Rodando)
            try { _processo!.StandardInput.Close(); } catch (IOException) { }
    }

    public void Parar()
    {
        if (Rodando)
            try { _processo!.Kill(entireProcessTree: true); } catch (InvalidOperationException) { }
    }

    private async Task LerAsync(Stream fluxo, bool ehErro)
    {
        var decodificador = Encoding.UTF8.GetDecoder();
        var bytes = new byte[4096];
        var chars = new char[Encoding.UTF8.GetMaxCharCount(bytes.Length)];
        try
        {
            int lidos;
            while ((lidos = await fluxo.ReadAsync(bytes)) > 0)
            {
                var n = decodificador.GetChars(bytes, 0, lidos, chars, 0);
                if (n > 0)
                    SaidaRecebida?.Invoke(new string(chars, 0, n), ehErro);
            }
        }
        catch (IOException)
        {
            // processo encerrado
        }
    }

    public void Dispose()
    {
        Parar();
        _processo?.Dispose();
    }
}

public sealed record ResultadoExecucao(int CodigoSaida, TimeSpan Duracao, string? Explicacao)
{
    public bool Falhou => Explicacao is not null;
}
