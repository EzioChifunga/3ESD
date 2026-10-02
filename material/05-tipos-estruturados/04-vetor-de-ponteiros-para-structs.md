# VetorDePonteirosParaTiposEstruturadosVFod

> Fonte: `base/Revisão Tipos Estruturados/VetorDePonteirosParaTiposEstruturadosVFod.pdf`

<!-- página 1 -->

Vetores de Ponteiros para
   Tipos Estruturados

                                                                                                                                                                     1

<!-- página 2 -->

Ponteiro para estrutura

                                                                                                                                               2

<!-- página 3 -->

Revisão Ponteiro para Estrutura

struct ponto                                       int main() {
{                                                    Ponto pto1={10,3};
   float x;                                          Ponto * pp;
   float y;
};
typedef struct ponto Ponto;                          pp=&pto1;

int main() {
  Ponto pto1={10,3};
  Ponto * pp;
                                                     (*pp).x = (*pp).x + 3;  pp=&pto1;
  (*pp).x = (*pp).x + 3;
  //Alternativa de acesso: operador de acesso ->
  pp->y=pp->y+3
  return 0;
                                                      pp->y=pp->y+3}

                                                    Retangulo r, * rp;
                                                    rp = &r;
 Obs: Se uma estrutura grande deve ser passada para uma                                                                       então, são equivalentes:
 função, geralmente é mais eficiente passar um ponteiro do                                                        r.pt1.x ≡ rp->pt1.x ≡ (r.pt1).x ≡ (rp->pt1).x
 que copiar a estrutura inteira.
                                                                                                                                                                                                                        3

<!-- página 4 -->

Exemplo Área

Escreva função que retorna área de um retângulo.
Defina um dos pontos diretamente … a1 = {…} e o outro com o operador (.)

struct ponto
{                                               int main(void){
   float x;                                         struct ponto a1 = {1.0,1.0};
   float y;                                         struct ponto a2;
};                                                  struct retangulo r;
typedef struct ponto Ponto;
                                                    a2.x = 4.0;
struct retangulo                                    a2.y = 3.0;
{                                                   r.pt1 = a1;
    struct ponto pt1;                                r.pt2 = a2;
    struct ponto pt2;
};                                                   printf("%f\n", area(&r));
typedef struct retangulo Retangulo;                 return 0;
                                                }
float area(struct retangulo * pr);
                                                float area(struct retangulo * pr){
                                                    return (pr->pt2.x - pr->pt1.x) *
                                                        (pr->pt2.y - pr->pt1.y);
                                                }

                                                                                                                                                                                                                         4

<!-- página 5 -->

Alocação dinâmica

                                                                                                                                  5

<!-- página 6 -->

Alocação Dinâmica de Estruturas

