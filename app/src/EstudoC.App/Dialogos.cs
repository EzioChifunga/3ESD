using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using EstudoC.Core.Projetos;

namespace EstudoC.App;

/// <summary>Janelas simples de pergunta (sem dependências extras).</summary>
public static class Dialogos
{
    /// <summary>Pede um texto. Devolve null se cancelar.</summary>
    public static async Task<string?> PedirTextoAsync(Window dono, string titulo, string pergunta, string inicial = "")
    {
        var caixa = new TextBox { Text = inicial, MinWidth = 320 };
        var (janela, ok) = Montar(titulo, new StackPanel
        {
            Spacing = 8,
            Children = { new TextBlock { Text = pergunta }, caixa },
        });
        caixa.AttachedToVisualTree += (_, _) => { caixa.Focus(); caixa.SelectAll(); };
        var confirmado = await Mostrar(dono, janela, ok);
        return confirmado && !string.IsNullOrWhiteSpace(caixa.Text) ? caixa.Text.Trim() : null;
    }

    public static async Task<(string Nome, ModeloProjeto Modelo)?> NovoProjetoAsync(Window dono)
    {
        var nome = new TextBox { Text = "meu-programa", MinWidth = 320 };
        var simples = new RadioButton { Content = "Programa simples (main.c)", IsChecked = true, GroupName = "modelo" };
        var tad = new RadioButton { Content = "TAD (main.c + ponto.h + ponto.c)", GroupName = "modelo" };
        var (janela, ok) = Montar("Novo projeto", new StackPanel
        {
            Spacing = 8,
            Children = { new TextBlock { Text = "Nome do projeto:" }, nome, new TextBlock { Text = "Modelo:", Margin = new Thickness(0, 8, 0, 0) }, simples, tad },
        });
        nome.AttachedToVisualTree += (_, _) => { nome.Focus(); nome.SelectAll(); };
        if (!await Mostrar(dono, janela, ok) || string.IsNullOrWhiteSpace(nome.Text))
            return null;
        return (nome.Text.Trim(), tad.IsChecked == true ? ModeloProjeto.Tad : ModeloProjeto.OlaMundo);
    }

    public static async Task<bool> ConfirmarAsync(Window dono, string titulo, string mensagem)
    {
        var (janela, ok) = Montar(titulo, new TextBlock { Text = mensagem, TextWrapping = Avalonia.Media.TextWrapping.Wrap, MaxWidth = 480 });
        return await Mostrar(dono, janela, ok);
    }

    /// <summary>Lista os exemplos (agrupados por módulo) para escolher um.</summary>
    public static async Task<Core.Exemplos.Exemplo?> EscolherExemploAsync(Window dono, Core.Exemplos.CatalogoExemplos catalogo)
    {
        var lista = new ListBox { Height = 420, Width = 620 };
        lista.ItemsSource = catalogo.Todos.Select(e => new ListBoxItem
        {
            Tag = e,
            Content = new StackPanel
            {
                Spacing = 1,
                Children =
                {
                    new TextBlock { Text = $"{(e.Modulo == "00" ? "Primeiros passos" : "Módulo " + e.Modulo)}  ·  {e.ModoTexto}", FontSize = 11, Opacity = 0.65 },
                    new TextBlock { Text = e.Titulo, FontWeight = Avalonia.Media.FontWeight.SemiBold },
                },
            },
        }).ToList();
        lista.SelectedIndex = 0;
        var (janela, ok) = Montar("Carregar exemplo na sandbox", new StackPanel
        {
            Spacing = 8,
            Children = { new TextBlock { Text = "O código atual da sandbox será substituído pelo exemplo escolhido." }, lista },
        });
        lista.DoubleTapped += (_, _) => janela.Close(true);
        return await Mostrar(dono, janela, ok) && lista.SelectedItem is ListBoxItem { Tag: Core.Exemplos.Exemplo escolhido } ? escolhido : null;
    }

    public static async Task AvisarAsync(Window dono, string titulo, string mensagem)
    {
        var (janela, ok) = Montar(titulo, new TextBlock { Text = mensagem, TextWrapping = Avalonia.Media.TextWrapping.Wrap, MaxWidth = 480 }, cancelar: false);
        await Mostrar(dono, janela, ok);
    }

    private static (Window, Button) Montar(string titulo, Control conteudo, bool cancelar = true)
    {
        var ok = new Button { Content = "OK", IsDefault = true, MinWidth = 80, HorizontalContentAlignment = HorizontalAlignment.Center };
        var botoes = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Spacing = 8, Margin = new Thickness(0, 16, 0, 0) };
        botoes.Children.Add(ok);
        var janela = new Window
        {
            Title = titulo,
            SizeToContent = SizeToContent.WidthAndHeight,
            CanResize = false,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            ShowInTaskbar = false,
            Content = new StackPanel { Margin = new Thickness(20), Children = { conteudo, botoes } },
        };
        if (cancelar)
        {
            var cancela = new Button { Content = "Cancelar", IsCancel = true, MinWidth = 80, HorizontalContentAlignment = HorizontalAlignment.Center };
            cancela.Click += (_, _) => janela.Close(false);
            botoes.Children.Add(cancela);
        }
        janela.KeyDown += (_, e) =>
        {
            if (e.Key == Key.Escape)
                janela.Close(false);
        };
        return (janela, ok);
    }

    private static async Task<bool> Mostrar(Window dono, Window janela, Button ok)
    {
        ok.Click += (_, _) => janela.Close(true);
        return await janela.ShowDialog<bool?>(dono) == true;
    }
}
