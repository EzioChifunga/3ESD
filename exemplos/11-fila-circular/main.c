#include <stdio.h>

#define MAX 5

typedef struct {
    int itens[MAX];
    int ini;     /* posição do primeiro da fila */
    int fim;     /* posição onde entra o próximo */
    int n;       /* quantos estão na fila */
} Fila;

void fila_inicia(Fila *f) { f->ini = f->fim = f->n = 0; }
int fila_vazia(const Fila *f) { return f->n == 0; }
int fila_cheia(const Fila *f) { return f->n == MAX; }

int fila_insere(Fila *f, int x)
{
    if (fila_cheia(f)) return 0;
    f->itens[f->fim] = x;
    f->fim = (f->fim + 1) % MAX;     /* dá a volta no vetor */
    f->n++;
    return 1;
}

int fila_remove(Fila *f, int *x)
{
    if (fila_vazia(f)) return 0;
    *x = f->itens[f->ini];
    f->ini = (f->ini + 1) % MAX;
    f->n--;
    return 1;
}

void fila_mostra(const Fila *f)
{
    int i;
    printf("fila (%d): ", f->n);
    for (i = 0; i < f->n; i++)
        printf("%d ", f->itens[(f->ini + i) % MAX]);
    printf("  [ini=%d fim=%d]\n", f->ini, f->fim);
}

int main(void)
{
    Fila f;
    int senha, proxima = 1, i;

    fila_inicia(&f);
    for (i = 0; i < 4; i++)
        fila_insere(&f, proxima++);          /* chegam as senhas 1 a 4 */
    fila_mostra(&f);

    for (i = 0; i < 3; i++)                  /* PARE AQUI: marque com F9 */
    {
        fila_remove(&f, &senha);
        printf("Atendendo senha %d\n", senha);
    }
    fila_mostra(&f);

    while (fila_insere(&f, proxima))         /* enche a fila: os índices dão a volta */
        proxima++;
    fila_mostra(&f);
    printf("Fila cheia! A senha %d precisa esperar.\n", proxima);

    while (fila_remove(&f, &senha))
        printf("Atendendo senha %d\n", senha);
    return 0;
}
