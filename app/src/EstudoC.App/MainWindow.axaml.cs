using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Platform.Storage;
using EstudoC.App.Abas;
using EstudoC.Core;
using EstudoC.Core.Materiais;
using EstudoC.Core.Projetos;

namespace EstudoC.App;

public partial class MainWindow : Window
{
    public Servicos Servicos { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        PreencherTrilha();
        AtualizarProjetos();
        PastaProjetos.Text = Servicos.Workspace.Raiz;

        CaixaBusca.KeyDown += (_, e) =>
        {
            if (e.Key == Key.Enter)
                AbrirBusca(CaixaBusca.Text ?? "");
        };
        BotaoBuscar.Click += (_, _) => AbrirBusca(CaixaBusca.Text ?? "");
        BotaoNovoProjeto.Click +=async (_, _) => await NovoProjetoAsync();
        BotaoAbrirPasta.Click += async (_, _) => await AbrirPastaAsync();
        BotaoInicio.Click += (_, _) => AbrirInicio();
        ListaProjetos.SelectionChanged += (_, _) =>
        {
            if (ListaProjetos.SelectedItem is ListBoxItem { Tag: Projeto p })
                AbrirProjeto(p);
            ListaProjetos.SelectedItem = null;
        };
        AddHandler(KeyDownEvent, AoTeclar, RoutingStrategies.Tunnel);
        Closing += AoFechar;

        AbrirInicio();
        foreach (var arg in Program.Argumentos.Where(Directory.Exists))
            AbrirProjeto(new Projeto(arg));
        DefinirStatus(Servicos.Toolchain is null
            ? "⚠ Compilador não encontrado: os materiais funcionam, mas não será possível compilar."
            : $"Pronto. Compilador: {Servicos.Toolchain.Raiz}");
    }

    public void DefinirStatus(string texto) => BarraStatus.Text = texto;

    // ---------- trilha e projetos ----------

    private void PreencherTrilha()
    {
        var catalogo = Servicos.Catalogo;
        if (catalogo is null)
        {
            ArvoreTrilha.ItemsSource = new[] { new TreeViewItem { Header = "Pasta 'material' não encontrada." } };
            return;
        }

        var raiz = new List<TreeViewItem>
        {
            new() { Header = "🏠 " + catalogo.Inicio.Titulo, Tag = catalogo.Inicio },
        };
        foreach (var modulo in catalogo.Modulos)
        {
            var no = new TreeViewItem { Header = modulo.Titulo.Replace("Módulo ", ""), Tag = modulo.Guia };
            no.ItemsSource = modulo.Itens.Select(i => new TreeViewItem
            {
                Header = Icone(i.Tipo) + " " + i.Titulo,
                Tag = i,
            }).ToList();
            raiz.Add(no);
        }
        ArvoreTrilha.ItemsSource = raiz;
        ArvoreTrilha.SelectionChanged += (_, _) =>
        {
            if (ArvoreTrilha.SelectedItem is TreeViewItem { Tag: ItemMaterial item })
                AbrirMaterial(item);
        };
    }

    private static string Icone(TipoMaterial tipo) => tipo switch
    {
        TipoMaterial.Guia => "🧭",
        TipoMaterial.Codigo => "💾",
        _ => "📄",
    };

    public void AtualizarProjetos()
    {
        ListaProjetos.ItemsSource = Servicos.Workspace.Listar()
            .Select(p => new ListBoxItem { Content = "📁 " + p.Nome, Tag = p })
            .ToList();
    }

    // ---------- abas ----------

