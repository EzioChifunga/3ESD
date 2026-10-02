# TAD_FAETERJ

> Fonte: `base/Introdução TAD/TAD_FAETERJ.pdf`

<!-- página 1 -->

Tipos Abstratos de
     Dados

                                                 1                           1

<!-- página 2 -->

Tipos e estruturas de dados

  ●Tipo de dado
   – Conjunto de valores que uma variável pode assumir + operações

            •  Ex: tipo Booleano (verdadeiro ou falso)
            •  Tipos básicos variam de linguagem para linguagem
       ◦Em C: int, char, float, double

       ◦C não tem um tipo “Booleano”

2/13
  ●Estrutura de dado

   –  Coleções de variáveis de tipos possivelmente distintos estruturadas de formas diversas

            •  Ex: tipos estruturados, vetores, listas encadeadas
            •  Mecanismos de estruturação variam de linguagem para linguagem
       ◦Em C: ponteiros, struct, union

            • Uma estrutura de dado é uma representação concreta

                                                                                                                                                                      2

<!-- página 3 -->

Tipos Abstratos de Dados (TADs)

• Um TAD é um conjunto de coisas, que chamaremos itens, e um conjunto operações sobre essas
  coisas.
• As operações são especificadas em uma interface que chamamos API (applications programming
  interface). A interface diz o que cada operação faz, sem dizer como faz o que faz.

• Exemplo: TAD conjunto de inteiros
   •  coisas: valores inteiros
   •  Operacões: criar um conjunto vazio, incluir um valor no conjunto ,excluir um valor no
      conjunto, pertinência de um valor no conjunto, interseção de dois conjuntos, união de dois
      conjuntos, subtração de dois conjuntos, ...

•
• Um TAD pode ser implementado (ou seja, concretamente realizado) de diversas maneiras. (O
  conjunto, por exemplo, pode ser implementado em um vetor ou em uma lista encadeada)

• Um usuário do TAD é chamado cliente. Um cliente só deve usar as operações especificadas na API
 e não deve ter acesso aos detalhes da implementação da TAD.

• Nesse esquema cliente / interface / implementação, a interface funciona como um contrato entre
 o cliente e a implementação.

                                                                Livro de Sedgewick e Wayne
                                                                                                                                                             3

<!-- página 4 -->

Tipo Abstrato de Dados

●Tipo Abstrato de Dados (TAD): Modelo + operações definidas sobre o modelo
   – um TAD define:
    » um novo tipo de dado
    » o conjunto de operações para manipular dados desse tipo
   – um TAD facilita:
    » a manutenção e a reutilização de código
    » abstrato = “forma de implementação não precisa ser
       conhecida”

          •  para utilizar um TAD é necessário conhecer a sua funcionalidade,

            mas não a sua implementação

                                                                                                                                                               4

<!-- página 5 -->

Tipo Abstrato de Dados

●Interface de um TAD:
   – a interface de um TAD define:
    » o nome do tipo
    » os nomes das funções exportadas
          •  os nomes das funções devem ser prefixada pelo nome do tipo, evitando conflitos
            quando tipos distintos são usados em conjunto
          •  exemplo:
                 ListaCria - função para criar uma lista vazia
                ListaInsere1 - função para incluir um novo elemento na1ªpos da lista

   – Operações típicas:
            •  cria
            •  destroi
            •  atribui informação
            •  obtém informação
            •  compara
            •  clona
            •  imprime

                                                                                                                                                               5

<!-- página 6 -->

Tipo Abstrato de Dados

●Implementação de um TAD:
   – o arquivo de implementação de um TAD deve:
    » incluir o arquivo de interface do TAD:
          •  permite utilizar as definições da interface, que são necessárias na implementação
          •  garante que as funções implementadas correspondem às funções da interface
                   –  compilador verifica se os parâmetros das funções implementadas equivalem aos parâmetros dos protótipos
    » incluir as variáveis globais e funções auxiliares:
          • devem ser declaradas como estáticas
          •  visíveis apenas dentro do arquivo de implementação

                                                                                                                                                               6

<!-- página 7 -->

