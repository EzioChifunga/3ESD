#include <stdio.h>
#include <stdlib.h>
#include "horario.h"

struct horario {
    int hora;
    int min;
    int seg;
};

Horario* hor_cria(int h, int m, int s) {
    if (h < 0 || h > 23 || m < 0 || m > 59 || s < 0 || s > 59) {
        return NULL;
    }

    Horario *hor = (Horario*) malloc(sizeof(Horario));
    if (hor == NULL) return NULL;

    hor->hora = h;
    hor->min = m;
    hor->seg = s;
    return hor;
}

void hor_libera(Horario *h) {
    if (h != NULL) {
        free(h);
    }
}

char* hor_paraString(const Horario *h) {
    if (h == NULL) return NULL;
    char *str = (char*) malloc(9 * sizeof(char)); // "HH:MM:SS\0"
    if (str == NULL) return NULL;

    sprintf(str, "%02d:%02d:%02d", h->hora, h->min, h->seg);
    return str;
}

int hor_paraSegundos(const Horario *h) {
    if (h == NULL) return 0;
    return h->hora * 3600 + h->min * 60 + h->seg;
}

int hor_compara(const Horario *h1, const Horario *h2) {
    return hor_paraSegundos(h1) - hor_paraSegundos(h2);
}