namespace EstudoC.Core.Depuracao;

/// <summary>
/// Sessão de depuração com gdb. O programa roda numa janela de console própria
/// (no Windows o gdb não consegue redirecionar a E/S do programa para o app).
/// </summary>
public sealed class SessaoDepuracao(Toolchain.Toolchain toolchain) : IDepurador
{
    // A thread 1 é a do main(); ao pausar, o Windows para numa thread auxiliar.
    private const string ThreadPrincipal = "--thread 1";

    private ClienteGdbMi? _gdb;
    private readonly Dictionary<PontoParada, string> _pontos = [];

    public EstadoDepuracao Estado { get; private set; } = EstadoDepuracao.Parado;

    public event Action<InfoParada>? Parou;
    public event Action? Continuou;
    public event Action<int?>? Terminou;
    public event Action<string>? Mensagem;

    public async Task IniciarAsync(string executavel, string pastaTrabalho, IEnumerable<PontoParada> pontos)
    {
        _gdb = ClienteGdbMi.Iniciar(toolchain, pastaTrabalho);
        _gdb.Assincrono += AoReceberAssincrono;
        _gdb.Log += texto => Mensagem?.Invoke(texto);
        _gdb.Encerrado += () =>
        {
            if (Estado != EstadoDepuracao.Encerrado)
                Encerrar(null);
        };

        await _gdb.EnviarAsync("-gdb-set mi-async on");
        await _gdb.EnviarAsync("-gdb-set new-console on");
        await _gdb.EnviarAsync("-gdb-set print elements 200");
        await _gdb.EnviarAsync("-gdb-set charset UTF-8");
        await _gdb.EnviarAsync($"-file-exec-and-symbols {CitarCaminho(executavel)}");
        await _gdb.EnviarAsync($"-environment-cd {CitarCaminho(pastaTrabalho)}");

        foreach (var ponto in pontos)
            await AdicionarPontoAsync(ponto);

        Estado = EstadoDepuracao.Executando;
        await _gdb.EnviarAsync("-exec-run");
    }

    public Task ContinuarAsync() => Executar("-exec-continue");
    public Task PassoSobreAsync() => Executar($"-exec-next {ThreadPrincipal}");
    public Task PassoDentroAsync() => Executar($"-exec-step {ThreadPrincipal}");
    public Task PassoForaAsync() => Executar($"-exec-finish {ThreadPrincipal} --frame 0");

    public async Task PausarAsync()
    {
        if (_gdb is not null && Estado == EstadoDepuracao.Executando)
            await _gdb.EnviarAsync("-exec-interrupt");
    }

    public async Task PararAsync()
    {
        if (_gdb is null || Estado == EstadoDepuracao.Encerrado)
            return;
        try
        {
            if (Estado == EstadoDepuracao.Executando)
                await _gdb.EnviarAsync("-exec-interrupt", TimeSpan.FromSeconds(3));
            await _gdb.EnviarAsync("-gdb-set confirm off", TimeSpan.FromSeconds(3));
            await _gdb.EnviarAsync("-interpreter-exec console kill", TimeSpan.FromSeconds(5));
        }
        catch (Exception e) when (e is ErroGdbException or TimeoutException) { }
        await DisposeAsync();
        Encerrar(null);
    }

    public async Task AdicionarPontoAsync(PontoParada ponto)
    {
        if (_gdb is null || _pontos.ContainsKey(ponto))
            return;
        try
        {
            var r = await _gdb.EnviarAsync($"-break-insert -f {CitarCaminho($"{ponto.Arquivo}:{ponto.Linha}")}");
            if (r.Dados["bkpt"]?["number"]?.Texto is { } numero)
                _pontos[ponto] = numero;
        }
        catch (ErroGdbException e)
        {
            Mensagem?.Invoke($"Não foi possível criar o ponto de parada na linha {ponto.Linha}: {e.Message}\n");
        }
    }

    public async Task RemoverPontoAsync(PontoParada ponto)
    {
        if (_gdb is null || !_pontos.Remove(ponto, out var numero))
            return;
        try { await _gdb.EnviarAsync($"-break-delete {numero}"); }
        catch (ErroGdbException) { }
    }

    public async Task<IReadOnlyList<Quadro>> PilhaAsync()
    {
        if (_gdb is null || Estado != EstadoDepuracao.Pausado)
            return [];
        var r = await _gdb.EnviarAsync($"-stack-list-frames {ThreadPrincipal}");
        return r.Dados["stack"]?.Itens.Select(QuadroDe).ToList() ?? [];
    }

    public async Task<IReadOnlyList<Variavel>> VariaveisAsync(int nivelQuadro)
    {
        if (_gdb is null || Estado != EstadoDepuracao.Pausado)
            return [];
        try
        {
            var r = await _gdb.EnviarAsync($"-stack-list-variables {ThreadPrincipal} --frame {nivelQuadro} --all-values");
            var lista = new List<Variavel>();
            foreach (var v in r.Dados["variables"]?.Itens ?? [])
            {
                var nome = v["name"]?.Texto ?? "?";
                var tipo = await TipoAsync(nome, nivelQuadro);
                lista.Add(new Variavel(nome, v["value"]?.Texto ?? "<indisponível>", tipo, v["arg"]?.Texto == "1"));
            }
            return lista;
        }
        catch (ErroGdbException)
        {
            return []; // quadro sem informação de depuração
        }
    }

