using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaEdit;
using EstudoC.App.Editor;
using EstudoC.Core.Compilacao;
using EstudoC.Core.Depuracao;
using EstudoC.Core.Execucao;
using EstudoC.Core.Projetos;

namespace EstudoC.App.Abas;

/// <summary>Ambiente de programação de um projeto: editor, compilação, execução e depuração.</summary>
public partial class AbaProjeto : UserControl
{
    private enum Modo { Ocioso, Compilando, Executando, Depurando }

    private readonly MainWindow _janela;
    private readonly Projeto _projeto;
    private readonly ICompilador _compilador;
    private readonly Core.Toolchain.Toolchain _toolchain;

    private readonly List<ArquivoAberto> _arquivos = [];
    private ArquivoAberto? _atual;

    private readonly TextEditor _editor;
    private readonly MargemPontosParada _margem = new();
    private readonly RealceLinhaDepuracao _realce = new();
    private readonly MarcadoresDiagnostico _marcadores = new();
    private IReadOnlyList<Diagnostico> _diagnosticos = [];

    private readonly TextEditor _terminal;
    private readonly StringBuilder _pendenteTerminal = new();
    private bool _descargaAgendada;
    private const int LimiteTerminal = 400_000;

    private Modo _modo = Modo.Ocioso;
    private ExecutorPrograma? _executor;
    private SessaoDepuracao? _depurador;
    private string? _arquivoParada;
    private int _linhaParada;
    private int _quadroSelecionado;
    private readonly List<string> _observar = [];

    private readonly DispatcherTimer _timerVerificacao = new() { Interval = TimeSpan.FromMilliseconds(900) };
    private CancellationTokenSource? _ctsVerificacao;

    public AbaProjeto(Projeto projeto, MainWindow janela)
    {
        AvaloniaXamlLoader.Load(this);
        _janela = janela;
        _projeto = projeto;
        _compilador = janela.Servicos.Compilador!;
        _toolchain = janela.Servicos.Toolchain!;

        // editor de código
        _editor = FabricaEditor.Criar("c");
        _editor.TextArea.LeftMargins.Insert(0, _margem);
        _editor.TextArea.TextView.BackgroundRenderers.Add(_realce);
        _editor.TextArea.TextView.BackgroundRenderers.Add(_marcadores);
        _margem.ObterPontos = () => _atual?.PontosParada() ?? [];
        _margem.ObterLinhaAtual = () => MesmoArquivo(_arquivoParada, _atual?.Caminho) ? _linhaParada : 0;
        _margem.Clicou += AlternarPonto;
        _editor.TextChanged += (_, _) => AoEditar();
        _editor.TextArea.Caret.PositionChanged += (_, _) => AtualizarLinhaStatus();
        Controle<ContentControl>("AreaEditor").Content = _editor;
        _editor.AttachedToVisualTree += (_, _) => Dispatcher.UIThread.Post(() => _editor.Focus(), DispatcherPriority.Background);

        // terminal
        _terminal = FabricaEditor.Criar(null, somenteLeitura: true);
        _terminal.ShowLineNumbers = false;
        _terminal.WordWrap = true;
        _terminal.FontSize = 13;
        Controle<ContentControl>("AreaTerminal").Content = _terminal;

        LigarEventos();
        CarregarArquivos();
        AtualizarBotoes();
        EscreverTerminal($"Projeto: {_projeto.Pasta}\nUse ▶ Executar (Ctrl+F5) para rodar aqui ou 🐞 Depurar (F5) para ir linha a linha.\n" +
                         "Clique na margem à esquerda dos números de linha (ou F9) para marcar um ponto de parada.\n\n");
        _timerVerificacao.Tick += async (_, _) => await VerificarAsync();
        _timerVerificacao.Start(); // primeira verificação logo ao abrir
    }

    private T Controle<T>(string nome) where T : Control => this.FindControl<T>(nome)!;

    // ================= arquivos =================

