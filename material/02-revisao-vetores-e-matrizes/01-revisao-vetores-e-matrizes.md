# RevisãoCompactaVetorMatriz

> Fonte: `base/Revisão Vetores e Matrizes/RevisãoCompactaVetorMatriz.pdf`

<!-- página 1 -->

Revisão
Vetores e Matrizes

                                                 1                           1

<!-- página 2 -->

Problema

✔Ler a matrícula e 3 notas de 6 alunos:
✔  Calcular a média de cada aluno
✔  Exibir as notas dos alunos com média maior que  a média da turma

          P/cada aluno

            Lê dados

            Se média do aluno > média da turma

                 Exibir dados do aluno

                                                   Dados dos alunos são necessários para:
                          Qual a
                                                                    •  1º) calcular a média da turma                       média da
                                                                    •  2º) selecionar /exibir os acima                         turma?

          1)   Como armazenar as matrículas dos alunos?

          2)   Como armazenar as notas e médias dos alunos?

                                                                                                                                                                  2

<!-- página 3 -->

Desenvolvendo a solução

Mostrar os alunos que tem nota acima    UM ALUNO  matr , 3 notas e 1 médiada média da turma

                            Como armazenar a matrícula do aluno?Matr   P1  P2   P3
                             Como armazenar as notas do aluno?

1010    7,5   8,5    7,8        Como armazenar 3 alunos?

                                 int matr;
1033    8,4   9,2    6,8
                                 float notas[4];

1080    9,1   10,0  9,5                      notas

                              matr
1320    4,0   5,2    4,6

1221    5,7   3,4    4,3

3231    4,3   6,0    5,8                             Turma  conjunto de "alunos".

                                                                                                                                                                3

<!-- página 4 -->

Desenvolvendo a solução

     Matr             P1  P2   P3  Média

     1010                 7,5  8,5   7,8  7,8

     1033                 8,4  9,2   6,8  6,8

                           9,1  10,0  9,5  9,5     1080

                           4,0  5,2   4,6  4,6     1320

                           5,7  3,4   4,3  4,3     1221

     3231                 4,3  6,0   5,8  5,8

 Vetor de                         Matriz de
Matrículas                    Notas                                                    4

<!-- página 5 -->

Revisão Vetor

●  VETOR: Variável capaz de armazenar simultaneamente, diversos valores de mesmo tipo

    – A variável é dividida em várias células (posições) identificadas por um nº (índice)
    –  Cada valor fica armazenado em uma posição (célula)do vetor
    –  As células podem ser acessadas por seu endereço ou pelo número de seu índice

EM C:
    – Na declaração da variável vetor é necessário determinar a quantidade de células (elementos)
        e o tipo de dado dos elementos
    –  É possível declarar um vetor com valores iniciais
    – Um vetor de dimensão DIM os índices variam de 0 a DIM-1

                                     1010  1033  1080  1320  1221  3231
                  Vetor

                        0  1  2  3  4  5

                                                                                                                                                                 5

<!-- página 6 -->

Declaração de Vetores

  Sintaxe:  tipo nomeDoVetor[quantidade_de_itens];

  Exemplo:  int vMatr [6];

                            vMatr Representação interna:

     nomeDoVetor[índice]  == *(nomeDoVetor+índice)

 Quando o compilador C executa a declaração de um vetor, ele calcula o tamanho, em
 bytes, necessário para armazenar o vetor:
     quantidade_de_itens x tamanho_do_tipo
 Como é realizado:
1.  O nomeDoVetor é um ponteiro para o tipo_da_variável do vetor.
2.  Aloca o espaço de quantidade_de_itens x tamanho_do_tipo bytes
3.   Atribui ao nomeDoVetor (que é um ponteiro) o endereço inicial da a área alocada

                                                                                                                                                               6

<!-- página 7 -->

Vetores e Ponteiros: Resumo

Existe uma associação forte entre vetores e ponteiros, pois se existe a declaração:

    int vMatr[6]={1121,3456,8765,9890,2323,6677};

 o identificador vMatr, que representa o vetor, é um ponteiro constante que armazena
 o endereço inicial da árae de dados, isto é, vMatr, sem indexação aponta para o
  primeiro elemento do vetor.

           vMatr

                                                                                                                                                             7

