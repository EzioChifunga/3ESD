using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using EstudoC.Core.Materiais;

namespace EstudoC.App.Abas;

/// <summary>Resultados da busca em todos os materiais. Clique abre o material na linha encontrada.</summary>
public sealed class AbaBusca : UserControl
{
    private static readonly IBrush FundoDestaque = new SolidColorBrush(Color.FromArgb(0x70, 0xF2, 0xB7, 0x05));

    private readonly BuscaMateriais _busca;
    private readonly MainWindow _janela;
    private readonly TextBox _consulta = new() { Watermark = "Digite e aperte Enter (ignora acentos e maiúsculas)", MinWidth = 420 };
    private readonly TextBlock _resumo = new() { Opacity = 0.7, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(12, 0) };
    private readonly ListBox _resultados = new();
    private string _termoAtual = "";

    public AbaBusca(BuscaMateriais busca, MainWindow janela)
    {
        _busca = busca;
        _janela = janela;

        _consulta.KeyDown += (_, e) =>
        {
            if (e.Key == Key.Enter)
                Buscar(_consulta.Text ?? "");
        };
        // Tapped (e não SelectionChanged) para reabrir o mesmo resultado com um novo clique
        _resultados.Tapped += (_, _) => AbrirSelecionado();
        _resultados.KeyDown += (_, e) =>
        {
            if (e.Key == Key.Enter)
                AbrirSelecionado();
        };

        var topo = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(10, 8), Children = { _consulta, _resumo } };
        var painel = new DockPanel();
        DockPanel.SetDock(topo, Dock.Top);
        painel.Children.Add(topo);
        painel.Children.Add(_resultados);
        Content = painel;
    }

    private void AbrirSelecionado()
    {
        if (_resultados.SelectedItem is ListBoxItem { Tag: ResultadoBusca r })
            _janela.AbrirMaterial(r.Item, r.Linha, _termoAtual);
    }

    public void Buscar(string consulta)
    {
        _consulta.Text = consulta;
        _termoAtual = consulta.Trim();
        var resultados = _busca.Buscar(_termoAtual);
        _resumo.Text = _termoAtual.Length < 2
            ? "Digite pelo menos 2 letras."
            : resultados.Count == 0
                ? "Nada encontrado."
                : $"{resultados.Count}{(resultados.Count >= 500 ? "+" : "")} ocorrência(s) em {resultados.Select(r => r.Item).Distinct().Count()} arquivo(s)";
        _resultados.ItemsSource = resultados.Select(CriarItem).ToList();
    }

    private static ListBoxItem CriarItem(ResultadoBusca r)
    {
        var trecho = new TextBlock { TextWrapping = TextWrapping.Wrap, FontSize = 13 };
        trecho.Inlines!.Add(new Run(r.Trecho[..r.InicioDestaque]));
        trecho.Inlines.Add(new Run(r.Trecho.Substring(r.InicioDestaque, r.TamanhoDestaque)) { FontWeight = FontWeight.Bold, Background = FundoDestaque });
        trecho.Inlines.Add(new Run(r.Trecho[(r.InicioDestaque + r.TamanhoDestaque)..]));

        return new ListBoxItem
        {
            Tag = r,
            Padding = new Thickness(10, 6),
            Content = new StackPanel
            {
                Spacing = 2,
                Children =
                {
                    new TextBlock { Text = r.Local + $"  ›  linha {r.Linha}", FontSize = 11, Opacity = 0.65 },
                    trecho,
                },
            },
        };
    }
}
