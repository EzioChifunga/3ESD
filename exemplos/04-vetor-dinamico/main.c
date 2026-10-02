#include <stdio.h>
#include <stdlib.h>

/* Cria um vetor dinâmico com n posições lidas do teclado. */
int *leVetor(int n)
{
    int i;
    int *v = (int *) malloc(n * sizeof(int));
    if (v == NULL)
    {
        printf("Memória insuficiente!\n");
        exit(1);
    }
    for (i = 0; i < n; i++)
        scanf("%d", &v[i]);
    return v;   /* ok: a memória do malloc continua válida fora da função */
}

void imprime(const int *v, int n)
{
    int i;
    for (i = 0; i < n; i++)
        printf("%d ", v[i]);
    printf("\n");
}

int main(void)
{
    int n, extra, i;
    int *v, *maior;

    printf("Quantos números? ");
    scanf("%d", &n);
    printf("Digite %d números: ", n);
    v = leVetor(n);
    imprime(v, n);

    printf("Quantos números a mais? ");
    scanf("%d", &extra);
    maior = (int *) realloc(v, (n + extra) * sizeof(int));
    if (maior == NULL)
    {
        free(v);
        return 1;
    }
    v = maior;
    printf("Digite %d números: ", extra);
    for (i = n; i < n + extra; i++)
        scanf("%d", &v[i]);
    n += extra;

    printf("Vetor final (%d elementos): ", n);
    imprime(v, n);

    free(v);
    v = NULL;   /* evita usar a memória já liberada por engano */
    return 0;
}