<!-- página 8 -->

Exemplo de Declarações

1.  Só declaração:                     int v[4];

                   v

2.   Declaração com inicialização:                             int v[4]={17,42,23,34};

                  v

                                                                                                                                                                8

<!-- página 9 -->

Vetor : Acesso aos elementos

●DUAS FORMAS:

   –   Pelo endereço

   –   Por indexação

                                                                                                                                                                 9

<!-- página 10 -->

Vetor : Acesso pelo endereço

int v[4]={17,42,23,34};

                                                                                                                                         10

<!-- página 11 -->

Vetor : Acesso pelo endereço

Considerando que:
      v é um tipo de  "ponteiro constante", cujo valor não pode ser alterado e armazena o
     endereço do 1º elemento do vetor

     Os elementos de um vetor estão armazenados em áreas consecutivas na memória

    O endereço resultante do incremento de um ponteiro reflete o tamanho do tipo
     referenciado.

    Tem-se que:
      ✔  v   é o endereço da 1º elemento,
      ✔  v+1 é o endereço da 2º elemento,
      ✔  v+2 é o endereço do 3º elemento
      ✔  e assim sucessivamente

                                                                                                                                                                11

<!-- página 12 -->

Simulação: Acesso por endereço

Exibir
valores

                                                           ?

                                                                                         i

            int i;
            for(i=0;i<4;i++){
               printf("%d", *(v+i));
            }

                                                                                                                                                                 12

<!-- página 13 -->

Simulação: Acesso por endereço

Exibir
valores

                                                       0

                                                                                           i

                 (v+i)      17         *

            int i;
            for(i=0;i<4;i++){
               printf("%d", *(v+i));
            }

                                                                                                                                                                 13

<!-- página 14 -->

Simulação: Acesso por endereço

Exibir
valores

                                                      1

                                                                                         i

                 (v+i)      42         *

            int i;
            for(i=0;i<4;i++){
               printf("%d", *(v+i));
            }

                                                                                                                                                                 14

<!-- página 15 -->

Simulação: Acesso por endereço

Exibir
valores

                                                        2

                                                                                           i

                 (v+i)      23         *

            int i;
            for(i=0;i<4;i++){
               printf("%d", *(v+i));
            }

                                                                                                                                                                 15

<!-- página 16 -->

Simulação: Acesso por endereço

Exibir
valores

                                                       3

                                                                                           i

                 (v+i)      34         *

            int i;
            for(i=0;i<4;i++){
               printf("%d", *(v+i));
            }

                                                                                                                                                                 16

<!-- página 17 -->

Simulação: Acesso por endereço

Exibir
valores

                                                       4

                                                                                         i

            int i;
            for(i=0;i<4;i++){
               printf("%d", *(v+i));
            }

                                                                                                                                                                 17

<!-- página 18 -->

Simulação: Atribuição por endereço

Armazenar valor na posição i:                      *(v+i)= 77

                                                           2
                                                                                                i

                           *(v+i)= 77         (v+i)

                                       77

                                                                                                                                                              18

<!-- página 19 -->

Vetor

●DUAS FORMAS:

   –   Pelo endereço

   –   Por indexação

                                                                                                                                                                  19

<!-- página 20 -->

Vetor : Acesso por indexação

   Sintaxe:  nomeDoVetor[número_da_posição];

●  Cada POSIÇÃO (célula) é identificada por um NÚMERO
●  Valores nos Colchetes têm significados diferentes:

    – Na declaração de um vetor: informa a quantidade N de posições que devem ser alocadas
    – Após a declaração: informa a posições que será acessada para leitura ou gravação de
        informação. Deve ser um valor entre [0...(N-1)]

                                                                                                                                                                20

<!-- página 21 -->

Simulação : Acesso por indexação

Exibir
valores

                                                           ?

                                                                                         i

            int i;
            for(i=0;i<4;i++){
               printf("%d", v[i]);
            }

                                                                                                                                                                21

<!-- página 22 -->

Simulação : Acesso por indexação

Exibir
valores

                                                      0

                                                                                         i

                       V[i]         17

            int i;
            for(i=0;i<4;i++){
               printf("%d", v[i]);
            }

                                                                                                                                                                22