    private void CarregarArquivos(string? selecionar = null)
    {
        foreach (var caminho in _projeto.Arquivos())
            if (!_arquivos.Any(a => MesmoArquivo(a.Caminho, caminho)))
                _arquivos.Add(new ArquivoAberto(caminho));

        var lista = Controle<ListBox>("ListaArquivos");
        lista.ItemsSource = _arquivos.Select(a => new ListBoxItem { Content = Icone(a) + " " + a.Nome, Tag = a }).ToList();

        var alvo = _arquivos.FirstOrDefault(a => MesmoArquivo(a.Caminho, selecionar)) ?? _atual ?? _arquivos.FirstOrDefault();
        if (alvo is not null)
            MostrarArquivo(alvo);
    }

    private static string Icone(ArquivoAberto a) => a.Nome.EndsWith(".h", StringComparison.OrdinalIgnoreCase) ? "📘" : "📄";

    private void MostrarArquivo(ArquivoAberto arquivo)
    {
        if (_atual == arquivo)
            return;
        _atual = arquivo;
        _editor.Document = arquivo.Documento;
        _marcadores.Diagnosticos = _diagnosticos.Where(d => MesmoArquivo(d.Arquivo, arquivo.Caminho)).ToList();
        _realce.Linha = MesmoArquivo(_arquivoParada, arquivo.Caminho) ? _linhaParada : 0;

        var lista = Controle<ListBox>("ListaArquivos");
        lista.SelectedItem = lista.Items.OfType<ListBoxItem>().FirstOrDefault(i => i.Tag == arquivo);
        RedesenharEditor();
        AtualizarLinhaStatus();
    }

    private ArquivoAberto? Abrir(string? caminho)
    {
        if (caminho is null)
            return null;
        var arquivo = _arquivos.FirstOrDefault(a => MesmoArquivo(a.Caminho, caminho));
        if (arquivo is not null)
            MostrarArquivo(arquivo);
        return arquivo;
    }

    private void SalvarTudo()
    {
        foreach (var a in _arquivos)
            a.Salvar();
        AtualizarTitulosArquivos();
    }

    private void AtualizarTitulosArquivos()
    {
        foreach (var item in Controle<ListBox>("ListaArquivos").Items.OfType<ListBoxItem>())
            if (item.Tag is ArquivoAberto a)
                item.Content = Icone(a) + " " + a.Nome + (a.Modificado ? "  ●" : "");
    }

    private async Task NovoArquivoAsync(string extensao)
    {
        var nome = await Dialogos.PedirTextoAsync(_janela, "Novo arquivo", $"Nome do arquivo {extensao}:", "novo" + extensao);
        if (nome is null)
            return;
        if (!nome.EndsWith(extensao, StringComparison.OrdinalIgnoreCase))
            nome += extensao;
        var caminho = Path.Combine(_projeto.Pasta, nome);
        if (File.Exists(caminho))
        {
            await Dialogos.AvisarAsync(_janela, "Arquivo existente", $"Já existe um arquivo chamado {nome}.");
            return;
        }
        var guarda = Path.GetFileNameWithoutExtension(nome).ToUpperInvariant().Replace(' ', '_') + "_H";
        await File.WriteAllTextAsync(caminho, extensao == ".h" ? $"#ifndef {guarda}\n#define {guarda}\n\n\n\n#endif\n" : "");
        CarregarArquivos(caminho);
    }

    // ================= edição e verificação ao digitar =================

    private void AoEditar()
    {
        AtualizarTitulosArquivos();
        _timerVerificacao.Stop();
        _timerVerificacao.Start();
    }

