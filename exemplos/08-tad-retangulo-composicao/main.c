#include <stdio.h>
#include "retangulo.h"

int main(void)
{
    Retangulo *r = ret_cria(4.0f, 3.0f, 0.0f, 0.0f);

    printf("Área: %.1f\n", ret_area(r));          /* PARE AQUI: marque com F9 */
    printf("Diagonal: %.1f\n", ret_diagonal(r));
    printf("(2, 2) está dentro? %s\n", ret_contem(r, 2, 2) ? "sim" : "não");
    printf("(5, 1) está dentro? %s\n", ret_contem(r, 5, 1) ? "sim" : "não");

    ret_libera(r);
    return 0;
}
