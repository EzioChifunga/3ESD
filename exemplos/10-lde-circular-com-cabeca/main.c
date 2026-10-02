#include <stdio.h>
#include <stdlib.h>

typedef struct no {
    int info;
    struct no *ant, *prox;
} No;

typedef struct {
    No *cab;      /* nó cabeça: não guarda dado */
    int tam;
} Lista;

void lde_cria(Lista *l)
{
    l->cab = (No *) malloc(sizeof(No));
    if (l->cab == NULL) exit(1);
    l->cab->prox = l->cab->ant = l->cab;   /* vazia: aponta para si mesma */
    l->tam = 0;
}

int lde_vazia(const Lista *l) { return l->cab->prox == l->cab; }

/* busca com sentinela: devolve o primeiro nó com info >= x (ou a cabeça) */
No *lde_posicao(const Lista *l, int x)
{
    No *p;
    l->cab->info = x;                       /* sentinela garante a parada */
    for (p = l->cab->prox; p->info < x; p = p->prox)
        ;
    return p;
}

/* insere antes do nó "pos": sempre as mesmas 4 atribuições */
static void insere_antes(No *pos, No *novo)
{
    novo->prox = pos;
    novo->ant = pos->ant;
    pos->ant->prox = novo;
    pos->ant = novo;
}

/* 1 = inseriu, 0 = repetido */
int lde_insere_ordenado(Lista *l, int x)
{
    No *pos = lde_posicao(l, x), *novo;
    if (pos != l->cab && pos->info == x)
        return 0;
    novo = (No *) malloc(sizeof(No));
    if (novo == NULL) exit(1);
    novo->info = x;
    insere_antes(pos, novo);
    l->tam++;
    return 1;
}

/* 1 = removeu, 0 = não existe */
int lde_remove(Lista *l, int x)
{
    No *p = lde_posicao(l, x);
    if (p == l->cab || p->info != x)
        return 0;
    p->ant->prox = p->prox;
    p->prox->ant = p->ant;
    free(p);
    l->tam--;
    return 1;
}

void lde_imprime(const Lista *l)
{
    No *p;
    printf("ida:   cab");
    for (p = l->cab->prox; p != l->cab; p = p->prox)
        printf(" <-> %d", p->info);
    printf(" <-> cab   (%d elementos)\n", l->tam);
    printf("volta: cab");
    for (p = l->cab->ant; p != l->cab; p = p->ant)
        printf(" <-> %d", p->info);
    printf(" <-> cab\n");
}

void lde_libera(Lista *l)
{
    No *p = l->cab->prox, *t;
    while (p != l->cab)
    {
        t = p->prox;
        free(p);
        p = t;
    }
    free(l->cab);
    l->cab = NULL;
}

int main(void)
{
    Lista l;
    lde_cria(&l);

    lde_insere_ordenado(&l, 30);   /* lista vazia */
    lde_insere_ordenado(&l, 10);   /* início */
    lde_insere_ordenado(&l, 50);   /* fim */
    lde_insere_ordenado(&l, 20);   /* meio */
    lde_imprime(&l);               /* PARE AQUI: marque com F9 */

    printf("Inserir 20 de novo: %s\n", lde_insere_ordenado(&l, 20) ? "ok" : "repetido");

    lde_remove(&l, 10);            /* primeiro */
    lde_remove(&l, 50);            /* último */
    lde_imprime(&l);

    lde_libera(&l);
    return 0;
}
