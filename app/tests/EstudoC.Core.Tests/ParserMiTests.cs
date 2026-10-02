using EstudoC.Core.Depuracao;

namespace EstudoC.Core.Tests;

public class ParserMiTests
{
    [Fact]
    public void ResultadoComTuplaELista()
    {
        var r = ParserMi.AnalisarLinha(
            """6^done,variables=[{name="p",value="{x = 1, y = 2}"},{name="v",value="{4, 5, 6}"},{name="q",value="0x0"}]""");

        Assert.Equal(TipoRegistroMi.Resultado, r.Tipo);
        Assert.Equal(6, r.Token);
        Assert.Equal("done", r.Classe);
        var vars = r.Dados["variables"]!.Itens;
        Assert.Equal(3, vars.Count);
        Assert.Equal("{x = 1, y = 2}", vars[0]["value"]!.Texto);
    }

    [Fact]
    public void ListaDeResultados()
    {
        var r = ParserMi.AnalisarLinha(
            """8^done,stack=[frame={level="0",func="soma",fullname="C:\\x\\dbg.c",line="3"},frame={level="1",func="main",line="9"}]""");

        var pilha = r.Dados["stack"]!.Itens;
        Assert.Equal(2, pilha.Count);
        Assert.Equal("soma", pilha[0]["func"]!.Texto);
        Assert.Equal(@"C:\x\dbg.c", pilha[0]["fullname"]!.Texto);
    }

    [Fact]
    public void RegistroAssincronoStopped()
    {
        var r = ParserMi.AnalisarLinha(
            """*stopped,reason="signal-received",signal-name="SIGSEGV",frame={addr="0x1",func="main",args=[],line="13"},thread-id="1",stopped-threads="all" """.TrimEnd());

        Assert.Equal(TipoRegistroMi.Assincrono, r.Tipo);
        Assert.Equal('*', r.Prefixo);
        Assert.Equal("stopped", r.Classe);
        Assert.Equal("SIGSEGV", r.Dados["signal-name"]!.Texto);
        Assert.Equal("13", r.Dados["frame"]!["line"]!.Texto);
        Assert.Empty(r.Dados["frame"]!["args"]!.Itens);
    }

    [Fact]
    public void StreamComEscapes()
    {
        var r = ParserMi.AnalisarLinha("""~"9\t    int s = \"x\";\n" """.TrimEnd());

        Assert.Equal(TipoRegistroMi.Console, r.Tipo);
        Assert.Equal("9\t    int s = \"x\";\n", r.TextoStream);
    }

    [Fact]
    public void OctalUtf8()
    {
        // "ã" em UTF-8 = 0xC3 0xA3 = \303\243
        var r = ParserMi.AnalisarLinha("""1^done,value="\"n\303\243o\"" """.TrimEnd());
        Assert.Equal("\"não\"", r.Dados["value"]!.Texto);
    }

    [Fact]
    public void Erro()
    {
        var r = ParserMi.AnalisarLinha("""15^error,msg="No symbol \"i\" in current context." """.TrimEnd());
        Assert.Equal("error", r.Classe);
        Assert.Equal("No symbol \"i\" in current context.", r.Dados["msg"]!.Texto);
    }
}
