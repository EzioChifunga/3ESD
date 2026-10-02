using System.Text.RegularExpressions;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.VisualTree;
using AvaloniaEdit;
using EstudoC.App.Editor;
using EstudoC.Core.Materiais;
using Markdown.Avalonia;

namespace EstudoC.App.Abas;

/// <summary>
/// Mostra um material.
/// - Guias (README) e textos: modo LEITURA, renderizado (títulos, listas, blocos de código, páginas do PDF).
/// - Textos têm também o modo TEXTO ORIGINAL (layout exato extraído do PDF, com Ctrl+F).
/// - Códigos .c/.h: visualizador com realce de sintaxe e "Abrir como projeto".
/// </summary>
public sealed partial class AbaMaterial : UserControl
{
    private readonly ItemMaterial _item;
    private readonly MainWindow _janela;
    private readonly string _texto;
    private readonly ContentControl _area = new();
    private readonly Button? _alternar;
    private TextEditor? _original;
    private MarkdownScrollViewer? _leitura;
    private readonly List<int> _paginas = [];
    private bool _modoOriginal;

    [GeneratedRegex(@"^<!--\s*página\s+(\d+)\s*-->$")]
    private static partial Regex RegexPagina();

    public AbaMaterial(ItemMaterial item, MainWindow janela)
    {
        _item = item;
        _janela = janela;
        _texto = File.ReadAllText(item.Caminho);

        var barra = new DockPanel { Margin = new Thickness(10, 6) };
        var dica = new TextBlock { Opacity = 0.7, FontSize = 12, VerticalAlignment = VerticalAlignment.Center };

        if (item.Tipo == TipoMaterial.Codigo)
        {
            var abrir = new Button { Content = "▶ Abrir como projeto (cópia editável)", Classes = { "ferramenta" } };
            ToolTip.SetTip(abrir, "Copia este arquivo e os que ele inclui (#include \"...\") para um projeto novo, onde você pode editar, compilar e depurar.");
            abrir.Click += (_, _) => janela.AbrirComoProjeto(item);
            DockPanel.SetDock(abrir, Dock.Right);
            barra.Children.Add(abrir);
            dica.Text = "Código original do material (somente leitura). Ctrl+F procura neste arquivo.";
            _modoOriginal = true;
        }
        else if (item.Tipo == TipoMaterial.Texto && LeituraMaterial.PrecisaConverter(_texto))
        {
            _alternar = new Button { Classes = { "ferramenta" } };
            ToolTip.SetTip(_alternar, "Alterna entre a leitura formatada e o texto exatamente como foi extraído do PDF.");
            _alternar.Click += (_, _) => AlternarModo();
            DockPanel.SetDock(_alternar, Dock.Right);
            barra.Children.Add(_alternar);
            dica.Text = "Leitura formatada a partir do PDF original. Use “Texto original” para ver o layout exato e Ctrl+F.";
        }
        else
        {
            dica.Text = "A busca no topo procura em todos os materiais.";
        }
        barra.Children.Add(dica);

        var painel = new DockPanel();
        DockPanel.SetDock(barra, Dock.Top);
        painel.Children.Add(barra);
        painel.Children.Add(_area);
        Content = painel;

        MostrarModo(null);
    }

    private void AlternarModo()
    {
        _modoOriginal = !_modoOriginal;
        MostrarModo(null);
    }

    private void MostrarModo(string? destaque)
    {
        if (_alternar is not null)
            _alternar.Content = _modoOriginal ? "📖 Leitura formatada" : "📄 Texto original";

        if (_modoOriginal)
        {
            _original ??= CriarOriginal();
            _area.Content = _original;
        }
        else
        {
            _leitura = CriarLeitura(destaque);
            _area.Content = _leitura;
        }
    }

    private TextEditor CriarOriginal()
    {
        var editor = FabricaEditor.Criar(_item.Tipo == TipoMaterial.Codigo ? "c" : null, somenteLeitura: true);
        editor.Text = _texto;
        editor.WordWrap = false; // preserva o alinhamento do texto extraído do PDF
        editor.Padding = new Thickness(8, 4);
        return editor;
    }

