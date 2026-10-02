#include <stdio.h>
#include <stdlib.h>
#include "data.h"

struct data {
    int dia;
    int mes;
    int ano;
};

static int eh_bissexto(int a) {
    return (a % 4 == 0 && a % 100 != 0) || (a % 400 == 0);
}

static int dias_no_mes(int m, int a) {
    int dias[] = {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    if (m == 2 && eh_bissexto(a)) return 29;
    return dias[m];
}

static int data_valida(int d, int m, int a) {
    if (a < 1 || m < 1 || m > 12) return 0;
    if (d < 1 || d > dias_no_mes(m, a)) return 0;
    return 1;
}

/* Converte a data para total de dias desde o ano 1 para cálculo direto de diferença */
static int data_para_dias(const Data *d) {
    int a = d->ano;
    int m = d->mes;
    int total = d->dia;

    for (int i = 1; i < m; i++) {
        total += dias_no_mes(i, a);
    }
    
    int a_ant = a - 1;
    total += a_ant * 365 + (a_ant / 4) - (a_ant / 100) + (a_ant / 400);
    return total;
}

Data* dta_cria(int d, int m, int a) {
    if (!data_valida(d, m, a)) return NULL;

    Data *data = (Data*) malloc(sizeof(Data));
    if (data == NULL) return NULL;

    data->dia = d;
    data->mes = m;
    data->ano = a;
    return data;
}

void dta_libera(Data *d) {
    if (d != NULL) {
        free(d);
    }
}

char* dta_paraString(const Data *d) {
    if (d == NULL) return NULL;
    char *str = (char*) malloc(11 * sizeof(char)); // "DD/MM/AAAA\0"
    if (str == NULL) return NULL;

    sprintf(str, "%02d/%02d/%04d", d->dia, d->mes, d->ano);
    return str;
}

int dta_diferenca(const Data *d1, const Data *d2) {
    return data_para_dias(d1) - data_para_dias(d2);
}

int dta_compara(const Data *d1, const Data *d2) {
    if (d1->ano != d2->ano) return d1->ano - d2->ano;
    if (d1->mes != d2->mes) return d1->mes - d2->mes;
    return d1->dia - d2->dia;
}