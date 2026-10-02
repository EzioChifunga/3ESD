namespace EstudoC.Core.Depuracao;

public enum EstadoDepuracao { Parado, Executando, Pausado, Encerrado }

/// <summary>Onde e por que a execução parou.</summary>
public sealed record InfoParada(string Motivo, string? Arquivo, int Linha, string? Funcao, bool ErroFatal);

public sealed record Quadro(int Nivel, string Funcao, string? Arquivo, int Linha)
{
    public string Descricao => Arquivo is null ? $"{Funcao}  (sem código-fonte)" : $"{Funcao}  —  {Path.GetFileName(Arquivo)}:{Linha}";
}

public sealed record Variavel(string Nome, string Valor, string Tipo, bool EhArgumento);

public sealed record PontoParada(string Arquivo, int Linha);

/// <summary>Depurador independente de implementação (hoje: GDB/MI).</summary>
public interface IDepurador : IAsyncDisposable
{
    EstadoDepuracao Estado { get; }

    event Action<InfoParada>? Parou;
    event Action? Continuou;
    event Action<int?>? Terminou;
    event Action<string>? Mensagem;

    Task IniciarAsync(string executavel, string pastaTrabalho, IEnumerable<PontoParada> pontos);
    Task ContinuarAsync();
    Task PausarAsync();
    Task PassoSobreAsync();
    Task PassoDentroAsync();
    Task PassoForaAsync();
    Task PararAsync();

    Task AdicionarPontoAsync(PontoParada ponto);
    Task RemoverPontoAsync(PontoParada ponto);

    Task<IReadOnlyList<Quadro>> PilhaAsync();
    Task<IReadOnlyList<Variavel>> VariaveisAsync(int nivelQuadro);
    Task<string> AvaliarAsync(string expressao, int nivelQuadro);
}
