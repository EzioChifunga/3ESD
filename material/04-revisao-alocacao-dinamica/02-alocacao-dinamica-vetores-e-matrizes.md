# AlocaçãoDinamicadeMemóriaVF

> Fonte: `base/Revisão Alocação Dinâmica de Memória/AlocaçãoDinamicadeMemóriaVF.pdf`

<!-- página 1 -->

Alocação Dinâmica de
     Memória

                                                     1                                                      1

<!-- página 2 -->

Tópicos

●Alocação dinâmica de memória
●Vetores locais e funções
●Vetores alocados dinamicamente
●Matriz alocada dinamicamente

                                                                                                                                                                                                                     2

<!-- página 3 -->

Alocação dinâmica

●Uso da memória:
   – uso de variáveis globais (e estáticas):
    » espaço reservado para uma variável global existe enquanto o programa estiver sendo executado
   – uso de variáveis locais:
    » espaço existe apenas enquanto a função que declarou a variável está sendo executada
    » liberado para outros usos quando a execução da função termina
   – variáveis globais ou locais podem ser simples ou vetores:
    » para vetor, é necessário informar o número máximo de elementos pois o compilador precisa
          calcular o espaço a ser reservado
   – alocação dinâmica:
    » espaço de memória é requisitado em tempo de execução
    » espaço permanece reservado até que seja explicitamente liberado
          • depois de liberado, espaço estará disponibilizado para outros usos e não pode mais ser acessado
          • espaço alocado e não liberado explicitamente, será automaticamente liberado ao final da
           execução

                                                                                                                                                                                                                     3

<!-- página 4 -->

Uso da Memória

                                          Low Address
●  Uso da memória:                                                               Código do programa
    – memória estática:
                                                                         Variáveis globais e
       » código do programa                                                                                                                                                                                                                 memória  estática                                                                         Variáveis estáticas
       » variáveis globais
       » variáveis estáticas                                                                         Variáveis alocadas                                                                         Variáveis alocadas
                                                                  dinamicamente                                                                  dinamicamente
    – memória dinâmica:
       » variáveis alocadas dinamicamente                                                                              heap
       » memória livre                                          Memória livre       » variáveis locais                                                                                                                   memória  dinâmica                           stack

                                                                           Variáveis locais

                                                                              (Pilha de execução)

                                                   High Address

                                                                                                                                                                                                                    4

<!-- página 5 -->

Layout de Memória (segmentos)

                                                                         O tamanho da Stack é fixo (podendo ser
   Higher Address                           v = (int *) malloc(…);                                                                                                                        ajustado como opção do linker). É um
   Full RAM                       w = (int *) malloc(…);                                  segmento de memória eficiente e rápido.
           Command Line Arguments
                                                                         O limite para o Heap é o limite da                        …
                                                                                               memória virtual (que usa disco). Uma
                 Heap             90004 v[1]                                                                                                       máquina 32-bit tem um limite de 232                                            90002 v[0]
                                                                                                          (4GB) para a memória virtual, i.e.:                        …
                                                                                                                 aquela que um processo 32-bit pode      de                                     70004 w[1]
                                                                                                                      endereçar. Na verdade, esse espaço é                                            70000 w[0]                                Dinâmica
                                                                                                                                     dividido igualmente entre o Sistema e a                        …                 Layout                                                                                                              Aplicação do usuário, e.g. 2GB para
                                                                                                        cada um. Numa máquina 64-bit o limite                            Memória                                                                                             de endereçamento é de 8 TB !!!                                                  stack_size reservado
                                            …
                    Stack                Pilha de execução                     1020 w
                                                     Variáveis locais, array names, …                                                                               1016 v = 90002 int u[2];
      de                                                     Variáveis globais, variáveis estáticas      1012 z[1]        int z[2];                                Estática          Data Segments
                                                                               1008 z[0]        int * v;
                Code Segment         Código do programa                   1004 u[1]        int * w;                 Layout
                                                                               1002 u[0]                            Memória         Operational System                        …

            Lower Address
             0                           No exemplo acima, o ponteiro v está no endereço 1016 (no Stack) e contém o endereço 90002 (no Heap) dado pela
                                                 função malloc. A seta verde indica a ordem em que as variáveis foram definidas no código fonte.

As posições do Stack e do Heap (se na parte mais alta ou mais baixa da memória) e as
                                                                    A cada nova execução, os endereços mudam (e ainda bem que édireções de crescimento variam para diferentes máquinas e arquiteturas (mas de
                                                                                              assim, caso contrário um hacker teria mais facilidades de alterar oqualquer maneira sempre colidem). O exemplo acima é o usado pelo Visual Studio 2022,
                                                                                    seu programa)64-bit, modo Release.
                                                                                                                                                                                                                          5

