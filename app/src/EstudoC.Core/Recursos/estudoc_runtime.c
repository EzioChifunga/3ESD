/*
 * Ligado automaticamente a todo programa executado pelo Estudo C.
 * Fica em uma unidade de compilação separada para NÃO esconder erros do aluno
 * (ex.: esquecer #include <stdio.h> continua dando erro no código dele).
 */
#include <stdio.h>
#include <windows.h>

static void __attribute__((constructor)) estudoc_inicializa(void)
{
    /* Sem buffer: printf("Digite: ") aparece antes do scanf mesmo sem '\n'. */
    setvbuf(stdout, NULL, _IONBF, 0);
    setvbuf(stderr, NULL, _IONBF, 0);
    /* Sem janela "o programa parou de funcionar": o Estudo C explica o erro. */
    SetErrorMode(SEM_FAILCRITICALERRORS | SEM_NOGPFAULTERRORBOX);
    /* Acentos corretos quando o programa roda numa janela de console (modo depuração). */
    SetConsoleOutputCP(CP_UTF8);
    SetConsoleCP(CP_UTF8);
}
