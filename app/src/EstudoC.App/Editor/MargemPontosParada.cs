using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using AvaloniaEdit.Editing;
using AvaloniaEdit.Rendering;

namespace EstudoC.App.Editor;

/// <summary>Margem à esquerda: clique liga/desliga ponto de parada (●); seta (➜) marca a linha atual da depuração.</summary>
public sealed class MargemPontosParada : AbstractMargin
{
    private static readonly IBrush CorPonto = new SolidColorBrush(Color.FromRgb(0xE5, 0x14, 0x00));
    private static readonly IBrush CorSeta = new SolidColorBrush(Color.FromRgb(0xF2, 0xB7, 0x05));

    public Func<IReadOnlyList<int>> ObterPontos { get; set; } = () => [];
    public Func<int> ObterLinhaAtual { get; set; } = () => 0;
    public event Action<int>? Clicou;

    protected override Size MeasureOverride(Size availableSize) => new(20, 0);

    public override void Render(DrawingContext contexto)
    {
        var view = TextView;
        if (view is not { VisualLinesValid: true })
            return;

        contexto.FillRectangle(Brushes.Transparent, Bounds);
        var pontos = ObterPontos();
        var atual = ObterLinhaAtual();
        foreach (var linha in view.VisualLines)
        {
            var numero = linha.FirstDocumentLine.LineNumber;
            var y = linha.VisualTop - view.VerticalOffset + linha.Height / 2;
            if (pontos.Contains(numero))
                contexto.DrawEllipse(CorPonto, null, new Point(10, y), 6, 6);
            if (numero == atual)
            {
                var seta = new StreamGeometry();
                using (var g = seta.Open())
                {
                    g.BeginFigure(new Point(3, y - 5), true);
                    g.LineTo(new Point(12, y - 5));
                    g.LineTo(new Point(18, y));
                    g.LineTo(new Point(12, y + 5));
                    g.LineTo(new Point(3, y + 5));
                    g.EndFigure(true);
                }
                contexto.DrawGeometry(CorSeta, new Pen(Brushes.Black, 0.5), seta);
            }
        }
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        var view = TextView;
        if (view is null || !view.VisualLinesValid)
            return;
        var y = e.GetPosition(view).Y + view.VerticalOffset;
        var linha = view.GetVisualLineFromVisualTop(y);
        if (linha is null)
            return;
        Clicou?.Invoke(linha.FirstDocumentLine.LineNumber);
        e.Handled = true;
    }

    protected override void OnTextViewChanged(TextView? antigo, TextView? novo)
    {
        if (antigo is not null)
            antigo.VisualLinesChanged -= Redesenhar;
        base.OnTextViewChanged(antigo, novo);
        if (novo is not null)
            novo.VisualLinesChanged += Redesenhar;
        InvalidateVisual();
    }

    private void Redesenhar(object? sender, EventArgs e) => InvalidateVisual();
}
