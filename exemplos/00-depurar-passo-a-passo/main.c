#include <stdio.h>

/* Calcula n! = 1 * 2 * ... * n */
long fatorial(int n)
{
    long resultado = 1;
    int i;

    for (i = 2; i <= n; i++)
        resultado = resultado * i;

    return resultado;
}

int main(void)
{
    int n = 5;
    int i;
    long soma = 0;

    for (i = 1; i <= n; i++)
    {
        long f = fatorial(i);   /* PARE AQUI: marque esta linha com F9 */
        soma = soma + f;
        printf("%d! = %ld\n", i, f);
    }

    printf("Soma dos fatoriais de 1 a %d = %ld\n", n, soma);
    return 0;
}
