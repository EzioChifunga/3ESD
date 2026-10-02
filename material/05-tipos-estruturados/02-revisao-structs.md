# RevisãoStructs

> Fonte: `base/Revisão Tipos Estruturados/RevisãoStructs.pdf`

<!-- página 1 -->

Revisão
 struct

                                   1                           1

<!-- página 2 -->

Tipo Estrutura: Problema

●  Linguagem traz alguns tipos de dados básicos capazes de representar coisas

   simples

       – int, float, char etc.

● Como representar/armazenar dados estruturados?

  – Data: dia, mês, ano

  – Cliente: cpf, nome, cartão de crédito

  – Aluno: matrícula, nome, histórico escolar

      Como armazenar/agregar em uma variável algo mais complexo
       composto por uma coleção de dados de tipos diferentes ?

                                                                                                                                                               2

<!-- página 3 -->

Tipo Estrutura: Motivação

– manipulação de dados compostos ou estruturados

– Exemplos:

                                                     Ponto
  » ponto no espaço bidimensional
                                                 X
       • representado por duas coordenadas (x e y),            Y

       mas tratado como um único objeto (ou tipo)

                                                     Aluno
  » dados associados a aluno:
                                               Nome

                                                               Matr       • aluno representado pelo seu
                                                End     Rua
        nome, número de matrícula, endereço, etc .,
                                                     No
          estruturados em um único objeto (ou tipo)
                                                        Compl

  Como armazenar/agregar em uma variável
 um coleção de dados de tipos diferentes ?

                                                                                                                                                         3

<!-- página 4 -->

Estruturas

●Tipo estrutura é um tipo composto por itens (campos ou membros)
   identificados por nomes que podem ser de tipos distintos

 nome da estrutura                             nomes de                         a   b    c
                                              membros/campos                    qq
                                     int   char   float

                         Membros/C
                         ampos

  Agregado heterogêneo de dados

  Unem "diversas variáveis" em um único tipo

  São tipos de dados criados pelo programador

                                                                                                                                                               4

<!-- página 5 -->

Tipo Estrutura

