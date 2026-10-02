#include <stdio.h>

int main(void)
{
    char nome[50];
    int idade;
    float altura;

    printf("Qual é o seu nome? ");
    scanf("%49s", nome);          /* string: sem & (o nome do vetor já é um endereço) */

    printf("Quantos anos você tem? ");
    scanf("%d", &idade);          /* número: precisa do & */

    printf("Qual a sua altura (ex.: 1.75)? ");
    scanf("%f", &altura);

    printf("\nOlá, %s!\n", nome);
    printf("Daqui a 10 anos você terá %d anos.\n", idade + 10);
    printf("Sua altura em centímetros: %.0f cm\n", altura * 100);

    return 0;
}
