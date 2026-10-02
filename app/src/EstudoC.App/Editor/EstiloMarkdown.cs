using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using ColorTextBlock.Avalonia;
using Markdown.Avalonia;

namespace EstudoC.App.Editor;

/// <summary>Ajustes visuais da leitura em Markdown: código em fonte monoespaçada (preserva colunas dos slides).</summary>
public static class EstiloMarkdown
{
    public static MarkdownScrollViewer Aplicar(MarkdownScrollViewer viewer)
    {
        foreach (var seletor in new Func<Selector?, Selector>[]
                 {
                     s => s.Class("CodeBlock").Descendant().OfType<TextBlock>(),
                     s => s.Class("CodeBlock").Descendant().OfType<TemplatedControl>(),
                     s => s.OfType<TextBlock>().Class("CodeBlock"),
                     s => s.OfType<CTextBlock>().Class("CodeBlock"),
                     s => s.OfType<CCode>(),
                 })
        {
            viewer.Styles.Add(new Style(seletor)
            {
                Setters =
                {
                    new Setter(TextBlock.FontFamilyProperty, FabricaEditor.FonteMono),
                    new Setter(TextBlock.FontSizeProperty, 13.0),
                },
            });
        }
        return viewer;
    }
}