    private async Task VerificarAsync()
    {
        _timerVerificacao.Stop();
        if (_modo == Modo.Compilando)
            return;
        _ctsVerificacao?.Cancel();
        var cts = _ctsVerificacao = new CancellationTokenSource();
        var conteudos = _arquivos.ToDictionary(a => a.Caminho, a => a.Documento.Text);
        try
        {
            var diagnosticos = await Task.Run(() => _compilador.VerificarAsync(_projeto, conteudos, cts.Token));
            if (!cts.IsCancellationRequested)
                MostrarDiagnosticos(diagnosticos, trocarAba: false);
        }
        catch (OperationCanceledException) { }
        catch (Exception e) when (e is IOException or InvalidOperationException or System.ComponentModel.Win32Exception)
        {
            _janela.DefinirStatus("Verificação de erros indisponível: " + e.Message);
        }
    }

    private void MostrarDiagnosticos(IReadOnlyList<Diagnostico> diagnosticos, bool trocarAba)
    {
        _diagnosticos = diagnosticos;
        if (_atual is not null)
            _marcadores.Diagnosticos = diagnosticos.Where(d => MesmoArquivo(d.Arquivo, _atual.Caminho)).ToList();
        RedesenharEditor();
        AtualizarLinhaStatus();

        var visiveis = diagnosticos.Where(d => d.Severidade != Severidade.Nota).ToList();
        Controle<ListBox>("ListaProblemas").ItemsSource = visiveis.Select(CriarItemProblema).ToList();
        var erros = visiveis.Count(d => d.Severidade == Severidade.Erro);
        var avisos = visiveis.Count - erros;
        Controle<TabItem>("AbaProblemas").Header = visiveis.Count == 0 ? "Problemas" : $"Problemas ({erros} erro(s), {avisos} aviso(s))";
        if (trocarAba && erros > 0)
            Controle<TabControl>("AbasInferiores").SelectedItem = Controle<TabItem>("AbaProblemas");
    }

    private ListBoxItem CriarItemProblema(Diagnostico d)
    {
        var cor = d.Severidade == Severidade.Erro ? Brushes.IndianRed : Brushes.DarkOrange;
        var painel = new StackPanel { Spacing = 2 };
        painel.Children.Add(new TextBlock
        {
            Text = $"{(d.Severidade == Severidade.Erro ? "⛔ erro" : "⚠ aviso")}   {d.Posicao}   {d.Mensagem}",
            Foreground = cor,
            TextWrapping = TextWrapping.Wrap,
        });
        if (d.Dica is not null)
            painel.Children.Add(new TextBlock { Text = "💡 " + d.Dica, TextWrapping = TextWrapping.Wrap, Opacity = 0.85, Margin = new Thickness(22, 0, 0, 0) });
        var item = new ListBoxItem { Content = painel, Tag = d, Padding = new Thickness(8, 4) };
        item.Tapped += (_, _) => IrPara(d.Arquivo, d.Linha, d.Coluna);
        return item;
    }

    private void IrPara(string? arquivo, int linha, int coluna = 1)
    {
        if (Abrir(arquivo) is null || linha < 1 || linha > _editor.Document.LineCount)
            return;
        _editor.TextArea.Caret.Line = linha;
        _editor.TextArea.Caret.Column = Math.Max(1, coluna);
        _editor.ScrollTo(linha, coluna);
        _editor.TextArea.Caret.BringCaretToView();
        _editor.Focus();
    }

    private void AtualizarLinhaStatus()
    {
        if (_atual is null)
            return;
        var linha = _editor.TextArea.Caret.Line;
        var texto = $"{_atual.Nome}   linha {linha}, coluna {_editor.TextArea.Caret.Column}";
        var diag = _diagnosticos.FirstOrDefault(d => d.Linha == linha && d.Severidade != Severidade.Nota && MesmoArquivo(d.Arquivo, _atual.Caminho));
        if (diag is not null)
            texto += $"   —   {diag.SeveridadeTexto}: {diag.Mensagem}" + (diag.Dica is null ? "" : $"   💡 {diag.Dica}");
        Controle<TextBlock>("TextoLinha").Text = texto;
    }

    // ================= compilar / executar =================