<!-- página 23 -->

Simulação : Acesso por indexação

Exibir
valores

                                                      1

                                                                                         i

                       V[i]         42

            int i;
            for(i=0;i<4;i++){
               printf("%d", v[i]);
            }

                                                                                                                                                                23

<!-- página 24 -->

Simulação : Acesso por indexação

Exibir
valores

                                                      2

                                                                                         i

                       V[i]         23

            int i;
            for(i=0;i<4;i++){
               printf("%d", v[i]);
            }

                                                                                                                                                                24

<!-- página 25 -->

Simulação : Acesso por indexação

Exibir
valores

                                                      3

                                                                                         i

                       V[i]         34

            int i;
            for(i=0;i<4;i++){
               printf("%d", v[i]);
            }

                                                                                                                                                                25

<!-- página 26 -->

Simulação : Acesso por indexação

Exibir
valores

                                                      4

                                                                                         i

            int i;
            for(i=0;i<4;i++){
               printf("%d", v[i]);
            }

                                                                                                                                                                26

<!-- página 27 -->

Simulação: Atribuição por indexação

Armazenar valor na posição i:                      v[i]= 77

                                                           2
                                                                                                i

                           v[i]= 77         V[i]

                                       77

                                                                                                                                                              27

<!-- página 28 -->

Vetor passados para funções

Passagem de vetor para função:
  –  consiste em passar o endereço da primeira posição do vetor:
      "passar um vetor para uma função" ↔  "passar o endereço inicial do vetor"
  – elementos do vetor não são copiados para a função
  – argumento copiado é apenas o endereço do primeiro elemento
  – a função chamada recebe uma referência para o vetor , ou seja, a função chamada,
     quando acessa os elementos, acessa as mesmas posições de memória da função
     que declarou vetor.

CONSEQUÊNCIAS:
  –  se atribuirmos um valor a um elemento do vetor passado como parâmetro, este
     elemento também é alterado no vetor original.
  – A referência do vetor passada para a função indica apenas o início do espaço de
     memória a partir do qual os elementos estão armazenados.
  – Para a função chamada, a dimensão do vetor original é indiferente – a função
     sempre recebe apenas uma referência para o início da área de memória.

                                                                                                                                                              28

<!-- página 29 -->

Vetores Passados para Funções

                                                                                                                 29

<!-- página 30 -->

Vetores Passados para Funções

                                                                                                                 30

<!-- página 31 -->

Vetores Passados para Funções

                                                                                                                 31

<!-- página 32 -->

Vetores Passados para Funções

                                                                                                                 32

<!-- página 33 -->

Vetores Passados para Funções

                                                                                                                 33

<!-- página 34 -->

Vetores Passados para Funções

                                                                                                                 34

<!-- página 35 -->

Vetores Passados para Funções

                                                                                                                 35

<!-- página 36 -->

Vetores Passados para Funções

                                                                                                                 36

<!-- página 37 -->

Vetores Passados para Funções

                                                                                                                 37

<!-- página 38 -->

Vetor x Funções

   – Como tratar o problema da quantidade de elementos?
   – Como a função "sabe" quando o vetor "terminou"?
   – Como tratar o caso de ser no máximo n elementos

» SOLUÇÃO:
   A função chamada deve:
    •  receber também o número de elementos do vetor como parâmetro
    •  retornar a quantidade de elementos preenchidos

                                                                                                                                                               38

<!-- página 39 -->

Desenvolvendo a solução

●Mostrar os alunos que tem nota acima da média da turma

Matr   P1  P2   P3    UM Aluno matr , 3 notas e 1 média

1010    7,5   8,5    7,8                        Como armazenar as notas dos (no máximo)
                            6 alunos desta turma?
1033    8,4   9,2    6,8

                               mNotas
1080    9,1   10,0  9,5

1320    4,0   5,2    4,6

1221    5,7   3,4    4,3

3231    4,3   6,0    5,8

                                                                                                                                                                39

<!-- página 40 -->

Resposta

               24 variáveis simples

               ou

6 vetores (um por aluno), cada qual com 4 elementos

                  (3 notas + média)

               ou

 4 vetores (um por nota), cada qual com 6 elementos

                  (um por aluno)

                                                                                                                                                 40

