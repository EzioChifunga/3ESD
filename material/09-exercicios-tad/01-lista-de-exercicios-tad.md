# ListaExerciciosTAD

> Fonte: `base/Lista de TAD/ListaExerciciosTAD.pdf`

<!-- página 1 -->

LISTA DE TAD

1.  Usando o TAD conjunto abaixo, resolva o seguinte problema:

/* TAD conjunto*/
/* Elementos do conjunto são acessados através do endereço de um de seus elementos */
/* Tipo elemento de conjunto*/
typedef struct elem ElemConjunto;
/* Cria um conjunto de inteiros vazio */
ElemConjunto* conj_Cria(void);
/*Libera um conjunto de inteiros */
void conj_Libera(ElemConjunto*A);
/* Inclui um novo nº no Conjunto caso ele ainda não pertenca ao conjunto */
ElemConjunto* conj_Insere(ElemConjunto *C, int x);
/* Verifica se um nº pertence ao Conjunto - 1 se pertence ou 0 caso contrário */
int conj_Ehmembro(ElemConjunto*A, int x);
/* Retira um nº do Conjunto caso ele pertenca ao conjunto */
ElemConjunto* conj_Remove(int x, ElemConjunto *C);

/* Uniao de dois conjuntos retornando o conjunto Uniao */
ElemConjunto* conj_Uniao(ElemConjunto *A, ElemConjunto *B);

/* Intersecao de dois conjuntos retornando o conjunto Intersecao */
ElemConjunto* conj_Intersecao(ElemConjunto* A, ElemConjunto* B);

/* Diferencao entre dois conjuntos retornando o conjunto Diferenca */
ElemConjunto* conj_Diferenca(ElemConjunto*A, ElemConjunto*B);

/* Mostra na tela os elementos de um conjunto */
void conj_exibe(ElemConjunto*A);
/* Libera um conjunto */
void conj_Libera (ElemConjunto*A);

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------

2.  Utilizando o TAD Racional, definido abaixo,   crie o módulo principal de um programa (calculadora.c) que
   implemente uma calculadora baseada em números racionais. A calculadora deve permitir que o usuário realize as
   operações de soma, subtração, multiplicação e divisão de números racionais fornecido, exibindo os resultados das
   operações na tela. Obrigatoriamente todos os números utilizados pela calculadora devem ser representados
    pelo tipo abstrato Racional.
   TAD RACIONAL: Um número racional positivo é o que pode ser expresso como quociente de dois inteiros positivos:
    a/b.
     Interface:
       typedef struct rac  Racional;

        // funções exportadas
        // cria um número Racional
       Racional* rac_cria(int x, int y);
        // Destrói um número Racional criado
        void rac_libera(Racional* x);
        // soma dois racionais
       Racional* rac_add(Racional* x, Racional* y);
        // subtrai dois racionais
       Racional* rac_sub(Racional* x, Racional* y);

<!-- página 2 -->

// Multiplica dois racionais
       Racional* rac_mul(Racional* x, Racional* y);
        // divide dois racionais
       Racional* rac_divide(Racional* x, Racional *y);
        //retorna uma string que representa o Racional
       char * rac_exibe(Racional* x);
         //verifica se dois numeros racionais são iguais
         int  rac_iguais(Racional* x, Racional* y);

    Utilizando o TAD Racionall, crie o módulo principal de um programa (calculadora.c) que implemente uma
    calculadora baseada em números racionais. A calculadora deve permitir que o usuário realize as operações de
   soma, subtração, multiplicação e divisão de números racionais fornecido, exibindo os resultados das operações na
     tela. Obrigatoriamente todos os números utilizados pela calculadora devem ser representados pelo tipo abstrato
    Racional.

Racional.h:
// Interface do TAD
// definição do tipo racional TAD
typedef  struct rac   Racional;

// funções exportadas
// cria um número Racional
Racional* rac_cria(int x, int y);
// Destrói um número Racional criado
void rac_libera(Racional* x);
// soma dois racionais
Racional*  rac_add(Racional* x, Racional* y);
// subtrai dois racionais
Racional*  rac_sub(Racional* x, Racional* y);
// Multiplica dois racionais
Racional* rac_mul(Racional* x, Racional* y);
// divide dois racionais
Racional* rac_divide(Racional* x, Racional *y);
//retorna uma string que representa o Racional
char * rac_exibe(Racional* x);
//verifica se dois numeros racionais são iguais
int   rac_iguais(Racional* x, Racional* y);