●Sintaxe:                                   struct qq{
    struct nomeEstrutura {                int a;
                                                 char b;         tipo1 nome1;
                                                 float c;
         ...                                             };
         tipoN nomeN;
    };
                                             int main(void)
                                             {
                                                 struct q   minha;
  Criar variáveis:                             minha.a = 10;                                                 minha.b = ´f´;
    struct nomeEstrutura lista de Variáveis;             minha.c = 10.0

●O acesso aos membros de uma estrutura é feito através do
  operador: . (Ponto)

                                                                                                                                                               5

<!-- página 6 -->

Tipo Estrutura- Resumo

●Tipo estrutura:
   – tipo de dado com campos compostos de tipos mais simples
   – elementos acessados através do operador de acesso "ponto" (.)

     struct ponto           /* declara ponto do tipo struct */
      { float x;
        float y;
       };
       ...
      int main(void) {
        struct ponto p;           /* declara p como variável do tipo struct ponto */
            ...
        p.x = 10.0;         /* acessa os elementos de ponto */
        p.y = 5.0;

                                                                                                                                                               6

<!-- página 7 -->

Tipo Estrutura: Lendo/Exibindo os campos

 /* Captura e imprime as coordenadas de um ponto qualquer */
 #include <stdio.h>                                     Basta escrever &p.x em
 struct ponto {                                              lugar de &(p.x).
    float x;                               O operador de acesso ao
    float y;                                      campo da estrutura tem
                                                             precedência sobre o };
                                                           operador "endereço de" int main (void)
 {
    struct ponto p;
    printf("Digite as coordenadas do ponto(x y): ");
    scanf("%f %f", &p.x, &p.y);
    printf("O ponto fornecido foi: (%.2f,%.2f)\n", p.x, p.y);
    return 0;
 }

                                                                                                                                                             7

<!-- página 8 -->

Passagem de estruturas por valor para funções

 – análoga à passagem de variáveis simples
 – função recebe toda a estrutura como parâmetro:
   » função acessa a cópia da estrutura na pilha
   » função não altera os valores dos campos da estrutura original
   » operação pode ser custosa se a estrutura for muito grande

/* função que imprima as coordenadas do ponto */
void imprime (struct ponto p)
{
   printf("O ponto fornecido foi: (%.2f,%.2f)\n", p.x, p.y);
}
int main (void)
{
   struct ponto p;
   printf("Digite as coordenadas do ponto(x y): ");
   scanf("%f %f", &p.x, &p.y);
   imprime(p);
   return 0;
}

                                                                                                                                                           8

<!-- página 9 -->

Estuturas como valor de retorno

/* Captura e imprime as coordenadas de um ponto qualquer */
#include <stdio.h>
struct ponto { float x; float y; };

struct ponto le( void){
 struct ponto  tmp;
 printf("Digite as coordenadas do ponto(x y): ");
 scanf("%f %f", &tmp.x, &tmp.y);
 return tmp;
}

int main (void){
   struct ponto p=le();
   printf("O ponto fornecido foi: (%.2f,%.2f)\n", p.x, p.y);
   return 0;
}

                                                                                                                                                                 9

<!-- página 10 -->

Tipo Estrutura: ponteiro para estrutura

●Ponteiros para estrutura:
   – acesso ao valor de um campo x da variável p com uma estrutura:  p.x
   – acesso ao valor de um campo x de uma variável com uma estrutura apontada por
      pp:    pp->x
   – acesso ao endereço do campo x de uma variável com uma estrutura apontada por
     pp :   &pp->x

   struct ponto p;
   struct ponto *pp=&p;
   ...
   (*pp).x = 12.0;/* formas equivalentes de acessar o valor de um campo x */

   pp->x = 12.0;

   p.x = 12.0;

   (&p)->x = 12.0;

                                                                                                                                                               10

<!-- página 11 -->

Qual o valor de...?

                                                            Pilha de memória
struct ponto {
   float x;
   float y;
};                                                                                      7020

int main ( )                                                                    7016
{
                                                                                      7012
    struct ponto p = { 10.,20};
    struct ponto *pp=&p;                                                   7008
 ...                                                y    20    7004
}                                           p
                                        main       x    10    7000

 Qual o valor de …?

                                        &(pp->y)                      pp->x   p.y
                                                        &((&p)->y)
            pp.x                                 (&p)->x
                                                   &(p.y)

                                                                                                                                                           11

<!-- página 12 -->

Estruturas – Passagem por Referência

•  Assim como acontece com variáveis simples, também é possível passar
   estruturas por referencia:

 /* função que imprima as coordenadas do ponto */
 void imprime(struct ponto* pp)
 {
   printf("O ponto fornecido foi: (%f, %f)\n", pp->x, pp->y);
 }

•  A função recebe o endereço da estrutura como parâmetro:
   – a função pode alterar os valores dos campos da estrutura original
   – operação menos custosa do que a passagem por valor

                                                                                                                                                                12

<!-- página 13 -->

Exemplo com Passagem por Referência

 void imprime(struct ponto* pp)
 {
   printf("O ponto fornecido foi: (%f, %f)\n", pp->x, pp->y);

 }

 void captura(struct ponto* pp)
 {
   printf("Digite as coordenadas do scanf("%f %f", &pp->x, &pp->y);
   pp->y);                           ponto(x y): ");
}

int main(void)
{
  struct ponto p;
  captura(&p);
  imprime(&p);
  return 0;

}

                                                                                                                                                                13

<!-- página 14 -->

Alocação dinâmica de estruturas

– tamanho do espaço de memória alocado dinamicamente é dado pelo
  operador sizeof aplicado sobre o tipo estrutura

– função malloc retorna o endereço do espaço alocado, que é então
  convertido para o tipo ponteiro da estrutura

struct ponto* p;
p = (struct ponto*) malloc (sizeof(struct ponto));

...
p->x = 12.0;
...
free(p);

                                                                                                                                                         14

<!-- página 15 -->

Definição de Novos Tipos

●typedef
   – permite criar nomes de tipos
   –  útil para abreviar nomes de tipos e para tratar tipos complexos

  struct ponto {                                               Representa a
      float x;                                                    estrutura ponto
      float y;
  };

                                                            Representa o tipo
  typedef struct ponto Ponto;                            ponteiro para a
                                                                  estrutura ponto
  typedef struct ponto *PPonto;

    » struct ponto representa uma estrutura com 2 campos do tipo float
    » Ponto   representa o tipo de estrutura struct ponto
    » PPonto representa o tipo ponteiro para a estrutura Ponto

                                                                                                                                                               15

<!-- página 16 -->

Definição de Novos Tipos

 struct ponto
                                                      Representa a
 {
                                                            estrutura ponto
   float x;
   float y;
 };                                                   Representa o tipo
                                                         ponteiro para a typedef struct ponto Ponto;
                                                            estrutura ponto typedef struct ponto *PPonto;

•  Simplifica a utilização da estrutura ponto:

  Ponto p1;
  PPonto p2;
  p1.x = 10.0;
  p2 = (PPonto)malloc(sizeof(Ponto));
  p2->x = 5.0;

                                                                                                                                                            16

<!-- página 17 -->

Definição de Novos Tipos

•  É possível combinar o comando typedef com a declaração da
   estrutura:

   typedef struct ponto
   {
     float x;
     float y;

   } Ponto;

                                                                                                                                                                17

<!-- página 18 -->

Estruturas Aninhadas

•  Os campos de uma estrutura podem ser outras estruturas

 Exemplo: Definição de um círculo composto por um ponto central e um raio.

  struct ponto
  {
    float x;
    float y;

  };
  typedef struct ponto Ponto;

  struct circulo
  {
    Ponto p; /* centro do círculo */
    float r; /* raio do círculo */

  };
  typedef struct circulo Circulo;

                                                                                                                                                                18

<!-- página 19 -->

Estruturas Aninhadas

•  Função para a calcular distância entre 2 pontos:      cálculo da distância:

float distancia(Ponto* p, Ponto* q)
{
  float d = sqrt(pow(q->x - p->x, 2) + pow(q->y - p->y, 2));
  return d;

}

 •  Função para determinar se um ponto está ou não dentro de um
    círculo:
 int interior (Circulo* c, Ponto* p)    &c->p : ponteiro para centro de c
 {                                       p       : ponteiro para o ponto
   float d = distancia(&c->p, p);
                                                   c->r    : raio do círculo
   if (d < c->r)
                                       d < c->r : testa se d é menor do raio     return 1;

   else
        return 0;
}

                                                                                                                                                                19

<!-- página 20 -->

Estruturas Aninhadas

• Função principal:

int main(void)
{
  Circulo c;
  Ponto p;
  printf("Digite as coordenadas do centro e o raio                                                  do circulo:\n");
  scanf("%f %f %f", &c.p.x, &c.p.y, &c.r);
  printf("Digite as coordenadas do ponto:\n");
  scanf("%f %f", &p.x, &p.y);
  if (interior(&c,&p) == 1)
    printf("Pertence ao interior!\n");
  else
    printf("Não pertence ao interior!\n");
  return 0;

}

                                                                                                                                                                20

<!-- página 21 -->

Estruturas Aninhadas

  • Função principal:          Modificações:
                                      Centro do círculo: alocado dinamicamente
                                      Usar a Captura ( Ponto *p)
int main(void)
{
  Circulo c;
  Ponto p;
  printf("Digite as coordenadas do centro e o raio                                                  do circulo:\n");
  scanf("%f %f %f", &c.p.x, &c.p.y, &c.r);
  printf("Digite as coordenadas do ponto:\n");
  scanf("%f %f", &p.x, &p.y);
  if (interior(&c,&p) == 1)
    printf("Pertence ao interior!\n");
  else
    printf("Não pertence ao interior!\n");
  return 0;

}

                                                                                                                                                                21

<!-- página 22 -->

Centro do círculo alocado dinamicamente

typedef struct ponto {
       float x;
       float y;
} Ponto;

typedef struct circulo {
       Ponto * p;      /* ponteiro para o centro do círculo */
       float r;    /* raio do círculo */
} Circulo;

int main (void)
{
    Circulo c;
    Ponto p;
    c.p=(Ponto*) malloc(sizeof(Ponto);
    if( c.p == NULL) { printf( "Erro"; exit(10;}
    printf("Circulo:\n");
    printf("\n\tRaio:\n"); scanf("%f ", &c.r);
    printf("\n\tCentro:\n"); captura(c.p) //o endereço do centro
    printf("Ponto:\n");    captura(&p)
    printf("Pertence ao interior = %d\n", interior(&c,&p));
    return 0;
}

                                                                                                                                                                 22

<!-- página 23 -->

Estruturas Aninhadas

• Estrutura de um
  retângulo:struct ponto
{
 float x;
 float y;
};
typedef struct ponto Ponto;

struct retangulo
{                                        Retangulo meu_retangulo;
                                        meu_retangulo.pt1.x = 1.0;
  Ponto pt1;                             meu_retangulo.pt1.y = 1.0;
                                        meu_retangulo.pt2.x = 4.0;  Ponto pt2;
                                        meu_retangulo.pt2.y = 3.0;};

typedef struct retangul Retangulo;
              o

                                                                                                                                                               23

<!-- página 24 -->

Funções para Criação de Estruturas

•  Função para criar a estrutura de um
   Ponto:
  Ponto criaPonto(int x, int y)
  {
    Ponto p;
    p.x = x;
    p.y = y;

    return p;

  }

•  Função para criar dinamicamente a estrutura de um Retângulo:

    Ponto* criaRetangulo(Ponto p1, Ponto p2){
          Retangulo *rect =(Retangulo*)malloc(sizeof(Retangulo));
          rect->pt1= p1;
          rect->pt2= p2;
          return rect;
     }

                                                                                                                                                            24

<!-- página 25 -->

Funções para Criação de Estruturas

•  Função Principal:    Forma 1

   int main (void)
   {
     Retangulo *r1;
     Ponto p1 = criaPonto(1,1);
     Ponto p2 = criaPonto(4,3);

     r1 = criaRetangulo(p1,p2);

     printf("P1: %d %d", r1->pt1.x, r1->pt1.y);
     printf("P2: %d %d", r1->pt2.x, r1->pt2.y);

     free(r1);

     return 0;
   }

                                                                                                                                                            25

<!-- página 26 -->

Funções para Criação de Estruturas

•  Função Principal:    Forma 2

  int main (void)
  {
    Retangulo *r1;

    r1 = criaRetangulo(criaPonto(10,10), criaPonto(20,25));

    printf("P1: %d %d", r1->pt1.x, r1->pt1.y);
    printf("P2: %d %d", r1->pt2.x, r1->pt2.y);

    free(r1);

    return 0;
  }

                                                                                                                                                            26

<!-- página 27 -->

Vetores de Estruturas

  ●Exemplo:
      – função para calcular o centro geométrico de conjunto de pontos
       » entrada: vetor de estruturas definindo o conjunto de
         pontos
       » saída:  centro geométrico, dado por:

08/09/2021                                                                                                                                27

                                                                                                                                                                      27

<!-- página 28 -->

Vetores de Estruturas

Ponto centro_geometrico(int n, Ponto *v)
{
  int i;
  Ponto p = {0, 0};
  for (i=0; i<n; i++)

  {
                                               v é um vetor de estruturas Ponto
    p.x += v[i].x;
    p.y += v[i].y;
                                      Função retornando estrutura:  }
                                                             •  para estruturas pequenas, este  p.x = p.x/n;
                                                   recurso facilita o uso da função  p.y = p.y/n;
                                                             •  para estruturas grandes, a cópia do
  return p;
                                                       valor de retorno pode ser cara
}

                                                                                                                                                                 28

<!-- página 29 -->

Vetores de Estruturas

int main (void)
{
  Ponto vet_pontos[3] = {{10,15}, {50, 30}, {20, 30}};

  Ponto centro;
                                                        Vetor de estrutura
                                                                    inicializado estaticamente.
  centro = centro_geometrico(3, vet_pontos);

  printf("Centro Geometrico: %f, %f\n", centro.x, centro.y);

  return 0;

}

                                                                                                                                                               29

<!-- página 30 -->

Vetores de Estruturas: Com Alocação Dinâmica

 Ponto *centro_geometrico(int n, Ponto* v)
 {
   int i;
   Ponto *p = (Ponto*)malloc(sizeof(Ponto));
   p->x = 0;

   p->y = 0;
   for (i=0; i<n; i++)
   {
     p->x          += v[i].x;
     p->y          += v[i].y;
   }                                    Função retornando ponteiro para
   p->x =  p->x/n;                           estrutura.
   p->y =  p->y/n;
          p;   return

 }

                                                                                                                                                                  30

<!-- página 31 -->

Exemplo

●Criar uma agenda para 5 pessoas com os seguintes dados de cada
  amigo:
   – Nome
   – Telefone
   – Data de Nascimento ( dia,mês,ano)

                                                                                                                                                               31

<!-- página 32 -->

Exemplo: Tabela de Pessoas

typedef struct dt{
   int dia;
   int mes;
   int ano;
} tData;

typedef struct pes {

   char nome[101];
   char fone[21];
   tData nasc;
} tPessoa;

tPessoa agenda[5] = {

 {"Itivaldo Buzo",  "9850-9973", {27, 7,1970}},
  {"Roberto Soares", "266-0879", {15,11,1971}},
  {"Márcia Ueji",    "576-8292", { 9, 5,1966}},
  {"Silvio Lago",    "9851-7715", {18, 3,1968}},
  {"Mie Kobayashi",  "9834-0192", { 4,12,1973}}
};

                                                                                                                                                             32

<!-- página 33 -->

Exercício: função Preenche a agenda.

void preenche(tPessoa a[], int n) {
 int i;

 for(i=0; i<5; i++) {
      printf("\n%da pessoa\n");
      printf("\nNome? ");
      scanf(" %[^\n],a[i].nome);
      printf("\nFone? ");
      scanf(" %[^\n],a[i].fone);
      printf("\nAniv? ");
      scanf("%d/%d/%d", &a[i].aniv.dia,
                 &a[i].aniv.mes,
                 &a[i].aniv.ano
  }
}

                                                                                                                                                             33

<!-- página 34 -->

Exercício: função Exibe a agenda.

void exibe(tpessoa a[], int n) {
 int i;

 for(i=0; i<n; i++) {
      printf("\n%da pessoa\n");
      printf("\nNome: %s", a[i].nome);
      printf("\nFone: %s", a[i].fone);
      printf("\nAniv: %02d/%02d/%d",  a[i].aniv.dia,
                                      a[i].aniv.mes,
                                      a[i].aniv.ano,
  }
}

                                                                                                                                                             34

<!-- página 35 -->

Exercício: função principal

int main(void) {
  tPessoa a[5];

  preenche(a,5);
  exibe(a,5);
}

                                                                                                                                                             35

<!-- página 36 -->

Vetores de Ponteiros para Estruturas

●Exemplo:
   – tabela com dados de alunos, organizada em um vetor
   – dados de cada aluno:
        matrícula:   número inteiro
      nome:   cadeia com até 80 caracteres
       endereço:   cadeia com até 120 caracteres
        telefone: cadeia com até 20 caracteres

                                                                                                                                                               36

<!-- página 37 -->

Tabela de alunos - Estática

●Solução 1:
   – Aluno
    »  estrutura ocupando 4+81+121+21 = 227 Bytes
   –  tab
    »  vetor de Aluno
    »  representa um desperdício significativo de memória, se o número de alunos bem inferior ao máximo
            estimado
     struct aluno {
        int mat;
        char nome[81];
        char end[121];
        char tel[21];
     };

     typedef struct aluno Aluno;

     #define MAX 100
     Aluno tab[MAX];

                                                                                                                                                               37

<!-- página 38 -->

Tabela de alunos - Dinâmica

●Solução 2 (usada no que se segue):
   –  tab
    »  vetor de ponteiros para Aluno
    »  elemento do vetor ocupa espaço de um ponteiro
    »  alocação dos dados de um aluno no vetor:
          •  nova cópia da estrutura Aluno é alocada dinamicamente
          •  endereço da cópia é armazenada no vetor de ponteiros
     struct»            posiçãoalunovazia do vetor:{     valor é o ponteiro nulo
        int mat;
        char nome[81];
        char end[121];
        char tel[21];
     };
     typedef struct aluno Aluno;
     #define MAX 100
     Aluno* tab[MAX];

                                                                                                                                                               38

<!-- página 39 -->

Inicializa Tabela de alunos - Dinâmica

●Inicializa - função para inicializar a tabela:
   – recebe um vetor de ponteiros
       (parâmetro deve ser do tipo "ponteiro para ponteiro")
   –  atribui NULL a todos os elementos da tabela

     void inicializa (int n, Aluno** tab)
     {
        int i;
        for (i=0; i<n; i++)
           tab[i] = NULL;
     }

                                                                                                                                                               39

<!-- página 40 -->

Inclui Um aluno na tabela  - Dinâmica

●Preenche - função para armazenar novo aluno na tabela:
   – recebe a posição onde os dados serão armazenados
   – dados são fornecidos via teclado
   –  se a posição da tabela estiver vazia, função aloca nova estrutura
   –  caso contrário, função atualiza a estrutura já apontada pelo ponteiro

       void preenche (int n, Aluno** tab, int i)
       {
          if (i<0 || i>=n) {
             printf("Indice fora do limite do vetor\n");
             exit(1);    /* aborta o programa */
          }
          if (tab[i]==NULL) {/* Posição desocupada? */
              tab[i] = (Aluno*)malloc(sizeof(Aluno));
              printf("Entre com a matricula:");
              scanf("%d", &tab[i]->mat);
          ...
       }

                                                                                                                                                               40

<!-- página 41 -->

Remove UM aluno na Tabela- Dinâmica

●Retira - função para remover os dados de um aluno da tabela:
   – recebe a posição da tabela a ser liberada
   –  libera espaço de mémória utilizado para os dados do aluno

 void retira (int n, Aluno** tab, int i)
 {
    if (i<0 || i>=n) {
       printf("Indice fora do limite do vetor\n");
       exit(1);    /* aborta o programa */
    }

    if (tab[i] != NULL)
    {
       free(tab[i]);
       tab[i] = NULL; /* indica que na posição não mais existe dado */
    }
 }

                                                                                                                                                               41

<!-- página 42 -->

Exibe UM aluno da Tabela - Dinâmica

●Imprime - função para imprimir os dados de um aluno da tabela:
   – recebe a posição da tabela a ser impressa

     void imprime (int n, Aluno** tab, int i)
     {
        if (i<0 || i>=n) {
           printf("Indice fora do limite do vetor\n");
           exit(1);    /* aborta o programa */
        }

        if (tab[i] != NULL)
        {
           printf("Matrícula: %d\n", tab[i]->mat);
           printf("Nome: %s\n", tab[i]->nome);
           printf("Endereço: %s\n", tab[i]->end);
           printf("Telefone: %s\n", tab[i]->tel);
        }
     }

                                                                                                                                                               42

<!-- página 43 -->

Exibe Todos alunos da Tabela - Dinâmica

●Imprime_tudo - função para imprimir todos os dados da tabela:
   – recebe o tamanho da tabela e a própria tabela

   void imprime_tudo (int n, Aluno** tab)
   {
      int i;
      for (i=0; i<n; i++)
          imprime(n,tab,i);
   }

                                                                                                                                                               43

<!-- página 44 -->

Teste da Tabela ed Alunos - Dinâmica

●Programa de teste

     #include <stdio.h>

     int main (void)
     {
        Aluno* tab[10];
        inicializa(10,tab);
        preenche(10,tab,0);
        preenche(10,tab,1);
        preenche(10,tab,2);
        imprime_tudo(10,tab);
        retira(10,tab,0);
        retira(10,tab,1);
        retira(10,tab,2);
        return 0;
     }

                                                                                                                                                               44

<!-- página 45 -->

Resumo

struct       struct ponto {  float x; float y; };

typedef      typedef struct ponto Ponto;
          typedef struct ponto *PPonto;

                                                                                                                                                            45
