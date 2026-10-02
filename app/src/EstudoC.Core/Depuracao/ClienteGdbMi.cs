using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace EstudoC.Core.Depuracao;

public sealed class ErroGdbException(string mensagem) : Exception(mensagem);

/// <summary>Conversa com o gdb pelo protocolo MI: envia comandos numerados e casa as respostas.</summary>
public sealed class ClienteGdbMi : IAsyncDisposable
{
    private readonly Process _processo;
    private readonly ConcurrentDictionary<int, TaskCompletionSource<RegistroMi>> _pendentes = new();
    private readonly SemaphoreSlim _escrita = new(1, 1);
    private int _proximoToken;

    /// <summary>Registros assíncronos (*stopped, *running, =thread-group-exited…).</summary>
    public event Action<RegistroMi>? Assincrono;
    /// <summary>Texto informativo do gdb (console/log).</summary>
    public event Action<string>? Log;
    public event Action? Encerrado;

    private ClienteGdbMi(Process processo) => _processo = processo;

    public static ClienteGdbMi Iniciar(Toolchain.Toolchain toolchain, string pastaTrabalho)
    {
        var info = new ProcessStartInfo(toolchain.Gdb)
        {
            WorkingDirectory = pastaTrabalho,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardInputEncoding = new UTF8Encoding(false),
        };
        foreach (var a in new[] { "--interpreter=mi3", "-q", "-nx" })
            info.ArgumentList.Add(a);
        toolchain.PrepararAmbiente(info);

        var processo = Process.Start(info) ?? throw new InvalidOperationException("Não foi possível iniciar o gdb.");
        var cliente = new ClienteGdbMi(processo);
        _ = Task.Run(cliente.LerSaidaAsync);
        _ = Task.Run(async () =>
        {
            while (await processo.StandardError.ReadLineAsync() is { } linha)
                cliente.Log?.Invoke(linha + "\n");
        });
        return cliente;
    }

    /// <summary>Envia um comando MI (ex.: "-exec-next") e espera ^done/^running. ^error vira exceção.</summary>
    public async Task<RegistroMi> EnviarAsync(string comando, TimeSpan? limite = null)
    {
        var token = Interlocked.Increment(ref _proximoToken);
        var tcs = new TaskCompletionSource<RegistroMi>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pendentes[token] = tcs;

        await _escrita.WaitAsync();
        try
        {
            await _processo.StandardInput.WriteLineAsync($"{token}{comando}");
            await _processo.StandardInput.FlushAsync();
        }
        catch (IOException)
        {
            _pendentes.TryRemove(token, out _);
            throw new ErroGdbException("O depurador foi encerrado.");
        }
        finally
        {
            _escrita.Release();
        }

        var resposta = await tcs.Task.WaitAsync(limite ?? TimeSpan.FromSeconds(30));
        if (resposta.Classe == "error")
            throw new ErroGdbException(resposta.Dados["msg"]?.Texto ?? "erro do gdb");
        return resposta;
    }

    private async Task LerSaidaAsync()
    {
        try
        {
            while (await _processo.StandardOutput.ReadLineAsync() is { } linha)
            {
                var registro = ParserMi.AnalisarLinha(linha);
                switch (registro.Tipo)
                {
                    case TipoRegistroMi.Resultado when registro.Token is { } t && _pendentes.TryRemove(t, out var tcs):
                        tcs.TrySetResult(registro);
                        break;
                    case TipoRegistroMi.Assincrono:
                        Assincrono?.Invoke(registro);
                        break;
                    case TipoRegistroMi.Console or TipoRegistroMi.Alvo or TipoRegistroMi.Log:
                        Log?.Invoke(registro.TextoStream ?? "");
                        break;
                }
            }
        }
        finally
        {
            foreach (var p in _pendentes.Values)
                p.TrySetException(new ErroGdbException("O depurador foi encerrado."));
            _pendentes.Clear();
            Encerrado?.Invoke();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_processo.HasExited)
        {
            try
            {
                await EnviarAsync("-gdb-exit", TimeSpan.FromSeconds(2));
            }
            catch (Exception e) when (e is ErroGdbException or TimeoutException) { }
            if (!_processo.WaitForExit(2000))
                try { _processo.Kill(true); } catch (InvalidOperationException) { }
        }
        _processo.Dispose();
    }
}