Exemplo: Lista de números inteiros

 ●Operações
    –  Cria Lista Vazia : ListaCria()
    – Insere número no começo da lista: ListaInsere1 ()
    – Remove de uma posição i : ListaRemovei

                                               Programa usuário do TAD: Implementação por Vetores:  20 13 02 30

                                              int main(void) {   void ListaInsere1(int x, Lista* L) {
                                                 Lista*L;      for(i=0;...) {...}
                                                 int x;      L[0] = x;
   }
                                                 x = 20;
Implementação por Listas Encadeadas                  L= ListaCria();
                                                 ListaInsere1(x, L);
             20        13        02        30
                                                 ...
                                              }
  void  ListaInsere1(int x, Lista *L)
  {
      p = CriaNovaCelula(x);
      L->primeiro = p;
      ...
  }
                                                                                                                                                                 7

<!-- página 8 -->

TADs em C

  ●Interface (.h)
   – Declara o TAD
          • Nome do tipo (opaco)

          • Protótipo de cada função exportada

  ●Implementação (.c)

8/13
   – Define o TAD
          • Definição do tipo (struct)

          • Definição das funções exportadas e  auxiliares

                                                                                                                                                                      8

<!-- página 9 -->

Exemplo TAD Ponto

●TAD Ponto
   – tipo de dado para representar um ponto no R2 com as seguintes
     operações:
       cria   cria um ponto com coordenadas x e y
      libera libera a memória alocada por um ponto
      acessaretorna as coordenadas de um ponto
      atribuiatribui novos valores às coordenadas de um ponto
      distancia  calcula a distância entre dois pontos
     compara  compara se dois pontos têm as mesmas coordenadas
      copia cria um novo ponto com as coordenadas do ponto recebido

                                         cria(x,y)→p
                                           destroi(p)
                                               atribui(p,x,y)
                                    obtem(p)→x,y
                                   compara(p,q)→b
                                    copia(p)→q
                                          exibe(p)
                                       distancia(p1,p2)→d
                                   compara(p1,p2)→b

                                                                                                                                                               9

<!-- página 10 -->

Exemplo TAD Ponto

●Interface de Ponto
   – define o nome do tipo e os nomes das funções exportadas
   – a composição da estrutura Ponto não faz parte da interface:
    » não é exportada pelo módulo
    » não é visível para outros módulos

   – os módulos que utilizarem o TAD Ponto:
    » não poderão acessar diretamente os campos da estrutura
       Ponto
    » só terão acesso aos dados obtidos através das funções
       exportadas

                                                                                                                                                               10

<!-- página 11 -->

Exemplo: ponto.h (interface parcial)

   /* TAD: Ponto (x,y) */
   /* Tipo exportado */
   typedef struct ponto Ponto;

   /* Funções exportadas */
   /* Função cria - Aloca e retorna um ponto com coordenadas (x,y) */
   Ponto* pto_cria( float x, float y);

   /* Função libera - Libera a memória de um ponto previamente criado */
   void pto_libera(Ponto* p);

11/1
3  /* Função acessa - Retorna os valores das coordenadas de um ponto */
   void pto_acessa(Ponto* p, float* x, float* y);

   /* Função atribui - Atribui novos valores às coordenadas de um ponto */
   void pto_atribui(Ponto* p, float x, float y);

   /* Função distancia - Retorna a distância entre dois pontos */
   float pto_distancia(Ponto* p1, Ponto* p2);

   /* Função getPonto - Retorna uma string com os valores do ponto
   formato(x,y)*/
   char* pto_getPonto(Ponto* p);

   /* Função exibe -Escreve na tela o ponto no formato  (xxx.xx,yyy.yy)*/
   void pto_exibe(Ponto* p);

                                                                                                                                                                      11

<!-- página 12 -->

Exemplo: ponto.c (implementação)

   /* TAD: Ponto (x,y) */
   /*definição do tipo exportado*/
   struct Ponto {
     double x;
     double y;
   } Ponto;

   /*definição das funções exportadas*/
   /* Função cria - Aloca e retorna um ponto com coordenadas (x,y) */
   Ponto* pto_cria(float x, float y){...}

   /* Função libera - Libera a memória de um ponto previamente criado */
12/1
3  void pto_libera(Ponto* p) {...}

   /* Função acessa - Retorna os valores das coordenadas de um ponto */
   void pto_acessa(Ponto* p, float* x, float* y) {...}

   /* Função atribui - Atribui novos valores às coordenadas de um ponto */
   void pto_atribui(Ponto* p, float x, float y) {...}

   /* Função distancia - Retorna a distância entre dois pontos */
   float pto_distancia(Ponto* p1, Ponto* p2) {...}

   /* Função getPonto - Retorna uma string com os valores do ponto formato(x,y)*/
   char* pto_getPonto(Ponto* p) {...}

   /* Função exibe -Escreve na tela o ponto no formato  (xxx.xx,yyy.yy)*/
   void pto_exibe(Ponto* p) {...}

                                                                                                                                                                      12

