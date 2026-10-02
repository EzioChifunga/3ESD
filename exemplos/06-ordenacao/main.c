#include <stdio.h>
#include <string.h>

#define N 6

void mostra(const char *rotulo, const int v[], int n)
{
    int i;
    printf("%-14s", rotulo);
    for (i = 0; i < n; i++)
        printf("%3d", v[i]);
    printf("\n");
}

void troca(int *a, int *b)
{
    int aux = *a;
    *a = *b;
    *b = aux;
}

void bolha(int v[], int n)
{
    int i, j;
    for (i = n - 1; i > 0; i--)
    {
        for (j = 0; j < i; j++)
            if (v[j] > v[j + 1])
                troca(&v[j], &v[j + 1]);
        mostra("  passada", v, n);
    }
}

void selecao(int v[], int n)
{
    int i, j, iMenor;
    for (i = 0; i < n - 1; i++)
    {
        iMenor = i;
        for (j = i + 1; j < n; j++)
            if (v[j] < v[iMenor])
                iMenor = j;
        troca(&v[i], &v[iMenor]);
        mostra("  passada", v, n);
    }
}

void insercao(int v[], int n)
{
    int i, j, atual;
    for (i = 1; i < n; i++)
    {
        atual = v[i];
        for (j = i - 1; j >= 0 && v[j] > atual; j--)
            v[j + 1] = v[j];         /* abre espaço deslocando para a direita */
        v[j + 1] = atual;
        mostra("  passada", v, n);
    }
}

int main(void)
{
    const int original[N] = {29, 10, 14, 37, 13, 5};
    int v[N];

    memcpy(v, original, sizeof v);
    mostra("Bolha:", v, N);
    bolha(v, N);

    memcpy(v, original, sizeof v);
    mostra("Seleção:", v, N);
    selecao(v, N);

    memcpy(v, original, sizeof v);
    mostra("Inserção:", v, N);
    insercao(v, N);
    return 0;
}
