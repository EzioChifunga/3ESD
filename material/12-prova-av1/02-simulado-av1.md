# SimuladoAV1_ESD

> Fonte: `base/Prova/SimuladoAV1_ESD.pdf`

<!-- página 1 -->

AV1 – ESD - 20251

Questão 1) (0.3 décimos por item)
QUESTÃO 1) 3.0 pontos
    1.  Sobre estruturas de dados elementares, assinale a alternativa INCORRETA.
             a) Em uma pilha, é implementada uma política LIFO (last-in, first-out).
            b) Ao tentar extrair algo de uma pilha vazia, acontece um erro de estouro negativo.
              c) Em uma lista encadeada, a ordem dos objetos é definida pelo índice da lista.
            d) Nas filas, a política implementada é a FIFO (first-in, first-out).
             e) O atributo topo de uma pilha indexa o elemento mais recentemente inserido.

    2.  Considere as seguintes estruturas de dados com as propriedades definidas a seguir:
             I - Inserção e remoção de elementos acontecem apenas na “cabeça” da estrutura.
             II - Inserção de um nó no meio da estrutura pode ser realizada com custo computacional constante.
             III - Respeito à política FIFO: o primeiro elemento que entra é o primeiro a sair.
       As descrições acima se referem às seguintes estruturas, respectivamente,
             a)  pilha, lista, fila.
            b)   fila, pilha, lista.
              c)    lista, pilha, fila.
            d)   pilha, fila, lista.

    3.  Considere que os números 10, 11, 12, 13, 14 foram inseridos, nessa ordem, em uma fila. Esses mesmos
      números foram inseridos na mesma ordem em uma pilha. Nesse caso,
             a)  o topo da pilha é o número 10.
            b)  o primeiro elemento a ser removido da pilha é o número 10.
              c)  o número 14 é o primeiro elemento a ser removido da fila.
            d)  o último elemento a ser removido da fila é o número 14.

    4.  Sobre o tema, Estrutura de Dados, analise as assertivas e assinale a alternativa correta:
                          I. Pilhas - São estruturas de dados do tipo LIFO (last-in first-out), onde o último elemento a ser
                inserido, será o primeiro a ser retirado. Assim, uma pilha permite acesso a apenas um item de dados
                     - o último inserido. Para processar o penúltimo item inserido, deve-se remover o último.
                          II. FILAS - São estruturas de dados do tipo FIFO (first-in first-out), onde o primeiro elemento a ser
                inserido, será o primeiro a ser retirado, ou seja, adiciona-se itens no fim e remove-se do início.
                          III. Lista linear é uma estrutura de dados na qual elementos de um mesmo tipo de dado estão
              organizados de maneira sequencial. Não necessariamente, estes elementos estão fisicamente em
               sequência, mas a ideia é que exista uma ordem posicional entre eles.
                  IV. Em uma lista linear em alocação sequencial, cada nó é formado por campos que armazenam
                 características distintas dos elementos da lista. Cada nó da lista pode possuir um identificador
            denominado chave, e a ordem dos elementos se dá pela contiguidade física
            a)  Apenas I e III estão corretas.
            b)  Apenas II e III estão corretas.
             c)  Apenas III e IV estão corretas.
            d)  Todas estão corretas.

    5. A pilha P e a fila F possuem originalmente n elementos cada (n>5), e suas operações são:
       a)  empilha(P, elemento): inserir elemento na pilha P;desempilha(P): remover da pilha P e retornar o
          elemento removido;
       b)  enfileira(F, elemento): inserir elemento na fila F;desenfileira(F), remover da fila F e retornar o elemento
           removido.
        Seja o pseudocódigo abaixo:
     para i = 1 até n, faça
           empilha(P, desempilha(P))
           enfileira(F, desenfileira(F))
     fim-para

      Ao final da execução do pseudocódigo, os estados finais de P e F são, respectivamente:

<!-- página 2 -->

AV1 – ESD - 20251

        a)  elementos em ordem original e elementos em ordem original.
        b)  elementos em ordem inversa e elementos em ordem inversa.
         c)  elementos em ordem original e elementos em ordem inversa.
        d)  elementos em ordem inversa e elementos em ordem original.

