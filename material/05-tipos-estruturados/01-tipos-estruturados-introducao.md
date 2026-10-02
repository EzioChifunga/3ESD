# TiposEstruturadosIntroducao_Aula1

> Fonte: `base/Revisão Tipos Estruturados/TiposEstruturadosIntroducao_Aula1.pdf`

<!-- página 1 -->

Tipos Estruturados

                                                                                                                                                    1

<!-- página 2 -->

Dados Compostos

●  Linguagem traz alguns tipos de dados básicos capazes de representar coisas simples

       – int, float, char etc.

● Como representar/armazenar dados estruturados compostos por uma coleção de dados de tipos diferentes mas

   tratado como um único objeto (ou tipo) como nos exemplos abaixo?
  −  Data: dia, mês, ano
  −  Cliente: cpf, nome, endereço
   −Aluno: matrícula, nome, gênero, data de nascimento
  −Ponto no espaço bidimensional: coordenadas (x e y),                                                              Aluno
    Data                                                                                      Ponto                                   Cliente                                  Matricula
        Dia                                                                               X                            Nome                     Nome
      Mês                                                                                    Y                                    CPF                          Genero
       Ano                            End       Rua                  Dt          Dia
                                                              Nasc                                  No                        Mes

                                     Compl                        Ano
                                                                                                                                                                                                                     2

<!-- página 3 -->

Dados Compostos: Estruturas

●Uma estrutura é um tipo composto de dados que podem ser de tipos diferentes. È definido pelo usuário e os itens que a
  compões são identificados por nomes

   Identificador                                                                      Identificadores dos
                                       dia  mes  ano                           tData :  do tipo estrutura (novo                                                        campos
   tipo de dados)

                                         Itens/ membros/campos

    Agregado heterogêneo de dados: une “diversos atributos” em um único tipo

    São tipos de dados criados pelo programador                                                                                           3

                                                                                                                                                                                                                     3

<!-- página 4 -->

struct:Tipo Estrutura- Declaração e uso

●Sintaxe:
    struct nomeEstrutura {             O acesso aos membros de uma estrutura é feito
         tipo1 nome1;
                                                         através do operador:                                                               .         ...
         tipoN nomeN;
    };
    typedef struct nomeEstrutura nomeTipo;

                          struct aluno{
                                              (1)                 (2)                               int  matr;
                               char nome[8];
  Aluno
                               char gen;
          Matricula              };
        Nome               typedef struct aluno
                          tAluno;
          Genero                                                         (3)

  Dt Nasc         Dia
              Mes                (1) struct aluno  al1;
                                  tAluno al2 ;                Ano
                               (2) al1.matr= 12345;
                               (3)  al2=al1;
                                                                                                                                                                                                                     4

<!-- página 5 -->

Exemplo

#include <string.h>
struct aluno{
    int  matr;
    char nome[8];
    char gen;
};
typedef struct aluno tAluno;

int main(void){
    struct aluno  al1;
    tAluno al2;
    al1.matr= 12345;
    strcpy(al1.nome,"Kiko");
    printf("\nGênero do estudante %s (matr: %d)",al1.nome,al1.matr);
    scanf(" %c",&al1.gen);
    al2=al1;
    printf("Aluno1: %s Matr:%d Gen: %c",al1.nome,al1.matr,al1.gen);
    printf("Aluno2: %s Matr:%d Gen: %c",al2.nome,al2.matr,al2.gen);
    return 0;
}

                                                                                                                                                                                                                  5

<!-- página 6 -->

Estruturas aninhadas

Aluno                 struct data{                                 (1)
        Matricula              int dia;
                        int mes;       Nome
                        int ano;
        Genero
                    };
Dt Nasc         Dia
                    typedef struct data tData;
            Mes

             Ano        struct aluno{
                        int  matr;
                        char nome[8];
                        char gen;
                        tData dtNasc;
                    };
                    typedef struct aluno tAluno;      (2)                                                                        (3)

                        tAluno  al1;     //1
                        tData dta;      //1
                        al1.dtNasc.dia=10;   //2
                        al1.dtNasc.mes=10;   //2
                        al1.dtNasc.ano=2000; //2
                        al1.matr= 12345;     //2
                        dta=al1.dtNasc;   //3

                                                                                                                                                                                                                    6