    struct ponto
                                     int main() {    {
       float x;                          Ponto pto1={10,3};
       float y;                          Ponto * pp;
    };
    typedef struct ponto Ponto;

    int main() {
      Ponto pto1={10,3};
      Ponto * pp;

      pp=(Ponto *)malloc(sizeof(Ponto));     pp=(Ponto *)malloc(sizeof(Ponto));
      pp->x = 30 ;
      pp->y = pto1.y;

      return 0;
    }

                                              pp->x = 30 ;
                                              pp->y = pto1.y;
Também podemos definir um novo tipo para representar o ponteiro para
uma estrutura:
     typedef struct ponto * PPonto;
E, nesse caso, fazemos: PPonto p; no lugar de Ponto * p; …
mas não recomendamos isto, por ser um procedimento menos claro.                                                                                                                                          6

<!-- página 7 -->

Vetores & estruturas

                                                                                                                                         7

<!-- página 8 -->

Vetores e Tipos Estruturados

●Dado um tipo estruturado T (com 3 campos, por exemplo), podemos ter um vetor de tipos estruturados:

                                          Estrutura T          e            vetor de estruturas:   T
                                              v[2];   v

●ou um vetor de ponteiros para o tipo estruturado T:

                                             Estrutura T     e      vetor de ponteiros para estruturas:  T * v[2];
                                  v
Note que, neste último caso, o que está guardado em v[0] e v[1] são endereços de uma estrutura (e não a própria estrutura).

●Um exemplo do primeiro caso, com alocação estática de memória:

     typedef struct ponto Ponto;           int main(void)
     struct ponto {                        {
        float x;                              Ponto v[3]; // vetor de struct estatico
        float y;                              v[0].x = 0.0f;  v[0].y = 0.0f;
     };                                       v[1].x = 10.0f; v[1].y = 0.0f;
                                              v[2].x = 5.0f; v[2].y = 10.0f;
                                              ...
                                              return 0;
                                           }

                                                                                                                                                                                                                     8

<!-- página 9 -->

Vetores e Tipos Estruturados (continuação)

●Um exemplo de vetor de ponteiros para estruturas, com alocação estática de memória:

  typedef struct ponto Ponto;          int main(void){
  struct ponto{                          Ponto * v[3]; // alocacao estatica
     float x;                            Ponto p1, p2, p3;
     float y;                            p1.x = 0.0f; p1.y = 0.0f;
  };                                     p2.x = 10.0f; p2.y = 0.0f;
                                         p3.x = 5.0f; p3.y = 10.0f;
                                         v[0] = &p1;
                                         v[1] = &p2;
                                         v[2] = &p3;
                                         ...
                                      }

●  Quando passamos um vetor de estruturas ou um vetor de ponteiros para estruturas como argumento de uma função, estamos passando apenas o endereço do início
   do vetor. Ou seja: a situação é equivalente, em termos de custo. Mas há uma grande vantagem em usar umarray de ponteiros para estrutura ao invés de umarray de
    estruturas: a memória contígua é menor (apenas ponteiros) e as estruturas podem ficar espalhadas na memória.

●  Mas, se o retorno é a estrutura ou o ponteiro para a estrutura, há uma diferença importante. O custo é muito alto no primeiro caso, porque a cópia do valor de retorno
     lida com uma grande área de memória. E.g.:
   –  caso vetor de struct: Ponto myFunction(Ponto * v, int n)
   –  caso vetor de ponteiros para struct: Ponto * myFunction(Ponto ** v, int n)

     Note que não há como retornar um ponteiro para um Ponto como variável local dentro da myFunction, porque a variável deixa de existir quando a função acaba (i.e. estaríamos
     retornando um valor de endereço inválido). Use malloc!
                                                                                                                                                                                                                     9

<!-- página 10 -->

Exemplos

                                                                                                              10

<!-- página 11 -->

Exercícios com Vetores e Estruturas

1.  Defina estaticamente um vetor v de tipo estruturado Ponto com 3 pontos 2D e escreva uma função centroGeom que calcula o centro
    geométrico destes pontos:
    Ponto centroGeom(Ponto * v, int n).
   Observe que, no retorno, haverá uma cópia custosa da estrutura.

 2. Refaça o exercício anterior definindo estaticamente um vetor v de ponteiros para o tipo estruturado Ponto e modifique a função para
 retornar um ponteiro para Ponto (criado dinamicamente):
    Ponto * centroGeom(Ponto ** v, int n). Note que neste caso não haverá cópia custosa no retorno.

 3.Refaça o exercícios 2 definindo tudo dinamicamente (i.e., o vetor v é dinâmico).

                                                              v2

                                                                                (5.0, 3.3)

                                                          v0                                                                   v1

                                                                                                                                                                                                                      11

<!-- página 12 -->

Gabarito do Exercício 1

#include <stdio.h>                                       Ponto centroGeom(Ponto * v, int n) {
                                                            int i;
typedef struct ponto Ponto;                                 Ponto p = { 0.0f, 0.0f }; // declara e
struct ponto                                                            inicializa p
                                                            for (i = 0; i<n; i++)   {{
                                                               p.x += v[i].x;   float x;
                                                               p.y += v[i].y;   float y;
                                                            }};
                                                            p.x /= n;
                                                            p.y /= n;Ponto centroGeom(Ponto * v, int n);
                                                            return p; // funcao retorna uma copia da
                                                         estruturaint main(void) {
                                                         }   Ponto v[3]; // vetor de struct estatico
   Ponto p;
   v[0].x = 0.0f; v[0].y = 0.0f;
   v[1].x = 10.0f; v[1].y = 0.0f;
   v[2].x = 5.0f; v[2].y = 10.0f;
   p = centroGeom(v, 3);
   printf("Centro Geometrico: (%.1f,%.1f)\n",
                   p.x, p.y);
   return 0;
}

