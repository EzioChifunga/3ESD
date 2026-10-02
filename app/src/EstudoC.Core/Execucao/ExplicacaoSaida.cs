namespace EstudoC.Core.Execucao;

/// <summary>Traduz códigos de término do Windows em explicações para quem está aprendendo C.</summary>
public static class ExplicacaoSaida
{
    public static string? Para(int codigo)
    {
        var u = unchecked((uint)codigo);
        return u switch
        {
            0xC0000005 => "Falha de segmentação (acesso inválido à memória). Causas comuns: ponteiro NULL ou não inicializado, " +
                          "índice fora do vetor, usar memória depois do free, esquecer o & no scanf.",
            0xC00000FD => "Estouro de pilha (stack overflow). Normalmente recursão sem caso base ou vetor local grande demais.",
            0xC0000094 => "Divisão inteira por zero.",
            0xC0000374 or 0xC0000409 => "Memória corrompida: escrita além do espaço alocado (malloc pequeno demais, string sem espaço para '\\0') ou free duplicado.",
            0xC000008E => "Divisão de ponto flutuante por zero.",
            0xC000001D => "Instrução inválida (o programa executou algo que não é código — memória corrompida?).",
            0xC0000135 => "Uma DLL necessária não foi encontrada.",
            0xC000013A => "Programa interrompido.",
            3 => "Código 3: se o seu código não usa return 3/exit(3), o programa foi abortado (abort() ou assert falhou).",
            _ => null,
        };
    }
}