Racional.c:
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "Racional.h"
struct rac
{
       int a;
       int b;
};
// Protótipos das funções auxiliares
// obtém o numerador do Racional
int getN(Racional* x);
// obtém o denominador do Racional
int getD(Racional* x);

// Implementação das funções
int getN(Racional* x){ return x->a;}
int getD(Racional* x){ return x->b;}

<!-- página 3 -->

Racional* rac_cria(int x, int y)
{
       Racional* r=(Racional*) malloc(sizeof(Racional));
       r->a = x;
       r->b = y;
       return r;
}

void rac_libera(Racional* x){
       free(x);
}

Racional* rac_add(Racional* x, Racional *y)
{
       Racional* r=(Racional*) malloc(sizeof(Racional));
       r->a = (getN(x)*getD(y) )+ (getN(y)*getD(x));
       r->b = getD(x) * getD(y);
       return(r);
}
Racional* rac_sub(Racional* x, Racional*  y)
{
       Racional* r=(Racional*)malloc(sizeof(Racional));
       r->a = ((getN(x)*getD(y)) - (getN(y)*getD(x)));
       r->b = getD(x) * getD(y);
       return(r);
}
Racional* rac_mul(Racional* x, Racional* y)
{
       Racional* r=(Racional*)malloc(sizeof(Racional));
       r->a = getN(x) * getN(y);
       r->b = getD(x) * getD(y);
       return(r);
}
Racional* rac_divide(Racional* x, Racional* y)
{
       Racional* r=(Racional*)malloc(sizeof(Racional));
       r->a = getN(x) * getD(y);
       r->b = getD(x) * getN(y);
       return(r);
}
char * rac_exibe(Racional* x)
{
       static char s[30];
       sprintf(s, "%i / %i", getN(x), getD(x));
       return s;
}

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------

3.  Usando os TADS Ponto e Retangulo, complete a main para ordenar a tabela de ponteiros usando a função
   ordena :
Escreva a função ordena que:
          - recebe um vetor de ponteiros para Retangulo e o número de retângulos
          - ordena o vetor crescentemente por área. Para uma mesma área os retângulos devem ficar
      ordenados crescentemente por proximidade ao eixo y (valor absoluto da coordenada x do
       vértice inferior esquerdo)
Implemente o método de ordenação de sua escolha: ordenação por seleção (Selection Sort) ou bolha
(Bubble Sort). Você deve obrigatoriamente escrever a função auxiliar compRet para comparar 2 retângulos
de acordo com o critério descrito (considerando a área e, para uma mesma área, a proximidade ao eixo y).

/* TAD:Retangulo */
/* Dependência de módulos */

<!-- página 4 -->

#include "ponto.h"
/* Tipo exportado */
typedef struct retangulo Retangulo;
/* Funções exportadas */
/* cria - Aloca e retorna um Retangulo com vertice(x,y) e altura e base */
Retangulo* ret_cria(float x, float y, float altura, float base);
/* libera - Libera a memória de um retangulo previamente criado */
void ret_libera(Retangulo* c);
/* area - Retorna o valor da área do retangulo */
float ret_area(Retangulo* c);
/* mostra os valores de um retangulo */
void ret_exibe(Retangulo* c);
/* copia - Aloca e retorna um retangulo com dados iguais a outro retangulo */
Retangulo* ret_copia(Retangulo*r);
/* Troca conteudo de ponteiros para Retangulos */
void ret_troca(Retangulo**r1, Retangulo**r2);
/* Acessa elementos do retangulo */
float ret_base(Retangulo* c);
float ret_altura(Retangulo* c);
Ponto*  ret_vertice(Retangulo* c);

/* TAD: Ponto (x,y) */
/* Tipo exportado */
typedef struct ponto Ponto;
/* Funções exportadas */
/* Função cria - Aloca e retorna um ponto com coordenadas (x,y) */
Ponto* pto_cria(float x, float y);
/* Função libera - Libera a memória de um ponto */
void pto_libera(Ponto* p);
/* Função acessa - Retorna as coordenadas de um ponto */
void pto_acessa(Ponto* p, float* x, float* y);
/* Função atribui - Atribui valores às coordenadas do ponto */
void pto_atribui(Ponto* p, float x, float y);
/* Função distancia - Retorna a distância entre dois pontos */
float pto_distancia(Ponto* p1, Ponto* p2);