    public async Task<string> AvaliarAsync(string expressao, int nivelQuadro)
    {
        if (_gdb is null || Estado != EstadoDepuracao.Pausado)
            return "<programa não está pausado>";
        try
        {
            var r = await _gdb.EnviarAsync($"-data-evaluate-expression {ThreadPrincipal} --frame {nivelQuadro} {Citar(expressao)}");
            return r.Dados["value"]?.Texto ?? "";
        }
        catch (ErroGdbException e)
        {
            return "<" + e.Message + ">";
        }
    }

    private async Task<string> TipoAsync(string nome, int nivelQuadro)
    {
        try
        {
            var r = await _gdb!.EnviarAsync($"-var-create {ThreadPrincipal} --frame {nivelQuadro} - * {Citar(nome)}");
            var tipo = r.Dados["type"]?.Texto ?? "";
            if (r.Dados["name"]?.Texto is { } varObj)
                await _gdb.EnviarAsync($"-var-delete {varObj}");
            return tipo;
        }
        catch (ErroGdbException)
        {
            return "";
        }
    }

    private async Task Executar(string comando)
    {
        if (_gdb is null || Estado != EstadoDepuracao.Pausado)
            return;
        try
        {
            await _gdb.EnviarAsync(comando);
        }
        catch (ErroGdbException e)
        {
            Mensagem?.Invoke(e.Message.Contains("outermost")
                ? "Não há função para sair: você já está na main.\n"
                : e.Message + "\n");
        }
    }

    private void AoReceberAssincrono(RegistroMi r)
    {
        switch (r.Classe)
        {
            case "running":
                Estado = EstadoDepuracao.Executando;
                Continuou?.Invoke();
                break;
            case "stopped":
                AoParar(r.Dados);
                break;
            case "thread-group-exited":
                var codigo = r.Dados["exit-code"]?.Texto is { } t ? unchecked((int)Convert.ToUInt32(t, 8)) : (int?)null;
                Encerrar(codigo);
                break;
        }
    }

    private void AoParar(MiTupla dados)
    {
        var motivo = dados["reason"]?.Texto ?? "";
        if (motivo is "exited-normally" or "exited" or "exited-signalled")
            return; // tratado em thread-group-exited

        Estado = EstadoDepuracao.Pausado;
        _ = InformarParadaAsync(motivo, dados);
    }

    private async Task InformarParadaAsync(string motivo, MiTupla dados)
    {
        var sinal = dados["signal-name"]?.Texto;
        // Ao pausar (SIGTRAP em thread auxiliar) ou ao parar dentro de biblioteca,
        // mostra o primeiro quadro da thread principal que tem código-fonte.
        Quadro? quadro = null;
        try
        {
            quadro = (await PilhaAsync()).FirstOrDefault(q => q.Arquivo is not null);
        }
        catch (ErroGdbException) { }

        var (texto, fatal) = (motivo, sinal) switch
        {
            ("breakpoint-hit", _) => ("Ponto de parada", false),
            ("end-stepping-range", _) => ("Passo concluído", false),
            ("function-finished", _) => ("Função concluída", false),
            ("signal-received", "SIGSEGV") => ("Falha de segmentação (acesso inválido à memória) nesta linha", true),
            ("signal-received", "SIGFPE") => ("Erro aritmético (divisão por zero?) nesta linha", true),
            ("signal-received", "SIGABRT") => ("Programa abortado (abort/assert)", true),
            ("signal-received", "SIGTRAP" or "SIGINT") => ("Pausado", false),
            ("signal-received", _) => ($"Sinal recebido: {sinal}", true),
            _ => ("Pausado", false),
        };
        Parou?.Invoke(new InfoParada(texto, quadro?.Arquivo, quadro?.Linha ?? 0, quadro?.Funcao, fatal));
    }

    private void Encerrar(int? codigo)
    {
        if (Estado == EstadoDepuracao.Encerrado)
            return;
        Estado = EstadoDepuracao.Encerrado;
        Terminou?.Invoke(codigo);
    }

    private static Quadro QuadroDe(MiValor f)
    {
        var arquivo = f["fullname"]?.Texto;
        if (arquivo is not null && !File.Exists(arquivo))
            arquivo = null;
        return new Quadro(
            int.TryParse(f["level"]?.Texto, out var nivel) ? nivel : 0,
            f["func"]?.Texto ?? "??",
            arquivo,
            int.TryParse(f["line"]?.Texto, out var linha) ? linha : 0);
    }

    /// <summary>Caminho entre aspas para o MI (barras normais evitam problemas com '\').</summary>
    private static string CitarCaminho(string s) => Citar(s.Replace('\\', '/'));

    /// <summary>Texto entre aspas no formato c-string do MI.</summary>
    private static string Citar(string s) =>
        "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";

    public async ValueTask DisposeAsync()
    {
        if (_gdb is not null)
        {
            var gdb = _gdb;
            _gdb = null;
            await gdb.DisposeAsync();
        }
    }
}
