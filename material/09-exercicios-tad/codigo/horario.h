#ifndef HORARIO_H
#define HORARIO_H

typedef struct horario Horario;

/* Aloca dinamicamente um Horario validando horas (0-23), minutos (0-59) e segundos (0-59). */
Horario* hor_cria(int h, int m, int s);

/* Libera a memória alocada para a estrutura Horario. */
void hor_libera(Horario *h);

/* Retorna uma string alocada dinamicamente no formato "HH:MM:SS". */
char* hor_paraString(const Horario *h);

/* Converte e retorna o valor total do horário traduzido em segundos. */
int hor_paraSegundos(const Horario *h);

/* Retorna negativo se h1 < h2, zero se h1 == h2, ou positivo se h1 > h2. */
int hor_compara(const Horario *h1, const Horario *h2);

#endif