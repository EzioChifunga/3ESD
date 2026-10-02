# RevisãoAlocaçãodeMemória

> Fonte: `base/Revisão Alocação Dinâmica de Memória/RevisãoAlocaçãodeMemória.pdf`

<!-- página 1 -->

Revisão
Alocação Dinâmica de
     Memória

                                                     1                           1

<!-- página 2 -->

Tópicos

●Alocação dinâmica de memória
●Vetores locais e funções

                                                                                                                                                               2

<!-- página 3 -->

Alocação dinâmica

●Uso da memória:
   – uso de variáveis globais (e estáticas):
    » espaço reservado para uma variável global existe
      enquanto o programa estiver sendo executado
   – uso de variáveis locais:
    » espaço existe apenas enquanto a função que declarou a
        variável está sendo executada
    » liberado para outros usos quando a execução da função
       termina
   – variáveis globais ou locais podem ser simples ou vetores:
    » para vetor, é necessário informar o número máximo de
       elementos pois o compilador precisa calcular o espaço a ser
       reservado

                                                                                                                                                               3

<!-- página 4 -->

Alocação dinâmica

●Uso da memória:
   – alocação dinâmica:
    » espaço de memória é requisitado em tempo de execução
    » espaço permanece reservado até que seja explicitamente
        liberado
          •  depois de liberado, espaço estará disponibilizado para outros usos e
             não pode mais ser acessado
          •  espaço alocado e não liberado explicitamente,
               será automaticamente liberado ao final da execução

                                                                                                                                                               4

<!-- página 5 -->

Alocação dinâmica

●  Uso da memória:                           Código do programa
    – memória estática:                                                      Variáveis globais e
        » código do programa                                                                                                                                                             memória  estática                                                      Variáveis estáticas        » variáveis globais
        » variáveis estáticas                           Variáveis alocadas
    – memória dinâmica:                                                 dinamicamente
        » variáveis alocadas
           dinamicamente
        » memória livre                                               Memória livre
        » variáveis locais                                                                                                                                                             memória  dinâmica

                                                        Variáveis locais

                                                         (Pilha de execução)

                                                                                                                                                              5

<!-- página 6 -->

Alocação dinâmica

Uso da memória:
    alocação dinâmica de memória:
       usa a memória livre
        se o espaço de memória livre for
        menor que o espaço requisitado,                Código do programa
         a alocação não é feita e
                                                      Variáveis globais e        o programa pode prever
         tratamento de erro                                                                                                                                                           memória  estática                                                      Variáveis estáticas
    pilha de execução:
         utilizada para alocar memória quando                 Variáveis alocadas
         ocorre chamada de função:
                                                 dinamicamente           sistema reserva o espaço
             para as variáveis locais da
             função
          quando a função termina,                     Memória livre
            espaço é liberado                                                                                                                                                                                           dinâmica                                                                                                                                                           memória             (desempilhado)
        se a pilha tentar crescer mais do que o
                                                       Variáveis locais         espaço disponível existente,
        programa é abortado com erro                                                         (Pilha de execução)

                                                                                                                                                             6

<!-- página 7 -->

Alocação dinâmica

●Funções da biblioteca padrão "stdlib.h"
   – contém uma série de funções pré-definidas:
    » funções para tratar alocação dinâmica de memória
    » constantes pré-definidas
    »  ....

                                                                                                                                                               7

<!-- página 8 -->

Alocação dinâmica

void * malloc(int num_bytes);

void free(void * p);

                                                                                                                                                                8

<!-- página 9 -->

Alocação dinâmica

●Função "malloc":
   – recebe como parâmetro o número de bytes que se deseja alocar
   – retorna um ponteiro genérico para o endereço inicial da área de memória alocada,
      se houver espaço livre:
    » ponteiro genérico é representado por void*
    » ponteiro é convertido automaticamente para o tipo apropriado
    » ponteiro pode ser convertido explicitamente
   – retorna um endereço nulo, se não houver espaço livre:
    » representado pelo símbolo NULL
●Operador "sizeof":
   – retorna o número de bytes ocupado por um tipo
●Função "free":
   – recebe como parâmetro o ponteiro da memória a ser liberada
    » a função free deve receber um endereço de memória que tenha sido alocado
          dinamicamente

                                                                                                                                                               9

<!-- página 10 -->

Alocação dinâmica

●Exemplo:
   – alocação dinâmica de um vetor de inteiros com 10 elementos
    » malloc retorna o endereço da área alocada para
       armazenar valores inteiros
    » ponteiro de inteiro recebe endereço inicial do espaço
       alocado

      int *v;
      v = (int *) malloc(10*sizeof(int));

                                                                                                                                                               10

