#include <stdio.h>

#define LIN 3
#define COL 4

void imprime(int m[][COL], int lin)
{
    int i, j;
    for (i = 0; i < lin; i++)
    {
        for (j = 0; j < COL; j++)
            printf("%4d", m[i][j]);
        printf("\n");
    }
}

int somaLinha(int m[][COL], int l)
{
    int j, soma = 0;
    for (j = 0; j < COL; j++)
        soma += m[l][j];
    return soma;
}

int somaColuna(int m[][COL], int lin, int c)
{
    int i, soma = 0;
    for (i = 0; i < lin; i++)
        soma += m[i][c];
    return soma;
}

/* devolve o maior valor e, por ponteiro, sua posição */
int maior(int m[][COL], int lin, int *pl, int *pc)
{
    int i, j, mx = m[0][0];
    *pl = *pc = 0;
    for (i = 0; i < lin; i++)
        for (j = 0; j < COL; j++)
            if (m[i][j] > mx)
            {
                mx = m[i][j];
                *pl = i;
                *pc = j;
            }
    return mx;
}

int main(void)
{
    int m[LIN][COL] = {
        { 3, 8, 1, 5 },
        { 7, 2, 9, 4 },
        { 6, 0, 2, 8 },
    };
    int i, l, c, mx;

    imprime(m, LIN);
    for (i = 0; i < LIN; i++)
        printf("Soma da linha %d = %d\n", i, somaLinha(m, i));
    for (i = 0; i < COL; i++)
        printf("Soma da coluna %d = %d\n", i, somaColuna(m, LIN, i));

    mx = maior(m, LIN, &l, &c);
    printf("Maior elemento: %d na posição [%d][%d]\n", mx, l, c);
    return 0;
}