                                                                                                                                                                                                                       12

<!-- página 13 -->

Gabarito do Exercício 2

#include <stdio.h>                                                  Ponto * centroGeom(Ponto ** v, int n){
#include <stdlib.h>                                                    int i;
                                                                       Ponto * p = (Ponto *)malloc(sizeof(Ponto));
typedef struct ponto Ponto;                                            if (p == NULL) return NULL;
struct ponto                                                           p->x = p->y = 0.0f;
{                                                                      for (i = 0; i<n; i++)   {
   float x;                                                               p->x += v[i]->x;
   float y;                                                               p->y += v[i]->y;
};                                                                     }
                                                                       p->x /= n;
Ponto * centroGeom(Ponto ** v, int n);                                 p->y /= n;
                                                                       return p; // funcao retorna um endereco!
int main(void){                                                     }
   Ponto * v[3]; // vetor estatico
   Ponto p1, p2, p3;
   Ponto * p;
   p1.x = 0.0f; p1.y = 0.0f;
   p2.x = 10.0f; p2.y = 0.0f;
   p3.x = 5.0f; p3.y = 10.0f;
   v[0] = &p1;
   v[1] = &p2;
   v[2] = &p3;
   p = centroGeom(v, 3);
   if (p == NULL) exit(1);                                             Note que não há como retornar um ponteiro para um Ponto como
   printf("Centro Geometrico: (%.1f,%.1f)\n", p->x, p->y);               variável local dentro da centroGeom, porque a variável deixa de existir
   free(p);                                                                     quando a função acaba (i.e. estaríamos retornando um valor de
   return 0;                                                                          endereço inválido). Use malloc!
}

                                                                                                                                                                                                                        13

<!-- página 14 -->

Gabarito do Exercício 3 – tudo dinâmico!

#include <stdio.h>                                                                            Ponto * centroGeom(Ponto ** v, int n){
#include <stdlib.h>                                                                               int i;
typedef struct ponto Ponto;                                                                               Ponto * p = (Ponto *)malloc(sizeof(Ponto));
struct ponto{                                                                               if (p == NULL) return NULL;
   float x;                                                                               p->x = p->y = 0.0f;
   float y;                                                                               for (i = 0; i<n; i++)   {
};                                                                                  p->x += v[i]->x;
Ponto * centroGeom(Ponto ** v, int n);                                                                                  p->y += v[i]->y;
                                                                               }
int main(void){                                                                               p->x /= n;
   Ponto ** v = (Ponto **)malloc(3*sizeof(Ponto *));                                                                               p->y /= n;
   Ponto * p;                                                                               return p; // funcao retorna um endereco!
   if (v == NULL) exit(1);                                                                            }
   // montagem manual(... MELHOR USAR UMA FUNCAO):
   v[0] = (Ponto *)malloc(sizeof(Ponto));
   if (v[0] == NULL) exit(1);
   v[0]->x = 0.0f;   v[0]->y = 0.0f;
   v[1] = (Ponto *)malloc(sizeof(Ponto));
   if (v[1] == NULL) exit(1);
   v[1]->x = 10.0f;  v[1]->y = 0.0f;
   v[2] = (Ponto *)malloc(sizeof(Ponto));
   if (v[2] == NULL) exit(1);
   v[2]->x = 5.0f;   v[2]->y = 10.0f;
   p = centroGeom(v, 3);
   if (p == NULL) exit(1);
   printf("CG: (%.1f,%.1f)\n", p->x, p->y);
   free(p);
   free(v[0]); free(v[1]); free(v[2]);
   free(v);
   return 0;
}
                                                                                                                                                                                                                 14

