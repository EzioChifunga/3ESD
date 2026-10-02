#include <stdio.h>

/* Recebe CÓPIAS dos valores: trocar aqui não afeta quem chamou. */
void trocaValor(int a, int b)
{
    int aux = a;
    a = b;
    b = aux;
}

/* Recebe os ENDEREÇOS: *a e *b são as variáveis originais. */
void trocaRef(int *a, int *b)
{
    int aux = *a;
    *a = *b;
    *b = aux;
}

/* "Devolve" dois resultados pelos parâmetros ponteiro. */
void minMax(int v[], int n, int *menor, int *maior)
{
    int i;
    *menor = *maior = v[0];
    for (i = 1; i < n; i++)
    {
        if (v[i] < *menor) *menor = v[i];
        if (v[i] > *maior) *maior = v[i];
    }
}

int main(void)
{
    int x = 10, y = 20;
    int *p = &x;
    int v[5] = {7, 3, 9, 1, 5};
    int menor, maior;

    printf("x mora no endereço %p e vale %d; p aponta para %p e *p = %d\n", (void *)&x, x, (void *)p, *p);

    trocaValor(x, y);                       /* PARE AQUI: marque com F9 */
    printf("Depois de trocaValor: x = %d, y = %d\n", x, y);

    trocaRef(&x, &y);
    printf("Depois de trocaRef:   x = %d, y = %d\n", x, y);

    *p = 99;                                /* muda x através do ponteiro */
    printf("Depois de *p = 99:     x = %d\n", x);

    minMax(v, 5, &menor, &maior);
    printf("Menor = %d, maior = %d\n", menor, maior);
    return 0;
}
