using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using EstudoC.App.Editor;
using EstudoC.Core.Exemplos;
using Markdown.Avalonia;

namespace EstudoC.App.Abas;

/// <summary>Um exemplo: guia (o que observar, passo a passo) ao lado do código, e botões para usá-lo.</summary>
public sealed class AbaExemplo : UserControl
{
    public AbaExemplo(Exemplo exemplo, MainWindow janela)
    {
        // ---- barra superior
        var abrirProjeto = new Button { Content = "📁 Abrir como projeto", Classes = { "ferramenta" } };
        ToolTip.SetTip(abrirProjeto, "Cria uma cópia do exemplo em Meus projetos para você editar à vontade.");
        abrirProjeto.Click += (_, _) => janela.AbrirExemploComoProjeto(exemplo);

        var abrirSandbox = new Button { Content = "🧪 Abrir na sandbox", Classes = { "ferramenta" } };
        ToolTip.SetTip(abrirSandbox, "Carrega o exemplo na sandbox (área livre de testes). Substitui o que estiver lá.");
        abrirSandbox.Click += async (_, _) => await janela.AbrirSandboxAsync(exemplo);

        var barra = new DockPanel { Margin = new Thickness(12, 8) };
        var botoes = new StackPanel { Orientation = Orientation.Horizontal, Children = { abrirSandbox, abrirProjeto } };
        DockPanel.SetDock(botoes, Dock.Right);
        barra.Children.Add(botoes);
        barra.Children.Add(new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10,
            Children =
            {
                new Border
                {
                    Background = CorModo(exemplo.Modo),
                    CornerRadius = new CornerRadius(4),
                    Padding = new Thickness(8, 2),
                    VerticalAlignment = VerticalAlignment.Center,
                    Child = new TextBlock { Text = exemplo.ModoTexto, Foreground = Brushes.White, FontWeight = FontWeight.SemiBold },
                },
                new TextBlock { Text = exemplo.Descricao, VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap, Opacity = 0.8, MaxWidth = 700 },
            },
        });

        // ---- guia do exemplo
        var guia = EstiloMarkdown.Aplicar(new MarkdownScrollViewer
        {
            Engine = new Markdown.Avalonia.Markdown { HyperlinkCommand = new AbaMaterial.Comando(_ => { }) },
            Markdown = File.ReadAllText(exemplo.Readme),
            Margin = new Thickness(20, 4, 8, 0),
            SelectionEnabled = true,
        });

        // ---- código (somente leitura) com seletor de arquivo
        var arquivos = exemplo.Arquivos();
        var visualizador = FabricaEditor.Criar("c", somenteLeitura: true);
        visualizador.Padding = new Thickness(6, 4);
        var seletor = new ComboBox
        {
            ItemsSource = arquivos.Select(Path.GetFileName).ToList(),
            SelectedIndex = Math.Max(0, arquivos.ToList().FindIndex(a => a.EndsWith("main.c"))),
            Margin = new Thickness(6),
            MinWidth = 160,
        };
        void MostrarArquivo()
        {
            if (seletor.SelectedIndex >= 0)
                visualizador.Text = File.ReadAllText(arquivos[seletor.SelectedIndex]);
        }
        seletor.SelectionChanged += (_, _) => MostrarArquivo();
        MostrarArquivo();

        var painelCodigo = new DockPanel();
        var topoCodigo = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Children = { new TextBlock { Text = "Código:", VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(8, 0, 0, 0), Opacity = 0.7 }, seletor },
        };
        DockPanel.SetDock(topoCodigo, Dock.Top);
        painelCodigo.Children.Add(topoCodigo);
        painelCodigo.Children.Add(visualizador);

        var corpo = new Grid { ColumnDefinitions = new ColumnDefinitions("5*,4,6*") };
        var rolagemGuia = guia;
        Grid.SetColumn(rolagemGuia, 0);
        var divisor = new GridSplitter { ResizeDirection = GridResizeDirection.Columns };
        Grid.SetColumn(divisor, 1);
        Grid.SetColumn(painelCodigo, 2);
        corpo.Children.Add(rolagemGuia);
        corpo.Children.Add(divisor);
        corpo.Children.Add(painelCodigo);

        var painel = new DockPanel();
        DockPanel.SetDock(barra, Dock.Top);
        painel.Children.Add(barra);
        painel.Children.Add(corpo);
        Content = painel;
    }

    public static IBrush CorModo(ModoExemplo modo) => modo switch
    {
        ModoExemplo.Depurar => new SolidColorBrush(Color.FromRgb(0xB2, 0x5E, 0x09)),
        ModoExemplo.CorrigirErros => new SolidColorBrush(Color.FromRgb(0xB0, 0x2A, 0x37)),
        _ => new SolidColorBrush(Color.FromRgb(0x1E, 0x7B, 0x3A)),
    };
}