<!-- página 6 -->

Alocação dinâmica

●Funções da biblioteca padrão "stdlib.h"
   – contém uma série de funções pré-definidas:
    » funções para tratar alocação dinâmica de memória
    » constantes pré-definidas
    »  ....

                                                                                                                                                                                                                     6

<!-- página 7 -->

Alocação dinâmica

Requer a biblioteca padrão stdlib (que também contém outros tipos e funções úteis)

  void * malloc(int num_bytes);

    Retorna ponteiro         Aloca área de memória. Por exemplo: malloc(40) aloca 40 bytes
    genérico (requer
    type casting)

  void free(void * p);

                               Libera área de memória.

  void * realloc(void * p, int num_bytes);

                             Realoca espaço.

    Rigorosamente falando, o tipo int especificado acima deve ser o tipo size_t. Isto é: void * malloc(  size_t num_bytes). Dependendo da
    implementação, size_t é unsigned int ou unsigned long.
   OBS: sizeof() retorna size_t. Por exemplo, sizeof(int *) é 4 bytes em máquina 32 bits ( x86) e 8 bytes em máquina 64 bits ( x64). No printf, use %d,
   %zu ou %zd se for x86 e %zd ou %ldd se for x64, sabendo que estes formatos não são padrão; e.g.: printf(  "%zd\n", sizeof(int *));

                                                                                                                                                                                                                       7

<!-- página 8 -->

Alocação dinâmica

 ●Função "malloc":
    – recebe como parâmetro o número de bytes que se deseja alocar
    – retorna um ponteiro genérico para o endereço inicial da área de memória alocada, se houver espaço livre:
     » ponteiro genérico é representado por void*
     » em ANSI C ponteiro é convertido automaticamente para o tipo apropriado
     » ponteiro pode ser convertido explicitamente
    – retorna um endereço nulo, se não houver espaço livre:
     » representado pelo símbolo NULL

 ●Operador "sizeof":
    – retorna o número de bytes ocupado por um tipo

 ●Função "free":
    – recebe como parâmetro o ponteiro da memória a ser liberada
•Após free(p)» seriaa funçãoprudentefreefazerdevep=NULLreceberparaumnãoendereçohaver um usode memóriadescuidadoquedo ptenhaliberadosido(masalocadolembredinamicamenteque isto é interno à função,
  e.g. se f(int * p, int n) faz free(p) e p = NULL não há como a main saber; … a não ser que f(int ** p, int n)).

                                                                                                                                                                                                                       8

<!-- página 9 -->

Alocação dinâmica de um vetor de inteiros

●Exemplo:
   – alocação dinâmica de um vetor de inteiros com 10 elementos
    » malloc retorna o endereço da área alocada para armazenar valores inteiros
    » ponteiro de inteiro recebe endereço inicial do espaço alocado
     int *v;
     v = (int *) malloc(10*sizeof(int));

                                                                                                                                                                                                                     9

<!-- página 10 -->

Alocação dinâmica

●Exemplo (cont.):
   – v armazena endereço inicial de uma área contínua de memória suficiente para armazenar 10 valores
      inteiros
   – v pode ser tratado como um vetor declarado estaticamente
    » v aponta para o inicio da área alocada
    » v[0] acessa o espaço para o primeiro elemento
    » v[1] acessa o segundo
    »  .... até v[9]

                                                                                                                                                                                                                     10

<!-- página 11 -->

Alocação dinâmica – teste obrigatório

– tratamento de erro após chamada a malloc
  » imprime mensagem de erro
  » aborta o programa (com a função exit)

       …
       v = (int*) malloc(10*sizeof(int));
       if (v==NULL){
          printf("Memoria insuficiente.\n");
          exit(1);  /* aborta o programa e retorna 1 para o sist. operacional */
       }
       …

       free(v);

                                                                                                                                                                                                              11

<!-- página 12 -->

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

                                                                                                                                                                                                            12

<!-- página 13 -->

Vetores locais a funções

●Área de memória de uma variável local:
   – só existe enquanto a função que declara a variável estiver sendo executada
   – requer cuidado quando da utilização de vetores locais dentro de funções
●Exemplo:
   – produto vetorial de dois vetores u e v em 3D,
     representados pelas três componentes x, y, e z

                                                                                                                                                                                                                     13

<!-- página 14 -->