<!-- página 13 -->

Exemplo: ponto.c (implementação)

  /* TAD: Ponto (x,y) */
   /*definição do tipo exportado*/
   struct Ponto {
    double x;
    double y;
   } Ponto;

  /*definição das funções exportadas*/
  /* Função cria - Aloca e retorna um ponto com coordenadas (x,y) */
13/1 Ponto* pto_cria(float x, float y){...}
3

  /* Função libera - Libera a memória de um ponto previamente criado */
  void pto_libera(Ponto* p){...};

  /* Função acessa - Retorna os valores das coordenadas de um ponto */
  void pto_acessa(Ponto* p, float* x, float* y){...}

  /* Função atribui - Atribui novos valores às coordenadas de um ponto
  */
  void pto_atribui(Ponto* p, float x, float y){...}

  /* Função distancia - Retorna a distância entre dois pontos */
  float pto_distancia(Ponto* p1, Ponto* p2){...}
                                                                                                                                                                      13

<!-- página 14 -->

Exemplo: ponto.c (implementação)

   #include<stdlib.h>
   #include"ponto.h"
   /*declarações exportadas*/
   /*definição do tipo exportado*/
   struct Ponto {
    double x;
    double y;
   } Ponto;

   /*definição das funções exportadas*/
14/1  Ponto *pto_cria  (double x,doubl ey){...}
3
   void pto_libera   (Ponto  *p) {...}
   void pto_atribui (Ponto *p, double x,double y) {...}
   void pto_atribuiX (Ponto *p, double x) {...}
   void pto_atribuiY (Ponto *p, double y) {...}
   void pto_obtem (Ponto *p1, Ponto  *p2) {...}
   float pto_obtemX  (Ponto  * p, double *x) {...}
   float pto_obtem Y (Ponto  * p, double *y) {...}
   int pto_compara  (Ponto  *p) {...}
   Ponto *pto_copia (Ponto  *p) {...}
   void pto_exibe(Ponto  *p) {...}
   char* pto_getPonto(Ponto  *p) {...}
   double pto_distancia  (Ponto  *, Ponto *) {...}

                                                                                                                                                                      14

<!-- página 15 -->

Exemplo: principal.c - uso do tipo Ponto

#include<stdio.h>
#include"ponto.h“  /*inclui declaraçõesdoTAD*/

int main (void)
{
  Ponto  *p  =  pto_cria (2.0, 1.5);
  Ponto *q = pto_cria (3.4, 2.1);
  float dist = pto_distancia (p, q);
  pto_imprime (p);
  pto_imprime (q);
  printf ("Distancia:  lf\n",  dist);
  pto_libera (p);
  pto_libera (q);
  return 0;
}

                                                                                                         10/1315

<!-- página 16 -->

pto_cria

• Função para criar um ponto dinamicamente:
   –  aloca a estrutura que representa o ponto

   –  inicializa os seus campos

   Ponto* pto_cria (float x, float y)
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

                                                                                                                                                               16

<!-- página 17 -->

pto_libera

• Função para liberar um ponto:
   – deve apenas liberar a estrutura criada dinamicamente através da função
      cria

    void pto_libera (Ponto* p)
    {
       free(p);
    }

                                                                                                                                                               17

<!-- página 18 -->

Pto_atribui e Pto_acessa

• Funções para acessar e atribuir valores às coordenadas de um ponto:
   – permitem que uma função cliente acesse as coordenadas do ponto sem
     conhecer como os valores são armazenados

    void pto_acessa (Ponto* p, float* x, float* y)
    {
       *x = p->x;
       *y = p->y;
    }

    void pto_atribui (Ponto* p, float x, float y)
    {
       p->x = x;
       p->y = y;
    }

                                                                                                                                                               18

<!-- página 19 -->

Pto_distancia

●Função para calcular a distância entre dois pontos

     float pto_distancia (Ponto* p1, Ponto* p2)
     {
        float dx = p2->x – p1->x;
        float dy = p2->y – p1->y;
        return sqrt(dx*dx + dy*dy);
     }

                                                                                                                                                               19