6. O Round-Robin é um tipo de escalonamento preemptivo mais simples e consiste em repartir uniformemente
   o tempo da CPU entre todos os processos prontos para a execução. Os processos são organizados em uma
    estrutura de dados, alocando-se a cada um uma fatia de tempo da CPU, igual a um número de quanta. Caso
  um processo não termine dentro de sua fatia de tempo, retorna para o fim da estrutura e uma nova fatia de
   tempo é alocada para o processo que está no começo da estrutura e que dela sai para receber o tempo de
   CPU.
   A estrutura de dados utilizada nesse tipo de escalonamento é:
a)  pilha.
b)  lista duplamente encadeada
c)   fila circular.
d)   fila simples.
e)   lista simplesmente encadeada

7.  Considerando as estruturas de dados pilhas e filas, é correto afirmar que:
          a política de atendimento aos jobs por um único processador em tempo compartilhado,
          implementada por fila circular, seria adequada para controlar a fila de arquivos a serem impressos
        numa impressora.
        uma pilha é conhecida como lista FIFO - First In First Out e uma fila é conhecida como lista LIFO - Last
             In First Out. Pessoas esperando por atendimento e pratos empilhados em uma mesa de restaurante
           são exemplos tipicamente associados a estas estruturas.
           as filas são as estruturas mais adequadas para a recuperação de elementos na ordem inversa em
          que foram inseridos, já que o último elemento inserido é o primeiro a ser retirado.
          a pilha (stack) é usada pelo Sistema Operacional para armazenar informações sobre as subrotinas
            ativas num programa de computador. Quem invoca a subrotina empilha o endereço de retorno;
         quando termina sua execução, a subrotina invocada desempilha o endereço de retorno.
           tanto a pilha quanto a fila são controladas por apenas 4 operações básicas: verifica se a estrutura
            está vazia, insere um elemento na estrutura, retira um elemento da estrutura e obtém todos os
          elementos da estrutura em ordem crescente ou decrescente.

8. A maioria dos softwares de aplicação possui comandos de "Desfazer" e "Refazer". O primeiro desfaz a última
   operação ou texto digitado, enquanto que, o segundo refaz uma operação ou texto desfeito, conforme
   sugerem os nomes dos comandos.
    Internamente, nos softwares, podem ser usadas duas estruturas de dados que armazenam as sucessivas
   operações de "Desfazer" e "Refazer", de modo que o próximo "Refazer" sempre recupera o último
    "Desfazer". Os tipos de estrutura de dados que podem ser usados para "Desfazer" e "Refazer" são,
    respectivamente:
        a)  Fila e Fila
        b)  Fila e Pilha
         c)  Pilha e Fila
        d)  Pilha e Pilha
        e)  Pilha e Fila duplamente encadeada

9.  Acerca da estrutura de dados do tipo filas, considere as operações de inserção e remoção de uma fila F
    abaixo:
            1. enfileira ('amarelo', F)
            2. enfileira ('branco', F)
            3. enfileira ('verde', F)
            4. enfileira ('vermelho', F)
            5. desenfileira (F)
            6. desenfileira (F)

<!-- página 3 -->