Vetores locais a funções

– variável p declarada localmente:
  » área de memória que a variável p ocupa deixa de ser válida quando a função prod_vetorial termina
  » função que chama prod_vetorial não pode acessar a área apontada pelo valor retornado

           float* prod_vetorial (float* u, float* v)
           {
              float p[3];
              p[0] = u[1]*v[2] – v[1]*u[2];
              p[1] = u[2]*v[0] – v[2]*u[0];
              p[2] = u[0]*v[1] – v[0]*u[1];
              return p;
           }

                                                                                                                                                                                                              14

<!-- página 15 -->

Vetores locais a funções

          float* prod_vetorial (float* u, float* v) {
             float *p = (float*) malloc(3*sizeof(float));
             p[0] = u[1]*v[2] – v[1]*u[2];
             p[1] = u[2]*v[0] – v[2]*u[0];
             p[2] = u[0]*v[1] – v[0]*u[1];
             return p;
          }

– variável p alocada dinamicamente
  » área de memória que a variável p ocupa permanece válida mesmo após o término da função
      prod_vetorial
  » função que chama prod_vetorial pode acessar o ponteiro retornado
  » problema - alocação dinâmica para cada chamada da função:
       • ineficiente do ponto de vista computacional
       • requer que a função que chama seja responsável pela liberação do espaço

                                                                                                                                                                                                              15

<!-- página 16 -->

Vetores locais a funções

         void prod_vetorial (float* u, float* v, float* p){
            p[0] = u[1]*v[2] – v[1]*u[2];
            p[1] = u[2]*v[0] – v[2]*u[0];
            p[2] = u[0]*v[1] – v[0]*u[1];
         }

– espaço de memória para o resultado passado pela função que chama:
  » função prod_vetorial recebe três vetores,
       • dois vetores com dados de entrada
       • um vetor para armazenar o resultado
  » solução mais adequada pois não envolve alocação dinâmica

                                                                                                                                                                                                              16

<!-- página 17 -->

Resumo

Funções para gerência de memória:

     sizeof   retorna o número de bytes ocupado por um tipo

    malloc  recebe o número de bytes que se deseja alocar
             retorna um ponteiro para o endereço inicial, ou
             retorna um endereço nulo (NULL)
          realloc: recebe um ponteiro de uma área dinamicamente alocada
           e uma quantidade de bytes,
             retorna um ponteiro para o endereço inicial da nova  área,
       ou  retorna um endereço nulo (NULL)
     free recebe o ponteiro da memória a ser liberada

                                                                                                                                                                                  17

<!-- página 18 -->

Alocação dinâmica de matrizes

Método 1: alocação única
Com a alocação única, os elementos da matriz são alocados em um único vetor, linearmente. Os elementos da matriz são acessados
explicitamente através de aritmética de ponteiros.

                                                                                                         https://www.inf.ufpr.br/roberto/ci067/14_alocmat.html
                                                                                                                                                                                                                      18

<!-- página 19 -->

Alocação dinâmica de matrizes

Método 2: vetor de ponteiros de linhas separadas
Com a alocação de um vetor de ponteiros de linhas separadas, a matriz é vista e alocada como um vetor de ponteiros para linhas, que
são vetores de elementos. O vetor de ponteiros de linhas e os vetores de cada linha devem ser alocados separadamente.
A vantagem deste método é que o acesso aos elementos da matriz usa a mesma sintaxe do acesso a uma matriz estática, o que torna a
programação mais simples.

                                                                                                         https://www.inf.ufpr.br/roberto/ci067/14_alocmat.html
                                                                                                                                                                                                                      19

<!-- página 20 -->

Alocação dinâmica de matrizes

Método 3: vetor de ponteiros de linhas contíguas
Com a alocação de um vetor de ponteiros de linhas contíguas, a matriz é vista e alocada como um vetor de ponteiros para linhas, mas as
linhas são alocadas como um único vetor de elementos. O vetor de ponteiros de linhas e o vetor de elementos são alocados
separadamente.
Além de usar a mesma sintaxe do acesso que o método anterior (mesma que uma matriz estática), este método tem mais duas
vantagens: (i) somente precisa de duas operações de alocação de memória, e (ii) todos os elementos da matriz estão alocados
sequencialmente na memória, o que facilita operações de cópia de matrizes (usando ''memcpy'') ou de leitura/escrita da matriz para um
arquivo (usando ''fread'' ou ''fwrite'').

            https://www.inf.ufpr.br/roberto/ci067/14_alocmat.html
                                                                                                                                                                                                                      20
