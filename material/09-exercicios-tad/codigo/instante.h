/*
●	Requisitos do TAD:
1.	inst_cria(d, m, a, h, min, s): Instancia dinamicamente o objeto alocando internamente as estruturas Data e Horario.
2.	inst_libera(i): Libera sequencialmente os ponteiros data e horario antes de liberar a estrutura receptora Instante.
3.	inst_paraString(i): (Obrigatório) Retorna uma string alocada no formato "DD/MM/AAAA HH:MM:SS". Deve invocar obrigatoriamente dta_paraString e hor_paraString, concatenar seus resultados e desalocar as strings intermediárias.
4.	inst_compara(i1, i2): Compara dois instantes temporais, retornando um valor negativo se i1 < i2, zero se idênticos ou positivo se i1 > i2.
*/
#ifndef INSTANTE_H
#define INSTANTE_H

#include "data.h"
#include "horario.h"

/* Estrutura opaca */
typedef struct instante Instante;

/* Instancia dinamicamente o objeto alocando internamente as estruturas Data e Horario. */
Instante* inst_cria(int d, int m, int a, int h, int min, int s);

/* Libera sequencialmente os ponteiros data e horario antes de liberar a estrutura receptora Instante. */
void inst_libera(Instante *i);

/* Retorna uma string alocada no formato "DD/MM/AAAA HH:MM:SS".
   Invocação obrigatória de dta_paraString e hor_paraString. */
char* inst_paraString(const Instante *i);

/* Compara dois instantes temporais:
   Retorna negativo se i1 < i2, zero se idênticos ou positivo se i1 > i2. */
int inst_compara(const Instante *i1, const Instante *i2);

/* Retorna a diferença em segundos entre i1 e i2 (i1 - i2). */
int inst_diferenca(const Instante *i1, const Instante *i2);
/* Cria e retorna um novo Instante somando minutos ao instante i base. */
Instante* inst_somaMinutos(const Instante *i, int minutos);
#endif /* INSTANTE_H */