#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include "ponto.h"
#include "retangulo.h"

int main(void)
{
       int i;
       Retangulo* tab[4];
       Ponto * origem = pto_cria(0,0);
       tab[0]= ret_cria(2,2,2,2);
       tab[1] = ret_cria(-1,-1,2,2);
       tab[2] = ret_cria(-1,-1,3,3);
       tab[3]= ret_cria(2,2,4,3);
       for(i=0;i<4;i++)
       {
              printf( "\nRetangulo %d",i+1);
              ret_exibe(tab[i]);
              printf("\t Distancia da Origem%f", pto_distancia(ret_vertice(tab[i]), origem));
       }
       return 0;
}
Retangulo.h
/* TAD:Retangulo */
/* Dependência de módulos */
#include "ponto.h"
/* Tipo exportado */
typedef struct retangulo Retangulo;
/* Funções exportadas */

<!-- página 5 -->

/* cria - Aloca e retorna um círculo com centro(x,y) e raio r */
Retangulo* ret_cria(float x, float y, float altura, float base);
/* libera - Libera a memória de um círculo previamente criado */
void ret_libera(Retangulo* c);
/* area - Retorna o valor da área do círculo */
float ret_area(Retangulo* c);
/* interior - Verifica se um ponto p está dentro do círculo */
int ret_interior(Retangulo* c, Ponto* p);
/* mostra os valores de um circulo */
void ret_exibe(Retangulo* c);
/* copia - Aloca e retorna um círculo com dados iguais a outro retangulo */
Retangulo* ret_copia(Retangulo*r);
/* Troca conteudo de ponteiros para Retangulos */
void ret_troca(Retangulo**r1, Retangulo**r2);
/* Acessa elementos do retangulo */
float ret_base(Retangulo* c);
float ret_altura(Retangulo* c);
Ponto*  ret_vertice(Retangulo* c);

Retangulo.c
#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include "retangulo.h"

struct retangulo {
       Ponto* vie;
       float altura,base;
};
Retangulo* ret_cria(float x, float y, float altura, float base)
{
       Retangulo* c = (Retangulo*)malloc(sizeof(Retangulo));
       c->vie = pto_cria(x,y);
       c->altura = altura;
       c->base = base;
       return c;
}
void ret_libera(Retangulo* c)
{
       pto_libera(c->vie);
       free(c);
}
float ret_area(Retangulo* c)
{
       return c->altura*c->base;
}
int ret_interior(Retangulo* c, Ponto* p)
{
       float d = pto_distancia(c->vie,p);
       return (d<c->base);
}

void ret_exibe(Retangulo* c)
{
       float x,y;
       pto_acessa(c->vie,&x,&y);
       printf("\n Base:%f  Altura:%f Vértice:(%f,%f)", c->base,c->altura,x,y);
       return;
}
float ret_base(Retangulo* c)
{
       return c->base;
}
float ret_altura(Retangulo* c)
{
       return c->altura;
}

<!-- página 6 -->

Ponto*  ret_vertice(Retangulo* c)
{
       return c->vie;
}
Retangulo* ret_copia(Retangulo*r)
{
       float x,y;
       pto_acessa(r->vie,&x,&y);
       Retangulo* c = (Retangulo*)malloc(sizeof(Retangulo));
       c->vie = pto_cria(x,y);
       c->altura = r->altura;
       c->base = r->base;
       return c;
}
void ret_troca(Retangulo**r1, Retangulo**r2)
{
       Retangulo * aux;
       Ponto *p;
       aux = *r1;
       printf("\naux");
       ret_exibe(aux);
       *r1=*r2;
       (*r2)=aux;

       return ;
}

Ponto.h
/* TAD: Ponto (x,y) */
/* Tipo exportado */
typedef struct ponto Ponto;
/* Funções exportadas */
/* Função cria - Aloca e retorna um ponto com coordenadas (x,y) */
Ponto* pto_cria(float x, float y);
/* Função libera - Libera a memória de um ponto */
void pto_libera(Ponto* p);
/* Função acessa - Retorna as coordenadas de um ponto */
void pto_acessa(Ponto* p, float* x, float* y);
/* Função atribui - Atribui valores às coordenadas do ponto */
void pto_atribui(Ponto* p, float x, float y);
/* Função distancia - Retorna a distância entre dois pontos */
float pto_distancia(Ponto* p1, Ponto* p2);

