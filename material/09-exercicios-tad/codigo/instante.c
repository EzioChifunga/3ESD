#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "instante.h"

struct instante {
    Data *data;
    Horario *horario;
};

/* Função auxiliar privada para converter um instante em segundos totais */
static long long inst_para_segundos_totais(const Instante *i) {
    if (i == NULL) return 0;
    
    // dta_diferenca com uma data base (01/01/0001) nos dá o total de dias passados
    Data *data_base = dta_cria(1, 1, 1);
    long long dias = dta_diferenca(i->data, data_base);
    dta_libera(data_base);

    long long seg_dias = dias * 86400LL; // 86400 segundos em um dia
    long long seg_horario = hor_paraSegundos(i->horario);

    return seg_dias + seg_horario;
}

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "instante.h"

// Função auxiliar para converter Instante para time_t
static time_t inst_paraTimeT(int d, int m, int a, int h, int min, int s) {
    struct tm t = {0};
    t.tm_mday = d;
    t.tm_mon = m - 1;      // tm_mon varia de 0 a 11
    t.tm_year = a - 1900;  // tm_year conta anos a partir de 1900
    t.tm_hour = h;
    t.tm_min = min;
    t.tm_sec = s;
    t.tm_isdst = -1;       // Deixa a biblioteca decidir se há horário de verão

    return mktime(&t);
}

Instante* inst_somaMinutos(const Instante *i, int minutos) {
    if (i == NULL) return NULL;

    // 1. Converte o instante atual para segundos base
    // Nota: Supondo que você tenha getters no TAD ou acesso à struct interna
    // Caso a struct esteja opaca no .c, acesse os campos d, m, a, h, min, s diretamente da struct interna:
    time_t tempo_base = inst_paraTimeT(i->data->dia, i->data->mes, i->data->ano,
                                       i->horario->hora, i->horario->min, i->horario->sec);

    // 2. Soma os minutos convertidos em segundos
    tempo_base += (minutos * 60);

    // 3. Converte os segundos de volta para struct tm
    struct tm *nova_data = localtime(&tempo_base);

    // 4. Instancia e retorna o novo objeto Instante
    return inst_cria(
        nova_data->tm_mday,
        nova_data->tm_mon + 1,
        nova_data->tm_year + 1900,
        nova_data->tm_hour,
        nova_data->tm_min,
        nova_data->tm_sec
    );
}

Instante* inst_cria(int d, int m, int a, int h, int min, int s) {
    Instante *i = (Instante*) malloc(sizeof(Instante));
    if (i == NULL) return NULL;

    i->data = dta_cria(d, m, a);
     /* Se falhar em criar a data , limpa tudo e retorna NULL */
    if (i->data == NULL) {
        free(i);
        return NULL;
    }
    i->horario = hor_cria(h, min, s);

    /* Se falhar em criar  o horário, limpa tudo e retorna NULL */
    if ( i->horario == NULL) {
        if (i->data) dta_libera(i->data);
        free(i);
        return NULL;
    }

    return i;
}

void inst_libera(Instante *i) {
    if (i != NULL) {
       
            dta_libera(i->data);
       
            hor_libera(i->horario);
            free(i);
    }
}

char* inst_paraString(const Instante *i) {
    if (i == NULL) return NULL;

    /* Obtém strings intermediárias dos TADs base */
    char *str_dta = dta_paraString(i->data);
    char *str_hor = hor_paraString(i->horario);

    if (str_dta == NULL || str_hor == NULL) {
        free(str_dta);
        free(str_hor);
        return NULL;
    }

    /* Formato: "DD/MM/AAAA HH:MM:SS" -> 10 + 1 + 8 + 1 (null terminator) = 20 caracteres */
    size_t tamanho = strlen(str_dta) + strlen(str_hor) + 2; 
    char *str_inst = (char*) malloc(tamanho * sizeof(char));

    if (str_inst != NULL) {
        sprintf(str_inst, "%s %s", str_dta, str_hor);
    }

    /* Liberação obrigatória das strings intermediárias */
    free(str_dta);
    free(str_hor);

    return str_inst;
}

int inst_compara(const Instante *i1, const Instante *i2) {
    if (i1 == NULL || i2 == NULL) return 0;

    /* Compara primeiramente as datas */
    int comp_data = dta_compara(i1->data, i2->data);
    if (comp_data != 0) {
        return comp_data;
    }

    /* Se a data for idêntica, compara os horários */
    return hor_compara(i1->horario, i2->horario);
}

int inst_diferenca(const Instante *i1, const Instante *i2) {
    if (i1 == NULL || i2 == NULL) return 0;
    
    long long s1 = inst_para_segundos_totais(i1);
    long long s2 = inst_para_segundos_totais(i2);

    return (int)(s1 - s2);
}