using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;

namespace EstudoC.App.Editor;

public static class FabricaEditor
{
    public static readonly FontFamily FonteMono = new("Cascadia Mono, Consolas, Courier New, monospace");

    public static bool TemaEscuro => Application.Current?.ActualThemeVariant == ThemeVariant.Dark;

    /// <summary>Editor com fonte monoespaçada e, se <paramref name="linguagem"/> for dada, realce de sintaxe.</summary>
    public static TextEditor Criar(string? linguagem, bool somenteLeitura = false)
    {
        var editor = new TextEditor
        {
            FontFamily = FonteMono,
            FontSize = 14,
            ShowLineNumbers = true,
            IsReadOnly = somenteLeitura,
            HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
        };
        editor.Options.ConvertTabsToSpaces = true;
        editor.Options.IndentationSize = 4;
        editor.Options.HighlightCurrentLine = !somenteLeitura;
        editor.Options.EnableHyperlinks = false;
        editor.Options.EnableEmailHyperlinks = false;

        if (linguagem is not null)
        {
            var opcoes = new RegistryOptions(TemaEscuro ? ThemeName.DarkPlus : ThemeName.LightPlus);
            var textMate = editor.InstallTextMate(opcoes);
            textMate.SetGrammar(opcoes.GetScopeByLanguageId(linguagem));
        }
        return editor;
    }
}