AV1 – ESD - 20251

               7. enfileira ('azul', F)
               8. enfileira (desenfileira (F), F)

     O resultado final das operações resulta em:
            a)  [verde, azul, vermelho].
            b)  [branco, azul, amarelo].
             c)  [verde, azul].
            d)  [amarelo, branco].
            e)  [vermelho, azul, verde].

    10. A armazenagem de dados é importante para que, quando for necessário, esses dados possam ser
       apresentados automaticamente em uma ordem predeterminada. Os dados podem ser recuperados na
      mesma ordem de armazenamento ou em ordem diferente. Considerando que as estruturas de dados
        auxiliam na realização de tais operações, assinale a opção correta.
             a)  Considere que cinco documentos (doc1, doc2, doc3, doc4 e doc5) estejam em uma fila para
              impressão e que o operador deseje imprimir imediatamente os documentos doc1, doc3 e doc5,
              deixando doc2 e doc4 em ordem na fila. Considere, ainda, que a operação de fila insere (x) insira o
             elemento x no final da fila, e que remove( ) seja a função que promove o retorno do elemento
                retirado à fila. Nessa situação, a sequência de operações para o resultado pretendido é: remove( ),
               insere(remove( )), remove( ), insere(remove( )), remove( ).
            b) Na implementação das listas de encadeamento circulares, cada elemento da lista possui um
               ponteiro para o elemento que o precede. Assim, ao se retirar um elemento da lista, pode-se definir o
               ponteiro next do elemento precedente para o elemento posterior àquele que será removido.
              c)  Considere que Roberto tenha feito uso de uma lista encadeada simples para programar o
             armazenamento e o posterior acesso aos dados acerca dos equipamentos instalados em sua
              empresa. Considere, ainda, que, após realizar uma consulta acerca do equipamento X, Roberto
               precisou acessar outro equipamento Y que se encontrava, nessa lista, em posição anterior ao
             equipamento X. Nessa situação, pela forma como os ponteiros são implementados em uma lista
             encadeada simples, o algoritmo usado por Roberto realizou a consulta ao equipamento Y sem
                  reiniciar a pesquisa do começo da lista.

10. Prova: IF-SP - 2019 - IF-SP - Informática
Abaixo tem-se uma tabela que ilustra o conjunto de nós de uma lista duplamente encadeada, contendo o total de 5 nós.
 elemento   Anterior   Próximo  conteúdo
     1         3        4         ?
     2                  5         ?
     3         5        1         ?
     4         1                   ?
     5         2        3         ?

Ao imprimir a estrutura na ordem correta, o conteúdo apresentado será I – F – S – P – 2019, dessa forma, assinale a alternativa
que contém os dados que preenchem, corretamente, a coluna “conteúdo”, de cima para baixo.
Alternativas
      f)  P – I – S – 2019 – F
     g)  2019 – P – S – F – I
    h)  S – P – I – F – 2019
       i)   S – I – F – 2019 – P
---------------------------------------------------------------------------------
11. Prova: COMVEST UFAM - 2019 - UFAM - Técnico de Laboratório - Tecnologia da Informação
Assinale a proposição VERDADEIRA:
Alternativas
    e)  Na busca binária o vetor não precisa estar ordenado.
      f)  Na busca sequencial o vetor não precisa estar ordenado.
     g)  Na busca sequencial o vetor precisa estar ordenado.
    h)  A busca sequencial sempre garante que o elemento a ser procurado será encontrado.
       i)  A busca binária sempre garante que o elemento a ser procurado será encontrado.
---------------------------------------------------------------------------------

<!-- página 4 -->

AV1 – ESD - 20251

 12. DECAN - Técnico de Informática – 2021
A principal vantagem da lista encadeada sobre a lista sequencial estático é:
a) Acesso direto
b) Menor uso de memória
c Inserção e remoção sem necessidade de deslocamento de elementos
d) Facilidade de ordenação
e) Acesso aleatório

<!-- página 5 -->

AV1 – ESD - 20251

---------------------------------------------------------------------------------
13.. Prova: Instituto Access - Instituto de Acesso à Educação, Capacitação Profissional e Desenvolvimento Humano - UFFS - Técnico de Tecnologia da Informação
- 2023
Observe o quadro I – DEFINIÇÕES, que se refere à estrutura de dados PILHA, e que suporta três operações básicas, descritas a
seguir:

Quadro I - DEFINIÇÕES
OPERAÇÃO      SIGNIFICADO

PUSH(P,e)        Insere um elemento qualquer na pilha P.

POP(P)        Remove o elemento de topo na pilha P.

TOP(P)           Acessa, sem remover, o elemento do topo da pilha P.

Quadro II - OPERAÇÕES
SEQUÊNCIA DE OPERAÇÕES

1     Push(SUL,QUARAÍ)

2     Push(SUL,ITAQUI)

3     Push(SUL,URUGUAIANA)

4     Push(SUL,LIVRAMENTO)

