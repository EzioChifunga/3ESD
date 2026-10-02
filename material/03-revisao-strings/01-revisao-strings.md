# RevisãoStrings

> Fonte: `base/Revisão Strings/RevisãoStrings.pdf`

<!-- página 1 -->

Revisão
Cadeias de Caracteres
        (Strings)

                                                      1                           1

<!-- página 2 -->

Tópicos

•  Revisão Caracteres
•  Cadeias de caracteres
   – Leitura de caracteres e cadeias de caracteres
   – Exemplos de funções que manipulam cadeias de caracteres
   – Funções recursivas para cadeias de caracteres
   – Constante cadeia de caracteres
•  Vetor de cadeias de caracteres

                                                                                                                                                                2

<!-- página 3 -->

Revisão Caracter

•  tipo char:

   – tamanho de char = 1 byte = 8 bits = 256 valores distintos

  COMO O TIPO CHAR É REPRESENTADO INTERNAMENTE?

   – tabela de códigos:  nº    caracter

        • define correspondência entre caracteres e códigos numéricos

       • exemplo: ASCII
       • alguns alfabetos precisam de maior representatividade
          –  alfabeto chinês tem mais de 256 caracteres

                                                                                                                                                                3

<!-- página 4 -->

Tabela ASCII

                                               4

<!-- página 5 -->

Códigos ASCII de alguns caracteres de controle

●

                     0    nul     null: nulo

                     7    bel     bell: campainha

                     8     bs     backspace: volta e apaga um caractere

                     9     ht     tab: tabulação horizontal

                     10    nl     newline ou line feed: muda de linha

                     13    cr     carriage return: volta ao início da linha

                    127   del     delete: apaga um caractere

                                                                                                                                                               5

<!-- página 6 -->

Caracteres

•  Constante de caractere:
   –  caractere envolvido com apóstrofe ou aspas simples
   – exemplo:
       •  'a' representa uma constante de caractere
       •  'a' resulta no valor numérico associado ao caractere a

  char c = 'a';
  printf("%d %c\n", c, c);

    • printf imprime o conteúdo da variável c usando dois formatos:
          – com o formato para inteiro, %d, imprime 97
          – com o formato de caractere, %c, imprime a (código 97 em ASCII)

                                                     6

                                                                                                                                                                6

<!-- página 7 -->

Usando a Representação dos CaracteresI

 char maiuscula(char c) {
   /* Verifica de é letra minúscula */
   if (c >= 'a' && c <= 'z')
     c = (c – 'a') + 'A';
   return c;
 }

