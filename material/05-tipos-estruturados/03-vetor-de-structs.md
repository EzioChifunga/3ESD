# VetorDeTiposEstruturadosVFod

> Fonte: `base/Revisão Tipos Estruturados/VetorDeTiposEstruturadosVFod.pdf`

<!-- página 1 -->

Vetores de
Tipos Estruturados

                                                                                                                                                    1

<!-- página 2 -->

Estruturas

●Uma estrutura pode ser vista como um novo tipo de dado, que é formado por composição de variáveis de outros
   tipos
   – Pode ser declarada em qualquer escopo.
   –  Ela é declarada da seguinte forma:

     struct nomedastruct{                                            char nome[50];
        tipo1  campo1;
        tipo2  campo2;                                                                 int idade;
        tipo3  campo3;
        ...                                                             char gen
        tipoN  campoN;
                                                                      pessoa     }
     typedef struct nomedastruct tipoNovo;

                                                      struct pes{
                                                          char nome[50];
                                                          int idade;
                                                          char gen;
                                                      };
                                                      typedef struct pes tPessoa;

                                                                                                                                                                                                                     2

<!-- página 3 -->

Vetor de Estruturas

Lembrando:
  – struct: define uma composição de dados que podem ser de tipos diferentes;
  – vetor: é uma “coleção” de elementos de mesmo tipo.

  struct pes{
      char nome[50];
      int idade;
      char gen;
  };
  typedef struct pes tPessoa;

  int main(void){            int main(void){
     tPessoa v[4];              int n=4;
                                tPessoa * v= (tPessoa*)malloc(sizeof(tPessoa)*n);

          char nome[50];  char nome[50];  char nome[50];  char nome[50];
          int idade;      int idade;      int idade;      int idade;
          char gen;       char gen;       char gen;       char gen;

                             v[1]                         v[3]             v[0]                         v[2]

                                                                                                                                                                                                                   3

<!-- página 4 -->

Vetores de Estruturas

●Exemplo:
   – função para calcular o centro geométrico de conjunto de pontos
    » entrada: vetor de estruturas definindo o conjunto de pontos
    » saída:  centro geométrico, dado por:

                                                                                                                                                                                                                     4

<!-- página 5 -->

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

                                                                                                                                                                                         5

<!-- página 6 -->

Vetores de Estruturas

   Ponto centro_geom (int n, Ponto* v) {
      int i;
      Ponto p = {0.0f, 0.0f};  /* declara e inicializa ponto */
      for (i=0; i<n; i++) {
         p.x += v[i].x;
         p.y += v[i].y;
      }                              Como v é um
      p.x /= n;                                      vetor de
      p.y /= n;                                estruturas…
      return p;
   }

                             Função retornando estrutura:
                                     •  para estruturas pequenas, este recurso facilita o uso da função
                                     •  para estruturas grandes, a cópia do valor de retorno pode ser cara
– função retornando estrutura:
  » para estruturas pequenas, este recurso facilita o uso da função
  » para estruturas grandes, a cópia do valor de retorno pode ser caro

                                                                                                                                                                                                              6

<!-- página 7 -->

Vetores de Estruturas

●Exemplo:
   – função para calcular a área de um polígono plano
      delimitado por uma sequência de n pontos
    » a área do polígono é a soma das áreas dos trapézios
          formados pelos lados do polígono e o eixo x                                      y                                pi+1                                                                                                                                                               pi
    » a área do trapézio definido pela aresta
         que vai do ponto pi ao ponto pi+1 é dada por:                                                                                    yi               yi+1

    » algumas “áreas” são negativas
    »  as áreas externas ao polígono são anuladas                                                                                        xi               xi+1         x
    » se a sequência de pontos do polígono
            for dada em sentido anti-horário,
          a “área” terá valor negativo e
          a área do polígono é o valor absoluto do resultado da soma.

                                                                                                                                                                                                                     7

<!-- página 8 -->

Vetores de Estruturas

       float area (int n, Ponto* p) {
          int i, j;
          float a = 0;
          for (i=0; i<n; i++) {
             j = (i+1) % n;          /* próximo índice (incremento circular) */
             a += (p[ j ].x - p[ i ].x)*(p[ i ].y + p[ j ].y)/2;
          }
          return fabs(a);
       }

fabs
  » função definida em math.h
  »  retorna o valor absoluto de um valor real

                                                                                                                                                                                                              8

<!-- página 9 -->

Calculando o número de registros ( structs) do arquivo

#include <stdio.h>              long calcularTamanhoArquivo (char *nomearq) {
/*Aluno: nome, a                     /*fseek – movimenta a posição corrente de escrita ou leitura no arquivo para um local
matrícula, faltas e  4            específico
notas */                                ftell – retorna a posição corrente de leitura ou escrita no arquivo (em bytes)*/
                                                                                                            corrente de leitura: início do arquivostruct al{                          FILE *arquivo=fopen(nomearq,"rb");//posição
  char nome[30];                    long tamanho;
  int matr;
                                                                                            // movimenta a posição corrente de leitura no arq para o fim  int faltas;                      fseek(arquivo, 0, SEEK_END);
                                                                                        posição do ponteiro do arquivo (byteoffset). no final do arquivo  float vNotas[4];                  tamanho = ftell(arquivo);//
};                                  fclose(arquivo);
typedef struct al tAluno;           return tamanho;
                                }

                                      int main(void) {
                                         FILE * arqE;
void exibe(tAluno *al){                 long nReg;
  printf("\n%s %d %d ",                 nReg=calcularTamanhoArquivo ("alunos.dat")/sizeof(tAluno);
    al->nome,al->matr,al->faltas);      printf("\n\n%ld",nReg);
  for(int i=0;i<4;i++){                 tAluno *v=(tAluno*)malloc(nReg*sizeof(tAluno));
    printf(" %.1f                      arqE=fopen("alunos.dat","rb");
",al->vNotas[i]);                       fread(v,sizeof(tAluno),nReg,arqE);
  }                                    fclose(arqE);
}                                       for(int i=0;i<nReg;i++)
                                          exibe(&v[i]);
                                         return 0;
                                      }

                                                                                                                                                                                                                          9
