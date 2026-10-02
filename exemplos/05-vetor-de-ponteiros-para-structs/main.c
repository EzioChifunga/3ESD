#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define MAX 10

typedef struct {
    char nome[50];
    int idade;
} tPessoa;

tPessoa *criaPessoa(const char *nome, int idade)
{
    tPessoa *p = (tPessoa *) malloc(sizeof(tPessoa));
    if (p != NULL)
    {
        strcpy(p->nome, nome);
        p->idade = idade;
    }
    return p;
}

/* ordena por idade trocando apenas os ponteiros (insertion sort) */
void ordenaPorIdade(tPessoa *v[], int n)
{
    int i, j;
    for (i = 1; i < n; i++)
    {
        tPessoa *atual = v[i];
        for (j = i - 1; j >= 0 && v[j]->idade > atual->idade; j--)
            v[j + 1] = v[j];
        v[j + 1] = atual;
    }
}

int main(void)
{
    tPessoa *v[MAX];
    int n = 0, i;

    v[n++] = criaPessoa("Carla", 25);
    v[n++] = criaPessoa("Ana", 21);
    v[n++] = criaPessoa("Bruno", 19);

    for (i = 0; i < n; i++)                        /* PARE AQUI: marque com F9 */
        printf("v[%d] = %p -> %s, %d\n", i, (void *)v[i], v[i]->nome, v[i]->idade);

    ordenaPorIdade(v, n);

    printf("\nOrdenado por idade:\n");
    for (i = 0; i < n; i++)
        printf("v[%d] = %p -> %s, %d\n", i, (void *)v[i], v[i]->nome, v[i]->idade);

    for (i = 0; i < n; i++)
        free(v[i]);
    return 0;
}
