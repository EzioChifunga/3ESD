#ifndef DATA_H
#define DATA_H

typedef struct data Data;

/* Aloca dinamicamente uma Data validando dia, mês e ano. */
Data* dta_cria(int d, int m, int a);

/* Libera a memória alocada para a estrutura Data. */
void dta_libera(Data *d);

/* Retorna uma string alocada dinamicamente no formato "DD/MM/AAAA". */
char* dta_paraString(const Data *d);

/* Retorna a diferença em dias entre d1 e d2 (d1 - d2). */
int dta_diferenca(const Data *d1, const Data *d2);

/* Retorna negativo se d1 < d2, zero se d1 == d2, ou positivo se d1 > d2. */
int dta_compara(const Data *d1, const Data *d2);

#endif