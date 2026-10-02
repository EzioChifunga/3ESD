# ExercFIxacaolista encadeada

> Fonte: `base/Listas/ExercFIxacaolista encadeada.pdf`

<!-- página 1 -->

Considere uma lista simplesmente encadeada do tipo abaixo:
struct aluno
{
         int mat;
       char nome[21];
         float nota;
       char sexo;
        struct aluno *prox;
};
typedef struct aluno Aluno;
/* função de criação: retorna uma lista vazia */
Aluno* lst_cria (void)
{
       return NULL;
}
void lst_libera (Aluno* lst)
{
       Aluno *p = lst, *t;
       while (p != NULL)
          {
                   t = p->prox; /* guarda referência p/ próx. Aluno */
                free (p); /* libera a memória apontada por p */
            p = t; /* faz p apontar para o próximo */
          }
}
/* inserção no início: retorna a lista atualizada */
int main (void)
{
       Aluno *lal;

          lal = lst_cria();
          lal = lst_insere(lal, 33, "Caio", 3.3, 'M');
          lal = lst_insere(lal, 11, "Ana", 1.1, 'F');
          lal = lst_insere(lal, 44, "Edu", 4.4, 'M');
          lal = lst_insere(lal, 66, "Luiz", 6.6, 'M');
          lal = lst_insere(lal, 22, "Bia", 2.2, 'F');
          lal = lst_insere(lal, 55, "Jane", 5.5, 'F');

/* inserir aqui a chamada para as suas funções e testes.

    1.   Cria a função lst_insere, que recebe o endereço do 1º elemento da lista e os dados de um novo elemento.
        Esta função deve criar um novo nó, colocá-lo com o primeiro nó da lista e retornar o endereço do novo nó
       para que o ponteiro para o primeiro elemento da lista seja atualizado.
                  li                      l                       li
                2N                 1        1        1

       /* inserção no início: retorna o endereço do novo primeiro elemento para atualizar a lista */
       Aluno* lst_insere (Aluno* lst, int mat, char *nome, float nota, char sexo) ;

    2.  Escreva a função conta_elementos (), que recebe uma lista de alunos e retorna o número de alunos nessa
          lista.

<!-- página 2 -->

3.  Escreva a função exibe_lista(), que recebe uma lista de alunos e exibe a matrícula, o nome, a nota e o sexo
      de todos os alunos.
    4.  Escreva a função ReCURSIVA exibe_lista(), que recebe uma lista de alunos e exibe a matrícula, o nome, a
       nota e o sexo de todos os alunos na ordem inversa que estão na lista.
    5.  Escreva a função RECURSIVA conta_sexo (), que recebe uma lista de alunos e um sexo, e retorna o número
      de pessoas com esse sexo na lista.
        Dica: uma lista vazia, não tem elemento, portanto o ponteiro para o primeiro elemento de um lista vazia é
      NULL
    6.  Escreva a função copia_lista(), que recebe uma lista de alunos e uma nota, e copia em uma nova lista os
       alunos que tem nota >= nota recebida, incluindo nesta nova lista de forma ordenada por mat.
    7.  Escreva a função busca_aluno_da_lista(), que recebe uma lista de alunos, e uma matrícula e retorna o
       endereço do nó que contém esta matrícula, se o mesmo existir. ou NULL caso contrário
    8.  Escreva a função remove_primeiro_aluno_da_lista(), que recebe uma lista de alunos, e remove o primeiro
       aluno da lista, se o mesmo existir.
    9.  Escreva a função remove_segundo_aluno_da_lista(), que recebe uma lista de alunos, e remove o segundo
       aluno da lista, se o mesmo existir.
    10. Escreva a função remove_último_aluno_da_lista(), que recebe uma lista de alunos, e remove o último aluno
       da lista, se o mesmo existir.
    11. Escreva a função divide_lista(), que recebe uma lista de alunos, e a divide em duas ( na metade). Retorna o
       endereço do primeiro nó da segunda metade
    12. Escreva a função separa_lista(), que recebe uma lista de alunos, e transfere os alunos do sexo ‘M’, para uma
       nova lista. Retorna o endereço do primeiro nó da lista de transferidos. Para simplificar, garante-se que o
       primeiro nó da lista original NÃO é um ‘M’
    13. Escreva a função muda_lista(), que recebe uma lista de alunos do sexo ‘M’, encontra a maior nota e coloca
       todos os alunos com esta maior nota no início da lista. Retorna o endereço do primeiro nó da lista (DESAFIO)
    14. DESAFIO1: Escreva a função ordena_lista(), que recebe uma lista de alunos do sexo ‘F’, e ordena-a por mat.
       Retorna o endereço do primeiro nó da lista ordenada.
    15. DESAFIO2: Escreva a função junta_listas(), que recebe uma lista de alunos do sexo ‘F’ ordenada por mat e a
          lista gerada pela copia_lista, criando uma nova lista com a união das dus listas recebidas, mantendo a
       ordenação. Retorna o endereço do primeiro nó da lista resultante.

/* inserção no início: retorna a lista atualizada */
Aluno* lst_insereMinha (Aluno* lst, int mat, char *nome, float nota, char sexo)
{
       Aluno* novo = (Aluno*) malloc(sizeof(Aluno));
            if (novo == NULL) exit(1);
       novo->mat = mat;
       strcpy(novo->nome, nome);
       novo->nota = nota;
       novo->sexo = sexo;
       novo->prox = lst;
       return novo;
}
