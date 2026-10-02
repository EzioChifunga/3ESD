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

    private const string Apostila = """
        # Apostila

        > Fonte: `base/x/apostila.pdf`

        <!-- página 1 -->

        TIPOS ABSTRATOS DE DADOS EM C

        Como usar esta apostila
        Leia os capítulos antes da aula. Não tente decorar o código: desenhe o que acontece e use
        os checkpoints como teste de domínio.

        1. Antes do TAD: qual problema estamos tentando resolver?

        1.1 Tipos primitivos: ótimos, mas pequenos demais
        Em C, tipos como int e char representam valores básicos e p->prox acessa um campo.

        Atenção conceitual
        Uma struct é uma forma de agrupar dados.

        Definição: Em uma pilha, os itens são empilhados.
        Apostila de TAD • Página 1

        <!-- página 2 -->

        Questão 1) 3.0 pontos
        1. Sobre estruturas de dados, assinale a alternativa INCORRETA.
        a) Em uma pilha, é implementada uma política LIFO.
        b) Nas filas, a política é FIFO.
        I - Inserção e remoção acontecem apenas na cabeça.
        Apostila de TAD • Página 2

        <!-- página 3 -->

        Apostila de TAD • Página 3
        """;

    [Fact]
    public void EstruturaTitulosParagrafosEListas()
    {
        var md = string.Join("\n\n", LeituraMaterial.Converter(Apostila).Select(p => p.Markdown));

        Assert.Contains("### TIPOS ABSTRATOS DE DADOS EM C", md);                     // caixa alta → título
        Assert.Contains("#### Como usar esta apostila", md);                           // rótulo curto → subtítulo
        Assert.Contains("desenhe o que acontece e use os checkpoints", md);           // linhas quebradas reunidas
        Assert.Contains("## 1. Antes do TAD: qual problema estamos tentando resolver?", md);
        Assert.Contains("### 1.1 Tipos primitivos: ótimos, mas pequenos demais", md);
        Assert.Contains("tipos como `int` e `char`", md);                              // código em linha
        Assert.Contains("`p->prox`", md);
        Assert.Contains("> **Atenção conceitual**  \n> Uma `struct` é uma forma de agrupar dados.", md); // caixa
        Assert.Contains("**Definição:** Em uma pilha", md);                            // rótulo em negrito
        Assert.Contains("### Questão 1) 3.0 pontos", md);
        Assert.Contains("**1.** Sobre estruturas de dados", md);                       // item numerado
        Assert.Contains("- **a)** Em uma pilha", md);                                  // alternativas
        Assert.Contains("- **I.** Inserção e remoção", md);
        Assert.DoesNotContain("Página 1\n", md.Replace("###### Página 1\n", ""));     // rodapé repetido removido
        Assert.DoesNotContain("Apostila de TAD •", md);
    }

    [Fact]
    public void ContinuacaoDeFraseNaoViraItemNemTitulo()
    {
        const string texto = """
            # T

            > Fonte: `a.pdf`

            <!-- página 1 -->

            ● Uma pilha permite acesso a apenas um item de dados
            - o último inserido. Para processar o penúltimo, remova o último.

            3. Crie o TAD DVD com
            a seguinte estrutura:
            """;
        var md = LeituraMaterial.Converter(texto)[1].Markdown;

        Assert.Contains("- Uma pilha permite acesso a apenas um item de dados - o último inserido.", md);
        Assert.Contains("**3.** Crie o TAD DVD com a seguinte estrutura:", md);
        Assert.DoesNotContain("## 3.", md);
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