<!-- página 20 -->

Pto_exibe e Pto_getPonto

• Funções para exibir na tela e retornar uma string comr valores das
  coordenadas de um ponto:

   /* Função getPonto - Retorna a string ( x , y )*/
   char* pto_getPonto(Ponto* p1) {
       char* pt;
       int tam = sizeof(*p1)+ 8;
       pt = (char*)malloc(tam);
       sprintf(pt,"( %.2f , %.2f )", p1->x, p1->y);
       return pt;

   }

   /* Função exibe -Escreve na tela o ponto no formato  (xxx.xx,yyy.yy)*/
   void pto_exibe(Ponto* p) {
       printf("\n(%.2f,%.2f)", p->x, p->y);
   }

                                                                                                                                                               20

<!-- página 21 -->

Exercício: TAD Círculo

       •  Representa um círculo no espaço bidimensional
       •  Operações
            ◦  cria(x,y,r)→c

            ◦  destroi(c)

            ◦  compara(c)→b

            ◦  clona(c)→d
                           O tipo Circulo depende do tipo Ponto
            ◦  imprime(c)

21/1
3           ◦  area(c)→a

            ◦  interior(c,p)→b

            ◦  distanciaX(c)   d

            ◦  distanciaY(c)   d

            ◦  atribuiX(c,x)

            ◦  atribuiY(c,x)

            ◦  atribuiRaio(c,r)

            ◦  obtemX( c )   x

            ◦  obtemY( c )   y                                                                                                                      21

<!-- página 22 -->

Tipo Abstrato de Dados

●TAD Circulo
   – tipo de dado para representar um ponto círculo com as seguintes
     operações:
       cria   cria um círculo com centro (x,y)e raio r
      libera libera a memória alocada por um círculo
     area  calcula a área do círculo
      interior    verifica se um dado ponto está dentro do círculo

                                                                                                                                                               22

<!-- página 23 -->

circulo.h - arquivo com a interface do TAD

/* Tipo exportado */                         TAD Círculo usa TAD Ponto
typedef struct circulo Circulo;
                                          interface ponto.h incluída na interface pois a
                                    operação interior faz uso do tipo Ponto
/* Funções exportadas */
/* Função cria - Aloca e retorna um círculo com centro (x,y) e raio r */
Circulo* circ_cria(float x, float y, float r);

/* Função libera - Libera a memória de um círculo previamente criado */
void circ_libera(Circulo* c);

/* Função area - Retorna o valor da área do círculo */
float circ_area(Circulo* c);

/* Função interior - Verifica se um dado ponto p está dentro do círculo */
int circ_interior(Circulo* c, Ponto* p);

/* Função getCirculo - Retorna uma string lo no formato (c:centro,r:raio)*/
char* circ_getCirculo(Circulo* p);

/* Função exibe -Escreve na tela o círculo no formato  (c:centro,r:raio)*/
void circ_exibe(Circulo* p);

/* Função compara - Retorna 1 se o 1º círculo é == ao 2º*/
int circ_compara(Circulo* c1, Circulo* c2);

/* Função clona – cria um novo cículo igual ao recebido*/
Circulo* circ_clona(Circulo* c1);                                                    23

                                                                                                                                                              23

<!-- página 24 -->

Circulo.c

#include "circulo.h"                             char* circ_getCirculo(Circulo* p){
#define PI 3.14159                                  char* pt = (char*)malloc(sizeof(*p) + 8);
#include "ponto.h"                                                 sprintf(pt, "(centro:%s , raio:%.2f )",
                                                    pto_getPonto(p->p), p->r);
struct circulo {                                  return pt;
   Ponto* p;                                  }
   float r;
};                                            void circ_exibe(Circulo* p){
/* Funções exportadas */                          printf("(centro: % s, raio: % .2f)",
Circulo* circ_cria(float x, float y,            pto_getPonto(p->p), p->r);
  float r){                                   }
   Circulo* c =
      (Circulo*)malloc(sizeof(Circulo));
   c->p = pto_cria(x, y);                                              int circ_compara(Circulo* c1, Circulo* c2){
   c->r = r;                                                return(c1->r == c2->r && pto_compara(c1->p,
}                                               c2->p));
void circ_libera(Circulo* c){                 }
  pto_libera(c->p); free(c);
}
float circ_area(Circulo* c){                  Circulo* circ_clona(Circulo* cO){
   return PI * c->r * c->r;                      Circulo* cD =
}                                                   (Circulo*)malloc(sizeof(Circulo));
int circ_interior(Circulo* c, Ponto* p){         cD->p = pto_clona(cO->p);
   float d = pto_distancia(c->p, p);             cD->r = cO->r;
   return (d < c->r);                          return cD;
}                                             }

                                                                                                                                                                 24

