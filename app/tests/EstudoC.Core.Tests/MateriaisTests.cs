using EstudoC.Core.Materiais;

namespace EstudoC.Core.Tests;

/// <summary>Usa a pasta material/ real do repositório.</summary>
public class MateriaisTests
{
    private static readonly CatalogoMateriais Catalogo =
        new(Caminhos.LocalizarMateriais() ?? throw new InvalidOperationException("material/ não encontrado"));

    private static readonly BuscaMateriais Busca = new(Catalogo);

    [Fact]
    public void CarregaOs12ModulosEmOrdem()
    {
        Assert.Equal(12, Catalogo.Modulos.Count);
        Assert.StartsWith("Módulo 01", Catalogo.Modulos[0].Titulo);
        Assert.StartsWith("Módulo 12", Catalogo.Modulos[^1].Titulo);
    }

    [Fact]
    public void TitulosVemDosLinksDoReadme()
    {
        var tad = Catalogo.Modulos.Single(m => m.Pasta.EndsWith("09-exercicios-tad"));
        Assert.Contains(tad.Itens, i => i.Titulo == "Lista didática V3");
        Assert.Contains(tad.Itens, i => i.Tipo == TipoMaterial.Codigo && i.NomeArquivo == "conj.h");
    }

    [Fact]
    public void BuscaIgnoraAcentoEMaiuscula()
    {
        var r = Busca.Buscar("ALOCACAO DINAMICA");
        Assert.NotEmpty(r);
        Assert.Contains(r, x => x.Item.Modulo.Pasta.EndsWith("04-revisao-alocacao-dinamica"));
    }

    [Fact]
    public void BuscaInformaPaginaEDestaque()
    {
        var r = Busca.Buscar("lista duplamente encadeada");
        Assert.Contains(r, x => x.Item.Modulo.Pasta.EndsWith("10-listas-lineares") && x.Pagina is not null);

        var primeiro = r[0];
        var destacado = primeiro.Trecho.Substring(primeiro.InicioDestaque, primeiro.TamanhoDestaque);
        Assert.Equal("lista duplamente encadeada", BuscaMateriais.Normalizar(destacado));
    }

    [Fact]
    public void DependenciasDoTadInstante()
    {
        var instante = Catalogo.TodosItens().Single(i => i.NomeArquivo == "instante.h").Caminho;

        var nomes = Projetos.Workspace.Dependencias(instante).Select(Path.GetFileName).ToHashSet();

        Assert.True(nomes.SetEquals(["instante.h", "instante.c", "data.h", "data.c", "horario.h", "horario.c"]), string.Join(", ", nomes));
    }

    [Fact]
    public void NormalizarMantemTamanho()
    {
        const string texto = "Alocação Dinâmica de Memória";
        Assert.Equal(texto.Length, BuscaMateriais.Normalizar(texto).Length);
        Assert.Equal("alocacao dinamica de memoria", BuscaMateriais.Normalizar(texto));
    }
}
