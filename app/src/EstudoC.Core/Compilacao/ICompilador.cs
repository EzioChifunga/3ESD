using EstudoC.Core.Projetos;

namespace EstudoC.Core.Compilacao;

public interface ICompilador
{
    /// <summary>Compila todos os .c do projeto em um executável com informação de depuração.</summary>
    Task<ResultadoCompilacao> CompilarAsync(Projeto projeto, CancellationToken ct = default);

    /// <summary>
    /// Só verifica erros (sem gerar executável), usando o conteúdo em edição — mesmo ainda não salvo.
    /// <paramref name="conteudos"/>: caminho do arquivo → texto atual no editor.
    /// </summary>
    Task<IReadOnlyList<Diagnostico>> VerificarAsync(Projeto projeto, IReadOnlyDictionary<string, string> conteudos, CancellationToken ct = default);
}