<!-- página 25 -->

Usando o Círculo

●Considere o tipo estruturado Circulo, que representa um círculo no plano totalmente
   contido no primeiro quadrante:
●Escreva a função ordenaCirculos que:
   –  recebe um vetor de ponteiros para Circulo e o número de círculos;
   –  ordena o vetor em ordem crescente de distância ao eixo x (ou seja, os círculos mais próximos ao eixo x
      têm que ficar antes dos círculos mais afastados do eixo x). Os círculos igualmente distantes do eixo x
      devem ficar ordenados decrescentemente por área.
●Implemente o método de ordenação de sua escolha: ordenação por seleção (Selection
   Sort) ou bolha (Bubble Sort),etc..
Só para não haver dúvidas: um círculo está totalmente contido no primeiro quadrante
quando todos os pontos do círculo têm coordenada x > 0 e coordenada y > 0.

  O exemplo mostra 5 círculos representados por
    ((x,y),raio): C1((2,6),1), C2((7,4),2), C3((3,2),1),
   C4((7,3),1), C5((3,2),2). A ordenação destes círculos
  de acordo com o critério descrito é a seguinte:
   C5((3,2),2), C3((3,2),1), C2((7,4),2), C4((7,3),1),
   C1((2,6),1). Observe que C5 está mais próximo do
   eixo x do que C3 (veja a distância da borda inferior
  do círculo ao eixo x).

                                                                                                                                                               25

<!-- página 26 -->

Exemplo

●Faça um programa que crie e exiba o carnê de pagamento, isto é,
  os boletos mensais a serem pagos em uma compra parcelada,
  sem entrada.
A data da compra, o valor a ser parcelado e o número (n) de
parcelas será perguntado ao usuário.
Em cada boleto deverá constar o número do boleto (1 a n), a data
do vencimento e o valor do boleto.

●O intervalo entre as parcelas é de 20 dias.

●Os TADs tData e tBoleto são fornecidos.

                                                                                     26

                                                                                                                                                               26

<!-- página 27 -->

Data.h - arquivo com a interface de Data

typedef struct data tData;

/* Funções exportadas */
/* Função cria - Aloca e retorna uma data dia/mês/ano */
tData* dta_cria(int dia, int mes, int ano);

/* Função libera – Libera  memória de uma Data criada */
void dta_libera(tData* p);

/* Função acessa - Retorna os valores de uma Data */
void dta_acessa(tData* p, int* dia, int* mes, int* ano);

/* Função atribui - Atribui novos valores à Data */
void dta_atribui(tData* p, int dia, int mes, int ano);

/* Função intervalo - Retorna a data após X dias */
tData* dta_intervalo( tData* p1, int X);

/* Função exibe – Escreve na tela a data  no formato dd/mm/aaaa */
tData* dta_exibe(tData* p1);

/* Função getData - Retorna uma string com a data no formato dd/mm/aaaa*/
char* dta_getData( tData* p);

                                                                                                                                                               27

<!-- página 28 -->

boleto.h - arquivo com a interface de Data

#include "Data.h"
/* TAD: Folha Boleto (nº, valor, data de Vencimento) */
/* Tipo exportado */
typedef struct boleto tBoleto;

/* Funções exportadas */
/* Função cria - Aloca e retorna um boleto*/
tBoleto* bol_cria(int n, float valor, tData* dtVenc);

/* Função libera – Libera  memória de um Boleto criado */
void bol_libera(tBoleto* p);

/* Função acessa - Retorna os valores de um Boleto */
void bol_acessa(tBoleto* p, int* n, float* val, tData* dtVenc);

/* Função atribui - Atribui novos valores ao Boleto */
void bol_atribui(tBoleto* p, int n, float val, tData* dtVenc);

/* Função Exibe – Mostra os dados do Boleto na tela */
void bol_exibe(tBoleto* p1);

/* Função getBoleto - Retorna uma string com os valores do boleto */
char* bol_getBoleto( tBoleto* p);

                                                                                                                                                               28

