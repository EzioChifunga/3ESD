#include <stdio.h>
#include <stdlib.h>

int **criaMat2d(int nLinhas, int nColunas)
{
    int i;
    int **m = (int **) malloc(nLinhas * sizeof(int *));   /* vetor de linhas */
    if (m == NULL)
        return NULL;
    for (i = 0; i < nLinhas; i++)
    {
        m[i] = (int *) malloc(nColunas * sizeof(int));       /* cada linha */
        if (m[i] == NULL)
            exit(1);
    }
    return m;
}

void liberaMat2d(int **m, int nLinhas)
{
    int i;
    for (i = 0; i < nLinhas; i++)
        free(m[i]);          /* primeiro as linhas... */
    free(m);                 /* ...depois o vetor de ponteiros */
}

void imprimeMat2d(int **m, int nLinhas, int nColunas)
{
    int i, j;
    for (i = 0; i < nLinhas; i++)
    {
        for (j = 0; j < nColunas; j++)
            printf("%4d", m[i][j]);
        printf("\n");
    }
}

int main(void)
{
    int nLin, nCol, i, j, soma;
    int **matriz;

    printf("Quantas linhas e colunas? ");
    scanf("%d %d", &nLin, &nCol);

    matriz = criaMat2d(nLin, nCol);
    srand(0);
    for (i = 0; i < nLin; i++)
        for (j = 0; j < nCol; j++)
            matriz[i][j] = rand() % 21;      /* 0 a 20 */

    imprimeMat2d(matriz, nLin, nCol);         /* PARE AQUI: marque com F9 */

    for (i = 0; i < nLin; i++)
    {
        for (soma = 0, j = 0; j < nCol; j++)
            soma += matriz[i][j];
        printf("Soma da linha %d: %d\n", i, soma);
    }
    for (j = 0; j < nCol; j++)
    {
        for (soma = 0, i = 0; i < nLin; i++)
            soma += matriz[i][j];
        printf("Soma da coluna %d: %d\n", j, soma);
    }

    liberaMat2d(matriz, nLin);
    return 0;
}
