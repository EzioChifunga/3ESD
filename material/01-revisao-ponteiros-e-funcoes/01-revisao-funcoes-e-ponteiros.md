# RevisãoCompactaFuncoesPonteiro

> Fonte: `base/Revisão de Ponteiros/RevisãoCompactaFuncoesPonteiro.pdf`

<!-- página 1 -->

Revisão

                                   1                           1

<!-- página 2 -->

Programa

 Um algoritmo computacional é uma sequência de instruções que
  opera sobre um conjunto de entradas (valores) de modo a gerar
 um conjunto de saída(valores) que seja útil para o usuário

Entrad              Programa                       Saíd
  a                                               a

                           Variávei
                                s

                                                                                                                           2

<!-- página 3 -->

Estrutura de um programa em C

    /* programa para calcular a f1
                                                                                 1.   diretivas de pré-processador onde
         Autor : Joao Silva
                                                             se definem o valor de constantes         Ultima Atualização: 2/2/1900
                                                                   simbólicas, inclusão de bibliotecas,    */
                                                            declaração de rotinas, etc.    #include ...1                                                                                 2.  Um bloco de instruções principal e    #include <locale.h>
    #define  .....                                      outros blocos de rotinas.
    int f1( int a); //função que calcula o f1

    int main (void)
    {
     -------    área de declaração de variáveis da função main
      setlocale(LC_ALL,"portuguese");
      -------     área de comandos da função main
      return 0;
    }                                                                                      {   início de bloco
    int   f1 (int a)                                                                                               }  fim de bloco2    {
                                                                                                            ;   fim de instrução     -------    área de declaração de variáveis
      -------     área de comandos                           /* estes são delimitadores de
     return ...;                                                                                  início e fim de comentário */
    }
                                                                     // comenta 1 linha

                                                                                                                                                                     3

<!-- página 4 -->

Diretivas de pré-processamento

1.   diretivas de pré-processador onde se definem o valor de constantes simbólicas, inclusão de
     bibliotecas, declaração de protótipos,etc

     Comando                   Objetivo                               Sintaxe

  Inclusão de funções built-in   Inclusão de funções do C        #include <nomedabiblioteca.h>

  Declaração de constante     Declaração de constante        #define NOMECONSTANTE valor

                             Declaração de uma função. Sua
  Declaração de protótipos     definição (seu código ) está     nomefunção(parâmetros);
                            após a main()

                                                                                                                                                                  4

<!-- página 5 -->

Tipos de dados primitivos

Tipos Primitivos     Tamanho     Valores válidos

char               1 byte        -128 a 127

unsigned char       1 byte       0 a 255

short int           2 bytes        -32.768 a 32.767

unsigned short int    2 bytes       0 a 65.535

long int            4 bytes        -2.147.483.648 a 2.147.483.647

unsigned long int    4 bytes       0 a 4.294.967.295

float               4 bytes       10-38 a 1038

double             8 bytes       10-308 a 10308

                                                                                                                                                        5

<!-- página 6 -->

Declaração de variáveis

                                         tipo nome_variavel;
Declaração          Variável simples
                                                Ex: int idade;

Declaração c/       Variável simples         tipo nome_variavel = valor_inicial;
Inicialização       com valor              Ex: int idade = 18;

                                         tipo nome_variavel [tamanho];                     Variáveis
Vetores
                   unidimensionais         Ex: int v[10]; //Vetor de inteiros

                                                 //v[0] é o primeiro elemento e v[9] o último
                     Variáveis
Vetores                                   tipo nome_variavel [tamanho]= {v1, v2, ...};                   unidimensionais
Inicializados                                           Ex: char vogais[5] = {' a',' e', 'i', 'o', 'u'};               com valor

                                         tipo nome_variavel [qt de lin][qt de col];
Matrizes            Variáveis Bidimensionais
                                           Ex: float mat[4][3]; //Matriz de reais 4linx3col

                                         tipo nome_variavel [qtlin][qtcol]={ {...},...,{..}};Matrizes            Variáveis Bidimensionais
inicializadas      Com valor              Ex: int mat[2][3] = {1,2,3},{4,5,6}};

                                                                                                                                                                  6

<!-- página 7 -->

Operadores

   Tipo     Operador       Objetivo                 Exemplo
                   +               Adição        a = 4 + 1;    // 5
 Aritméticos              -             Subtração       a = 4 – 1;    // 3
                   *             Multiplicação      a = 2 * 4;    // 8
                        /                Divisão        a = 8 / 2;    // 4
             %           resto da divisão)     a = 5 % 2;     // 1
 Atribuição         =          Atribuição simples    a = 50;       // 50
                  +=         Soma e altera      a+=40;        //90
 Atribuição           -=          Diminui e altera     a-=10         //80
 Composta         *=          Multiplica e altera    a*=2          //160
                     /=            Divide e altera      a/=10         //16
               %=           Resto e altera      a%=10         //6
Incremento e       ++           Incremento       a++;          //7
Decremento             --           Decremento      a--;          //6

                                                                                                                                                               7

<!-- página 8 -->

Operadores

     Tipo       Operador             Objetivo                    Exemplo
             &&      "e" lógico              (a > 1) && (b < 1)
    Lógicos
                   ||      "ou" lógico             (a > 1) || (b < 1)
                            !      não (inversão)           !(a > 2)
                  ==       igual a                (a == 0)
  Relacionais         !=      diferente de            (a != 0)
(Comparação)
                  <     menor que             (a < 0)
                  >                         (a > 0) "                          maior que
                  <=     menor ou igual a         (a <= 0)
                  >=     maior ou igual a         (a >= 0)

                                                                    //var. inteira             &      Retorna o "endereço de"   int a;
  Referência                                int *p;    //declaração de ponteiro
(Apontadores)                               p = &a;    //atribui o endereço de a
antes do nome                               *p = 2;    //atribui  2 ao local
 de variáveis       *      Retorna o "conteúdo de"                  //apontado por p;
                                                             //como p aponta para o
                                                              //endereçode a, então a recebe 2.

                                                                                                                                                                  8