<!-- página 29 -->

Desenvolvendo a Solução

Obter data da compra, valor total e nº de parcelas
parcela = valor total/ nº de parcelas
x = 1
Enquanto x < nº de parcelas
 Cria boleto com
         Nº boleto = x
          Data do Vencimento = data da compra + 20* nº boleto
           Valor do Pagamento = parcela
 Exibir dados do boleto

                                                                                                                                                          29

<!-- página 30 -->

Principal.c

#include <stdio.h>
#include "boleto.h“
#include "Data.h"

int main(void) {

    tData* dtCompra;
    int dia=1, mes=9, ano=2021, n=4, i;
    float valor=1000, parcela;
    tBoleto* boleto;
    //Ler(&dia, &mes, &ano, &valor, &n);
    parcela = valor / n;
    dtCompra = dta_cria(dia, mes, ano);
    for (i = 0; i < n; i++) {
       boleto = bol_cria(i + 1, parcela, dta_intervalo(dtCompra,
                                                        i * 20));
        bol_exibe(boleto);
        bol_libera(boleto);
        printf("\n");
    };
return 0;
}

                                                                                                                                                              30

<!-- página 31 -->

Tipos Abstratos de Dados (TADs)

Dessa forma, o usuário pode abstrair da implementação específica.

Qualquer modificação nessa implementação fica restrita ao TAD

A escolha de uma representação específica é fortemente influenciada pelas
  operações a serem executadas

                                                                                                                                                          31

<!-- página 32 -->

Exemplo

Implemente um TAD ContaBancaria, com os campos número e saldo onde os clientes
  podem fazer as seguintes operações:
  –  Iniciar uma conta com um número e saldo inicial
  – Depositar um valor
  – Sacar um valor
  – Imprimir o saldo

Faça um pequeno programa para testar o seu TAD

                                                                                                                                                             32

<!-- página 33 -->

ContaBancaria.h

/* Tipo exportado */
typedef struct ctaBancaria ContaBancaria;

/* Funções exportadas */
/* Função Inicializa – preenche os dados de uma cta */
void Inicializa (ContaBancaria*cta, int num, float val);

/* Função Depósito – Realiza um depósito em uma cta*/
void Deposito   (ContaBancaria*cta, float val);

/* Função Saque – Realiza um saque em uma cta*/
void Saque      (ContaBancaria*cta, float val);

/* Função Imprime – Exibe os dados de uma cta*/
void Imprime    (ContaBancaria cta);

                                                                                                                                                               33

<!-- página 34 -->

#include<stdio.h>             ContaBancaria.cpp
#include"Contabancaria.h“
struct ctaBancaria{
   int numero;
   float saldo;
};

void Inicializa(ContaBancaria *conta, int numero, float saldo) {
     conta->numero = numero;
     conta->saldo = saldo;
}

void Deposito (ContaBancaria *conta, float valor) {
     conta->saldo += valor;
}

void Saque (ContaBancaria *conta, float valor) {
     conta->saldo -= valor;
}

void Imprime (ContaBancaria conta) {
     printf("Numero: %d\n", conta.numero);
     printf("Saldo: %f\n", conta.saldo);
}

                                                                                                                                                                   34

<!-- página 35 -->

main.cpp

#include<stdio.h>
#include<stdlib.h>
#include "ContaBancaria.h"

int main (void)
{
    ContaBancaria conta1;
    Inicializa(&conta1, 918556, 300.00);
    printf("\nAntes da movimentacao:\n ");
    Imprime(conta1);
    Deposito(&conta1, 50.00);
    Saque(&conta1, 70.00);
    printf("\nDepois da movimentacao:\n ");
    Imprime (conta1);

    system("PAUSE");
    return(0);
}

                                                                                                                                                               35

<!-- página 36 -->

Tipos Abstratos de Dados – Exemplo
                       TAD Racional

     Um número racional positivo é o que pode ser

        expresso como quociente de dois inteiros positivos:
          a
         b

Prof. Pedro Luís Antonelli Anhanguera Educacional

                                                                                                                                                                    36

<!-- página 37 -->

Tipos Abstratos de Dados - Exemplo

Operações válidas para o TAD Racional
Positivo:
                     0 ∗ 1    +(1 ∗0)
Soma: 0    1       +   =        (0 ∗1)
        0    1

                          0 ∗ 1 − (1 ∗0)
