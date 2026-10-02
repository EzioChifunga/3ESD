#include <stdio.h>

#define MAX 100

typedef struct {
    int v[MAX];   /* elementos, sempre em ordem crescente */
    int n;        /* quantos elementos a lista tem */
} ListaSeq;

void lst_inicia(ListaSeq *l) { l->n = 0; }

/* Busca binária. Devolve 1 se achou. Em *pos fica a posição onde está
   (ou onde DEVERIA estar, se não achou). */
int lst_busca(const ListaSeq *l, int x, int *pos)
{
    int ini = 0, fim = l->n - 1, meio;
    while (ini <= fim)
    {
        meio = (ini + fim) / 2;
        if (l->v[meio] == x) { *pos = meio; return 1; }
        if (x < l->v[meio]) fim = meio - 1;
        else ini = meio + 1;
    }
    *pos = ini;
    return 0;
}

/* 1 = incluiu, 0 = lista cheia, -1 = já existe */
int lst_inclui(ListaSeq *l, int x)
{
    int pos, i;
    if (l->n == MAX) return 0;
    if (lst_busca(l, x, &pos)) return -1;
    for (i = l->n; i > pos; i--)      /* abre espaço */
        l->v[i] = l->v[i - 1];
    l->v[pos] = x;
    l->n++;
    return 1;
}

/* 1 = excluiu, 0 = não existe */
int lst_exclui(ListaSeq *l, int x)
{
    int pos, i;
    if (!lst_busca(l, x, &pos)) return 0;
    for (i = pos; i < l->n - 1; i++)   /* fecha o buraco */
        l->v[i] = l->v[i + 1];
    l->n--;
    return 1;
}

void lst_mostra(const ListaSeq *l)
{
    int i;
    printf("Lista (%d): [", l->n);
    for (i = 0; i < l->n; i++)
        printf(i ? ", %d" : "%d", l->v[i]);
    printf("]\n");
}

int main(void)
{
    ListaSeq l;
    int op, x, pos;

    lst_inicia(&l);
    while (1)
    {
        printf("\n1 x = incluir | 2 x = excluir | 3 x = buscar | 0 = sair: ");
        if (scanf("%d", &op) != 1 || op == 0)
            break;
        scanf("%d", &x);
        switch (op)
        {
        case 1:
            switch (lst_inclui(&l, x))
            {
            case 1: printf("%d incluído\n", x); break;
            case 0: printf("lista cheia\n"); break;
            default: printf("%d já está na lista (sem repetição)\n", x);
            }
            break;
        case 2:
            printf(lst_exclui(&l, x) ? "%d excluído\n" : "%d não está na lista\n", x);
            break;
        case 3:
            if (lst_busca(&l, x, &pos))
                printf("%d está na posição %d\n", x, pos);
            else
                printf("%d não está; entraria na posição %d\n", x, pos);
            break;
        }
        lst_mostra(&l);
    }
    return 0;
}
