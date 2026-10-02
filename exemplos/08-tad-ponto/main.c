#include <stdio.h>
#include "ponto.h"

int main(void)
{
    Ponto *p1 = pto_cria(0.0f, 0.0f);
    Ponto *p2 = pto_cria(3.0f, 4.0f);
    float x, y;

    if (p1 == NULL || p2 == NULL)
    {
        printf("Memória insuficiente\n");
        return 1;
    }

    pto_acessa(p2, &x, &y);
    printf("p2 = (%.1f, %.1f)\n", x, y);
    printf("Distância p1-p2 = %.2f\n", pto_distancia(p1, p2));

    pto_atribui(p1, 6.0f, 8.0f);
    printf("Depois de mover p1 para (6, 8): distância = %.2f\n", pto_distancia(p1, p2));

    pto_libera(p1);
    pto_libera(p2);
    return 0;
}