Subtração: 0     1           −   =       (0 ∗1)
             0   1

                                                                                                                                              37

<!-- página 38 -->

Tipos Abstratos de Dados - Exemplo

Operações válidas para o TAD
Racional:
Multiplicação: 0  ∗                    1 =0 ∗1
                1   1   (1 ∗1)

Divisão: 2   ÷  1          4       3 =2*3(4 * 1)

                                                                                                                                              38

<!-- página 39 -->

// Interface do TAD
// definição do tipo racional TAD
typedef struct rac  Racional*;                      TAD
// funções exportadas
                                                Racional-
// Lê um número racional                                         Exemplo
void rac_le (Racional* r);

// cria um número racional
Racional* rac_cria (int x, int y);
                                                      “racional.h”
// soma dois racionais
Racional* rac_add(Racional* x, Racional* y);

// subtrai dois racionais
Racional* rac_sub(Racional* x, Racional* y);

// Multiplica dois racionais
Racional* rac_mul(Racional* x, Racional* y);

// divide dois racionais
Racional* rac_divide(Racional* x, Racional* y);

//retorna uma string que representa o racional
char * rac_exibe(Racional* x);

//verifica se dois numeros racionais são iguais
int  rac_iguais(Racional* x, Racional* y);                                                                                        39

<!-- página 40 -->

racional.c

#include <string.h>
#include <stdlib.h>
#include "racional.h"
struct rac   {
     int a;
     int b;
}
// Protótipos das funções auxiliares
// obtém o numerador do racional     int getN(Racional* x);
// obtém o denominador do racional   int getD(Racional* x);