    private void AbrirAba(string chave, string titulo, Control conteudo)
    {
        var existente = Abas.Items.OfType<TabItem>().FirstOrDefault(t => (string?)t.Tag == chave);
        if (existente is not null)
        {
            Abas.SelectedItem = existente;
            return;
        }

        var aba = new TabItem { Tag = chave, Content = conteudo };
        var fechar = new Button { Content = "✕", Classes = { "fechar" }, VerticalAlignment = VerticalAlignment.Center };
        ToolTip.SetTip(fechar, "Fechar aba");
        fechar.Click += async (_, _) => await FecharAbaAsync(aba);
        aba.Header = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Children = { new TextBlock { Text = titulo, VerticalAlignment = VerticalAlignment.Center, MaxWidth = 260, TextTrimming = Avalonia.Media.TextTrimming.CharacterEllipsis }, fechar },
        };
        ToolTip.SetTip(aba, titulo);
        Abas.Items.Add(aba);
        Abas.SelectedItem = aba;
    }

    private T? AbaExistente<T>(string chave) where T : Control =>
        Abas.Items.OfType<TabItem>().FirstOrDefault(t => (string?)t.Tag == chave)?.Content as T;

    private static async Task FecharConteudoAsync(object? conteudo)
    {
        if (conteudo is AbaProjeto projeto)
            await projeto.EncerrarAsync();
    }

    private async Task FecharAbaAsync(TabItem aba)
    {
        await FecharConteudoAsync(aba.Content);
        Abas.Items.Remove(aba);
        ArvoreTrilha.SelectedItem = null; // permite reabrir o mesmo item com um clique
    }

    public void AbrirInicio()
    {
        if (Servicos.Catalogo is { } c)
            AbrirMaterial(c.Inicio);
    }

    public void AbrirMaterial(ItemMaterial item, int linha = 0, string? destaque = null)
    {
        var chave = "material:" + item.Caminho;
        var titulo = item.Tipo == TipoMaterial.Guia ? item.Modulo.Titulo : item.Titulo;
        if (AbaExistente<AbaMaterial>(chave) is not { } aba)
        {
            aba = new AbaMaterial(item, this);
            AbrirAba(chave, titulo, aba);
        }
        else
        {
            AbrirAba(chave, titulo, aba);
        }
        if (linha > 0)
            aba.IrParaLinha(linha, destaque);
    }

    /// <summary>Links dentro dos guias: outro arquivo de material ou outro módulo.</summary>
    public void AbrirLink(string pastaAtual, string url)
    {
        if (url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            DefinirStatus("Link externo (requer internet): " + url);
            return;
        }
        var semAncora = Uri.UnescapeDataString(url.Split('#')[0]);
        var caminho = Path.GetFullPath(Path.Combine(pastaAtual, semAncora));
        if (Servicos.Catalogo?.Encontrar(caminho) is { } item)
            AbrirMaterial(item);
        else
            DefinirStatus("Arquivo não faz parte da trilha: " + semAncora);
    }

    public void AbrirBusca(string consulta)
    {
        if (Servicos.Busca is null)
            return;
        if (AbaExistente<AbaBusca>("busca") is { } aba)
        {
            AbrirAba("busca", "🔎 Busca", aba);
            aba.Buscar(consulta);
            return;
        }
        var nova = new AbaBusca(Servicos.Busca, this);
        AbrirAba("busca", "🔎 Busca", nova);
        nova.Buscar(consulta);
    }

    public void AbrirProjeto(Projeto projeto)
    {
        if (Servicos.Compilador is null || Servicos.Toolchain is null)
        {
            _ = Dialogos.AvisarAsync(this, "Compilador ausente", "O compilador embutido (pasta 'toolchain') não foi encontrado. Reinstale o Estudo C.");
            return;
        }
        var chave = "projeto:" + projeto.Pasta;
        var aba = AbaExistente<AbaProjeto>(chave) ?? new AbaProjeto(projeto, this);
        AbrirAba(chave, "⚙ " + projeto.Nome, aba);
    }

    public void AbrirComoProjeto(ItemMaterial item)
    {
        var projeto = Servicos.Workspace.CriarDeMaterial(item.Caminho);
        AtualizarProjetos();
        AbrirProjeto(projeto);
        DefinirStatus($"Projeto '{projeto.Nome}' criado com cópias de: {string.Join(", ", projeto.Arquivos().Select(Path.GetFileName))}");
    }

    private async Task NovoProjetoAsync()
    {
        if (await Dialogos.NovoProjetoAsync(this) is not { } escolha)
            return;
        var projeto = Servicos.Workspace.Criar(escolha.Nome, escolha.Modelo);
        AtualizarProjetos();
        AbrirProjeto(projeto);
    }

    private async Task AbrirPastaAsync()
    {
        var pastas = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions { Title = "Pasta do projeto (arquivos .c e .h)" });
        if (pastas.FirstOrDefault()?.TryGetLocalPath() is { } caminho)
            AbrirProjeto(new Projeto(caminho));
    }

    // ---------- teclado e fechamento ----------

    private void AoTeclar(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.F && e.KeyModifiers == (KeyModifiers.Control | KeyModifiers.Shift))
        {
            CaixaBusca.Focus();
            CaixaBusca.SelectAll();
            e.Handled = true;
        }
    }

    private bool _fechando;

    private async void AoFechar(object? sender, WindowClosingEventArgs e)
    {
        if (_fechando)
            return;
        e.Cancel = true;
        _fechando = true;
        foreach (var aba in Abas.Items.OfType<TabItem>().ToList())
            await FecharConteudoAsync(aba.Content); // salva arquivos e encerra programas/depuração
        Close();
    }
}