<!-- página 11 -->

Alocação dinâmica

●Exemplo (cont.):

     v = (int *) malloc(10*sizeof(int));

                                                                                                                                                               11

<!-- página 12 -->

Alocação dinâmica

●Exemplo (cont.):
   – v armazena endereço inicial de uma área contínua de memória suficiente
     para armazenar 10 valores inteiros
   – v pode ser tratado como um vetor declarado estaticamente
    » v aponta para o inicio da área alocada
    » v[0] acessa o espaço para o primeiro elemento
    » v[1] acessa o segundo
    »  .... até v[9]

                                                                                                                                                               12

<!-- página 13 -->

Alocação dinâmica

     Exemplo (cont.):
      – tratamento de erro após chamada a malloc
       » imprime mensagem de erro
       » aborta o programa (com a função exit)

…
v = (int*) malloc(10*sizeof(int));
if (v==NULL)
{
   printf("Memoria insuficiente.\n");
   exit(1);  /* aborta o programa e retorna 1 para o sist. operacional */
}
…

free(v);

                                                                                                                                                                13

<!-- página 14 -->

Alocação dinâmica

#include <stdlib.h>
int main ( void ) {
   float *v;
   float med, var;
   int i,n;

   printf("Entre n e depois os valores\n");
   scanf("%d",&n);
   v = (float *) malloc(n*sizeof(float));
   if (v==NULL) { printf("Falta memoria\n"); exit(1); }

   for ( i = 0; i < n; i++ )
      scanf("%f", &v[i]);

   med = media(n,v);
   var = variancia(n,v,med);
   printf ( "Media = %f   Variancia = %f  \n", med, var);
   free(v);
   return 0;
}

                                                                                                                                                                14

<!-- página 15 -->

Vetores locais a funções

Área de memória de uma variável local:
  – só existe enquanto a função que declara a variável
     estiver sendo executada
  – requer cuidado quando da utilização de vetores locais
    dentro de funções
Exemplo:
  – produto vetorial de dois vetores u e v em 3D,
    representados pelas três componentes x, y, e z

                                                                                                                                                      15

<!-- página 16 -->

Vetores locais a funções

float* prod_vetorial (float* u, float* v)
{
   float p[3];
   p[0] = u[1]*v[2] – v[1]*u[2];
   p[1] = u[2]*v[0] – v[2]*u[0];
   p[2] = u[0]*v[1] – v[0]*u[1];
   return p;
}

– variável p declarada localmente:
  » área de memória que a variável p ocupa deixa de ser
     válida
    quando a função prod_vetorial termina
  » função que chama prod_vetorial não pode acessar a                  16
    área apontada pelo valor retornado

<!-- página 17 -->

Vetores locais a funções

 float* prod_vetorial (float* u, float* v) {
    float *p = (float*) malloc(3*sizeof(float));
    p[0] = u[1]*v[2] – v[1]*u[2];
    p[1] = u[2]*v[0] – v[2]*u[0];
    p[2] = u[0]*v[1] – v[0]*u[1];
    return p;
 }

– variável p alocada dinamicamente
  » área de memória que a variável p ocupa permanece válida mesmo após
     o término da função prod_vetorial
  » função que chama prod_vetorial pode acessar o ponteiro retornado
  » problema - alocação dinâmica para cada chamada da função:
       • ineficiente do ponto de vista computacional
       • requer que a função que chama seja responsável pela liberação do
        espaço

                                                                                                                                                         17

<!-- página 18 -->

Vetores locais a funções

void prod_vetorial (float* u, float* v, float* p){
   p[0] = u[1]*v[2] – v[1]*u[2];
   p[1] = u[2]*v[0] – v[2]*u[0];
   p[2] = u[0]*v[1] – v[0]*u[1];
}

– espaço de memória para o resultado
  passado pela função que chama:
  » função prod_vetorial recebe três vetores,
       • dois vetores com dados de entrada
       • um vetor para armazenar o resultado
  » solução mais adequada pois não envolve alocação dinâmica

                                                                                                                                                         18

<!-- página 19 -->

Resumo

Funções para gerência de memória:

     sizeof   retorna o número de bytes ocupado por um tipo

    malloc  recebe o número de bytes que se deseja alocar
             retorna um ponteiro para o endereço inicial, ou
             retorna um endereço nulo (NULL)

     free recebe o ponteiro da memória a ser liberada

                                                                                                                                                        19