5     Top(SUL)

6      Push(SUL,Pop(SUL))

7     Push(SUL,SANTANA)

8      Push(SUL,Top(SUL))

9     Pop(SUL)

Tendo como ponto de partida uma pilha SUL inicialmente vazia e a sequência de operações indicadas no quadro II - OPERAÇÕES,
ao final das operações o elemento que se encontra no topo da pilha é
Alternativas
     a)  ITAQUI.
    b)  QUARAÍ.
     c)  SANTANA.
    d)  LIVRAMENTO.
    e)  URUGUAIANA
---------------------------------------------------------------------------------
14 . Prova: VUNESP - Prefeitura - Analista - Área: Dados – 2023
Analise o trecho de programa a seguir, apresentado na forma de uma pseudolinguagem (português estruturado).
Tipo M = matriz[1..3,1..3] de inteiros;
Inteiro: i, j, x;
M: Mat;
x ← 1;
Para i de 1 até 3 faça [
 Para j de 1 até 3 faça [
    Mat[i,j] ← i + 2*j + x;
  ]
  x ← x + 1;
]
Considerando a execução desse trecho de programa, o número de posições da matriz Mat que, ao final da execução, apresenta
um valor maior ou igual a 10 é:
     a)  0
    b)  1
     c)  2
    d)  3
    e)  4
             ---------------------------------------------------------------------------------

<!-- página 6 -->

AV1 – ESD - 20251

<!-- página 7 -->

AV1 – ESD - 20251

QUESTÃO 2) 3.0 pontos
Usando o TAD conjunto abaixo, resolva o seguinte problema:
José Carlos e Marlene são os pais de Valéria. A família quer viajar nas férias de julho. José Carlos conseguiu tirar suas
férias na fábrica do dia 2 ao dia 28. Marlene obteve licença no escritório de 5 a 30. As férias de Valéria na escola vão
de 1 a 25. Durante quais dias a família poderá viajar sem faltar as suas obrigações?

/* TAD conjunto*/
/* Elementos do conjunto são acessados através do endereço de um de seus elementos */
/* Tipo elemento de conjunto*/
typedef struct conj Conjunto;

/* Cria um conjunto de inteiros vazio */
Conjunto* conj_Cria(void);
/*Libera um conjunto de inteiros */
void conj_Libera(Conjunto*A);
/* Inclui um novo nº no Conjunto caso ele ainda não pertenca ao conjunto */
int conj_Insere(ElemConjunto *C, int x);
/* Verifica se um nº pertence ao Conjunto - 1 se pertence ou 0 caso contrário */
int conj_Ehmembro(Conjunto*A, int x);
/* Retira um nº do Conjunto caso ele pertenca ao conjunto */
int conj_Remove(int x, Conjunto *C);

/* Uniao de dois conjuntos retornando o conjunto Uniao */
Conjunto* conj_Uniao(Conjunto *A, Conjunto *B);

/* Intersecao de dois conjuntos retornando o conjunto Intersecao */
Conjunto* conj_Intersecao(Conjunto* A, Conjunto* B);

/* Diferencao entre dois conjuntos retornando o conjunto Diferenca */
Conjunto* conj_Diferenca(Conjunto*A, Conjunto*B);

/* Mostra na tela os elementos de um conjunto */
void conj_exibe(Conjunto*A);
/* Libera um conjunto */
void conj_Libera (Conjunto*A);

<!-- página 8 -->

AV1 – ESD - 20251

QUESTÃO 3) 3.0 pontos – Uso de estruturas encadeadas(pilha,fila,lista)

Considere o tipo estruturado Livro (aqui não é TAD) abaixo, que representa um livro de uma editora:
typedef struct livro Livro;
struct livro
{       char    autor[51];
       char    titulo[31];
         int     qtdVendida; /* quantidade de exemplares vendidos */
};
Considere também a existência de uma lista simplesmente encadeada de livros, ordenada crescentemente  por
qtdVendida (quantidade de exemplares vendidos), cujo nó da lista encadeada é representado pelo tipo estruturado
No abaixo:

