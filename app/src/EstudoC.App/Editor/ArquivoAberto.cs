using System.Text;
using AvaloniaEdit.Document;

namespace EstudoC.App.Editor;

/// <summary>Um arquivo do projeto aberto no editor, com seus pontos de parada.</summary>
public sealed class ArquivoAberto
{
    private static readonly UTF8Encoding Utf8SemBom = new(false);
    private readonly List<TextAnchor> _pontos = [];
    private string _textoSalvo;

    public string Caminho { get; }
    public string Nome => Path.GetFileName(Caminho);
    public TextDocument Documento { get; }

    public bool Modificado => Documento.Text != _textoSalvo;

    public ArquivoAberto(string caminho)
    {
        Caminho = caminho;
        _textoSalvo = File.ReadAllText(caminho).Replace("\r\n", "\n");
        Documento = new TextDocument(_textoSalvo) { FileName = caminho };
    }

    public void Salvar()
    {
        if (!Modificado)
            return;
        var texto = Documento.Text;
        File.WriteAllText(Caminho, texto, Utf8SemBom);
        _textoSalvo = texto;
    }

    /// <summary>Linhas (1..n) com ponto de parada. As âncoras acompanham a edição do texto.</summary>
    public IReadOnlyList<int> PontosParada()
    {
        _pontos.RemoveAll(a => a.IsDeleted);
        return _pontos.Select(a => a.Line).Distinct().Order().ToList();
    }

    public bool TemPonto(int linha) => PontosParada().Contains(linha);

    /// <summary>Liga/desliga o ponto de parada. Devolve true se ficou ligado.</summary>
    public bool AlternarPonto(int linha)
    {
        if (linha < 1 || linha > Documento.LineCount)
            return false;
        var existentes = _pontos.Where(a => !a.IsDeleted && a.Line == linha).ToList();
        if (existentes.Count > 0)
        {
            _pontos.RemoveAll(existentes.Contains);
            return false;
        }
        var ancora = Documento.CreateAnchor(Documento.GetLineByNumber(linha).Offset);
        ancora.MovementType = AnchorMovementType.BeforeInsertion;
        ancora.SurviveDeletion = false;
        _pontos.Add(ancora);
        return true;
    }
}