int algarismonumero(char c) {
      int d;
  /* Verifica de é um algarismo */
  if (c >= '0' && c <= 9')
    d = (c – '0') ;
  return d;
}

                                                                                                                                                      7

<!-- página 8 -->

Leitura de Caracteres - scanf

•  scanf com o especificador de formato %c (cont.):
   – não pula os "caracteres brancos"
       • "caractere branco" = espaço (' '), tabulação ('\t') ou nova linha ('\n')
   – se o usuário teclar um espaço antes da letra:
       • o código do espaço será capturado
       • a letra será capturada apenas na próxima chamada de scanf
   – para pular todos os "caracteres brancos" antes do caractere:
       • basta incluir um espaço em branco no formato, antes do especificador

     char a;
     ...
     scanf(" %c", &a);  /* o branco no formato pula brancos da entrada */
     ...

                                                                     8

                                                                                                                                                                8

<!-- página 9 -->

Cadeias de caracteres - Strings

•  Representação de cadeia de caracteres:
   – vetor do tipo char, terminado pelo caractere nulo ('\0')
       • é necessário reservar uma posição adicional no vetor
       para o caractere de fim da cadeia
   – função para manipular cadeias de caracteres:
       • recebe como parâmetro um vetor de char
       • processa caractere por caractere até encontrar o caractere
        nulo, sinalizando o final da cadeia

  String: sequência de caracteres finalizada pelo '\0'

                                                                                                                                                                9

<!-- página 10 -->

Declaração e Inicialização de Cadeias

 •  Inicialização de cadeias de caracteres:
   – caracteres entre aspas duplas
   – caractere nulo é representado implicitamente
   – Exemplo:
        • variável cidade dimensionada e inicializada com 4 elementos

int main ( void )                              int main ( void )
{                              {
   char cidade[ ] = "Rio";                                 char cidade[ ]={'R', 'i', 'o', '\0'};                    ≡   printf("%s \n", cidade);                                 printf("%s \n", cidade);
   return 0;                                 return 0;
}                              }

                 Rio

                  Press any key to continue
                                                                                                                                                                 10

<!-- página 11 -->

Declaração e Inicialização de Cadeias

•  Exemplos:

               char s1[] = "Rio de Janeiro";
               char s2[81];
               char s3[81] = "Rio";
                 char s4[] = "";

s1  é uma cadeia de 14 caracteres armazenada em vetor com 15 elementos, inicializada com o
   conteúdo declarado e terminada com '\0';

s2 pode armazenar uma cadeia com até 80 caracteres em um vetor com 81 elementos (já que uma
    cadeia de caracteres sempre tem que ser terminada com o caractere '\0', que ocupa uma posição
   a mais no vetor). s2 não foi inicializada;

s3 pode armazenar uma cadeia com até 80 caracteres em um vetor com 81 elementos, mas apenas
    os quatro primeiros elementos são inicializados com 'R', 'i', 'o' e '\0', respectivamente.
s4  é uma cadeia de caracteres vazia (vetor com 1 elemento que armazena apenas o caractere '\0');

                                                             s4

                                                          0

                                                                             \0
                                                                                                                                                                 11

<!-- página 12 -->

Leitura de Cadeias de caracteres

•  Leitura de caracteres e cadeias de caracteres
   – através de scanf
   – especificadores de formato definem o comportamento do scanf

                                                                                                                                                                12

<!-- página 13 -->

Leitura de Cadeias

•  scanf com o especificador de formato %s
   –  lê uma cadeia de caracteres não brancos
     – "caractere branco" = espaço (' '), tabulação ('\t') ou nova
         linha ('\n')
   – pula os eventuais caracteres brancos antes da cadeia
   – exemplo:

       char cidade[81];
       ...
       scanf("%s", cidade);
       ...

       • &cidade NÃO é usada pois a cadeia é um vetor
       • o código acima funciona apenas para capturar nomes simples
          –  se o usuário digitar Rio de Janeiro, apenas Rio será capturada,
                pois %s lê somente uma seqüência de caracteres não brancos

                                                     13

                                                                                                                                                                13

<!-- página 14 -->

Leitura de Cadeias

•  scanf com o especificador de formato %[...]
   –  %[...] lista entre os colchetes todos os caracteres
             aceitos na leitura
   –  %[^...]     lista entre os colchetes todos os caracteres
          não aceitos na leitura
   – exemplos:
       • %[aeiou]
          –  lê seqüências de vogais
          –  leitura prossegue até encontrar um caractere que não seja uma vogal
       • %[^aeiou]
          –  lê seqüências de caracteres que não são vogais
          –  leitura prossegue até encontrar um caractere que seja uma vogal

                                                                                                                                                                14

<!-- página 15 -->

Leitura de Cadeias

• Como permitir a leitura de palavras compostas com scanf?
   scanf (" %80[^\n]", string);"

                     aceita qualquer caracter exceto o enter

        "pula brancos da frente"

   Lê seqüencias de caracteres até encontrar o caractere de mudança de linha ('\n’)

       • captura linha fornecida pelo usuário até que ele tecle
       "Enter"
       • inclusão do espaço no formato garante que eventuais
        caracteres brancos que precedam a cadeia de caracteres
       sejam descartados
       • Garante que são lidos no máximo 80 caracteres  o vetor
        definido com 81 posições

                                                                     15

                                                                                                                                                                15

<!-- página 16 -->

Funções da biblioteca string.h

•  strlen – determina o comprimento de uma cadeia
•  strcpy – copia uma cadeia origem para outra destino
•  strcmp – compara duas cadeias
•  strcat – concatena duas cadeias

                                                                                                                                                                16

<!-- página 17 -->

Cadeias de caracteres

•  Exemplos:

      char s1[] = "Rio de Janeiro";

   –  s1 é um vetor de char, inicializado com a cadeia Rio de Janeiro, seguida do caractere
        nulo

   –  s1 ocupa 15 bytes de memória

   –  é válido escrever s1[0]='X', alterando o conteúdo da cadeia para Xio de Janeiro, pois s1
       é um vetor, permitindo alterar o valor de seus elementos

      char* s2 = "Rio de Janeiro";

   –  s2 é um ponteiro para char, inicializado com o endereço da área de memória onde a
        constante Rio de Janeiro está armazenada

   –  s2 ocupa 4 bytes (espaço de um ponteiro)

   – não é válido escrever s2[0]='X', pois não é possível alterar um valor constante

                                                                                                                                                                17

<!-- página 18 -->

Ponteiros e Strings

cidade: constante que                            cidade: constante que
armazena o endereço                       armazena o endereço
     inicial do vetor                                             inicial do vetor

 char cidade[4] ;                      char cidade[4] ="Rio";
 cidade = "Rio";                      char *  city;
 ERRADO!!!!                              city = "Rio";
                             CERTO!!!

    Tentativa de                            Atribuição do endereço
      atribuir o                          da constante "Rio" à
   endereço da                                   variável city
 constante "Rio" à
  constante cidade

                                                                                                                                           18

<!-- página 19 -->

Geram a mesma saída?

int main(void) {                    int main(void) {
      char * mensagem = "Ola";            char  mensagem[] = "Ola";
      printf("%s", ++mensagem);           printf("%s", ++mensagem);
      return 0;                           return 0;
}                                   }

                                   ERRO DE COMPILAÇÃO:  Exibe la
                                                 tentativa de modificar
  Ponteiro variável
                                             ponteiro constante

                                                                                                                                                             19

<!-- página 20 -->

Vetor de cadeia de caracteres

●Alocação de vetor de cadeia de caracteres:
   – alocação estática:
    » alocação como matriz estática de elementos do tipo char
   – alocação dinâmica:
    » alocação como vetor de ponteiros
    » cada cadeia de caracteres (elemento do vetor)
      é alocada dinamicamente

                                                                                                                                                               20

<!-- página 21 -->

Vetor de Strings – Alocação Estática

Declaração:
   char  nomeVetStr[nºdeStrings][nºdeCaracteresdasStrings];

Como acessar uma string individual?
  nomeVetStr[nºstr]
Como acessar uma letra de uma string do vetor?
  nomeVetStr[nºstr][nºLetra]

     char
     alunos[50][81];={"Ana","Pedro","Jose","Maria"};
     ...
     i = 2;
     j=3
     printf("%s\n", alunos[i]);  // exibe Jose
     printf("%c\n", alunos[i][j]); // exibe e
  }

      (alunos[ i ][ j ] acessa a (j+1)-ésima letra do nome do (i+1)-ésimo aluno)
                                                                     21

                                                                                                                                                               21

<!-- página 22 -->

Declaração e Inicialização

#include <stdio.h>
int main(void)
{
  char meses[12][20]={"Janeiro","Fevereiro" , "Março“,
                            "Abril", "Maio", "Junho",
                            "Julho","Agosto","Setembro","Outubro",
                           "Novembro","Dezembro"};
     int dia, mês, ano;
     printf("Digite o dia,mes e ano do evento: );
     scanf("%i%i%i",&dia,&mes,&ano);
     printf("\nRio, %d de %s de %i",
           dia,meses[mes-1],ano);
     return 0;
 }

                                                                                                                                                             22

<!-- página 23 -->

Usando um Vetor Strings Alocado Estaticamente:
                              Pode entrar na festa?

int busca ( char vstr[][7], int n, char str[]){
     int i;
     for (i=0; i < n; i++){
          if (strcmp(vstr[i],str) == 0)
          return i;
      }
     return -1;
}
                                                                                                                               list[0]    C  a    r      l    o   s    \o

                                                                                                                               list[1]   A  n   a   \0
int main(void){                                                                                       list[2]    P  e  d   r   o   \0
 char nome[40];                                                                                      list[3]   A  n  d   r    e   \0
 char list[4][7]= {"Carlos", "Ana", "Pedro", "Andre"};

 printf ("Digite seu nome:"); scanf(" %6[^\n], nome);
 if ( busca( list,4,nome) !=-1)
  printf ("Você pode entrar.");
 else
  printf ("Você não pode entrar.");
 return 0;
}

                                                                                                                                                             23

<!-- página 24 -->

Usando um Vetor de ponteiros para Strings:
                              Pode entrar na festa?

int busca ( char vstr[][7], int n, char str[]){
     int i;
     for (i=0; i < n; i++){
          if (strcmp(vstr[i],str) == 0)
          return i;
      }                                                                                                 list[0]       C   a    r      l    o   s    \o
                                                                                                                   list[1]      A  n   a   \0
     return -1;                                                                                                                   list[2]       P   e  d   r   o   \0
}                                                                                                                 list[3]      A  n  d   r    e   \0

int main(void){
 char nome[40];
 char list[4]= {"Carlos", "Ana", "Pedro", "Andre"};

 printf ("Digite seu nome:"); scanf(" %6[^\n], nome);
 if ( busca( list,4,nome) !=-1)
  printf ("Você pode entrar.");
 else
  printf ("Você não pode entrar.");
 return 0;
}

                                                                                                                                                             24

<!-- página 25 -->

Inicialização de um Vetor de Strings x
                         Vetor de ponteiros para Strings

●Versão Vetor de Strings

                                                    list[0]    C  a    r      l    o   s    \o

                                                    list[1]   A  n   a   \0
                                                    list[2]    P  e  d   r   o   \0
                                                    list[3]   A  n  d   r    e   \0

●Versão Vetor de Ponteiros

                                              list[0]       C   a    r      l    o   s    \o
                                              list[1]      A  n   a   \0
                                              list[2]       P   e  d   r   o   \0
                                              list[3]      A  n  d   r    e   \0

                                                                                                                                                               25

<!-- página 26 -->

Como criar a lista de convidados com nomes fornecidos pelo usuário?

Estaticamente                               int main(void){
int preenche ( char * vstr[], int max);          char nome[40], list[MAX][41];
{ char nome[40];                                  int n;
  int i=0;                                        n=preenche( list, MAX);
   printf ("Digite o nome do convidado:");        printf ("Digite seu nome:");
        scanf(" %40[^\n], nome);                 scanf(" %6[^\n], nome);
  while ( i<max && strlen(nome) >0){              if ( busca( list,n,nome) != -1)
   strcpy(vstr[i],nome) ;                          printf ("Você pode entrar.");
   printf ("Digite o nome do convidado:");       else
   scanf(" %40[^\n], nome);                        printf ("Você não pode entrar.");
   i++;                                          return 0;
  }                                           }
  return i; }

                                                                                                                                                             26

<!-- página 27 -->

Como criar a lista de convidados com nomes fornecidos pelo usuário?

int preenche ( char * vstr[], int max);
int busca ( char * vstr[], int n, char str[]);
int main(void){
    char nome[40], list[MAX][41];
    int n;
    n=preenche( list, MAX);
    printf ("Digite seu nome:");
    scanf(" %6[^\n], nome);
    if ( busca( list,n,nome) != -1)
     printf ("Você pode entrar.");
    else
     printf ("Você não pode entrar.");
    return 0;
}

                                                                                                                                                                27

<!-- página 28 -->

Criando a lista de convidados estática com nomes digitados

int main(void){
   char nome[40], list[MAX][41];
   int n;
   int i=0;
   printf ("Digite o nome do convidado ou enter para finalizar:");
   scanf(“ %40[^\n], nome);

   while (i<max && strlen(nome) >0){
    strcpy(vstr[i],nome) ;
    printf ("Digite o nome do convidado ou enter:");
    scanf(“ %40[^\n], nome);
    i++;
   }
   n=i;
   ……
  }

                                                                                                                                                                28

<!-- página 29 -->

Criando a lista de convidados dinâmica com nomes digitados

int main(void)
{
                                                                                                           list[0]
   char nome[40];                                                              list[1]
   char * list[MAX];                                                      list[2]
   int n,i=0;                                                                         list[3]

   printf ("Digite o nome do convidado ou enter para finalizar:");
   scanf(" %40[^\n], nome);
   while( i<0 && strlen(nome )!=0){
    strcpy(list[i],nome) ;
    printf ("Digite o nome do convidado ou enter:");
    scanf(" %40[^\n], nome);
    i++;
   }

         ERRADO!!!!!!!

 Não há área reservada para armazenar as strings

                                                                                                                                                                29

<!-- página 30 -->

Criando a lista de convidados dinâmica com nomes digitados

int main(void)
{
                                                                                                           list[0]
   char nome[40];                                                              list[1]
   char * list[MAX];                                                      list[2]
   int n,i=0;                                                                         list[3]

   printf ("Digite o nome do convidado ou enter para finalizar:");
   scanf(" %40[^\n], nome);
   while( i<0 && strlen(nome )!=0){
    list[i] = (char*) malloc ((n+1)*sizeof(char));
    strcpy(list[i],nome) ;
    printf ("Digite o nome do convidado ou enter:");
    scanf(" %40[^\n], nome);
    i++;
   }

                                                                                                                                                                30

<!-- página 31 -->

Criando a lista dinâmica de convidados usando a função duplica

int main(void)
{                          Como a área para as strings foi alocada
   char nome[40];                dinamicamente, elas devem ser liberadas
   char * list[MAX];               quando não são mais necessárias
   int n,i=0;

   printf ("Digite o nome do convidado ou enter para finalizar:");
   scanf(" %40[^\n], nome);

   while (i<MAX &&  (strlen(nome)) !=0){
    *(list+i )= duplica(nome);
    printf ("Digite o nome do convidado ou enter:");
    scanf(" %40[^\n], nome);
    i++;
 }

     char* duplica (char* s){
        int n = strlen(s);
        char* d = (char*) malloc ((n+1)*sizeof(char));
        strcpy(d,s);
        return d;
     }

                                                                                                                                                                31

<!-- página 32 -->

Criando a lista dinâmica de convidados usando a função duplica

int main(void)
{
  char nome[40];
  char * list[MAX];
  int n,i=0;

  printf ("Digite o nome do convidado ou enter para
   finalizar:");
   scanf(" %40[^\n], nome);

  while (i<MAX &&  (strlen(nome)) !=0){
   *(list+i )= duplica(nome);
   printf ("Digite o nome do convidado ou enter:");
   scanf(" %40[^\n], nome);
   i++;
  }
 ……….
 for(i=0; i<n i++){
   free(list[i]);
 }

                                                                                                                                                                32

<!-- página 33 -->

Criando a lista dinâmica de convidados usando a função duplica

int main(void)
{
   char nome[40];
   char * list[MAX];
   int n;

   n = preenche( list);
   ……….                         int preenche ( char**list )

   int preenche ( ?????????????????  )
   {
          char nome[41]; int i;
          printf ("Digite o nome do convidado ou enter:");
      scanf(" %40[^\n], nome);
      while (i<0 &&  (strlen(nome)) !=0)    {
          list[i] = duplica(nome);
          printf ("Digite o nome do convidado ou enter:");
          scanf(" %40[^\n], nome);
          i++;
      }
      return i;
   }

                                                                                                                                                                33

<!-- página 34 -->

Resumo

 caracteres
– representados pelo tipo char, com o auxílio de tabela de códigos (de caracteres)

 cadeia de caracteres
– representada por vetor do tipo char, terminada pelo caractere nulo ('\0')

 inicialização de cadeia de caracteres
– caracteres entre aspas duplas

                                                                                                                                                          34

<!-- página 35 -->

Resumo

 leitura de caracteres e cadeias de caracteres

– através de scanf com especificadores de formato

     %c   lê o valor de um único caractere fornecido via teclado
          não pula os "caracteres brancos"

     %s   lê uma cadeia de caracteres não brancos
            pula os eventuais caracteres brancos antes da cadeia

        %[...]       lista entre os colchetes todos os caracteres aceitos na leitura
        %[^...]      lista entre os colchetes todos os caracteres não aceitos

                                                                                                                                                          35

<!-- página 36 -->

Resumo

Biblioteca de cadeias de caracteres string.h

"comprimento" strlen
"copia"         strcpy
"concatena"     strcat
"compara"strcmp

                                                                                                                                                        36