<!-- página 9 -->

Entrada e Saída

                                    Saída     Entrada

  scanf(formato, lista de endereços)       printf(formato,  argumentos);

  Função para capturar e armazenar         Função para saída de valores
  valores fornecidos via teclado.          segundo
                                          um determinado formato.
Ex.:
scanf("%d",&n);                           Ex.:
scanf("%d %f %c",&h,&m,&l);               printf("Inteiro = %i Real = %f\n",
                                            33, 5.3);
                            %c       char
                          %d ou %i  int
                          %u       unsigned int
                                %f       double ou float
                           %e      double ou float (científico)
                             %s       cadeia de caracteres
                                   \n       quebra de linha
                                             \t        tabulação

                                         \"        caractere "

                                          \\        caractere \

                                                                                                                                                                9

<!-- página 10 -->

Tomada de Decisão

  Comando             Objetivo           Exemplo

                                  i=7;
if (cond)        Seleção        j=8;
{                                x=14
                                  if ( i > 0 )
     ....                                         y = x / i;
};                               if ( j < 8 )
                                      z = y;
                                      y = x / i;

                                    if ( j < 8 ){
                                         z = y
                                         y = x / i;
                                    }

                                                                                                                                                  10

<!-- página 11 -->

Tomada de Decisão

  Comando              Objetivo                       Exemplo

                  Escolha entre  if ( i > 0 )
                                      y = x / i;if (cond)                    alternativas                              else {
{                                     x = i;
                                      y = f( x );      ....
                              }
}
                              if ( i > 0 )     /* sem chaves */else
                                   if ( j > i )
{                                      x = j;
                                   else       ....
                                       x = i;
};

                              if ( i > 0 ) {   /* com chaves */
                                   if ( j > i )
                                   x = j;
                              }
                              else
                                  x = i;

                                                                                                                                                         11

<!-- página 12 -->

Repetição

  Comando                            Objetivo                      Sintaxe

                                         int n ;
while (cond)                  Repetição
                                         scanf("%i",&n);
{                                  indeterminada
                                         while (n != 10)     ....
                                         {}
                                            printf("%i",n);

                                            scanf("%i",&n);

                                         }

                                      Repetição
for(inic;teste;incr)    determinada    for (n=0;n<10;n++)
{                                        {
       ....                                printf("\n%i", n);
}                                         }

                                                                                                                                                             12

<!-- página 13 -->

Sub Rotinas

            float calcArea(float altura, float base)
            {
Funções              return altura * base;
            }

            void exibeArea(float altura, float base)
            {
Procedimentos
               printf("A area é: %f", altura * base);
            }

                                                                                                                                                             13

<!-- página 14 -->

Exemplo 1

