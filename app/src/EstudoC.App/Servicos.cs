using EstudoC.Core;
using EstudoC.Core.Compilacao;
using EstudoC.Core.Materiais;
using EstudoC.Core.Projetos;
using EstudoC.Core.Toolchain;

namespace EstudoC.App;

/// <summary>Serviços compartilhados pela aplicação (carregados uma vez na abertura).</summary>
public sealed class Servicos
{
    public Toolchain? Toolchain { get; }
    public ICompilador? Compilador { get; }
    public CatalogoMateriais? Catalogo { get; }
    public BuscaMateriais? Busca { get; }
    public Workspace Workspace { get; } = Workspace.Padrao();

    public Servicos()
    {
        Toolchain = Toolchain.Localizar();
        if (Toolchain is not null)
            Compilador = new CompiladorGcc(Toolchain);

        if (Caminhos.LocalizarMateriais() is { } pasta)
        {
            Catalogo = new CatalogoMateriais(pasta);
            Busca = new BuscaMateriais(Catalogo);
        }
    }
}
