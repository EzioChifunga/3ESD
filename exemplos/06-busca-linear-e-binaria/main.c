#include <stdio.h>

#define N 20

int buscaLinear(const int v[], int n, int x, int *comparacoes)
{
    int i;
    *comparacoes = 0;
    for (i = 0; i < n; i++)
    {
        (*comparacoes)++;
        if (v[i] == x)
            return i;
    }
    return -1;
}

int buscaBinaria(const int v[], int n, int x, int *comparacoes)
{
    int ini = 0, fim = n - 1, meio;
    *comparacoes = 0;
    while (ini <= fim)
    {
        meio = (ini + fim) / 2;
        (*comparacoes)++;
        if (v[meio] == x)
            return meio;
        if (x < v[meio])
            fim = meio - 1;      /* descarta a metade da direita */
        else
            ini = meio + 1;      /* descarta a metade da esquerda */
    }
    return -1;
}

int main(void)
{
    int v[N], i, x, pos, c1, c2;

    for (i = 0; i < N; i++)
        v[i] = 3 + i * 5;        /* 3, 8, 13, ..., 98 (ordenado) */

    printf("Vetor: ");
    for (i = 0; i < N; i++)
        printf("%d ", v[i]);
    printf("\n");

    while (1)
    {
        printf("\nValor a procurar (Fim da entrada para sair): ");
        if (scanf("%d", &x) != 1)
            break;
        pos = buscaLinear(v, N, x, &c1);
        buscaBinaria(v, N, x, &c2);
        if (pos >= 0)
            printf("Achado na posição %d. ", pos);
        else
            printf("Não está no vetor. ");
        printf("Comparações: linear = %d, binária = %d\n", c1, c2);
    }
    return 0;
}