<!-- página 15 -->

Vetores de Ponteiros para Estruturas

●Exemplo:
   – tabela com dados de alunos, organizada em um vetor
   – dados de cada aluno:
         matrícula:  número inteiro
       nome:     cadeia com até 80 caracteres
        endereço:  cadeia com até 120 caracteres
         telefone:   cadeia com até 20 caracteres

                                                                                                                                                                                                                     15

<!-- página 16 -->

Vetores de Ponteiros para Estruturas

●Solução 1:                                 ●Solução 2 (usada no que se segue):
   – Aluno                                        –  tab
    »  estrutura ocupando 4+81+121+21 = 227 Bytes              »  vetor de ponteiros para Aluno
   –  tab                                    »  elemento do vetor ocupa espaço de um ponteiro
    »  vetor de Aluno                                          »  alocação dos dados de um aluno no vetor:

                                                                                               nova cópia da estrutura Aluno é alocada dinamicamente    »  representa um desperdício significativo de memória,                     •
            se o número de alunos bem inferior ao máximo                         •  endereço da cópia é armazenada no vetor de ponteiros
            estimado
   struct aluno {                              » structposição vaziaalunodo vetor:{   valor é o ponteiro nulo
      int mat;                                                int mat;
      char nome[81];                                          char nome[81];
      char end[121];                                          char end[121];
      char tel[21];                                           char tel[21];
   };                                                      };
                                                           typedef struct aluno Aluno;
   typedef struct aluno Aluno;
                                                           #define MAX 100
   #define MAX 100                                         ...
   ....
   Aluno tab[MAX];                                         Aluno* tab[MAX];

                                                                                                                                                                                                                     16

<!-- página 17 -->

Vetores de Ponteiros para
                                     Estruturas●Inicializa - função para inicializar a tabela:
   – recebe um vetor de ponteiros
       (parâmetro deve ser do tipo “ponteiro para ponteiro”)
   –  atribui NULL a todos os elementos da tabela

                   void inicializa (int n, Aluno** tab) {
                      int i;
                      for (i=0; i<n; i++)
                         tab[i] = NULL;
                   }

                                                                                                                                                                                                                     17

<!-- página 18 -->

Vetores de Ponteiros para Estruturas

●Preenche - função para armazenar novo aluno na tabela:
   – recebe a posição onde os dados serão armazenados
   – dados são fornecidos via teclado
   –  se a posição da tabela estiver vazia, função aloca nova estrutura
   –  caso contrário, função atualiza a estrutura já apontada pelo ponteiro

                    void preenche (int n, Aluno** tab, int i) {
                       if (i<0 || i>=n) {
                          printf("Indice fora do limite do vetor\n");
                          exit(1);    /* aborta o programa */
                       }

                       if (tab[i]==NULL)
                          tab[i] = (Aluno*)malloc(sizeof(Aluno));
                       printf("Entre com a matricula:");
                       scanf("%d", &tab[i]->mat);
                       ...
                    }

                                                                                                                                                                                                                     18

<!-- página 19 -->

Vetores de Ponteiros para Estruturas

●Retira - função para remover os dados de um aluno da tabela:
   – recebe a posição da tabela a ser liberada
   –  libera espaço de memória utilizado para os dados do aluno

            void retira (int n, Aluno** tab, int i) {
               if (i<0 || i>=n) {
                  printf("Indice fora do limite do vetor\n");
                  exit(1);    /* aborta o programa */
               }

               if (tab[i] != NULL) {
                  free(tab[i]);
                  tab[i] = NULL;  /* indica que na posição não mais existe dado */
               }
            }

                                                                                                                                                                                                                     19

<!-- página 20 -->

Vetores de Ponteiros para Estruturas