    private async Task<ResultadoCompilacao?> CompilarAsync()
    {
        if (_modo != Modo.Ocioso)
            return null;
        SalvarTudo();
        DefinirModo(Modo.Compilando);
        EscreverTerminal("🔨 Compilando…\n");
        try
        {
            var r = await Task.Run(() => _compilador.CompilarAsync(_projeto));
            MostrarDiagnosticos(r.Diagnosticos, trocarAba: true);
            if (r.Sucesso)
                EscreverTerminal($"✔ Compilado sem erros{(r.Avisos > 0 ? $" ({r.Avisos} aviso(s) — veja a aba Problemas)" : "")}.\n");
            else
                EscreverTerminal($"✖ {r.Erros} erro(s) de compilação. Veja a aba Problemas (clique para ir até a linha).\n");
            return r;
        }
        catch (Exception e) when (e is IOException or InvalidOperationException or System.ComponentModel.Win32Exception)
        {
            EscreverTerminal("✖ Falha ao chamar o compilador: " + e.Message + "\n");
            return null;
        }
        finally
        {
            DefinirModo(Modo.Ocioso);
        }
    }

    private async Task ExecutarAsync()
    {
        if (_modo != Modo.Ocioso)
            return;
        LimparTerminal();
        var r = await CompilarAsync();
        if (r is not { Sucesso: true, Executavel: { } exe })
            return;

        Controle<TabControl>("AbasInferiores").SelectedIndex = 0;
        EscreverTerminal($"▶ Executando {Path.GetFileName(exe)}\n────────────────────────────────────────\n");
        var executor = _executor = new ExecutorPrograma();
        executor.SaidaRecebida += (texto, _) => EscreverTerminal(texto);
        executor.Terminou += resultado => Dispatcher.UIThread.Post(() => AoTerminarExecucao(executor, resultado));
        try
        {
            executor.Iniciar(exe, _projeto.Pasta, _toolchain);
            DefinirModo(Modo.Executando);
            Controle<TextBox>("CaixaEntrada").Focus();
        }
        catch (Exception e) when (e is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            EscreverTerminal("✖ Não foi possível iniciar o programa: " + e.Message + "\n");
            _executor = null;
        }
    }

    private void AoTerminarExecucao(ExecutorPrograma executor, ResultadoExecucao r)
    {
        DescarregarTerminal();
        var codigo = r.Explicacao is not null && r.CodigoSaida < 0 ? $"0x{r.CodigoSaida:X8}" : r.CodigoSaida.ToString();
        EscreverTerminal($"\n────────────────────────────────────────\n■ Programa terminou com código {codigo} em {r.Duracao.TotalSeconds:0.00} s.\n");
        if (r.Explicacao is not null)
        {
            EscreverTerminal($"⛔ {r.Explicacao}\n💡 Use 🐞 Depurar (F5): a execução para exatamente na linha do problema.\n");
            _janela.DefinirStatus("O programa terminou com erro: " + r.Explicacao);
        }
        if (_executor == executor)
            _executor = null;
        executor.Dispose();
        DefinirModo(Modo.Ocioso);
    }

    private void EnviarEntrada()
    {
        var caixa = Controle<TextBox>("CaixaEntrada");
        var texto = caixa.Text ?? "";
        caixa.Text = "";
        if (_executor is { Rodando: true })
        {
            EscreverTerminal(texto + "\n");
            _executor.EnviarLinha(texto);
        }
        else if (_modo == Modo.Depurando)
        {
            _janela.DefinirStatus("Na depuração, digite a entrada na janela de console do programa.");
        }
    }

    // ================= depuração =================

