#ifndef RETANGULO_H
#define RETANGULO_H

typedef struct retangulo Retangulo;

/* Retângulo de lados paralelos aos eixos, dado por dois cantos opostos. */
Retangulo *ret_cria(float x1, float y1, float x2, float y2);
void ret_libera(Retangulo *r);
float ret_area(const Retangulo *r);
float ret_diagonal(const Retangulo *r);
/* 1 se o ponto (x, y) está dentro (ou na borda), 0 caso contrário */
int ret_contem(const Retangulo *r, float x, float y);

#endif
