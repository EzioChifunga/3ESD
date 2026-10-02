#include <stdlib.h>
#include "ponto.h"
#include "retangulo.h"

struct retangulo {
    Ponto *infEsq;   /* canto inferior esquerdo */
    Ponto *supDir;   /* canto superior direito */
};

static float menor(float a, float b) { return a < b ? a : b; }
static float maior(float a, float b) { return a > b ? a : b; }

Retangulo *ret_cria(float x1, float y1, float x2, float y2)
{
    Retangulo *r = (Retangulo *) malloc(sizeof(Retangulo));
    if (r == NULL)
        return NULL;
    r->infEsq = pto_cria(menor(x1, x2), menor(y1, y2));
    r->supDir = pto_cria(maior(x1, x2), maior(y1, y2));
    return r;
}

void ret_libera(Retangulo *r)
{
    pto_libera(r->infEsq);
    pto_libera(r->supDir);
    free(r);
}

float ret_area(const Retangulo *r)
{
    float x1, y1, x2, y2;
    pto_acessa(r->infEsq, &x1, &y1);
    pto_acessa(r->supDir, &x2, &y2);
    return (x2 - x1) * (y2 - y1);
}

float ret_diagonal(const Retangulo *r)
{
    return pto_distancia(r->infEsq, r->supDir);
}

int ret_contem(const Retangulo *r, float x, float y)
{
    float x1, y1, x2, y2;
    pto_acessa(r->infEsq, &x1, &y1);
    pto_acessa(r->supDir, &x2, &y2);
    return x >= x1 && x <= x2 && y >= y1 && y <= y2;
}