    private async Task DepurarOuContinuarAsync()
    {
        if (_depurador is { Estado: EstadoDepuracao.Pausado })
        {
            await _depurador.ContinuarAsync();
            return;
        }
        if (_modo != Modo.Ocioso)
            return;

        LimparTerminal();
        var r = await CompilarAsync();
        if (r is not { Sucesso: true, Executavel: { } exe })
            return;

        var pontos = _arquivos.SelectMany(a => a.PontosParada().Select(l => new PontoParada(a.Caminho, l))).ToList();
        var depurador = _depurador = new SessaoDepuracao(_toolchain);
        depurador.Parou += info => Dispatcher.UIThread.Post(() => _ = AoPararAsync(info));
        depurador.Continuou += () => Dispatcher.UIThread.Post(() =>
        {
            LimparParada();
            AtualizarBotoes();
        });
        depurador.Terminou += codigo => Dispatcher.UIThread.Post(() => _ = AoTerminarDepuracaoAsync(depurador, codigo));
        depurador.Mensagem += texto =>
        {
            if (!texto.StartsWith("[New Thread") && !texto.StartsWith("[Thread") && !texto.StartsWith("[Switching"))
                EscreverTerminal(texto);
        };

        DefinirModo(Modo.Depurando);
        EscreverTerminal("🐞 Depurando. A entrada e a saída do programa aparecem numa JANELA DE CONSOLE separada.\n" +
                         (pontos.Count == 0
                             ? "   Nenhum ponto de parada marcado: o programa roda até o fim (ou até um erro). Use ⏸ Pausar para interromper.\n"
                             : $"   {pontos.Count} ponto(s) de parada.\n"));
        try
        {
            await depurador.IniciarAsync(exe, _projeto.Pasta, pontos);
        }
        catch (Exception e) when (e is ErroGdbException or TimeoutException or System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            EscreverTerminal("✖ Não foi possível iniciar o depurador: " + e.Message + "\n");
            await AoTerminarDepuracaoAsync(depurador, null);
        }
        AtualizarBotoes();
    }

    private async Task AoPararAsync(InfoParada info)
    {
        if (_depurador is null)
            return;
        _arquivoParada = info.Arquivo;
        _linhaParada = info.Linha;
        _realce.EhErro = info.ErroFatal;
        if (info.Arquivo is not null)
            IrPara(info.Arquivo, info.Linha);
        _realce.Linha = MesmoArquivo(_arquivoParada, _atual?.Caminho) ? _linhaParada : 0;
        RedesenharEditor();

        var onde = info.Arquivo is null ? "(fora do seu código)" : $"{Path.GetFileName(info.Arquivo)}:{info.Linha} em {info.Funcao}()";
        Controle<TextBlock>("TextoEstado").Text = (info.ErroFatal ? "⛔ " : "⏸ ") + info.Motivo;
        _janela.DefinirStatus($"{info.Motivo} — {onde}");
        if (info.ErroFatal)
            EscreverTerminal($"\n⛔ {info.Motivo}: {onde}\n💡 Veja os valores das variáveis na aba Depuração. Um ponteiro com valor 0x0 é NULL.\n");

        Controle<TabControl>("AbasInferiores").SelectedItem = Controle<TabItem>("AbaDepuracao");
        AtualizarBotoes();

        var pilha = await _depurador.PilhaAsync();
        var listaPilha = Controle<ListBox>("ListaPilha");
        listaPilha.ItemsSource = pilha.Select(q => new ListBoxItem { Content = q.Descricao, Tag = q, Opacity = q.Arquivo is null ? 0.5 : 1 }).ToList();
        var quadro = pilha.FirstOrDefault(q => q.Arquivo is not null);
        await MostrarQuadroAsync(quadro?.Nivel ?? 0);
        listaPilha.SelectedItem = listaPilha.Items.OfType<ListBoxItem>().FirstOrDefault(i => Equals(i.Tag, quadro));
    }

    private async Task MostrarQuadroAsync(int nivel)
    {
        if (_depurador is null)
            return;
        _quadroSelecionado = nivel;
        var variaveis = await _depurador.VariaveisAsync(nivel);
        Controle<ListBox>("ListaVariaveis").ItemsSource = variaveis.Select(v => new ListBoxItem
        {
            Content = new SelectableTextBlock
            {
                Text = $"{(v.EhArgumento ? "(parâmetro) " : "")}{v.Nome}  :  {v.Tipo}  =  {v.Valor}",
                TextWrapping = TextWrapping.Wrap,
                FontFamily = FabricaEditor.FonteMono,
            },
        }).ToList();
        await AtualizarObservacoesAsync();
    }

