using EstudoC.Core.Compilacao;

namespace EstudoC.Core.Tests;

public class ParserDiagnosticosTests
{
    // Saída real do gcc 16 (w64devkit)
    private const string SaidaGcc = """
        C:\pasta\erro.c: In function 'main':
        C:\pasta\erro.c:4:5: error: expected ',' or ';' before 'printf'
        C:\pasta\erro.c:5:5: error: 'y' undeclared (first use in this function)
        C:\pasta\erro.c:5:5: note: each undeclared identifier is reported only once for each function it appears in
        C:\pasta\erro.c:6:14: error: initialization of 'int *' from 'int' makes pointer from integer without a cast
        C:\pasta\erro.c:6:10: warning: unused variable 'p'
        """;

    [Fact]
    public void ReconheceErrosAvisosENotas()
    {
        var d = ParserDiagnosticos.Analisar(SaidaGcc);

        Assert.Equal(5, d.Count);
        Assert.Equal(3, d.Count(x => x.Severidade == Severidade.Erro));
        Assert.Equal(@"C:\pasta\erro.c", d[0].Arquivo);
        Assert.Equal((4, 5), (d[0].Linha, d[0].Coluna));
        Assert.Equal(Severidade.Aviso, d[4].Severidade);
    }

    [Fact]
    public void DaDicasEmPortugues()
    {
        var d = ParserDiagnosticos.Analisar(SaidaGcc);

        Assert.Contains("ponto e vírgula", d[0].Dica);
        Assert.Contains("'y'", d[1].Dica);
        Assert.Contains("ponteiro", d[3].Dica);
    }

    [Fact]
    public void ReconheceErroDoLigador()
    {
        const string saida = """
            C:/w64devkit/bin/ld.exe: C:\Temp\ccAqXgsO.o:link.c:(.text+0x13): undefined reference to `f'
            collect2.exe: error: ld returned 1 exit status
            """;

        var d = Assert.Single(ParserDiagnosticos.Analisar(saida));

        Assert.Equal(Severidade.Erro, d.Severidade);
        Assert.Equal("link.c", d.Arquivo);
        Assert.Equal("undefined reference to 'f'", d.Mensagem);
        Assert.Contains("'f'", d.Dica);
    }

    [Theory]
    [InlineData("implicit declaration of function 'printf'", "stdio.h")]
    [InlineData("implicit declaration of function 'malloc'", "stdlib.h")]
    [InlineData("implicit declaration of function 'strcpy'", "string.h")]
    [InlineData("undefined reference to 'WinMain'", "main")]
    [InlineData("suggest parentheses around assignment used as truth value", "==")]
    public void DicasComuns(string mensagem, string esperado) =>
        Assert.Contains(esperado, DicasErros.Para(mensagem));
}
