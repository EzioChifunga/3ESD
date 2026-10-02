#include <stdio.h>
#include <stdlib.h>
#include <string.h>

typedef struct pes {
    char nome[50];
    int idade;
    char gen;
} tPessoa;

void lePessoa(tPessoa *p)
{
    scanf("%49s %d %c", p->nome, &p->idade, &p->gen);
}

void mostraPessoa(tPessoa p)
{
    printf("%-10s %3d anos  %c\n", p.nome, p.idade, p.gen);
}

int buscaPorNome(tPessoa v[], int n, const char *nome)
{
    int i;
    for (i = 0; i < n; i++)
        if (strcmp(v[i].nome, nome) == 0)
            return i;
    return -1;
}

int indiceMaisVelha(tPessoa v[], int n)
{
    int i, iMax = 0;
    for (i = 1; i < n; i++)
        if (v[i].idade > v[iMax].idade)
            iMax = i;
    return iMax;
}

int main(void)
{
    int n, i, pos;
    char nome[50];
    tPessoa *v;

    printf("Quantas pessoas? ");
    scanf("%d", &n);
    v = (tPessoa *) malloc(n * sizeof(tPessoa));
    if (v == NULL)
        return 1;

    for (i = 0; i < n; i++)
    {
        printf("Pessoa %d (nome idade sexo): ", i + 1);
        lePessoa(&v[i]);
    }

    printf("\nCadastro:\n");
    for (i = 0; i < n; i++)
        mostraPessoa(v[i]);

    printf("Mais velha: ");
    mostraPessoa(v[indiceMaisVelha(v, n)]);

    printf("Nome para buscar: ");
    scanf("%49s", nome);
    pos = buscaPorNome(v, n, nome);
    if (pos >= 0)
        printf("Encontrada na posição %d\n", pos);
    else
        printf("Não encontrada\n");

    free(v);
    return 0;
}