<!-- página 7 -->

Tipo Estrutura: Lendo/Exibindo os campos

      /* Captura e imprime as coordenadas de um ponto qualquer */
      #include <stdio.h>                              Basta escrever &p.x em
      struct ponto {                                      lugar de &(p.x).
         float x;                          O operador de acesso ao
         float y;                                campo da estrutura tem
                                                             precedência sobre o      };
                                                           operador "endereço de"      int main (void)
      {
         struct ponto p;
         printf("Digite as coordenadas do ponto(x y): ");
         scanf("%f %f", &p.x, &p.y);
         printf("O ponto fornecido foi: (%.2f,%.2f)\n", p.x, p.y);
         return 0;
      }

                                                                                                                                                                                        7

<!-- página 8 -->

Tipo Estrutura: Lendo/Exibindo os campos

#include <stdio.h>
#include <stdlib.h>
                                                                                                 Basta escrever &al.matrstruct data{
    int dia;            int main(void){                                 em lugar de &(al.matr).
    int mes;                tAluno  al;
    int ano;                                                         O operador de acesso ao
};                                                                             campo da estrutura tem
typedef struct data tData;      printf("Digite a matr do aluno");                                                                                               precedência sobre o
                         scanf("%d",&al.matr);
struct aluno{                                                                               operador "endereço de"                         printf("Digite o 1º nome do aluno");
    int  matr;
    char nome[8];             scanf(" %s",al.nome);
    char gen;               printf("Digite o gênero do aluno");
    tData dtNasc;             scanf(" %c",&al.gen);
};
typedef struct aluno tAluno;    printf("Digite dt de nascimento(dd/mm/aaaa) do aluno");
                         scanf("%d/%d/%d",&al.dtNasc.dia,&al.dtNasc.mes,&al.dtNasc.ano);

                         printf("Matr: %d\n",al.matr);
                         printf("Nome: %s\n",al.nome);
                         printf("Gênero: %c\n",al.gen);
                         printf("Dt Nasc: %d/%d/%d",al.dtNasc.dia,al.dtNasc.mes,al.dtNasc.ano);
                         return 0;
                     }

                                                                                                                                                                                                                          8

<!-- página 9 -->