●Imprime - função para imprimir os dados de um aluno da tabela:
   – recebe a posição da tabela a ser impressa

             void imprime (int n, Aluno** tab, int i) {
                if (i<0 || i>=n) {
                   printf("Indice fora do limite do vetor\n");
                   exit(1);    /* aborta o programa */
                }

                if (tab[i] != NULL) {
                   printf("Matrícula: %d\n”, tab[i]->mat);
                   printf("Nome: %s\n”, tab[i]->nome);
                   printf("Endereço: %s\n”, tab[i]->end);
                   printf("Telefone: %s\n”, tab[i]->tel);
                }
             }

•  Imprime_tudo - função para imprimir todos os dados da tabela:
   – recebe o tamanho da tabela e a própria tabela
                   void imprime_tudo (int n, Aluno** tab) {
                      int i;
                      for (i=0; i<n; i++)
                          imprime(n,tab,i);
                   }                                                                                                                                                                       20

<!-- página 21 -->

Vetores de Ponteiros para Estruturas

●Programa de teste

                 #include <stdio.h>

                 int main (void) {
                    Aluno* tab[MAX];
                    inicializa(MAX,tab);
                    preenche(MAX,tab,0);
                    preenche(MAX,tab,1);
                    preenche(MAX,tab,2);
                    imprime_tudo(MAX,tab);
                    retira(MAX,tab,0);
                    retira(MAX,tab,1);
                    retira(MAX,tab,2);
                    return 0;
                 }

                                                                                                                                                                                                                     21

<!-- página 22 -->

Exemplo

#include <stdio.h>                                                    void imprime (int n, Aluno** tab, int i) {
#define MAX 100                                                          if (i<0 || i>=n) {
struct aluno {                                                              printf("Indice fora do limite do vetor\n");
   int mat;                                                                 exit(1);    /* aborta o programa */
   char nome[81];                                                        }
   char end[121];   char tel[21];                                        if (tab[i] != NULL) {
};                                                                          printf("Matrícula: %d\n”, tab[i]->mat);
typedef struct aluno Aluno;                                                 printf("Nome: %s\n”, tab[i]->nome);
void inicializa (int n, Aluno** tab);                                       printf("Endereço: %s\n”, tab[i]->end);
void imprime_tudo (int n, Aluno** tab);                                     printf("Telefone: %s\n”, tab[i]->tel);
void imprime (int n, Aluno** tab, int i);                                }
void retira (int n, Aluno** tab, int i);                              }
void preenche (int n, Aluno** tab, int i);                            void retira (int n, Aluno** tab, int i) {
int main (void) {                                                        if (i<0 || i>=n) {
   Aluno* tab[MAX];                                                         printf("Indice fora do limite do vetor\n");
   inicializa(MAX,tab);                                                     exit(1);    /* aborta o programa */
   preenche(MAX,tab,0);                                                  }
   preenche(MAX,tab,1);
   preenche(MAX,tab,2);                                                  if (tab[i] != NULL) {
   imprime_tudo(MAX,tab);                                                   free(tab[i]);
   retira(MAX,tab,0);                                                       tab[i] = NULL;  /* indica que na posição não mais existe dado */
   retira(MAX,tab,1);                                                    }
   retira(MAX,tab,2);                                                 }
   return 0;                                                          void preenche (int n, Aluno** tab, int i) {
}                                                                        if (i<0 || i>=n) {
#void inicializa (int n, Aluno** tab) {                                     printf("Indice fora do limite do vetor\n");
   int i;                                                                   exit(1);    /* aborta o programa */
   for (i=0; i<n; i++)                                                   }
      tab[i] = NULL;
}                                                                        if (tab[i]==NULL)
                                                                            tab[i] = (Aluno*)malloc(sizeof(Aluno));
void imprime_tudo (int n, Aluno** tab) {                                 printf("Entre com a matricula:");
   int i;                                                                scanf("%d", &tab[i]->mat);
   for (i=0; i<n; i++)                                                   ...
       imprime(n,tab,i);                                              }
}
                                                                                                                                                                                                                      22
