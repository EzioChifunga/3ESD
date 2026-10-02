#include <stdio.h>
#include <string.h>

#define MAX 100

typedef struct {
    char itens[MAX];
    int topo;            /* índice do último empilhado; -1 = vazia */
} Pilha;

void pilha_inicia(Pilha *p) { p->topo = -1; }
int pilha_vazia(const Pilha *p) { return p->topo == -1; }
int pilha_cheia(const Pilha *p) { return p->topo == MAX - 1; }

int empilha(Pilha *p, char c)
{
    if (pilha_cheia(p)) return 0;
    p->itens[++p->topo] = c;
    return 1;
}

int desempilha(Pilha *p, char *c)
{
    if (pilha_vazia(p)) return 0;
    *c = p->itens[p->topo--];
    return 1;
}

/* Devolve 1 se balanceada; senão mostra o problema e devolve 0. */
int verifica(const char *expr)
{
    Pilha p;
    char aberto;
    int i;

    pilha_inicia(&p);
    for (i = 0; expr[i] != '\0'; i++)
    {
        char c = expr[i];
        if (c == '(' || c == '[' || c == '{')
        {
            if (!empilha(&p, c)) { printf("  expressão muito longa\n"); return 0; }
        }
        else if (c == ')' || c == ']' || c == '}')
        {
            if (!desempilha(&p, &aberto))
            {
                printf("  '%c' na posição %d fecha algo que não foi aberto\n", c, i);
                return 0;
            }
            if ((c == ')' && aberto != '(') || (c == ']' && aberto != '[') || (c == '}' && aberto != '{'))
            {
                printf("  '%c' na posição %d não combina com '%c'\n", c, i, aberto);
                return 0;
            }
        }
    }
    if (!pilha_vazia(&p))
    {
        printf("  sobrou '%c' aberto\n", p.itens[p.topo]);
        return 0;
    }
    return 1;
}

int main(void)
{
    char linha[MAX + 2];

    printf("Digite expressões (uma por linha):\n");
    while (fgets(linha, sizeof linha, stdin) != NULL)
    {
        linha[strcspn(linha, "\n")] = '\0';
        if (linha[0] == '\0')
            continue;
        printf("%s\n", linha);
        if (verifica(linha))
            printf("  balanceada\n");
    }
    return 0;
}