    private async Task AtualizarObservacoesAsync()
    {
        var itens = new List<ListBoxItem>();
        foreach (var expr in _observar)
        {
            var valor = _depurador is { Estado: EstadoDepuracao.Pausado } d ? await d.AvaliarAsync(expr, _quadroSelecionado) : "—";
            var item = new ListBoxItem
            {
                Content = new SelectableTextBlock { Text = $"{expr}  =  {valor}", TextWrapping = TextWrapping.Wrap, FontFamily = FabricaEditor.FonteMono },
                Tag = expr,
            };
            ToolTip.SetTip(item, "Delete remove esta expressão");
            itens.Add(item);
        }
        Controle<ListBox>("ListaObservar").ItemsSource = itens;
    }

    private async Task AdicionarObservacaoAsync()
    {
        var caixa = Controle<TextBox>("CaixaObservar");
        var expr = caixa.Text?.Trim();
        caixa.Text = "";
        if (string.IsNullOrEmpty(expr) || _observar.Contains(expr))
            return;
        _observar.Add(expr);
        await AtualizarObservacoesAsync();
    }

    private async Task AoTerminarDepuracaoAsync(SessaoDepuracao depurador, int? codigo)
    {
        if (_depurador != depurador)
            return;
        _depurador = null;
        LimparParada();
        Controle<ListBox>("ListaVariaveis").ItemsSource = null;
        Controle<ListBox>("ListaPilha").ItemsSource = null;
        var explicacao = codigo is { } c ? ExplicacaoSaida.Para(c) : null;
        EscreverTerminal(codigo is null
            ? "■ Depuração encerrada.\n"
            : $"■ Programa terminou com código {codigo}.{(explicacao is null ? "" : " ⛔ " + explicacao)}\n");
        DefinirModo(Modo.Ocioso);
        _janela.DefinirStatus("Depuração encerrada.");
        await depurador.DisposeAsync();
        await AtualizarObservacoesAsync();
    }

    private void LimparParada()
    {
        _arquivoParada = null;
        _linhaParada = 0;
        _realce.Linha = 0;
        _realce.EhErro = false;
        Controle<TextBlock>("TextoEstado").Text = _modo == Modo.Depurando ? "▶ Executando…" : "";
        RedesenharEditor();
    }

    private async void AlternarPonto(int linha)
    {
        if (_atual is null)
            return;
        var ligado = _atual.AlternarPonto(linha);
        RedesenharEditor();
        if (_depurador is not null)
        {
            var ponto = new PontoParada(_atual.Caminho, linha);
            if (ligado)
                await _depurador.AdicionarPontoAsync(ponto);
            else
                await _depurador.RemoverPontoAsync(ponto);
        }
    }

    private async Task PararAsync()
    {
        if (_executor is { Rodando: true })
            _executor.Parar();
        if (_depurador is { } d)
            await d.PararAsync();
    }

    /// <summary>Chamado ao fechar a aba ou a janela: salva e encerra o que estiver rodando.</summary>
    public async Task EncerrarAsync()
    {
        _timerVerificacao.Stop();
        _ctsVerificacao?.Cancel();
        SalvarTudo();
        await PararAsync();
    }

    // ================= estado da interface =================

    private void DefinirModo(Modo modo)
    {
        _modo = modo;
        Controle<TextBlock>("TextoEstado").Text = modo switch
        {
            Modo.Compilando => "🔨 Compilando…",
            Modo.Executando => "▶ Executando…",
            Modo.Depurando => "▶ Executando (depuração)…",
            _ => "",
        };
        AtualizarBotoes();
    }