Ponto.c
#include <stdlib.h>
#include <stdio.h>
#include <math.h>
#include "ponto.h"
struct ponto {
float x;
float y;
};
Ponto* pto_cria(float x, float y)
{
Ponto* p = (Ponto*) malloc(sizeof(Ponto));
if (p == NULL) {
printf("Memória insuficiente!\n");
exit(1);
}
p->x = x;
p->y = y;
return p;
}
void pto_libera(Ponto* p){
free(p);
}
void pto_acessa(Ponto* p, float* x, float* y){
*x = p->x;

<!-- página 7 -->

*y = p->y;
}
void pto_atribui(Ponto* p, float x, float y) {
p->x = x;
p->y = y;
}
float pto_distancia(Ponto* p1, Ponto* p2) {
float dx = p2->x - p1->x;
float dy = p2->y - p1->y;
return sqrt(dx*dx + dy*dy);
}

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------

4.  Supondo que um número real seja representado pela seguinte estrutura:

    struct numeroreal

     {

        int esquerda;
        int direita;

      };

   onde esquerda e direita representam respectivamente os números posicionados a esquerda e a direita do ponto
    decimal. Se esquerda for um número negativo, o número real representado será negativo.

   Implemente o TAD NumeroReal (interface numeroreal.h e implementação numeroreal.c) para representar e
    realizar operações aritméticas em números representados pela estrutura numeroreal.

   O TAD NumeroReal deve exportar e implementar as seguintes funções:

             real_cria: a função recebe dois números inteiros representado os valores dos dois lados do ponto
           decimal de um número real. A função deve criar e retornar um ponteiro para a representação real
           destes números;

             real_libera: a função recebe um ponteiro para um NumeroReal e libera a memória utilizada pelo número
              real;

            real_converteFloat: a função recebe um ponteiro para um valor representado pelo tipo abstrato
          NumeroReal e retorna um float correspondente ao número real;

           real_soma: a função recebe dois ponteiros para o tipo abstrato NumeroReal e retorna um ponteiro para
       um NumeroReal contendo a soma dos números reais recebidos por parâmetro;

            real_subtrai: a função recebe dois ponteiros para o tipo abstrato NumeroReal e retorna um ponteiro
           para um NumeroReal contendo a subtração dos números reais recebidos por parâmetro;

             real_multiplica: a função recebe dois ponteiros para o tipo abstrato NumeroReal e retorna um ponteiro
           para um NumeroReal contendo o produto dos números reais recebidos por parâmetro;

<!-- página 8 -->

real_subtrai: a função recebe dois ponteiros para o tipo abstrato NumeroReal e retorna um ponteiro
           para um NumeroReal contendo o resultado da divisão dos números reais recebidos por parâmetro;

    Utilizando o TAD NumeroReal, crie o módulo principal de um programa (calculadora.c) que implemente uma
    calculadora baseada em números reais. A calculadora deve permitir que o usuário realize as operações de soma,
    subtração, multiplicação e divisão de números fornecidos, exibindo os resultados das operações na tela.
    Obrigatoriamente todos os números utilizados pela calculadora devem ser representados pelo tipo abstrato
   NumeroReal.

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------
5.  A) Projete e desenvolva um novo Tipo Abstrato de Dados (TAD) que é uma fração. O nome do TAD deve ser
    Fracao.  Uma fração é definida por 3 (três) componentes: o numerador (como um número natural), o
   denominador (como um número natural maior do que zero) e o sinal (1 se positivo e -1 se negativo).
Implemente as funções deste TAD de modo que seja possível:
 1.  Criar e retornar um ponteiro para uma nova fração, recebendo para isto um número natural como numerador,
   um número natural maior do que zero como denominador e 1 ou -1 dependendo do sinal da fração.
 2.  Exibir na tela uma fração no seguinte formato : -2/3, +1/2, +3/1
 3. Uma função que receba 2 frações e retorne 1 se a primeira é maior que a segunda, -1 se a primeira é menor e 0
    se são iguais.
 4. Uma função para liberar uma fração.
 5.  Acessar o numerador e denominador ( funções separadas) ( get)
 6.  Modificar o numerador e denominador ( funções separadas)(set)
 7.  Modificar uma fração ( numerador e denominador simultaneamente)
 8.  Acessar uma fração ( numerador e denominador simultaneamente)
 9.  Somar duas frações: recebe duas frações e retorna uma NOVA FRAÇÃO com a soma