Construa um programa que tendo como entrada três pontos quaisquer do plano que formam um triângulo
(ou seja, as coordenadas x e y do ponto A ,do ponto B e do ponto C) calcule e mostre o perímetro deste
triângulo calculado por:
                ( d(A,B) + d(A,C) + d(B,C)

A fórmula da distância euclidiana é:
            d² = (Xa-Xb)² + (Ya-Yb)².

A saída deve ter o seguinte formato:
               =================================
               =================================
                 Perimetro: xxx.xx
               =================================
               =================================

                                                                                                                                                                 14

<!-- página 15 -->

Analisando a Solução

                                                       Na solução do problema há uma série de
                                                                            tarefas  mais  simples    que  ao  serem
                                   Algoritmo:                                                                   sequencialmente executadas, o resolvem.
Entrada:                        Entrada de dados do ponto A
                                                      A solução de cada tarefa é descrita por uma
    ptos A, B, C
                                                                    sequência de instruções
                        Entrada de dados do ponto B
Saída:
    ============       Entrada de dados do ponto C                   Estratégia de solução para
    ============                                                                 evitar esta redundância
   Perímetro                        Calcular d(A,B)
    ============                                                  Cada tarefa é programada
                          Calcular       Calcular d(A,C)           em um FUNÇÃO (rotina).    ============
                         perímetro
                                         Calcular d(B,C)

                             Exibir moldura
                                                    O programa ativa as rotinas
                                                                               desejadas para resolver o
                             Exibir perímetro
                                                                              problema

                             Exibir moldura

                                                                                                                                                                   15

<!-- página 16 -->

Funções e Procedimentos

Uma função matemática lembra uma máquina que recebe valores para os seus argumentos de

entrada, processa-os e retorna um valor como saída (Figs A e B).

Em computação este conceito é estendido permitindo que estas “máquinas” produzam “efeitos

colaterais” (como escrever uma mensagem na telal) (Fig. C). Eventualmente, até admite-se que a

“máquina” não retorna valor e é chamada de procedimento (Fig. D)

       Fig A             Fig B                            Fig C             Fig D

                                                                                                                                                             16

<!-- página 17 -->

Funções em C

Função: Sequência de instruções autônoma e independente , identificada por um nome. Realiza
uma tarefa específica.
   – As funções em C representam o mecanismo pelo qual podemos estender a linguagem.

  Uma função pode receber e/ou fornecer dados a outras funções, desde que seja chamada.

   Há funções fornecidas pela linguagem ( cujos códigos estão nas bibliotecas) : sqrt(), printf(),
   etc.

   O programador também pode escrever suas próprias funções, chamadas de funções de
  usuário,

  Uma função de usuário deve ser definida ( ou pelo menos declarada) ANTES de ser ativada

                                                                                                                                                            17

<!-- página 18 -->

Programando com funções

                                                                                                     18

<!-- página 19 -->

A função main e outras funções

●O código é dividido em 2 “partes”:

   –  a função main (principal) : única obrigatória e por onde inicia a execução do programa

   – demais funções (subrotinas) : são executadas apenas quando chamadas ( ativadas). Descrevem
      a solução de uma tarefa específica. Por exemplo, o cálculo da distância entre dois pontos,
       calcular perímetro, a diferença em dias entre duas datas, etc.
 ▪  As funções devem ser definidas ( ou pelo menos declarada) antes de suas ativações:
      #include <stdio.h>
   float detDistancia(....){
        ......
        return dist;
   }
   float calcPerim(...){
        ...
        L1=detDistancia(xA,yA,xB,yB);                 Antes de codificar, você deve pensar no
        L2=detDistancia(xA,yA,xC,yC);                programa  como  um  conjunto  de
        ...                                                componentes  que  realizam  serviços
        return perim;
                                                               específicos,    da    forma    mais
   }                                                independente possível em relação aos
    int main(void){                                                       outros componentes.
        ...
        perim=calcPerim(xA,yA,xB,yB,xC,yC);
        ...
        return 0;
   }

                                                                                                                                                                 19

<!-- página 20 -->

Fluxo de Execução

●Função: sequência de instruções independente que executa uma tarefa, com mesma
   estrutura de um programa, mas ativado por outro código

●Uma função pode receber e/ou fornecer dados a outras funções, desde que seja
   chamada.

●Todo programa em C tem pelo menos uma função (a main) que é automaticamente
   ativada quando o programa é iniciado. Pode chamar outras funções, algumas das quais,
  podem ainda chamar outras e assim por diante.

●  Toda função tem seu próprio nome, e quando este nome é encontrado na execução
   de outra função, a execução do programa é desviado para o corpo da função. Quando
   ela retorna, retorna para o ponto de onde saiu.

                                                                                                                                                                 20

<!-- página 21 -->

Estrutura de uma função

                                            Lista de Parâmetros: Declaração das variáveis
Um programa C não pode                     que  armazenam os valores recebidos.  Se uma
ter duas funções com o mesmo nome.          função não tem uma lista de parâmetros
                                            colocamos  void: Essas variáveis (parâmetros)
                                            armazenam  os valores  transmitidos quando a
                                            função é ativada.  Se void, significa ausência
                                            de parâmetros

    tipo_do_dado_de_retorno  nome_da_função  ( tipo var1, tipo var2,...,tipo varn )
    {
                   ...
                   ...  Corpo da função: seqüência de instruções
                   ...
                   return  (valor de retorno);

    }

                                                                            Consiste no bloco de
tipo do valor                                                      comandos que
(int,char,float,void,...)                                            compõem a função.
que a função retorna no                         O comando return tem duas
término de sua                                              finalidades:
execução. Esse valor é
enviado à rotina que fez                 •Encerrar a execução da função,
a chamada da função                    retornando para quem a chamou
(função de origem). Se                 •Retornar opcionalmente, um valor
void , significa que a                                        para a função que chamou
função não tem valor de
retorno                                                                                                                                                       21

<!-- página 22 -->

Usando Funções

●  Funções em C recebem VALORES (e não nomes de variáveis) e retornam VALORES.

                                 3
                                          Numa área local e isolada de
float f(float x, float y)                                                     memória, x recebe o valor de 1.0 e
{                                                          y recebe o valor de 2.0 e g retorna
   float z = x*x + y*y;
                                                   o valor calculado de 5.0. Ao   printf("%f^2 + %f^2  = %f\n",x,y,z);
                                                              terminar, esta memória local   return z;
}                                                     é totalmente apagada !
                    4
int main(void)
                                                        2{
   float z = 1.0;
   float w = 2.0;
   printf("%f\n",10+f(z,w)); /*imprime 15*/        f é chamada com o primeiro
   return 0;                                  argumento igual a 1.0 e o
}                                              segundo argumento igual a
                                                     2.0                          1

                                                                                                                                                            22

<!-- página 23 -->

Usando Funções

●Como são valores que são passados para a função, ela desconhece as variáveis que continham estes
   valores. Os nomes podem coincidir, mas são variáveis distintas.

    float dobra(float x)
    {
       x = 2 * x;
                                                               são variáveis       return x;
    }                                                                      distintas

    int main(void)
    {
       float x = 5.0;
       printf("%f\n", dobra(x)); /*imprime 10.0 */
       printf("%f\n", x); /* imprime 5.0 */               o valor de x
       return 0;                                                permanece
    }                                                                          inalterado !!

Há uma maneira de fazer com que uma função em C altere o valor de uma variável que pertence ao
código que a chamou. A possibilidade de alterar valores de variáveis que pertencem a outras funções,
torna as funções em C muito poderosas, mas também requerem atenção redobrada (pois alteram o
ambiente).

                                                                                                                                                                 23

<!-- página 24 -->

Construindo as funções do exemplo

                                                                                                                                   24

<!-- página 25 -->

Analisando a Solução

                                    Algoritmo:

1.  Tarefa obter pontos          Entrada de dados do ponto A

2.  Tarefa calcular perímetro      Entrada de dados do ponto B

3.  Tarefa exibir perímetro        Entrada de dados do ponto C

                                                     Calcular d(A,B)

                                    Calcular         Calcular d(A,C)            Códigos com reuso
                                perímetro
                                                     Calcular d(B,C)

                                                                              criar funções para
                                       Exibir moldura
                                                                  implementá-los.

                                       Exibir perímetro

                                       Exibir moldura

                                                                                                                                                             25

<!-- página 26 -->

Criando a função para cálculo da distância

●  Qual o objetivo desta função?
   – Calcular e “responder”, ao módulo que a ativou, a distância entre os pontos recebidos
   – retornar ao módulo que a chamou, um número real que representa a distância entre os pontos
       recebidos.

●Para calcular a distância entre 2 pontos, quais os dados são necessários?

  – coordenadas do ponto1 e do ponto 2, 4 valores reais

 ▪Como transformar as coordenadas dos 2 pontos em distância?

                                                                                                                                                                 26

<!-- página 27 -->

Função detDistancia

float detDistancia( float  x1,float  y1,float  x2, float  y2)
{
    float d;
    d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
    return d;
}

                                                                                                                                                                27

<!-- página 28 -->

Criando a função calcPerim()

Qual é o objetivo da função? Retornar à rotina que a chamou o perímetro do
                              triângulo ( nº) formado pelos pontos recebidos

   Precisa de 6 números reais ( coordenadas dos pontos)
   Retorna: 1 número

 float calcPerim(float xA, float yA, float xB, float yB, float xC, float yC)
 {
       float perim;
                                         Calcular d(A,B)

                                         Calcular d(A,C)

                                         Calcular d(B,C)

                                Somar Distancias

       return perim;
 }

                                                                                                                                                              28

<!-- página 29 -->

Criando a função calcPerim()

float calcPerim(float xA, float yA,
                float xB, float yB,
                float xC, float yC){
    float perim;
    float dAB,dAC,dBC;
           /* Calcula Distancias */
    dAB = detDistancia(xA,yA,xB,yB);
    dAC = detDistancia(xA,yA,xC,yC);
    dBC = detDistancia(xC,yC,xB,yB);
           /* Calcula Perímetro */
    perim=dAB+dBC+dAC;
    return perim;
}

                                                                                                                                                                29

<!-- página 30 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai

                                      xA       yA           xB                                                                         n                                                                     yB             xC       yC
                    0    0      1    1        0    1

                                           dAB     dBC                                                                                dAC

                                               float detDistancia( float  x1,float  y1,float  x2, float
                                               y2)
                        detDistancia                                               {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 30

<!-- página 31 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                         mai
                                    xA       yA           xB                      xC       yC n                                                                   yB
                   0    0      1    1        0    1

                                          dAB     dBC                                                                               dAC

                                                                             float detDistancia( float  x1,float  y1,float  x2, float  y2)
                       0     0             1     1                      {
                                                                                     float d;
                   x1   y1        x2   y2                            d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                                                     return d;
                                                                             }
                      detDistancia

                                                                                                                                                               31

<!-- página 32 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                           dAB     dBC                                                                                dAC

                         0     0             1     1
                    x1   y1        x2   y2
                                               float detDistancia( float  x1,float  y1,float  x2, float
                           d              y2)
                        detDistancia            {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 32

<!-- página 33 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                           dAB     dBC                                                                                dAC

                         0     0             1     1
                    x1   y1        x2   y2                         1
                                               float detDistancia( float  x1,float  y1,float  x2, float
                            d                                               y2)
                           Distancia            {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 33

<!-- página 34 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                           dAB     dBC                                                                                dAC

                                     1

                         0     0             1     1
                    x1   y1        x2   y2
                                 1
                                               float detDistancia( float  x1,float  y1,float  x2, float
                           d              y2)
                        detDistancia            {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 34

<!-- página 35 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                         1

                                           dAB     dBC                                                                                dAC

                                               float detDistancia( float  x1,float  y1,float  x2, float
                                               y2)
                          detDistancia          {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 35

<!-- página 36 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                1

                                           dAB     dBC                                                                                dAC

                                               float detDistancia( float  x1,float  y1,float  x2, float
                                               y2)
                        detDistancia            {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 36

<!-- página 37 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                1

                                           dAB     dBC                                                                                dAC

                         0     0             0     1
                    x1   y1        x2   y2
                                               float detDistancia( float  x1,float  y1,float  x2, float
                                               y2)
                        detDistancia            {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 37

<!-- página 38 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                1

                                           dAB     dBC                                                                                dAC

                         0     0             0     1
                    x1   y1        x2   y2
                                               float detDistancia( float  x1,float  y1,float  x2, float
                           d              y2)
                        detDistancia            {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 38

<!-- página 39 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                1

                                           dAB     dBC                                                                                dAC

                         0     0             0     1
                    x1   y1        x2   y2                         1
                            d            float detDistancia( float  x1,float  y1,float  x2, float
                                               y2)
                        detDistancia                                               {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 39

<!-- página 40 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                                1

                                           dAB     dBC                                                                                dAC

                                                            1
                         0     0             1     1
                    x1   y1        x2   y2
                                 1                float detDistancia( float  x1,float  y1,float  x2, float
                           d              y2)
                        detDistancia            {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 40

<!-- página 41 -->

Funcionamento

dAB = detDistancia(xA,yA,xB,yB);
dAC =detDistancia(xA,yA,xC,yC);
dBC =det Distancia(xB,yB,xC,yC);

                                                                          mai
                                      xA       yA           xB                      xC       yC n                                                                     yB
                    0    0      1    1        0    1

                          1                               1

                                           dAB     dBC                                                                                dAC

                                               float detDistancia( float  x1,float  y1,float  x2, float
                                               y2)
                          detDistancia          {
                                                     float d;
                                                     d = sqrt(pow((x1-x2),2.0) +pow((y1-y2),2.0));
                                                     return d;
                                               }

                                                                                                                                                                 41

<!-- página 42 -->

ativando a Tarefa: Calcular perímetro

#include <stdio.h>
#include <math.h>
.........
int main(void)
{
   float xA, yA, xB, yB, xC, yC, perim;
   /* Captura dados */
   printf ("\nDigite as coordenadas do ponto A: ");    scanf ("%f
%f",&xA,&yA);
   printf ("\nDigite as coordenadas do ponto B: ");    scanf ("%f
%f",&xB,&yB);
   printf ("\nDigite as coordenadas do ponto C: ");    scanf ("%f
%f",&xC,&yC);
   /* Calcula Perímetro */
   perim = calcPerim(xA, yA, xB, yB, xC, yC) ;
   /* Exibe Respostas */
   printf("\n ================================= ");
   printf("\n ================================= ");
   printf ("\n\n\t\t Perimetro: %.2f ", perim);
   printf("\n ================================= ");
   printf("\n ================================= ");
   return 0;
}

                                                                                                                                                                42

<!-- página 43 -->

Tarefa: Exibe Dados

#include <stdio.h>
#include <math.h>
.........
int main(void)
{
    float xA, yA, xB, yB, xC, yC, perim;
    /* Captura dados */
    printf ("\nDigite as coordenadas do ponto A: ");  scanf ("%f
%f",&xA,&yA);
    printf ("\nDigite as coordenadas do ponto B: ");  scanf ("%f
%f",&xB,&yB);
    printf ("\nDigite as coordenadas do ponto C: ");  scanf ("%f
%f",&xC,&yC);
    /* Calcula Perímetro */
    perim = calcPerim(xA, yA, xB, yB, xC, yC) ;
    /* Exibe Respostas */                                     função  Exibe Dados
    printf("\n ================================= ");
    printf("\n ================================= ");
    printf ("\n\n\t\t Perimetro: %.2f ", perim);
    printf("\n ================================= ");
    printf("\n ================================= ");
    return 0;
}

                                                                                                                                                                43

<!-- página 44 -->

Tarefa: Exibe Dados

Valores Recebidos? perimetro
                                               função  Exibe Dados
 Fonte? Rotina que chamou
                                                      recebe: perimetro
Valor Retornado? Nenhum                               retorna: ????
Objetivo: Mostrar dados recebidos ao Usuário

  void ExibeDados( float perimetro)
  {

                  Exibir moldura

                  Exibir perímetro

                  Exibir moldura                                       Códigos com reuso  função

     return;
  }

                                                                                                                                                             44

<!-- página 45 -->

Tarefa: Exibe Dados

void Moldura( void)
{
    printf("\n =================================");
    printf("\n =================================");
    return;                                                                            função Moldura
}                                                                                 recebe: nada
                                                                                     etorna: nada
void ExibeDados( float perimetro)
{
    moldura();
    printf ("\n\n\t\t Perimetro: %.2f ", perimetro);
    moldura();
    return;
}

                                                                                                                                                                45

<!-- página 46 -->

ProgPerimetro.c

#include <stdio.h>
#include <math.h>
float Distancia(float x1, float y1, float x2, float  y2){...}
float calcPerim(float xA,float yA,float xB,float  yB,float xC,float  yC){...}
void Moldura(void){....}
void ExibeDados( float perimetro){…}

int main(void)
{
  float xA, yA, xB, yB, xC, yC,  perim;
  /* Captura dados */
  printf ("\nDigite as coordenadas do ponto A: ");    scanf ("%f
  %f",&xA,&yA);
  printf ("\nDigite as coordenadas do ponto B: ");    scanf ("%f
  %f",&xB,&yB);
  printf ("\nDigite as coordenadas do ponto C: ");    scanf ("%f
  %f",&xC,&yC);
  /* Calcula Perímetro */
  perim= calcPerim(xA,yA,xB,yB,xC,yC)
  ExibeDados(perim);
  return 0;
}

                                                                                                                                                                46

<!-- página 47 -->

Parâmetros x Argumentos

• Os valores que uma função precisa
 para realizar sua tarefa são
                                                    Parâmetros
 chamados argumentos. (Os
 argumentos constituem a “matéria
 prima” para que a função preste o
  serviço para o qual está                                float  areaRet(float  l1,float  l2)
 programada.)                                {
                                                                        Variável Local
• Estes valores são entregues para a        float res;
 função quando ela é ativada .             res = l1*l2;
                                    return  res;
• Cada um destes valores precisam ser                                }
 armazenados em variáveis desta
                                                        Argumentos função (conhecidas como                                int main( void)
 parâmetros). Os parâmetros são      {
 declarados no cabeçalho da função.        int alt = 5;
                                      int area = areaRet( 3 ,alt)• Outras variáveis que a função possa
                                    printf("\n Area: %f",area);  precisar para realizar sua tarefa são
                                    return  0;
 declaradas normalmente no início da
                                }
 sequência de instruções.

                                                                                                                                                                47

<!-- página 48 -->

Resumo: Escopo das Variáveis

• As variáveis “existem” apenas dentro das funções em que foram declaradas
• As variáveis podem ser declaradas:
     • dentro das funções (Variáveis Locais)
     • na lista de parâmetros das funções (Variáveis Formais)

VARIÁVEIS LOCAIS: declaradas dentro do corpo da função.
           criadas no início da execução do bloco que as declarou
         não possuem valor inicial
           destruídas ao final da execução do bloco
        podem ser alteradas dentro da função
            invisíveis para outras funções

Parâmteros FORMAIS: declaradas no cabeçalho da função (angulo)
  ✔criadas DURANTE a ativação da função
  ✔preenchidas com os valores que a função recebeu de que a ativou
  ✔podem ser alteradas dentro da função
 ✔  destruídas ao final da execução do bloco
   ✔invisíveis para outras funções

                                                                                                                                                                 48

<!-- página 49 -->

Funções: Resumo

1.  Como o próprio nome diz, uma função é um segmento de programa que soluciona
   uma determinada tarefa (tem uma funcionalidade). É uma sequência de
    instruções que executa uma tarefa ou calcula um valor, identificada por um nome.
    Este código é autônomo e independente, com mesma estrutura de um programa,
   mas sua execução é dependente de chamada por outro código.

2.  Facilita a solução de problemas complexos

3. A main é a única função obrigatória. O programa é iniciado por ela.

4. Com o código modularizado em funções fica mais fácil entendê-lo, mantê-lo,
    atualizá-lo e reusá-lo.

5.  Para usar uma função, é necessário defini-la ANTES de sua ativação.

6.  Para a função ser executada, é preciso chamá-la ( por seu nome) entregando-lhe os
    valores que ela usa

7.  As variáveis são LOCAIS À FUNÇÃO QUE AS DECLAROU, isto é, só a função “dona” da
     variável pode usá-la!!!

8.  A função pode retornar no máximo um valor

                                                                                                                                                               49

<!-- página 50 -->

ProgPerimetro.c: Tarefa obter pontos?

#include <stdio.h>
#include <math.h>
float Distancia(float x1, float y1, float x2, float  y2){...}
float calcPerim(float xA,float yA,float xB,float  yB,float xC,float  yC){...}
void Moldura(void){....}
void ExibeDados( float perimetro){…}

int main(void)
{
  float xA, yA, xB, yB, xC, yC,  perim;
  /* Obter pontos*/
  printf ("\nDigite as coordenadas do ponto A: ");    scanf ("%f
  %f",&xA,&yA);
  printf ("\nDigite as coordenadas do ponto B: ");    scanf ("%f
  %f",&xB,&yB);
  printf ("\nDigite as coordenadas do ponto C: ");    scanf ("%f
  %f",&xC,&yC);
  /* Calcula Perímetro */
  perim= calcPerim(xA,yA,xB,yB,xC,yC) Obter Um Ponto
  ExibeDados(perim);                                           recebe: identificação do Ponto
  return 0;
                                            retorna: x e y desse ponto}

                                                                                                                                                                50

<!-- página 51 -->

Como fazer?

 ●Problema: O comando return devolve apenas um valor

●  Solução: A função obterPonto deve ter acesso às variáveis da main()

     –  COMO? Recebendo os endereços das variáveis em vez de seus valores

                 Esse mecanismo de passagem de parâmetros é chamado de PASSAGEM POR
              ENDEREÇO

   – COMO declarar os parâmetros da obterPonto para armazenar os endereços
       recebidos?

      Os parâmetros do cabeçalho da função devem armazenar endereços, isto é, devem
        ser do tipo endereço PONTEIROS

                                                                                                                                                                 51

<!-- página 52 -->

Ponteiros

  Definição: um ponteiro é uma variável cujo conteúdo
  é um endereço de memória;

• São iguais às variáveis comuns mas armazenam um endereço, e não um dado
 ou valor.
•  Este endereço normalmente é a posição de uma outra variável na memória;
•  Se uma variável contém o endereço de uma outra, é dito que a primeira
   variável aponta para a segunda.

 COMO DECLARAR?

                                                                                                                                                             52

<!-- página 53 -->

Declaração de Ponteiros

                                       Endereço de memória

    tipo * identificador da variável;

   Tipo do valor armazenado no endereço

A declaração de uma variável do tipo ponteiro (ou apontador) consiste do tipo base
(aquele para o qual o ponteiro vai apontar), um * e o nome da variável.

 *  - indica ao C que é a declaração de um ponteiro

                                                                                                                                                               53

<!-- página 54 -->

Operadores de Ponteiros

●Existem dois operadores especiais para ponteiros:

  *   indireção

   – Devolve o valor apontado pelo ponteiro.

 &  operador de endereço

   – Devolve o endereço na memória de seu operando.

                                                                                                                                                               54

<!-- página 55 -->

Ponteiros

int  a;     Trecho de memória      int  a;      Trecho de memória

                           A2hint *p;                             int *p;              A2h
                             a=5;              a:   A3h   ???                                                  a:   A3h   5
                           A4h             p=&a;                  A4h

              p:   A5h                              p:   A5h
                       ???                             A3h
                           A6h                                                    A6h

                           ...                                                    ...
                            .                                                      .

               Trecho de memória      int  a;      Trecho de memória
int  a;                                                                                  A2h                           A2h                             int *p;
int *p;
                                                                                  A3h                                                        40                             a=5;         a:              a:   A3h   5a=5;
                                                                                  A4h                           A4h             p=&a;
                             *p=40;       p:                           A5h              p:                                                          A5h
                                                   A3h                       ???                                                                                  A6h                           A6h

                                                                                  ...                           ...
                                                                                   .                            .

                                                                                                                                                             55

<!-- página 56 -->

Ponteiros: Atribuição

int  a=5;                   Trecho de memória
int *p1,*p2;
                a:    A2h    5         A atribuição direta entre
p1=&a;
                               A8h                  ponteiros passa o endereço de               p1:p2=p1;
                                   memória apontado por um para
                               A4h
                                   o outro.
               p1:   ABh  A2h
               p2:   ACh  A2h

                              ....

int   a=5;                   Trecho de memória                            O operador de endereço &, quando
int  *p1=&a;            A2h                a:         5                                     usado como operador sobre um
int* *pP1;
                p1:    A8h   A2h          ponteiro, devolve o endereço
pP1=&p1;
                                 A4h              ocupado por este ponteiro!
                                  Observe que aqui as expressões *p2 e *p3

                                 ABh                                  vão resultar em um erro, já que estes
                                        ponteiros estarão apontando para áreas de               pP1:   ACh   A8h
                                 memória que não estão associadas com
                                ....         nenhuma variável. O único endereço de
                                 memória acessável é o de x                            56

<!-- página 57 -->

Ponteiros: Aritmética

int  a=5;      Trecho de memória

                               A2hint *p1=&a;  a:                         5            Operações aritméticas válidas com
                                                 ponteiros:int *p2=p1;          A8h
                                                                •  adição
                               A4h                              •  subtração
               p1:   ABh  A2h                     •  incremento
                                                                •  decremento
               p2:   ACh  A2h

                               ....

                  Trecho de memória      o TAMANHO DO TIPO BASE DO PONTEIRO
 p2++;                              A2h                é sempre usado no cálculo das operações               a:        5
                                                 aritméticas.
              p1:   A8h

                              A4h

              p1:   ABh  A2h                                   *p2 vai resultar em erro, pois não
              p2:   ACh  A4h          aponta para uma área de memória
                             ....                 associada a uma variável

                                                                                                                                                                57

<!-- página 58 -->

Aplicação de ponteiros na
comunicação de funções

 Passagem de Parâmetros usando Ponteiros

                                                                                                                                      58

<!-- página 59 -->

Passagem de Parâmetros

●Toda função define um processamento a ser realizadoe comunica-se com o
  "mundo externo" através de passagem e retorno de valores.

●  Este processamento depende dos valores armazenados nos parâmetros da
   função.

●  Assim, para usar uma função é necessário "entregar" os valores
  (argumentos) adequados.

 • O processo de informar os valores processados pela função, na mesma
  ordem em que foram definidos durante a declaração da função, chama-se
  passagem de parâmetros.

      if (abs(a-b) == 1)      float abs ( float num);

●  Pode-se transferir para uma função tanto o dado armazenado em uma
   variável (passagem por valor) como o endereço da variável.

●Normalmente, a passagem de parâmetros a uma função é por valor.

                                                                                                                                                               59

<!-- página 60 -->

Passagem de endereços para funções

  Exemplo

                 Função f
                                     Função g
                int n= 4;
                int t ant;
                int  suc;
                //Ativar g para
                atualizar n,ant e suc

●  função f ativa a função g para que ela calcule um novo valor para n e
   respectivo antecessor e sucessor
    –  g não pode alterar diretamente valores de variáveis de f,
    –  porém se f passar para g os valores dos endereços de memória onde as
        variáveis de  f estão armazenadas, g pode alterar, indiretamente, os
        valores das variáveis de f

                                                                                                                                                               60

<!-- página 61 -->

Passagem de endereços para funções: Simulação da função g

              n = g(&ant,&suc,n);

                                              AB5

                                              AB4
                         ant      ?

                                              AB3
                         suc      ?

                                              AB2
                      n     4

                                              AB1

                                              AB0

           instrução da função f
         que chama a função g

                                                                                                                                                          61

<!-- página 62 -->

Simulação da função g

                          g
      f
AB5                                  BB5
                         AB4   px

AB4                                  BB4
                                        py   ant                      AB3
           ?

AB3                                    BB3
   suc                               4    n           ?
AB2                                  BB2
   n    4
AB1                                  BB1

AB0
                                    BB0

                            nn == 5;5;
                             *px = n-1;
                            *py = n+1;
                                 return n;
                                                                                                                                                 62

<!-- página 63 -->

Simulação da função g

                              g
      f
AB5                                  BB5
                         AB4   px

AB4                                  BB4
                                        py   ant                      AB3
           ?

AB3                                    BB3          n   suc           ?                 5

AB2
                                    BB2
   n    4

AB1
                                    BB1

AB0
                                    BB0

                            n = 5;
                            *px*px==n-1;n-1
                            *py = n+1;
                                 return n;
                                                                                                                                                 63

<!-- página 64 -->

Simulação da função g

                              g
      f
AB5                                  BB5
                         AB4   px

AB4                                  BB4
                                        py   ant                      AB3
       4?

AB3                                    BB3
   suc                               5    n           ?
AB2                                  BB2
   n    4
AB1                                  BB1

AB0
                                    BB0

                            n = 5;
                             *px = n-1;
                            *py = n+1;                            *py = n+1;
                                 return n;
                                                                                                                                                 64

<!-- página 65 -->

Simulação da função g

                              g
      f
AB5                                  BB5
                         AB4   px

AB4                                  BB4
                                        py   ant                      AB3
       4

AB3                                    BB3
   suc                               5    n       6?
AB2                                  BB2
   n    4
AB1                                  BB1

AB0
                                    BB0

                            n = 5;
                             *px = n-1;
                            *py = n+1;
                                returnreturn n;n;
                                                                                                                                                 65

<!-- página 66 -->

Simulação da função g

n = g(&ant,&suc,n);                                                                BB5
                                                                   AB4
                     AB5
                                                                BB4
                                                                   AB3

                     AB4
   ant    4                                            BB3      5

                     AB3                               BB2
   suc    6

                     AB2                               BB1
    n     5

                     AB1                               BB0

                     AB0
                                  n = 5;
                 5     *px = n-1
                                   *py = n+1;
                                        return n;

                                                                                                                         66

<!-- página 67 -->

Passagem de endereço como parâmetro de função

Permite compartilhar variáveis entre funções!!
Permite que uma função "produza" mais de um valor

                                                                                                                                                          67

<!-- página 68 -->

Voltando à tarefa de obter os pontos

#include <stdio.h>
#include <math.h>
float Distancia(float x1, float y1, float x2, float  y2){...}
float calcPerim(float xA,float yA,float xB,float  yB,float xC,float  yC){...}
void Moldura(void){....}
void ExibeDados( float perimetro){…}

int main(void)
{
  float xA, yA, xB, yB, xC, yC,  perim;
  /* Obter pontos*/
  printf ("\nDigite as coordenadas do ponto A: ");    scanf ("%f
  %f",&xA,&yA);
  printf ("\nDigite as coordenadas do ponto B: ");    scanf ("%f
  %f",&xB,&yB);
  printf ("\nDigite as coordenadas do ponto C: ");    scanf ("%f
  %f",&xC,&yC);
  /* Calcula Perímetro */
  perim= calcPerim(xA,yA,xB,yB,xC,yC) Obter Um Ponto
  ExibeDados(perim);                                           recebe: identificação do Ponto
  return 0;
                                            retorna: x e y desse ponto}

                                                                                                                                                                68

<!-- página 69 -->

Uma Solução

void obterPonto(char ponto,float *cX, float* cY)
{
   float x,y;
   printf("Ponto %c\n",ponto)
   printf ("\tCoordenada x :");
   scanf ("%f",&x);
   printf ("\tCoordenada y");
   scanf ("%f",&y);
   * cX = x;
   * cY = y;
   return;
}

                                 Solução não
                               muito adequada

                                                                                                                                                                 69

<!-- página 70 -->

Simulação da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main                obterPonto

AB5                                 BB8

                         pto                                'A'
AB4                                 BB7
                         cX
  xA       ?             AB4
AB3                                 BB6cY
  yA       ?             AB3          void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2                                 BB5
                                                 float x,y;
  xB      ?                                                 printf("Ponto %c\n",ponto)
AB1                                    BB4                         printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f",&x);
AB0                                    BB3                         printf ("\tCoordenada y");
                                                 scanf ("%f",&y);
                          x          ?                                                 * cX = x;
                                    BB2                         * cY = y;
                         y         ?               return;
                                    BB1                     }
                                     ?

                                    BB0
                                     ?

                                                                                                                                                         70

<!-- página 71 -->

Simulação da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main                obterPonto

AB5                                 BB8

                         pto                                'A'
AB4                                 BB7
                         cX
  xA       ?             AB4
AB3                                 BB6cY
  yA       ?             AB3          void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2                                 BB5
                                                 float x,y;
  xB      ?                                                 printf("Ponto %c\n",ponto)
AB1                                    BB4                         printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f",&x);
AB0                                    BB3                         printf ("\tCoordenada y");
                                                 scanf ("%f",&y);
                          x       9.0                                                 * cX = x;
                                    BB2                         * cY = y;
                         y       6.0              return;
                                    BB1                     }

                                    BB0

                                                                                                                                                         71

<!-- página 72 -->

Simulação da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main                obterPonto

AB5                                 BB8

                         pto                                'A'
AB4                                 BB7
                         cX
  xA   9.0             AB4
AB3                                 BB6cY
  yA   6.0             AB3          void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2                                 BB5
                                                 float x,y;
  xB      ?                                                 printf("Ponto %c\n",ponto)
AB1                                    BB4                         printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f",&x);
AB0                                    BB3                         printf ("\tCoordenada y");
                                                 scanf ("%f",&y);
                          x       9.0                                                 * cX = x;
                                    BB2                         * cY = y;
                         y       6.0              return;
                                    BB1                     }

                                    BB0

                                                                                                                                                         72

<!-- página 73 -->

Simulação da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main
                               BB5
AB5                                  AB4

                               BB4
AB4                                  AB3
  xA   9.0                  BB3
                                           5
AB3
  yA   6.0                  BB2                   void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2
                                                 float x,y;
  xB      ?                                                 printf("Ponto %c\n",ponto)                               BB1
AB1                                               printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f",&x);
AB0                             BB0                     printf ("\tCoordenada y");
                                                 scanf ("%f",&y);
                                                 * cX = x;
                                                 * cY = y;
                                                 return;
                                             }

                                                                                                                                                         73

<!-- página 74 -->

Outra Solução

void obterPonto(char ponto,float *cX, float* cY)
{
   float x,y;
   printf("Ponto %c\n",ponto)
   printf ("\tCoordenada x :");
   scanf ("%f",&x);    scanf ("%f",cX);
   printf ("\tCoordenada y");
   scanf ("%f",&y);    scanf ("%f",cY);
   * cX = x;
   * cY = y;
   return;
}

                                 Solução adequada

                                                                                                                                                                 74

<!-- página 75 -->

Simulação 2 da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main                obterPonto

AB5                                 BB8

                         pto                                'A'
AB4                                 BB7
                         cX
  xA       ?             AB4
AB3                                 BB6cY
  yA       ?             AB3          void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2                                 BB5
  xB      ?                                                 printf("Ponto %c\n",ponto)
AB1                                    BB4                         printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f", cX);
AB0                                    BB3                         printf ("\tCoordenada y");
                                                 scanf ("%f",cY);
                                     ?                                                      return;
                                    BB2                     }
                                     ?

                                    BB1
                                     ?

                                    BB0
                                     ?

                                                                                                                                                         75

<!-- página 76 -->

Simulação 2 da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main                obterPonto

AB5                                 BB8

                         pto                                'A'
AB4                                 BB7
                         cX
  xA   9.0             AB4
AB3                                 BB6cY
  yA       ?             AB3          void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2                                 BB5
  xB      ?                                                 printf("Ponto %c\n",ponto)
AB1                                    BB4                         printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f", cX);
AB0                                    BB3                         printf ("\tCoordenada y");
                                                 scanf ("%f",cY);
                                                      return;
                                    BB2                     }

                                    BB1

                                    BB0

                                                                                                                                                         76

<!-- página 77 -->

Simulação 2 da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main                obterPonto

AB5                                 BB8

                         pto                                'A'
AB4                                 BB7
                         cX
  xA   9.0             AB4
AB3                                 BB6cY
  yA       ?             AB3          void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2                                 BB5
  xB      ?                                                 printf("Ponto %c\n",ponto)
AB1                                    BB4                         printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f", cX);
AB0                                    BB3                         printf ("\tCoordenada y");
                                                 scanf ("%f",cY);
                                                      return;
                                    BB2                     }

                                    BB1

                                    BB0

                                                                                                                                                         77

<!-- página 78 -->

Simulação 2 da obterPonto

                         obterPonto( ‘A’,&xA,&xB);

 main
                               BB5
AB5                                  AB4

                               BB4
AB4                                  AB3
  xA   9.0                  BB3
                                           5
AB3
  yA   6.0                  BB2                   void obterPonto (char ponto,float *cX,float* cY)                                             {
AB2
                                                 float x,y;
  xB      ?                                                 printf("Ponto %c\n",ponto)                               BB1
AB1                                               printf ("\tCoordenada x :");
  yB      ?                                      scanf ("%f",&x);
AB0                             BB0                     printf ("\tCoordenada y");
                                                 scanf ("%f",&y);
                                                 * cX = x;
                                                 * cY = y;
                                                 return;
                                             }

                                                                                                                                                         78

<!-- página 79 -->

Ativando a função obterPonto

#include <stdio.h>
#include <math.h>
float Distancia(float x1, float y1, float x2, float  y2){...}
float calcPerim(float xA,float yA,float xB,float  yB,float xC,float  yC){...}
void Moldura(void){....}
void ExibeDados( float perimetro){…}

int main(void)
{
  float xA, yA, xB, yB, xC, yC,  perim;
  /* Obter pontos*/
   obterPonto(&xA,&yA);
   obterPonto(&xB,&yB);
   obterPonto(&xC,&yC);
  /* Calcula Perímetro */
  perim= calcPerim(xA,yA,xB,yB,xC,yC)
  ExibeDados(perim);
  return 0;
}

                                                                                                                                                                79
