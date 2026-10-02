#ifndef PONTO_H
#define PONTO_H

/* TAD Ponto: um ponto no plano (x, y).
   Tipo OPACO: quem usa o TAD só conhece o nome "Ponto", não os campos. */
typedef struct ponto Ponto;

/* Cria um ponto. Devolve NULL se faltar memória. */
Ponto *pto_cria(float x, float y);

/* Libera a memória do ponto. */
void pto_libera(Ponto *p);

/* Devolve as coordenadas pelos ponteiros x e y. */
void pto_acessa(const Ponto *p, float *x, float *y);

/* Muda as coordenadas. */
void pto_atribui(Ponto *p, float x, float y);

/* Distância entre dois pontos. */
float pto_distancia(const Ponto *p1, const Ponto *p2);

#endif
