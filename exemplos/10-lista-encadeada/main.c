#include <stdio.h>
#include <stdlib.h>

typedef struct no {
    int info;
    struct no *prox;
} No;

No *lst_cria(void) { return NULL; }

int lst_vazia(No *lst) { return lst == NULL; }

/* insere no início: o novo nó passa a ser o primeiro */
No *lst_insere(No *lst, int x)
{
    No *novo = (No *) malloc(sizeof(No));
    if (novo == NULL) exit(1);
    novo->info = x;
    novo->prox = lst;
    return novo;
}

/* insere mantendo a ordem crescente */
No *lst_insere_ordenado(No *lst, int x)
{
    No *ant = NULL, *p = lst, *novo;
    while (p != NULL && p->info < x)
    {
        ant = p;
        p = p->prox;
    }
    novo = (No *) malloc(sizeof(No));
    if (novo == NULL) exit(1);
    novo->info = x;
    novo->prox = p;
    if (ant == NULL)          /* entra antes do primeiro: muda o início */
        return novo;
    ant->prox = novo;
    return lst;
}

No *lst_busca(No *lst, int x)
{
    No *p;
    for (p = lst; p != NULL; p = p->prox)
        if (p->info == x)
            return p;
    return NULL;
}

No *lst_retira(No *lst, int x)
{
    No *ant = NULL, *p = lst;
    while (p != NULL && p->info != x)
    {
        ant = p;
        p = p->prox;
    }
    if (p == NULL)            /* não achou */
        return lst;
    if (ant == NULL)          /* era o primeiro */
        lst = p->prox;
    else
        ant->prox = p->prox;
    free(p);
    return lst;
}

void lst_imprime(No *lst)
{
    No *p;
    printf("lista: ");
    for (p = lst; p != NULL; p = p->prox)
        printf("%d -> ", p->info);
    printf("NULL\n");
}

void lst_libera(No *lst)
{
    No *p = lst, *t;
    while (p != NULL)
    {
        t = p->prox;          /* guarda o próximo ANTES de liberar */
        free(p);
        p = t;
    }
}

int main(void)
{
    No *lst = lst_cria();

    lst = lst_insere(lst, 30);
    lst = lst_insere(lst, 10);
    lst_imprime(lst);

    lst = lst_insere_ordenado(lst, 20);
    lst = lst_insere_ordenado(lst, 5);
    lst = lst_insere_ordenado(lst, 40);
    lst_imprime(lst);                       /* PARE AQUI: marque com F9 */

    printf("20 está na lista? %s\n", lst_busca(lst, 20) ? "sim" : "não");

    lst = lst_retira(lst, 5);               /* primeiro */
    lst = lst_retira(lst, 20);              /* meio */
    lst = lst_retira(lst, 99);              /* não existe */
    lst_imprime(lst);

    lst_libera(lst);
    return 0;
}
