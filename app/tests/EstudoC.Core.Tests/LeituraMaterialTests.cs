using EstudoC.Core.Materiais;

namespace EstudoC.Core.Tests;

public class LeituraMaterialTests
{
    private const string Pdf = """
        # Slides

        > Fonte: `base/x/slides.pdf`

        <!-- página 1 -->

        Vetores de
        Tipos Estruturados

                                         1

        <!-- página 2 -->

        ●Uma estrutura é um novo tipo
           – Pode ser declarada em qualquer escopo;
          struct pes{
              char nome[50];
              int idade;
          };
        Capítulo 3 — Lista Sequencial            14
        """;

    [Fact]
    public void SeparaPaginasListasECodigo()
    {
        var paginas = LeituraMaterial.Converter(Pdf);

        Assert.Equal([null, 1, 2], paginas.Select(p => p.Numero));
        Assert.Contains("> Fonte:", paginas[0].Markdown);
        Assert.DoesNotContain("\n1\n", paginas[1].Markdown);          // número de slide solto some
        var p2 = paginas[2].Markdown;
        Assert.Contains("- Uma estrutura é um novo tipo", p2);
        Assert.Contains("- Pode ser declarada em qualquer escopo;", p2); // texto com ; não vira código
        Assert.Contains("```\nstruct pes{\n    char nome[50];", p2); // código sem escapes, recuo relativo preservado
        Assert.Contains("Capítulo 3 — Lista Sequencial — p. 14", p2);
    }

    [Fact]
    public void DestacaTermoBuscadoSemAcento()
    {
        var paginas = LeituraMaterial.Converter(Pdf, "estrutura");
        Assert.Contains("**Estrutura**dos", paginas[1].Markdown);
    }

    [Fact]
    public void MarkdownDeVerdadePassaDireto()
    {
        const string docx = "# Trabalho\n\n> Fonte: `base/material/t.docx`\n\n## Objetivo\n- item\n";
        var paginas = LeituraMaterial.Converter(docx);
        Assert.Single(paginas);
        Assert.Contains("## Objetivo\n- item", paginas[0].Markdown);
    }

    [Fact]
    public void EscapaCaracteresDoMarkdown()
    {
        var paginas = LeituraMaterial.Converter("# T\n\n> Fonte: `a.txt`\n\nuse *p e v_1 [x]\n# não é título\n");
        Assert.Contains(@"use \*p e v\_1 [x]", paginas[0].Markdown);
        Assert.Contains("​# não é título", paginas[0].Markdown);
    }

    [Fact]
    public void ConverteTodosOsMateriaisSemErro()
    {
        var catalogo = new CatalogoMateriais(Caminhos.LocalizarMateriais()!);
        foreach (var item in catalogo.TodosItens().Where(i => i.Tipo == TipoMaterial.Texto))
        {
            var paginas = LeituraMaterial.Converter(File.ReadAllText(item.Caminho));
            Assert.NotEmpty(paginas);
            // blocos de código sempre fechados
            Assert.All(paginas, p => Assert.Equal(0, p.Markdown.Split("```").Length % 2 - 1));
        }
    }
}