10. Multiplicar duas frações: recebe duas frações e retorna uma NOVA FRAÇÃO com a multiplicação
11. Dividir duas frações: recebe duas frações e retorna uma NOVA FRAÇÃO com a divisão

Observe que você deve então escrever os arquivos fracao.c e fracao.h.

B) Escreva agora um programa completo criando o(s) arquivo(s) que julgar necessários onde o vetor de tamanho 5
de ponteiros para o TAD Fracao seja criado e inicializado com informações lidas do teclado. Em seguida, devem ser
exibidas a maior fração e a menor dentre as 5.

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------
6.  Nesta questão você deve projetar e desenvolver um novo Tipo Abstrato de Dados (TAD) que represente um
   produto numa loja. O nome do TAD deve ser Produto. Um produto deve armazenar as informações: nome (uma
    cadeia de no máximo 15 caracteres), quantidade em estoque e preço. As seguintes operações devem estar
    disponíveis:

           - criar e retornar um (ponteiro para um) novo produto, recebendo para isso o nome, a quantidade em
       estoque e o preço desse novo produto;

           - exibir na tela nome, quantidade em estoque e preço de um produto recebido;

           - verificar se um produto recebido tem estoque igual a 0(zero), retornando 1 se o estoque estiver zerado, ou
      0 em caso contrário;

           - liberar o espaço em memória ocupado por um produto recebido;

        -acessar campos do TAD ( funções separadas)

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------

<!-- página 9 -->

7.  A sua equipe de programadores está trabalhando no desenvolvimento de um sistema para o gerenciamento de
     livros de uma biblioteca. A sua tarefa é implementar um TAD para representar os livros neste sistema.

    Sabe-se que um livro é representado pelo seguinte tipo estruturado:

    struct livro

     {

      char titulo[50];
      char autor[30];
      char genero[10];
        int ano;

      };

   As funções que devem ser exportadas pelo TAD Livro (na interface do TAD), são as seguintes:

         A função livro_cria que recebe por parâmetro o título, autor, gênero e ano de publicação do livro, cria
       um livro com esses dados e retorna um ponteiro para o novo Livro.

         A função livro_libera que recebe um ponteiro para um Livro e o destrói.

         A função livro_exibe que recebe um ponteiro para um Livro e exibe seus campos na tela.

          Quatro  funções  de  obtenção  dos  dados  armazenados em um  TAD  Livro  (denominadas
           livro_obtemGenero, livro_obtemAutor, livro_obtemTitulo, e livro_obtemAno) que recebem um ponteiro
           para Livro e retornam o valor em questão (e.g. livro_obtemTitulo retorna (o ponteiro para) a cadeia de
            caracteres do titulo de um TAD Livro recebido, assim como livro_obtemAno retorna o ano de um TAD
            Livro recebido).

          Quatro  funções  de  modificação  dos  dados  armazenados em um TAD  Livro  (denominadas
            livro_alteraGenero, livro_alteraAutor, livro_alteraTitulo, e livro_alteraAno) que recebem um ponteiro
           para Livro e um valor e modificam o valor do campo em questão (e.g. livro_alteraTitulo recebe um
           ponteiro para um livro e uma cadeia de caracteres do novo titulo. modificando o valor deste campo).

        Uma função, denominada livro_verificaNoModernismo que recebe um ponteiro para Livro e verifica se
           esse livro pertence ao segundo período do modernismo brasileiro (1930 a 1945). Esta função retorna -1
           se o ano da obra for anterior a 1930, retorna 0 se for no período 1930 a 1945, e retorna 1 se o ano for
            posterior a 1945.

Escreva o conteúdo do arquivo livro.h com a interface deste TAD Livro, incluindo a definição da estrutura e também
todas as funções disponíveis para o usuário. Em seguida, escrevendo o módulo (arquivo livro.c ) que implementa
todas as funções do TAD Livro.

Em seguida, escreve o modulo principal de um programa (principal.c) que crie e inicialize um vetor de ponteiros para
Livro utilizando as funções disponibilizadas pelo TAD e inserindo neste vetor os seguintes livros:

"Novos Poemas","Vinicius de Morais","poesia",1938

"Poemas Escritos na India","Cecilia Meireles","poesia",1962

<!-- página 10 -->

"Orfeu da Conceição","Vinicius de Morais","teatro",1954
"Ariana, a Mulher","Vinicius de Morais","poesia",1936

No modulo principal do seu programa, implemente as seguintes funções:

      Usando o TAD Livro, escreva a função ordenaLivros que ordena o vetor de ponteiros para Livros em ordem
       crescente por nome de autor, depois por gênero e finalmente por ano de publicação. A função recebe um
        vetor de ponteiros para Livro e o número de livros existentes no vetor.

      Usando o TAD Livro, escreva a função de busca binária buscaLivroModerno que recebe um vetor de
        ponteiros para Livro, o número de livros, um nome de autor e um gênero, e retorna um ponteiro para o livro
       mais recente do autor, do tipo indicado, no segundo período do modernismo brasileiro. Considere que o
        vetor está ordenado conforme o critério utilizado pela sua função ordenaLivros.

       Por exemplo, para o vetor de ponteiros criado no programa principal, se a função receber os argumentos
        autor="Vinicius  de  Morais"  e  gênero="poesia",  ela  deve  retornar um  ponteiro  para:  "Novos
        Poemas","Vinicius de Morais","poesia",1938. Caso nenhum livro atendendo à descrição seja encontrada, a
       função retorna NULL.

Em seguida, continue a implementação do modulo principal (principal.c) utilizando as funções criadas para ordenar o
vetor de ponteiros para Livro e em seguida buscar um livro no vetor. O seu programa deve exibir na tela O NOME DO
LIVRO E O NOME DO AUTOR dos livros do vetor ordenado e também o livro retornado pela busca.

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------

8.  Implemente o TAD lista encadeada e dinâmica. Cada elemento da lista tem uma string e um valor real
    .Utilizando o TAD lista, modele e implemente um sistema de lista de compras para uma casa: cada item de
    cozinha que acaba é inserido na lista para ser comprado na próxima ida ao mercado; quando um item é
   comprado, ele deve sair da lista.

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------

9.  Crie um tipo abstrato de dados(TAD) que represente um aluno: nome, matricula, faltas, lista de provas/notas
     (utilize o TAD lista do item 6). As seguintes operações devem estar disponíveis:

        a) - criar e retornar um (ponteiro para um) novo aluno, recebendo para isso o nome, a matrícula, faltas. Esta
       função cria lista de provas realizadas perguntando o usuário o nome da prova ( P1, teste 1 da P1...) e
        respectiva nota. Término, nome da prova == fim

           - exibir na tela os dados do aluno recebido

        -calcular a média das notas do aluno recebido

           - retorna se o aluno está aprovado ou não. 1 para aprovado e 0 reprovado. Considere reprovado faltas > 25
      ou nota menor que 5.

           - melhor aluno - receba dois alunos e retorne o melhor, isto é, aquele que tenha a maior média das notas.
   Caso dois alunos tenham a mesma nota, o melhor será quem tem menos falta

           - liberar o espaço em memória ocupado por um aluno recebido;

        -acessar campos do TAD: funções separadas para obter os valores e outras para modificar os valores

<!-- página 11 -->

Faça um programa, usando o TAD aluno, que obtenha e armazene em um vetor de ponteiros para aluno, os
   dados dos 6 alunos de uma turma. Ordene-o decrescente por média / crescente por falta e exiba-o. Para cada
    aluno deve ser exibido seu dados , média e situação ( APROVADO/REPROVADO)

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------
10. Crie um TAD que represente uma equação de segundo grau: a, b e c. As funções que devem ser exportadas pelo
   TAD (na interface do TAD), são as seguintes:

     - exibir a equação: "ax2 +bx +c".
     - calcular a função Delta: D=b2 -4ac.
     - calcular as raízes da equação em variáveis cujos endereços são recebidos. A função retorna o nº de raízes
    calculadas (0 se Delta<0; 1z se Delta ==0, 2 se Delta>0)

---------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------

11. Crie os TADS e suas funções usuais ( criar, liberar, obter/alterar campos, exibir, copiar):
        ●  Produto: Nome, preço, e estoque.
        ●  Venda: Numero e Produtos(vetor de ponteiros para tipos abstratos produto).

                           criar função para retornar o valor total da venda.