<!-- página 41 -->

Conjunto Bidimensional

                    Vários vetores de mesmo tipo --> matriz de vetores

   =
                 5

              4  Alunosvetores
6 6      3

         2

     1

   0

       0     1     2     3

           3 Notas + Média = 4 elementos por vetor

                                                                                                                                                         41

<!-- página 42 -->

Matriz

notas como  matriz: 6 alunos, cada um com 3 NOTAS +a MÉDIA

                                       Representação espacial
A VANTAGEM É QUE TEMOS TODOS OS DADOS
                                         0  1  2  3 ARMAZENADOS EM UMA ÚNICA ESTRUTURA
                                     0
Para  acessar um  elemento  da  matriz,
                                     1utilizamos indexação dupla:
           Primeiro índice : linha;                                     2
         Segundo índice : coluna
                                     3         5.0

                                     4

  notas[0][0]   notas[0][1] …  notas[0][3]               5
  notas[1][0]   notas[1][1] …  notas[1][3]

  notas[2][0]   notas[2][1] …  notas[2][3]                                     6
                                         .

  notas[6][0]   notas[6][1] …  notas[6][3]
                                           mNotas[3] [1]

                                                                                                                                                              42

<!-- página 43 -->

Matrizes: Conjuntos Bidimensionais

Portanto para acessar um elemento da matriz, utilizamos indexação dupla:
                               mat[i][j]
  – Para uma matriz com m linhas e n colunas, os índices usados no acesso aos
    elementos devem satisfazer: 0<=i<m e 0<=j<n

Como é armazenada uma matriz em memória:
  – A memória do computador é linear.
  – Uma matriz declarada em C é armazenada na memória linha por linha.

 m[i][j] = i * n + j    onde n é o número de colunas.

 Exemplo: m[1][2] = 1*3 + 2 = 5

                                                                                                                                                             43

<!-- página 44 -->

Declaração/Acesso de Matrizes

•  Declaração de uma matriz:

        #define  LIN  6
        #define  COL  4

           …..
           float  mNotas [LIN] [COL];

 •  Acesso a um elemento de Notas:

       printf("%6.2f", mNotas[1] [COL - 1]);

                                    linha
                                                        coluna

                                                                                                                                                            44

<!-- página 45 -->

Simulação da Leitura dos elementos de uma
               matriz m 2 X 3

                                                                                                                                                        45

<!-- página 46 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0

                                              1

                                                                    i

                                                                                     j

                                                                                                                                                                 46

<!-- página 47 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0

                                              1

                                                                    i   0

                                                                                     j

                                                                                                                                                                 47

<!-- página 48 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0

                                              1

                                                                    i   0

                                                                                     j   0

                                                                                                                                                                 48

<!-- página 49 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9

                                              1

                                                                    i   0

                                                                                     j   0

                                                                                                                                                                 49

<!-- página 50 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9

                                              1

                                                                    i   0

                                                                                     j   1

                                                                                                                                                                 50

<!-- página 51 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4

                                              1

                                                                    i   0

                                                                                     j   1

                                                                                                                                                                 51

<!-- página 52 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4

                                              1

                                                                    i   0

                                                                                     j   2

                                                                                                                                                                 52

<!-- página 53 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1

                                                                    i   0

                                                                                     j   2

                                                                                                                                                                 53

<!-- página 54 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1

                                                                    i   0

                                                                                     j   3

                                                                                                                                                                 54

<!-- página 55 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1

                                                                    i   1

                                                                                     j   3

                                                                                                                                                                 55

<!-- página 56 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1

                                                                    i   1

                                                                                     j   0

                                                                                                                                                                 56

<!-- página 57 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3

                                                                    i   1

                                                                                     j   0

                                                                                                                                                                 57

<!-- página 58 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3

                                                                    i   1

                                                                                     j   1

                                                                                                                                                                 58

<!-- página 59 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3    2

                                                                    i   1

                                                                                     j   1

                                                                                                                                                                 59

<!-- página 60 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3    2

                                                                    i   1

                                                                                     j   2

                                                                                                                                                                 60

<!-- página 61 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3    2    1

                                                                    i   1

                                                                                     j   2

                                                                                                                                                                 61

