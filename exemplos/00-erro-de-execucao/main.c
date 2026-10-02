#include <stdio.h>
#include <string.h>

typedef struct {
    char nome[20];
    float nota;
} Aluno;

/* Devolve o endereço do aluno com esse nome, ou NULL se não existir. */
Aluno *procura(Aluno turma[], int n, const char *nome)
{
    int i;
    for (i = 0; i < n; i++)
        if (strcmp(turma[i].nome, nome) == 0)
            return &turma[i];
    return NULL;
}

int main(void)
{
    Aluno turma[3] = { {"Ana", 8.5f}, {"Bia", 7.0f}, {"Caio", 9.2f} };
    Aluno *a;

    a = procura(turma, 3, "Bia");
    printf("%s tirou %.1f\n", a->nome, a->nota);

    a = procura(turma, 3, "Duda");          /* não existe: a == NULL */
    printf("%s tirou %.1f\n", a->nome, a->nota);   /* acesso via ponteiro NULL! */

    return 0;
}
