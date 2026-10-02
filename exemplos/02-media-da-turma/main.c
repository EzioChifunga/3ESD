#include <stdio.h>

#define N_ALUNOS 4

int main(void)
{
    int   matr[N_ALUNOS];
    float n1[N_ALUNOS], n2[N_ALUNOS], n3[N_ALUNOS];
    float media[N_ALUNOS];
    float somaTurma = 0, mediaTurma;
    int i;

    /* 1) lê e guarda os dados de todos os alunos */
    for (i = 0; i < N_ALUNOS; i++)
    {
        printf("Aluno %d - matrícula e 3 notas: ", i + 1);
        scanf("%d %f %f %f", &matr[i], &n1[i], &n2[i], &n3[i]);
        media[i] = (n1[i] + n2[i] + n3[i]) / 3;
        somaTurma += media[i];
    }

    /* 2) só agora é possível calcular a média da turma */
    mediaTurma = somaTurma / N_ALUNOS;
    printf("\nMédia da turma: %.2f\n", mediaTurma);

    /* 3) percorre de novo para selecionar quem ficou acima */
    printf("Acima da média:\n");
    for (i = 0; i < N_ALUNOS; i++)
        if (media[i] > mediaTurma)
            printf("  %d  notas %.1f %.1f %.1f  média %.2f\n", matr[i], n1[i], n2[i], n3[i], media[i]);

    return 0;
}