typedef struct no No;
struct no
{       Livro *info;
      No   *prox;
};

Escreva a função não recursiva exibeOsXLivrosMaisVendidosDeUmAutor que:
    - recebe a lista de livros, ou seja, o endereço do primeiro nó da lista de livros, um autor e um número X;
     - exibe na tela o título dos X livros mais vendidos desse autor, ordenados DECRESCENTEMENTE por quantidade
vendida.
A sua função deverá obrigatoriamente usar uma estrutura auxiliar (pilha ou fila), que deve ser a mais apropriada
para resolver o problema. Esta estrutura deve ser criada e manipulada dentro da função, considerando a existência
do TAD PilhaDeLivros e do TAD FilaDeLivros.

TAD PilhaDeLivros: guarda endereços de livros e a interface é apresentada a seguir:
typedef struct pilhaDeLivros PilhaDeLivros;
                função:                                               descrição:
PilhaDeLivros *piCria(void);                  cria e retorna (um ponteiro para) uma pilha de livros
                                                     ana                                    eduvoid piPush(PilhaDeLivros *pi,                                Livro *liv);                                      recebe leo(um ponteirobia para) umaleo pilha de livrosedu  e o endereçovik    de umleo            ana
                                  mar            dor  livro criado,céu   inserindogaia o endereçorua  desse livroluz  na pilhagod                três              dias
Livro * piPop (PilhaDeLivros *pi);          recebe (um ponteiro para) uma pilha de livros e retira o endereço
                                  do livro que está no topo da pilha, retornando-o

int piVazia (PilhaDeLivros *pi);            recebe (um ponteiro para) uma pilha de livros e retorna 1, se a
                                             pilha está vazia, ou 0, em caso contrário

void piLibera (PilhaDeLivros *pi);          recebe (um ponteiro para) uma pilha de livros e libera o espaço
                            em memoria ocupado pela pilha

TAD FilaDeLivros: guarda endereços de livros e a interface é apresentada a seguir:
typedef struct filaDeLivros FilaDeLivros;

<!-- página 9 -->

AV1 – ESD - 20251

                    função:                                               descrição:
    FilaDeLivros *fiCria(void);                    cria e retorna (um ponteiro para) uma fila de livros

    void fiInsere(FilaDeLivros *fi, Livro *liv);   recebe (um ponteiro para) uma fila de livros e o endereço de um
                                                       livro criado, inserindo o endereço desse livro na fila

    Livro * fiRetira (FilaDeLivros * fi);          recebe (um ponteiro para) uma fila de livros e retira o endereço
                                     do livro que está na frente da fila, retornando-o

     int fiVazia (FilaDeLivros * fi);              recebe (um ponteiro para) uma fila de livros e retorna 1, se a fila
                                              está vazia, ou 0, em caso contrário

    void fiLibera (FilaDeLivros * fi);            recebe (um ponteiro para) uma fila de livros e libera o espaço em
                                     memoria ocupado pela fila

   Obs1: você usará apenas um dos TADs apresentados acima.
   Obs2: pode não haver X livros desse autor na lista. Se houver menos do que X livros do autor, todos os livros serão
    exibidos. Se não houver nenhum livro do autor, deve ser exibida na tela uma mensagem "Nao ha livros do autor
    {fulano} na lista", onde fulano é o nome do autor recebido como parâmetro.
    Por exemplo, para a lista da figura, se o autor fosse leo e o número pedido (X) fosse 3, teríamos:
     {leo, sal, 620}, {leo, três, 550} e {leo, rua, 400}
-----

<!-- página 10 -->

AV1 – ESD - 20251