    private void AtualizarBotoes()
    {
        var ocioso = _modo == Modo.Ocioso;
        var pausado = _depurador is { Estado: EstadoDepuracao.Pausado };
        var depurandoRodando = _depurador is { Estado: EstadoDepuracao.Executando };

        Controle<Button>("BotaoCompilar").IsEnabled = ocioso;
        Controle<Button>("BotaoExecutar").IsEnabled = ocioso;
        Controle<Button>("BotaoDepurar").IsEnabled = ocioso;
        Controle<Button>("BotaoContinuar").IsEnabled = pausado;
        Controle<Button>("BotaoPausar").IsEnabled = depurandoRodando;
        Controle<Button>("BotaoParar").IsEnabled = _modo is Modo.Executando or Modo.Depurando;
        Controle<Button>("BotaoPassoSobre").IsEnabled = pausado;
        Controle<Button>("BotaoPassoDentro").IsEnabled = pausado;
        Controle<Button>("BotaoPassoFora").IsEnabled = pausado;
        Controle<Button>("BotaoEnviar").IsEnabled = _modo == Modo.Executando;
        Controle<Button>("BotaoFimEntrada").IsEnabled = _modo == Modo.Executando;
    }

    private void RedesenharEditor()
    {
        _editor.TextArea.TextView.InvalidateLayer(AvaloniaEdit.Rendering.KnownLayer.Background);
        _editor.TextArea.TextView.InvalidateLayer(AvaloniaEdit.Rendering.KnownLayer.Selection);
        _margem.InvalidateVisual();
    }

    // ================= terminal =================

    /// <summary>Pode ser chamado de qualquer thread. Agrupa a saída para não travar a tela com printf em laço.</summary>
    private void EscreverTerminal(string texto)
    {
        lock (_pendenteTerminal)
        {
            _pendenteTerminal.Append(texto.Replace("\r\n", "\n"));
            if (_descargaAgendada)
                return;
            _descargaAgendada = true;
        }
        Dispatcher.UIThread.Post(DescarregarTerminal, DispatcherPriority.Background);
    }

    private void DescarregarTerminal()
    {
        string texto;
        lock (_pendenteTerminal)
        {
            texto = _pendenteTerminal.ToString();
            _pendenteTerminal.Clear();
            _descargaAgendada = false;
        }
        if (texto.Length == 0)
            return;
        var doc = _terminal.Document;
        doc.Insert(doc.TextLength, texto);
        if (doc.TextLength > LimiteTerminal)
            doc.Remove(0, doc.TextLength - LimiteTerminal / 2);
        _terminal.ScrollToLine(doc.LineCount);
    }

    private void LimparTerminal()
    {
        lock (_pendenteTerminal)
            _pendenteTerminal.Clear();
        _terminal.Document.Text = "";
    }

    // ================= eventos =================

