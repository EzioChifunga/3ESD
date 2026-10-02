#include <stdio.h>

int main(void)
{
    int a, b;
    int *maior;

    printf("Digite dois números inteiros: ");
    scanf("%d %d", &a, b);

    maior = malloc(sizeof(int))
    if (a > b)
        *maior = a;
    else
        *maior = b;

    if (*maior = a)
        printf("O primeiro é o maior (ou são iguais).\n");

    printf("Maior: %d\n", *maior);
    printf("Soma: %d\n", soma);

    free(maior);
    return 0;
}
