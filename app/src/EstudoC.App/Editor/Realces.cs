using Avalonia;
using Avalonia.Media;
using AvaloniaEdit.Document;
using AvaloniaEdit.Rendering;
using EstudoC.Core.Compilacao;

namespace EstudoC.App.Editor;

/// <summary>Fundo amarelo (ou vermelho, se o programa quebrou) na linha onde a depuração parou.</summary>
public sealed class RealceLinhaDepuracao : IBackgroundRenderer
{
    private static readonly IBrush Normal = new SolidColorBrush(Color.FromArgb(0x55, 0xF2, 0xB7, 0x05));
    private static readonly IBrush Erro = new SolidColorBrush(Color.FromArgb(0x55, 0xE5, 0x14, 0x00));

    public int Linha { get; set; }
    public bool EhErro { get; set; }

    public KnownLayer Layer => KnownLayer.Background;

    public void Draw(TextView textView, DrawingContext contexto)
    {
        if (Linha < 1 || textView.Document is null || Linha > textView.Document.LineCount || !textView.VisualLinesValid)
            return;
        var visual = textView.GetVisualLine(Linha);
        if (visual is null)
            return;
        var topo = visual.VisualTop - textView.VerticalOffset;
        contexto.FillRectangle(EhErro ? Erro : Normal, new Rect(0, topo, textView.Bounds.Width, visual.Height));
    }
}

/// <summary>Sublinhado ondulado nos erros (vermelho) e avisos (laranja) do arquivo aberto.</summary>
public sealed class MarcadoresDiagnostico : IBackgroundRenderer
{
    private static readonly Pen PenErro = new(new SolidColorBrush(Color.FromRgb(0xE5, 0x14, 0x00)), 1.2);
    private static readonly Pen PenAviso = new(new SolidColorBrush(Color.FromRgb(0xE0, 0x8A, 0x00)), 1.2);

    public IReadOnlyList<Diagnostico> Diagnosticos { get; set; } = [];

    public KnownLayer Layer => KnownLayer.Selection;

    public void Draw(TextView textView, DrawingContext contexto)
    {
        var doc = textView.Document;
        if (doc is null || !textView.VisualLinesValid)
            return;

        foreach (var d in Diagnosticos)
        {
            if (d.Severidade == Severidade.Nota || d.Linha < 1 || d.Linha > doc.LineCount)
                continue;
            var linha = doc.GetLineByNumber(d.Linha);
            var (inicio, fim) = Trecho(doc, linha, d.Coluna);
            var segmento = new TextSegment { StartOffset = inicio, EndOffset = fim };
            foreach (var r in BackgroundGeometryBuilder.GetRectsForSegment(textView, segmento))
                DesenharOnda(contexto, d.Severidade == Severidade.Erro ? PenErro : PenAviso, r.BottomLeft, r.Right);
        }
    }

    /// <summary>Da coluna indicada até o fim da palavra (ou a linha toda se a coluna for inválida).</summary>
    private static (int, int) Trecho(TextDocument doc, DocumentLine linha, int coluna)
    {
        var texto = doc.GetText(linha);
        var inicioTexto = texto.Length - texto.TrimStart().Length;
        var col = Math.Clamp(coluna - 1, 0, Math.Max(0, texto.Length - 1));
        if (texto.Length == 0)
            return (linha.Offset, linha.Offset);
        var fim = col;
        while (fim < texto.Length && (char.IsLetterOrDigit(texto[fim]) || texto[fim] == '_'))
            fim++;
        if (fim == col)
            fim = Math.Min(texto.Length, col + 1);
        if (col < inicioTexto)
            (col, fim) = (inicioTexto, texto.Length);
        return (linha.Offset + col, linha.Offset + fim);
    }

    private static void DesenharOnda(DrawingContext contexto, Pen pen, Point inicio, double fimX)
    {
        var geo = new StreamGeometry();
        using (var g = geo.Open())
        {
            var x = inicio.X;
            var y = inicio.Y - 1;
            g.BeginFigure(new Point(x, y), false);
            var cima = true;
            while (x < Math.Max(fimX, inicio.X + 6))
            {
                x += 3;
                g.LineTo(new Point(x, cima ? y - 2 : y));
                cima = !cima;
            }
            g.EndFigure(false);
        }
        contexto.DrawGeometry(null, pen, geo);
    }
}