    private void LigarEventos()
    {
        Botao("BotaoSalvar", () => { SalvarTudo(); _janela.DefinirStatus("Arquivos salvos."); return Task.CompletedTask; });
        Botao("BotaoCompilar", async () => await CompilarAsync());
        Botao("BotaoExecutar", ExecutarAsync);
        Botao("BotaoDepurar", DepurarOuContinuarAsync);
        Botao("BotaoContinuar", () => _depurador?.ContinuarAsync() ?? Task.CompletedTask);
        Botao("BotaoPausar", () => _depurador?.PausarAsync() ?? Task.CompletedTask);
        Botao("BotaoParar", PararAsync);
        Botao("BotaoPassoSobre", () => _depurador?.PassoSobreAsync() ?? Task.CompletedTask);
        Botao("BotaoPassoDentro", () => _depurador?.PassoDentroAsync() ?? Task.CompletedTask);
        Botao("BotaoPassoFora", () => _depurador?.PassoForaAsync() ?? Task.CompletedTask);
        Botao("BotaoNovoC", () => NovoArquivoAsync(".c"));
        Botao("BotaoNovoH", () => NovoArquivoAsync(".h"));
        Botao("BotaoEnviar", () => { EnviarEntrada(); return Task.CompletedTask; });
        Botao("BotaoFimEntrada", () => { _executor?.FecharEntrada(); EscreverTerminal("^Z (fim da entrada)\n"); return Task.CompletedTask; });
        Botao("BotaoLimpar", () => { LimparTerminal(); return Task.CompletedTask; });
        Botao("BotaoObservar", AdicionarObservacaoAsync);

        Controle<TextBox>("CaixaEntrada").KeyDown += (_, e) =>
        {
            if (e.Key == Key.Enter)
            {
                EnviarEntrada();
                e.Handled = true;
            }
        };
        Controle<TextBox>("CaixaObservar").KeyDown += async (_, e) =>
        {
            if (e.Key == Key.Enter)
                await AdicionarObservacaoAsync();
        };
        Controle<ListBox>("ListaObservar").KeyDown += async (_, e) =>
        {
            if (e.Key == Key.Delete && Controle<ListBox>("ListaObservar").SelectedItem is ListBoxItem { Tag: string expr })
            {
                _observar.Remove(expr);
                await AtualizarObservacoesAsync();
            }
        };
        Controle<ListBox>("ListaArquivos").SelectionChanged += (_, _) =>
        {
            if (Controle<ListBox>("ListaArquivos").SelectedItem is ListBoxItem { Tag: ArquivoAberto a })
                MostrarArquivo(a);
        };
        Controle<ListBox>("ListaPilha").Tapped += async (_, _) =>
        {
            if (Controle<ListBox>("ListaPilha").SelectedItem is ListBoxItem { Tag: Quadro q })
            {
                if (q.Arquivo is not null)
                    IrPara(q.Arquivo, q.Linha);
                await MostrarQuadroAsync(q.Nivel);
            }
        };

        AddHandler(KeyDownEvent, AoTeclar, RoutingStrategies.Tunnel);
    }

    private void Botao(string nome, Func<Task> acao) =>
        Controle<Button>(nome).Click += async (_, _) =>
        {
            try
            {
                await acao();
            }
            catch (Exception e) when (e is ErroGdbException or TimeoutException or IOException)
            {
                EscreverTerminal("✖ " + e.Message + "\n");
            }
            AtualizarBotoes();
        };

    private async void AoTeclar(object? sender, KeyEventArgs e)
    {
        var ctrl = e.KeyModifiers.HasFlag(KeyModifiers.Control);
        var shift = e.KeyModifiers.HasFlag(KeyModifiers.Shift);
        Func<Task>? acao = (e.Key, ctrl, shift) switch
        {
            (Key.S, true, false) => () => { SalvarTudo(); _janela.DefinirStatus("Arquivos salvos."); return Task.CompletedTask; },
            (Key.B, true, false) => async () => await CompilarAsync(),
            (Key.F5, true, false) => ExecutarAsync,
            (Key.F5, false, false) => DepurarOuContinuarAsync,
            (Key.F5, false, true) => PararAsync,
            (Key.F6, false, false) => () => _depurador?.PausarAsync() ?? Task.CompletedTask,
            (Key.F9, false, false) => () => { AlternarPonto(_editor.TextArea.Caret.Line); return Task.CompletedTask; },
            (Key.F10, false, false) => () => _depurador?.PassoSobreAsync() ?? Task.CompletedTask,
            (Key.F11, false, false) => () => _depurador?.PassoDentroAsync() ?? Task.CompletedTask,
            (Key.F11, false, true) => () => _depurador?.PassoForaAsync() ?? Task.CompletedTask,
            _ => null,
        };
        if (acao is null)
            return;
        e.Handled = true;
        try
        {
            await acao();
        }
        catch (Exception ex) when (ex is ErroGdbException or TimeoutException or IOException)
        {
            EscreverTerminal("✖ " + ex.Message + "\n");
        }
        AtualizarBotoes();
    }

    private static bool MesmoArquivo(string? a, string? b) =>
        a is not null && b is not null &&
        string.Equals(Path.GetFullPath(a), Path.GetFullPath(b), StringComparison.OrdinalIgnoreCase);
}