Questão 2) (2.0 pontos)Contexto: Organização de Biblioteca Digital
   Uma biblioteca digital está implementando um sistema de controle de acesso e organização dos seus livros e leituras. O sistema
    deve lidar com as seguintes operações:

       I. Solicitações de Empréstimo: Leitores fazem pedidos para empréstimo de livros. Os pedidos são processados na ordem em
   que chegam.

       II. Histórico de Leituras Recentes: Ao navegar por livros, o sistema mantém um histórico dos últimos acessos para permitir que o
    usuário volte às leituras anteriores, de forma reversa.

       III. Catálogo Geral de Livros: O acervo completo da biblioteca deve permitir acesso, remoção e inserção de livros em qualquer
    posição, possibilitando a organização alfabética ou por categorias, priorizando o acesso. Inserções e remoções são operações
    pouco frequentes.

    Determine e justifique qual a estrutura de dados mais adequada (incluindo a forma de alocação), definindo-a ( struct e typedef)
    para:

       I. Solicitações de Empréstimo (0.5)

       II. Histórico de Leituras(0.5)

       III. Catálogo Geral de Livros(1.0)

<!-- página 11 -->

AV1 – ESD - 20251

Questão 3) (2.0 pontos) Considere que o TAD MATRIZ, definido abaixo, está disponível no arquivo Tad_Matriz.h
O TAD Matriz (Tipo Abstrato de Dados para Matrizes) representa uma estrutura de dados bidimensional composta por
elementos dispostos em linhas e colunas, geralmente acessados por dois índices: linha e coluna.
Operações do TAD Matriz:
- criar_matriz(linhas, colunas): retorna o endereço de uma matriz dinamicamente alocada com as dimensões fornecidas.
- set_valor(matriz*, i, j, valor): insere ou atualiza o valor na posição [i][j].
- get_valor(matriz*, i, j): retorna o valor da posição [i][j].
- soma(matriz1*, matriz2): retorna o endereço de uma nova matriz resultante da soma elemento a elemento.
- multiplicar(matriz1*, matriz2*): retorna o produto matricial.
- transposta(matriz*): devolve a matriz transposta.
- exibir_matriz(matriz*): exibe todos os elementos da matriz em formato tabular, com linhas e colunas numeradas.
- clona_linha(matriz*, linha): devolve uma nova matriz com 1 linha e n colunas, preenchidos com os valores das células da linha
especificada
- clona_coluna(matriz*, coluna): devolve uma nova matriz com n linhas e 1 coluna, preenchidos com os valores das células da
coluna especificada
- soma_linha(matriz*, linha): devolve a soma dos valores das células da linha especificada
- soma_coluna(matriz*, coluna): devolve a soma dos valores das células da coluna especificada
- liberar_matriz(matriz*): desaloca a memória
____________________________________________________________________________________________
Usando o TAD Matriz acima e a função preenche_acessos ( cujo cabeçalho você deve definir, mas não precisa implementá-la)
que preenche a estrutura recebida pela função com o número de acessos à página por dia x hora
resolva o seguinte problema:
Um site deseja monitorar o número de acessos em sua página ao longo da semana. Para isso, armazena os dados em uma
matriz de inteiros 7 x 24, onde:
       Cada linha representa um dia da semana (0 a 6 → de domingo a sábado),
       Cada coluna representa uma hora do dia (0 a 23)
Construa um programa, usando a preenche_acessos e o TAD Matriz, que calcule e mostre:
                 a) O total de acessos em um determinado dia; OU
                 b) A média de acessos por hora ao longo da semana; (média das 0h, 1h,2h,... )
                    c) Um dia e a hora com o maior número de acessos.

<!-- página 12 -->

AV1 – ESD - 20251

Questão 4) (3.0 pontos) Considere que foi implementado um TAD para uma lista simplesmente encadeada, sem nó cabeça,
contendo inteiros ordenados em ordem crescente:
Estrutura do nó:
struct No {
  int valor;
  struct No *prox;
} ;
typedef struct No tNo;

Estrutura da lista:
struct l {
  int qt;
  tNo *prim;
} ;
typedef struct L tlista;

Construa a função inverter_lista que, sem criar novos nós e sem realocar os valores dos campos valor, apenas alterando os
ponteiros (prox), reorganize a lista para que os nós passem a estar em ordem decrescente.
Protótipo: void inverter_lista(tLista* l);
Exemplo: Lista original (ordem crescente): [1] → [3] → [5] → [7] → NULL
Após a chamada:Lista modificada (ordem decrescente): [7] → [5] → [3] → [1] → NULL