<!-- página 62 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3    2    1

                                                                    i   1

                                                                                     j   3

                                                                                                                                                                 62

<!-- página 63 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3    2    1

                                                                    i   2

                                                                                     j   3

                                                                                                                                                                 63

<!-- página 64 -->

Simulação da Leitura

int m [MAXLIN][MAXCOL];

for (i=0;i<MAXLIN; i++)
       for (j=0;j <MAXCOL;j++)
           scanf("%i", &m[i] [j]);

                                                 0    1    2

                                              0  9    4    8

                                              1  3    2    1

                                                                    i   2

                                                                                     j   3

                                                                                                                                                                 64

<!-- página 65 -->

Solução do Problema com Matriz

Solução:

    declarar o vetor para armazenar as matriculas

    declarar a matriz para armazenar as notas

    preenchê-los com as matrículas e notas fornecidas, calculando a média de cada aluno e a média da turma

    Mostrar matr e notas dos alunos com média acima da média da turma

  int      vMatr[LIN];
  float  mNotas[LIN][COL];

                                                                                                                                                         65

<!-- página 66 -->

Passagem de Matrizes para Funções

• Uma diferença importante com relação a vetores é que o parâmetro que
   representa a matriz deve ter especificado o número de colunas da matriz.

   –  Isso impossibilita a codificação de uma função para manipular uma matriz
     de dimensão qualquer.

   – O máximo que podemos fazer é codificar uma função que manipula uma
      matriz com qualquer número linhas, mas o número de colunas deve ser
       especificado por uma constante no código.

   –  Isso é necessário pois o compilador precisa conhecer o número de colunas
      da matriz para fazer a conta de endereçamento.

                                                                                                                                                                66

<!-- página 67 -->

Passagem de Matrizes para Funções

                                                         m[i][j] = i *n + j onde n é
                                      o número de colunas.    2 3
                     2 | 3 | 1 | 9 | 4 | 7       Exemplo: m[1][1] 1*2+1 3    1 9
    4 7                             0    1    2    3   4   5
                                            2 | 3 | 1 | 9 | 4 | 7

                                                         m[i][j] = i *n + j onde n é
   2 3 1                          0    1    2    3   4   5       o número de colunas.
                     2 | 3 | 1 | 9 | 4 | 7
   9 4 7                                    Exemplo: m[1][1] 1*3+1 4

                                            2 | 3 | 1 | 9 | 4 | 7

O parâmetro que aponta para a matriz deve ter o número de colunas da matriz.
         tipo Funçao ( tipo matriz[ ][NCOL].....
o compilador precisa conhecer o número de colunas da matriz para calcular a posição

                                                                                                                                                                    67

<!-- página 68 -->

Uma solução: Só Leitura dos dados

                                                                                                                                                          j
                                                                                                                                        i

                                   E a média do aluno?
                                   E a média da turma?

                                                                                                                                                                 68

<!-- página 69 -->

Uma Solução

  Código de função principal:
                                                                                                                                  i

#define LIN 7
#define COL 6
……
int main (void)
{
   int n,vMatr[LIN];
   float med, mNotas[LIN][COL];
   n= preencheDados( vMatr,mNotas, TAM,&med);
   printf("Media da disciplina: %.2f", med);
   mostra_bons(vMatr,mNotas,n,med);

  return 0;
}

                                                                                                                                                                 69

<!-- página 70 -->

Uma solução: Leitura dos dados

                                                                                                     j

                                                                                   i

                                                                                                               70

<!-- página 71 -->

Uma Solução

                                                                                                                                       i
Mostrar a matr e notas dos alunos cuja média está acima da média
da turma
                                                                                                                                                 j

 void  mostra_bons(int vMatr[], float  mNotas [ ] [6] ,
            int n,float medturma){
       int i,j;
       for (i=0; i < n; i++){
          if ( mNotas[i][COL-1] > medturma){
   printf("\nMatr: %d", vMatr[i]);
          for (j=0; j < COL-2; j++) {
             printf("P%d: %.2f", j+1,mNotas[i][j]);
        printf("Med: %.2f", j+1,mNotas[i][COL-1]);
            }
     }
   }

                                                                                                                                                                 71
