#include <stdio.h>
#include <string.h>
#include <ctype.h>

int meuStrlen(const char *s)
{
    int i = 0;
    while (s[i] != '\0')
        i++;
    return i;
}

int tamanhoRecursivo(const char *s)
{
    if (*s == '\0')
        return 0;                       /* caso base: string vazia */
    return 1 + tamanhoRecursivo(s + 1); /* 1 + tamanho do resto */
}

void meuStrcpy(char *destino, const char *origem)
{
    int i;
    for (i = 0; origem[i] != '\0'; i++)
        destino[i] = origem[i];
    destino[i] = '\0';                  /* não esqueça o terminador! */
}

/* < 0 se a vem antes, 0 se iguais, > 0 se a vem depois */
int meuStrcmp(const char *a, const char *b)
{
    while (*a != '\0' && *a == *b)
    {
        a++;
        b++;
    }
    return (unsigned char)*a - (unsigned char)*b;
}

int contaVogais(const char *s)
{
    int n = 0;
    for (; *s; s++)
        if (strchr("aeiouAEIOU", *s) != NULL)
            n++;
    return n;
}

int main(void)
{
    char frase[100];
    char copia[100];
    int i;

    printf("Digite uma frase: ");
    if (fgets(frase, sizeof frase, stdin) == NULL)
        return 1;
    frase[strcspn(frase, "\n")] = '\0';  /* remove o Enter lido pelo fgets */

    printf("Tamanho: %d (strlen: %d, recursivo: %d)\n",
           meuStrlen(frase), (int)strlen(frase), tamanhoRecursivo(frase));

    meuStrcpy(copia, frase);
    for (i = 0; copia[i]; i++)
        copia[i] = toupper((unsigned char)copia[i]);
    printf("Em maiúsculas: %s\n", copia);

    printf("Vogais: %d\n", contaVogais(frase));
    printf("Comparação com \"Estrutura\": %d\n", meuStrcmp(frase, "Estrutura"));
    return 0;
}
