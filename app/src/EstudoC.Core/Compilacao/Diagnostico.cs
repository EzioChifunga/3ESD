namespace EstudoC.Core.Compilacao;

public enum Severidade { Erro, Aviso, Nota }

/// <summary>Um erro/aviso do compilador. Linha e coluna começam em 1 (0 = sem posição, ex.: erro de ligação).</summary>
public sealed record Diagnostico(
    string? Arquivo,
    int Linha,
    int Coluna,
    Severidade Severidade,
    string Mensagem,
    string? Dica)
{
    public string NomeArquivo => Arquivo is null ? "" : Path.GetFileName(Arquivo);

    public string SeveridadeTexto => Severidade switch
    {
        Severidade.Erro => "erro",
        Severidade.Aviso => "aviso",
        _ => "nota",
    };

    public string Posicao => Linha > 0 ? $"{NomeArquivo}:{Linha}:{Coluna}" : NomeArquivo;
}

public sealed record ResultadoCompilacao(bool Sucesso, IReadOnlyList<Diagnostico> Diagnosticos, string SaidaBruta, string? Executavel)
{
    public int Erros => Diagnosticos.Count(d => d.Severidade == Severidade.Erro);
    public int Avisos => Diagnosticos.Count(d => d.Severidade == Severidade.Aviso);
}