// Implementação das funções
Racional * rac_cria( int x, int y){
   Racional* r = malloc(sizeof(Racional);
   r->a = x;
    r->b = y;
    return r;
}

                                                                                                                                                            40

<!-- página 41 -->

racional.c

int getN(Racional* x){ return x->a;}
int getD(Racional* x){ return x->b;}

Racional* rac_sub(Racional* x, Racional* y){
     Racional* r=rac_cria(0,0);
     r->a = ((x.a*y.b) - (y.a*x.b));
     r->b = x.b * y.b;
     return(r);
}

Racional* rac_add(Racional* x, Racional* y){
     float a,b;
    a = ((x->a*y->b) + (y->a*x->b));

    b = x->b * y->b;
     return(rac_cria(a,b));
}

                                                                                                                                                            41

<!-- página 42 -->

racional.c cont.

Racional* rac_mul(Racional* x, Racional* y){
     return (rac_cria(x->a * y->a, x->b * y->b));
}
                                                 Para   o  TAD   ser
Racional* rac_divide(Racional* x, Racional* y){
                                                   independente      da
     return (rac_cria(x->a * y->b, x->b * y->a));
                                                     representação interna,
}
                                              o  desenvolvedor  do
char * rac_exibe(Racional* x){
                                       TAD  deveria sempre
     static char s[30];
                                                    acessar os membros
      sprintf(s, "%i / %i", getN(x), getD(x));
                                              do TAD via as funções
     return s;
                                                 getN e getD
 }
void rac_le(racional *r){
      int num,den;
     printf(“\n Forneça a fração comprada: nº/nº”);
     scanf(“%i/%i”,&num;&den);
     r->a = num;
     r->b=den;
     return;
 }

                                                                                                                                                             42

<!-- página 43 -->

Programa ex que usa oTAD Racional

#include <stdio.h>
#include <stdlib.h>
#include "racional.h"
int main(void)
{

      Racional* r1 = rac_cria(4,3);
      Racional* r2 = rac_ cria(2,1);

      Racional* resultado_Add = rac_ add(r1,r2);
      Racional* resultado_Sub = rac_ sub(r1,r2);
      Racional* resultado_Mul = rac_ mul(r1,r2);
      Racional* resultado_Divide = rac_ divide(r1,r2);

       printf("\n A soma    e : %s", rac_exibe(resultado_Add));
       printf("\n A diferenca e : %s      ", rac_exibe(resultado_Sub));
       printf("\n O produto e : %s      ", rac_exibe(resultado_Mul));
       printf("\n O quociente e : %s     ", rac_exibe(resultado_Divide));
      return 0;
}

                                                                                                                                                              43

<!-- página 44 -->

Mostrando o
resultado:

                                                                                                                                                          44

<!-- página 45 -->

Exercício

A Cacaunhagen vende frações de suas barras de chocolate. Como  as barras são
  divididas de variadas formas, precisa de uma calculadora especial capaz de
  somar, subtrair, multiplicar e dividir quantidades racionais.
Faça um programa em C que leia  os preços das barras e as quantidades compradas
  e mostre o total a pagar;
  Término: preço negativo;

                                         (c) Dept. Informática - PUC-Rio                          45

                                                                                                                                                             45

<!-- página 46 -->

Exercício: TAD Conjuntos (SET)

  Um conjunto é uma coleção de elementos
  Todos os elementos são diferentes entre si e a ordem entre eles é
    irrelevante
  Ex:
    {1,4} = {4, 1} ok
        {1,4,1} não ok

 Operações básicas:
      cria_conj_vazio, união, intersecção , diferença , insere, remove, membro

❖  Se A e B são conjuntos então A Ụ B é o conjunto de elementos que são membros
     de A ou B ou ambos
❖  Se A e B são conjuntos então A ∩ B é o conjuntos de elementos que estão em A e
    B
❖  Se A e B são conjuntos então A - B é o conjunto de elementos em A que não estão
    em B.
   A = {a,b,c} B = {b,d}   A Ụ B = {a,b,c,d}   A ∩ B = {b}   A - B = {a,c}

                                                                                                                                                               46

<!-- página 47 -->

Operações usuais

 União(A,B,C)
 Intersecção(A,B,C)
 Diferença(A,B,C)
 Membro(x,A)
 Cria_Conj_Vazio(A)
 Insere(x,A)
 Remove(x,A)
 Atribui(A,B)
 Min(A)
 Max(A)
 Igual(A,B)

                                                                                                                                                         47

<!-- página 48 -->

Definição das operações
  – União(A,B,C): toma os argumentos A e B que são conjuntos e retorna A U B à variavel
    C
  – Intersecção(A,B,C): toma os argumentos A e B que são conjuntos e retorna A ∩ B à
     variavel C
  – Diferença(A,B,C): toma os argumentos A e B que são conjuntos e retorna A - B à
     variavel C
  – Membro(x,A): toma o conjunto A e o objeto x cujo tipo é o tipo do elemento de A e
     retorna um valor booleano - true se x Є A e false cc.
  – Cria_Conj_Vazio(A): cria um conjunto vazio na variável A
  –  Insere(x,A): toma o conjunto A e o objeto x cujo tipo é o tipo do elemento de A e faz
     x um membro de A. Se x já é um membro de A, então a operação insere não muda A.
  – Remove(x,A): remove o objeto x, cujo tipo é o tipo do elemento de A, de A. O novo
     valor de A = A - {x}. Se x não pertence a A então a operação remove não altera A.
  –  Atribui(A,B): Seta o valor da variável conjunto A = ao valor da variável conjunto B.
  – Min(A): retorna o valor mínimo no conjunto A. Esta operação pode ser aplicada
    somente quando os membros do parâmetro A são linearmente ordenados. Por
     exemplo: Min({2,3,1}) = 1 e Min({'a','b','c'}) = 'a'.
  – Max(A): Similar a Min(A) só que retorna o máximo do conjunto.
  –  Igual(A,B): retorna true se e somente se os conjuntos A e B consistem dos mesmos
     elementos

                                                                                                                                                                   48

<!-- página 49 -->

Resumo

Módulo      arquivo com funções que representam apenas parte da
    implementação de um programa completo

Arquivo objeto    resultado de compilar um módulo
        geralmente com extensão .o ou .obj

Interface de módulo arquivo contendo apenas
        os protótipos das funções oferecidas pelo módulo, e
               os tipos de dados exportados pelo módulo

TAD         define um novo tipo de dado e
       o conjunto de operações para manipular dados do tipo

Interface de TAD define o nome do tipo e das funções exportadas

                                                                                                                                                        49

<!-- página 50 -->

Tipos Abstratos de Dados

Agrupa a estrutura de dados juntamente
com as operações que podem ser feitas
sobre esses dados.

A escolha de uma representação específica
é fortemente influenciada pelas operações
a serem executadas.

                                                                                                                                              50

<!-- página 51 -->

Tipos Abstratos de Dados

Usuário do TAD x Programador do TAD.

Usuário só “enxerga” a interface, não a
implementação.

Dessa forma, o usuário pode abstrair
da
implementação específica.

                                                                                                                                              51
