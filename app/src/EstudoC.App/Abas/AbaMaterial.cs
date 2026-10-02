using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using AvaloniaEdit;
using EstudoC.App.Editor;
using EstudoC.Core.Materiais;
using Markdown.Avalonia;

namespace EstudoC.App.Abas;

/// <summary>
/// Mostra um material. Guias (README) são renderizados como Markdown com links clicáveis;
/// textos convertidos de PDF e códigos aparecem num visualizador de texto (preserva o layout original).
/// </summary>
public sealed class AbaMaterial : UserControl
{
    private readonly ItemMaterial _item;
    private readonly TextEditor? _visualizador;

    public AbaMaterial(ItemMaterial item, MainWindow janela)
    {
        _item = item;
        var texto = File.ReadAllText(item.Caminho);

        if (item.Tipo == TipoMaterial.Guia)
        {
            var pasta = Path.GetDirectoryName(item.Caminho)!;
            var motor = new Markdown.Avalonia.Markdown
            {
                HyperlinkCommand = new Comando(url => janela.AbrirLink(pasta, url)),
                AssetPathRoot = pasta,
            };
            Content = new MarkdownScrollViewer
            {
                Engine = motor,
                Markdown = texto,
                Margin = new Thickness(24, 12, 24, 12),
                MaxWidth = 1100,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            return;
        }

        var ehCodigo = item.Tipo == TipoMaterial.Codigo;
        _visualizador = FabricaEditor.Criar(ehCodigo ? "c" : null, somenteLeitura: true);
        _visualizador.Text = texto;
        _visualizador.WordWrap = false; // preserva o alinhamento do texto extraído do PDF
        _visualizador.Padding = new Thickness(8, 4);

        var barra = new DockPanel { Margin = new Thickness(10, 6) };
        var dica = new TextBlock
        {
            Text = ehCodigo
                ? "Código original do material (somente leitura). Ctrl+F procura neste arquivo."
                : "Ctrl+F procura neste texto · a busca no topo procura em todos os materiais.",
            Opacity = 0.7,
            FontSize = 12,
            VerticalAlignment = VerticalAlignment.Center,
        };
        if (ehCodigo)
        {
            var abrir = new Button { Content = "▶ Abrir como projeto (cópia editável)", Classes = { "ferramenta" } };
            ToolTip.SetTip(abrir, "Copia este arquivo e os que ele inclui (#include \"...\") para um projeto novo, onde você pode editar, compilar e depurar.");
            abrir.Click += (_, _) => janela.AbrirComoProjeto(item);
            DockPanel.SetDock(abrir, Dock.Right);
            barra.Children.Add(abrir);
        }
        barra.Children.Add(dica);

        var painel = new DockPanel();
        DockPanel.SetDock(barra, Dock.Top);
        painel.Children.Add(barra);
        painel.Children.Add(_visualizador);
        Content = painel;
    }

    /// <summary>Rola até a linha e seleciona o trecho buscado.</summary>
    public void IrParaLinha(int linha, string? destaque)
    {
        if (_visualizador is null)
            return;
        Dispatcher.UIThread.Post(() =>
        {
            var doc = _visualizador.Document;
            if (linha < 1 || linha > doc.LineCount)
                return;
            var l = doc.GetLineByNumber(linha);
            var inicio = l.Offset;
            var tamanho = 0;
            if (!string.IsNullOrWhiteSpace(destaque))
            {
                var pos = BuscaMateriais.Normalizar(doc.GetText(l)).IndexOf(BuscaMateriais.Normalizar(destaque.Trim()), StringComparison.Ordinal);
                if (pos >= 0)
                    (inicio, tamanho) = (l.Offset + pos, destaque.Trim().Length);
            }
            _visualizador.ScrollTo(linha, 1);
            _visualizador.Select(inicio, tamanho);
            _visualizador.TextArea.Caret.Offset = inicio;
            _visualizador.TextArea.Caret.BringCaretToView();
            _visualizador.Focus();
        }, DispatcherPriority.Loaded);
    }

    public override string ToString() => _item.Titulo;

    private sealed class Comando(Action<string> acao) : ICommand
    {
        public event EventHandler? CanExecuteChanged { add { } remove { } }
        public bool CanExecute(object? parametro) => true;
        public void Execute(object? parametro) => acao(parametro?.ToString() ?? "");
    }
}