Passagem de estruturas por valor para funções

    –  análoga à passagem de variáveis simples
                                                    void exibe(tAluno al){
    – função recebe toda a estrutura como parâmetro:                                                       printf("Matr: %d\n",al.matr);
     » função acessa a cópia da estrutura na pilha                printf("Nome: %s\n",al.nome);
                                                       printf("Gênero: %c\n",al.gen);
                                                       printf("Data de nascimento: %d/%d/%d",     » função não altera os valores dos campos da estrutura original
                                                             al.dtNasc.dia,
     » operação pode ser custosa se a estrutura for muito grande           al.dtNasc.mes,#include <stdio.h>
#include <stdlib.h>       void exibe(tAluno al);             al.dtNasc.ano);
                                                      return;
struct data{
                       int main(void){                   }    int dia;
                           tAluno  al ;    int mes;
    int ano;
};                         printf("Digite a matr do aluno" );
typedef struct data tData;     scanf("%d",&al.matr);
                           printf("Digite o 1º nome do aluno" );
struct aluno{
                          scanf(" %s",al.nome);
    int  matr;
                           printf("Digite o gênero do aluno" );    char nome[8];
    char gen;                scanf(" %c",&al.gen);
    tData dtNasc;            printf("Digite dt de nascimento(dd/mm/aaaa) do aluno" );
};                         scanf("%d/%d/%d",&al.dtNasc.dia,&al.dtNasc.mes,&al.dtNasc.ano);
typedef struct aluno tAluno;

                           exibe(al);
                           return 0;
                       }

                                                                                                                                                                                                                        9

<!-- página 10 -->

Estuturas como valor de retorno

struct data{
    int dia;
                         int main(void){    int mes;
    int ano;                    tAluno  al;
};
typedef struct data tData;
                              al=retornaAluno();
struct aluno{                    exibe(al);
                                            tAluno retornaAluno(void){
    int  matr;                   return 0;
    char nome[8];                                  tAluno  al;
                         }    char gen;                                     printf("\n===== Dados do aluno ====\n");
    tData dtNasc;
                                               printf("Matr: ");};
typedef struct aluno tAluno;                          scanf("%d",&al.matr);
                                               printf("1º nome: ");
void exibe(tAluno al);
                                               scanf(" %s",al.nome);tAluno retornaAluno(void);
                                               printf("Gênero: ");
                                               scanf(" %c",&al.gen);
                                               printf("Dt de nascimento(dd/mm/aaaa): ");
void exibe(tAluno al){                              scanf("%d/%d/%d",
  printf("Matr: %d\n",al.matr);
                                                        &al.dtNasc.dia,  printf("Nome: %s\n",al.nome);
  printf("Gênero: %c\n",al.gen);                                &al.dtNasc.mes,
  printf("Data de nascimento: %d/%d/%d",                          &al.dtNasc.ano);
     al.dtNasc.dia,
                                              return al;     al.dtNasc.mes,
     al.dtNasc.ano);                             }
  return;
}
                                                                                                                                                                                                                          10

<!-- página 11 -->

Tipo Estrutura: ponteiro para estrutura

●Ponteiros para estrutura:
   – acesso ao valor de um campo x da variável p com uma estrutura: p.x
   – acesso ao valor de um campo x de uma variável com uma estrutura apontada por pp: (*pp).x   OU  pp->x
   – acesso ao endereço do campo x de uma variável com uma estrutura apontada por pp : &pp->x

               tData dt;
               tData*p=&dt;
               ...
               (*p).dia = 12;/* formas equivalentes de acessar o valor de um campo x */

                p->dia = 12;

                dt.dia = 12;

                                                                       Nesse caso, os parênteses são indispensáveis pois o               (&dt)->dia = 12;
                                                                         operador “conteúdo de”(*) tem precedência menor
                                                                  do que o operador de acesso ao campo da estrutura
                                                                                    “ponto”(.)

                                                                                                                                                                                                                     11

<!-- página 12 -->

Passagem de endereço de tipo estruturado para Função

struct data{                               apenas o ponteiro da estrutura é passado, mesmo que não seja
    int dia;
    int mes;                                   necessário alterar os valores dos campos dentro da função
    int ano;
};
typedef struct data tData;

struct aluno{                int main(void){
    int  matr;                   tAluno  al;
    char nome[8];
                                                 void preencheAluno(tAluno *pAl){    char gen;
    tData dtNasc;                 preencheAluno(&al);   printf("\n===== Dados do aluno ====\n");
};                            exibe(&al);           printf("Matr: ");
typedef struct aluno tAluno;
                             return 0;             scanf("%d",&(*pAl).matr);  // pAl->matr
void exibe(tAluno * al);       }                         printf("1º nome: ");
void preencheAluno(tAluno *pAl){                           scanf(" %s",pAl->nome);
                                                   printf("Gênero: ");
                                                    scanf(" %c",&pAl->gen);
                                                    printf("Dt de nascimento(dd/mm/aaaa): ");void exibe(tAluno  * al){
                                                    scanf("%d/%d/%d",  printf("Matr: %d\n",al->matr);
  printf("Nome: %s\n",al->nome);                        &pAl->dtNasc.dia,
  printf("Gênero: %c\n" ,al->gen);                       &pAl->dtNasc.mes,
  printf("Data de nascimento: %d/%d/%d" ,                 &pAl->dtNasc.ano);
     al->dtNasc.dia,                                                   return;
     al->dtNasc.mes,
                                                 }
     al->dtNasc.ano);
 }                                                                                                                                                                                                                          12

<!-- página 13 -->

Estruturas Aninhadas

 struct ponto                               y
                                                                pt2 (4,3) {
    float x;
    float y;
 };
                                                  pt1 (1,1)
 typedef struct ponto Ponto;                                    x

 struct retangulo
                                                                             ou:
 {                                                                             struct retangulo
    Ponto pt1;                                                           {
    Ponto pt2;                                                              struct ponto pt1;
                                                                                struct ponto pt2; };
                                                                             };
 typedef struct retangulo Retangulo;

Defina um objeto chamado tela (como sendo um retângulo) e estabeleça os valores do primeiro ponto da tela (pt1),
conforme figura acima:
                      Retangulo tela;
                      tela.pt1.x = 1.0;
                      tela.pt1.y = 1.0;

                                                                                                                                                                                                                    13