    /// <summary>Renderiza o Markdown (todas as páginas) com os estilos do Markdown.Avalonia.</summary>
    private MarkdownScrollViewer CriarLeitura(string? destaque)
    {
        var pasta = Path.GetDirectoryName(_item.Caminho)!;
        var motor = new Markdown.Avalonia.Markdown
        {
            HyperlinkCommand = new Comando(url => _janela.AbrirLink(pasta, url)),
            AssetPathRoot = pasta,
        };

        _paginas.Clear();
        var paginas = LeituraMaterial.Converter(_texto, destaque);
        var partes = new List<string>();
        for (var i = 0; i < paginas.Count; i++)
        {
            var markdown = paginas[i].Markdown;
            if (i == 0 && _item.Tipo == TipoMaterial.Texto)
                markdown = TrocarTitulo(markdown); // nome do arquivo do PDF → título amigável
            if (paginas[i].Numero is { } numero)
                _paginas.Add(numero);
            partes.Add(markdown);
        }
        return EstiloMarkdown.Aplicar(new MarkdownScrollViewer
        {
            Engine = motor,
            Markdown = string.Join("\n\n", partes),
            Margin = new Thickness(28, 4, 8, 0),
            SelectionEnabled = true,
        });
    }

    /// <summary>Rola até o título "###### Página N" (o k-ésimo título nível 6 é a k-ésima página).</summary>
    private void RolarParaPagina(int pagina)
    {
        if (_leitura is null)
            return;
        var indice = _paginas.IndexOf(pagina);
        var titulos = _leitura.GetVisualDescendants().OfType<ColorTextBlock.Avalonia.CTextBlock>()
            .Where(c => c.Classes.Contains("Heading6")).ToList();
        if (indice < 0 || indice >= titulos.Count)
            return;
        if (titulos[indice].TranslatePoint(new Point(0, 0), _leitura) is { } ponto)
            _leitura.ScrollValue = new Vector(0, Math.Max(0, _leitura.ScrollValue.Y + ponto.Y - 40));
    }

    private string TrocarTitulo(string markdown)
    {
        var linhas = markdown.Split('\n').ToList();
        var i = linhas.FindIndex(l => l.StartsWith("# "));
        if (i >= 0)
            linhas[i] = "# " + _item.Titulo;
        return string.Join('\n', linhas);
    }

    /// <summary>Vai até a linha (do arquivo original) de um resultado de busca, destacando o termo.</summary>
    public void IrParaLinha(int linha, string? destaque)
    {
        if (_modoOriginal || _item.Tipo == TipoMaterial.Codigo)
        {
            IrParaLinhaOriginal(linha, destaque);
            return;
        }

        MostrarModo(destaque);
        var pagina = PaginaDaLinha(linha);
        if (pagina is not { } p)
            return;
        // espera o Markdown ser medido e desenhado antes de calcular a posição
        Dispatcher.UIThread.Post(() => Dispatcher.UIThread.Post(() => RolarParaPagina(p), DispatcherPriority.Background), DispatcherPriority.Loaded);
    }

    private int? PaginaDaLinha(int linha)
    {
        int? pagina = null;
        var numero = 0;
        foreach (var l in _texto.Split('\n'))
        {
            if (++numero > linha)
                break;
            var m = RegexPagina().Match(l.Trim());
            if (m.Success)
                pagina = int.Parse(m.Groups[1].Value);
        }
        return pagina;
    }

    private void IrParaLinhaOriginal(int linha, string? destaque)
    {
        _original ??= CriarOriginal();
        _area.Content = _original;
        var editor = _original;
        Dispatcher.UIThread.Post(() =>
        {
            var doc = editor.Document;
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
            editor.ScrollTo(linha, 1);
            editor.Select(inicio, tamanho);
            editor.TextArea.Caret.Offset = inicio;
            editor.TextArea.Caret.BringCaretToView();
            editor.Focus();
        }, DispatcherPriority.Loaded);
    }

    public override string ToString() => _item.Titulo;

    internal sealed class Comando(Action<string> acao) : ICommand
    {
        public event EventHandler? CanExecuteChanged { add { } remove { } }
        public bool CanExecute(object? parametro) => true;
        public void Execute(object? parametro) => acao(parametro?.ToString() ?? "");
    }
}
