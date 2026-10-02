# Lista MIsta _ Materia Av1

> Fonte: `base/Prova/Lista MIsta _ Materia Av1.pdf`

<!-- página 1 -->

LISTA AV1
Tópico TAD
1.  Nesta questão você deve projetar e desenvolver um novo Tipo Abstrato de Dados (TAD) que represente um
   produto numa loja. O nome do TAD deve ser Produto. Um produto deve armazenar as informações: nome (uma
    cadeia de no máximo 15 caracteres), quantidade em estoque e preço. As seguintes operações devem estar
    disponíveis:
           - criar e retornar um (ponteiro para um) novo produto, recebendo para isso o nome, a quantidade em estoque
      e o preço desse novo produto;
           - exibir na tela nome, quantidade em estoque e preço de um produto recebido;
           - verificar se um produto recebido tem estoque igual a 0(zero), retornando 1 se o estoque estiver zerado, ou 0
     em caso contrário;
           - liberar o espaço em memória ocupado por um produto recebido;

2.  A. Projete e desenvolva um novo Tipo Abstrato de Dados (TAD) que uma fração. O nome do TAD deve ser Fracao.
   Uma fração é definida por 3 (três) componentes: o numerador (como um número natural), o denominador (como
  um número natural maior do que zero) e o sinal (1 se positivo e -1 se negativo).
Implemente as funções deste TAD de modo que seja possível:
 a)  Criar e retornar um ponteiro para uma nova fração, recebendo para isto um número natural como numerador, um
   número natural maior do que zero como denominador e 1 ou -1 dependendo do sinal da fração.
b)  Exibir na tela uma fração no seguinte formato : -2/3, +1/2, +3/1
 c) Uma função que receba 2 frações e retorne 1 se a primeira é maior que a segunda, -1 se a primeira é menor e 0 se
    são iguais.
d) Uma função para liberar uma fração.
Observe que você deve então escrever os arquivos fracao.c e fracao.h.

2B. Escreva agora um programa completo criando o(s) arquivo(s) que julgar necessários onde o vetor de tamanho 5 de
ponteiros para o TAD  Fracao seja criado e inicializado com informações lidas do teclado. Em seguida devem ser
exibidas a maior fração e a menor dentre as 5.

3.  Crie o TAD DVD com
a seguinte estrutura:
        struct   dvd {
          char  titulo[91];     /* titulo do filme */
            int codigo;          /* codigo interno para identificar o filme */
          char  genero[21];   /* genero do filme, ex: drama, acao, comedia */
            int alugueis;         /* numero de vezes que o filme foi alugado */
           };
e operações:
       dvd_cria ( revebe infs e retorna um ponteiro para uma esturuta dinamicamente criada)
       dvd_exibe ( DVD* d), mostra campos do DVD
       dvd_destrói ( DVD* d), libera a estrutura recebida
     uma função de modificação p/cada campo
               void dvd_set_titulo(char *s,DVD* d), void dvd_set_codigo(int n, DVD*d),...)
     uma função de retorno de valor de cada campo
              char * dvd_get_titulo(DVD* d), int dvd_set_codigo(DVD *d),...

Crie o TAD Acervo cuja estrutura é:
        struct   acervo {
       DVD  **vet;        /* vetor de ponteiros para DVD */
            int N;               /* quantidade máxima de elemento do vetor */
            int n;               /* quantidade de posições ocupadas no vetor*/
           };

Acervo * acervo_cria:  cria uma estrutura do tipo acervo, com um vetor de N ponteiros para DVD (dinamicamente
alocado) com 0 elemento ocupado todos os elementos do vetor inicializados com valor NULL.

<!-- página 2 -->

acervo_cadastra: recebe um acervo , os dados de um novo DVD, e um número i, incluindo-o no acervo na posição. Se
já houver um Dvd no índice i, seus dados devem ser sobrescritos. Se não houver um elemento, a função deve alocar o
espaço de memória para um novo Dvd. A função deve retornar 0 se o índice for inválido e 1 caso contrário.

acervo_remove: recebe um acervo, assim como um índice i, e remove o elemento da posição i do vetor. Sua função
deve liberar a memória e colocar a posição com valor NULL. Se o índice i for inválido a função deve retornar 0. Se o
vetor já tiver com valor NULL na posição i, a função deve retornar -1. Caso contrário a função deve retornar 1.

acervo_imprime: recebe um recebe um acervo e imprime os dados de cada DVD na tela.

acervo_aluga: recebe um recebe um acervo, assim como um código de Dvd, e incrementa o número de aluguéis do
Dvd correspondente ao código de 1. A função deve retornar 0 se não encontrar um Dvd com o código correspondente
e 1 caso contrário.

Faça uma main para testá-los.

4.  Considere que uma empresa quer guardar os dados de cada um de seus funcionários (nome e ano de admissão) em alguma
    estrutura que possa ser facilmente criada e consultada. Em outras palavras, esta empresa quer criar um TAD Func com as
    seguintes funcionalidades:
    •    criar funcionário: Func * func_cria(char * nome, int ano);
    •   obter o nome de um funcionário: char * func_getNome(Func * f);
    •   obter o ano de admissão de um funcionário: int func_getAno(Func * f);
onde o seguinte tipo é definido: typedef struct funcionario Func;
Como é esperado de um TAD, a estrutura funcionario deve ficar escondida, invisível, em relação aos outros módulos do sistema:
struct funcionario
{
         char * nome; /* nome do funcionario */
           int ano;   /* ano de admissao */
};
A empresa também quer guardar todos os funcionários em um vetor de ponteiros para o tipo Func e quer oferecer um módulo
especial com os seguintes serviços:
    •   montar dinamicamente um vetor de ponteiros para funcionários, de tamanho n, a partir de nomes e anos definidos em
         vetores: Func ** montaVetor(int n, char ** nomes, int * anos)
    •    listar todos os funcionários representados pelo vetor: void listaVetor(int n, Func ** v);
    •   ordenar o vetor de ponteiros para funcionários pelo nome e para nomes iguais, pelo ano em ordem crescente de
        admissão:             void ordenaVetor(int n, Func ** v);
    •   buscar um funcionário pelo nome de maneira que seja o mais recente (maior ano de admissão), retornando o endereço da
         estrutura que armazena este funcionário:
             Func * buscaVetor(int n, Func ** v, char * nome);
Com estas considerações, a empresa quer que você desenvolva o seguinte sistema:
    •   Defina a interface do TAD Func no header file Func.h
    •   Implemente as funcionalidades do TAD no módulo Func.c
    •   Desenvolva um módulo empresaVetor.c contendo as funções de serviço envolvendo vetores (montaVetor, listaVetor,
        ordenaVetor e buscaVetor)
    •   Usando o TAD Func e o módulo empresaVetor, escreva um programa principal que faça o seguinte:
         o  monta o vetor de ponteiros para funcionários alocado dinamicamente
         o    lista todos os funcionários
         o  ordena o vetor
         o   busca o funcionário mais recente que tenha o nome dado pelo usuário
         o  imprime o ano de admissão deste funcionário

O seguinte output ilustra o funcionamento do programa (onde o nome pesquisado é Diana Maria):

<!-- página 3 -->

Ajuda: use os seguintes vetores de dados
          char * nomes[]= {"Diana Maria","Beatrice Dante","Ada Eva","Diana Maria","Beatrice Dante","Helena Troia","Diana Maria","Beatrice Dante"};
            int anos[]= {2002,2010,2010,2006,2009,2005,2000,2005};

5.  Desenvolva o TAD Quadrado  , que serve para representar um quadrado no plano, cuja interface encontra-se
    abaixo:
typedef struct quadrado Quadrado;

/* quaCria: recebe as coordenadas x e y do vértice superior direito, a medida do lado e o nome que identifica um quadrado, criando um
novo quadrado com os dados recebidos e retornando o seu endereço*/
Quadrado * quaCria(float x, float y, float lado, char *nome);
/* quaobtemXdeVSD: recebe um ponteiro para um quadrado e retorna a coordenada x do vértice superior direito*/
float quaObtemXdeVSD(Quadrado *q);
/* quaVSDobtemYdeVSD: recebe um ponteiro para um quadrado e retorna a coordenada y do vértice superior direito */
float quaObtemYdeVSD (Quadrado *q);
/* quaObtemLado: recebe um ponteiro para um quadrado e retorna a medida do lado do quadrado */
float quaObtemLado (Quadrado *q);
/* quaObtemNome: recebe um ponteiro para um quadrado e retorna o nome do quadrado */
char * quaObtemNome (Quadrado *q);
/* quaLibera: recebe um ponteiro para um quadrado e libera o espaço ocupado pelo quadrado*/
void quaLibera (Quadrado *q);

6. Uma clínica médica utiliza um sistema informatizado para auxiliar no gerenciamento de informações sobre
    pacientes e suas consultas. Nesse sistema, as informações referentes as consultas são armazenadas em um vetor
   de ponteiros para o TAD Consulta.

 Quando um médico não pode comparecer à clínica em um determinado dia, ele é substituído por outro profissional
 no atendimento aos seus pacientes. USANDO o TAD Consulta, escreva uma função que efetua a substituição de um
 médico responsável pela consulta de pacientes agendados para uma determinada data.

 A função recebe como parâmetros um vetor de ponteiros para consulta (vet) e o tamanho desse vetor (n), a data da
 substituição (d), o nome do médico que precisa faltar (med) e o nome do substituto (sub).

 A função retorna um inteiro indicando o número de substituições efetuadas. Considere que os elementos do vetor
 recebido como parâmetro estão em ordem aleatória.

 O protótipo da função é o seguinte:

         int troca_medico(Consulta **vet, int n, Data d, char *med, char *sub);

 onde Data corresponde ao tipo estruturado descrito abaixo.

         struct data {
                   int dia, mes, ano;

            } ;

<!-- página 4 -->

typedef struct data Data;

Inclua as definições de estrutura exatamente como acima no seu arquivo de resposta, mas não inclua a função main.
Inclua também a referência a quaisquer arquivos de biblioteca que você use (e.g., stdio.h).

O TAD Consulta tem a seguinte interface (arquivo Consulta.h):

typedef struct consulta Consulta;

                                          Aloca dinamicamente área para o tipo Consulta,
Consulta *Cons_cria_consulta (char
                                                      inicializa essa área com os valores recebidos como
*paciente, int d, int m, int a, int
                                       parâmetros e retorna o ponteiro para essa área (ou
hr, int, min, char *medico);
                                   NULL se não foi possível alocá-la).

                                         Copia uma consulta, ou seja, aloca uma nova área
                                        dinamicamente, inicializa essa área com valores
Consulta *Cons_copia_consulta
                                              idênticos ao da consulta recebida como parâmetro e
(Consulta *p);
                                           retorna o ponteiro para a nova área (ou NULL se
                                     não foi possível alocá-la).

                                             Altera o paciente a ser atendido em uma consulta.
void Cons_altera_paciente (Consulta
                            O ponteiro para a consulta e nome do novo paciente
*p, char *nome);
                                        são recebidos como parâmetros.

                                             Altera a data de uma consulta. O ponteiro para a
void Cons_altera data_consulta
                                           consulta e os novos valores de dia, mês e ano são
(Consulta *p, int d, int m, int a);
                                          recebidos como parâmetros.

                                             Altera o médico responsável por uma consulta. O
void Cons_altera_medico (Consulta
                                          ponteiro para a consulta e o nome do médico
*p, char *med);
                                             substituto são recebidos como parâmetros.

                                   Compara uma data, fornecida como parâmetro, com a
                                         data de uma consulta. O ponteiro para a consulta e
int Cons_verifica_data_consulta
                                   o dia, mês e ano a serem comparados são recebidos
(Consulta *p, int d, int m, int a);
                                 como parâmetros. Retorna 1, se as datas são
                                                   iguais, ou 0, em caso contrário.

                                   Compara um médico, recebido como parâmetro, com o
                                     médico responsável por uma consulta. O ponteiro
                                         para a consulta e o nome do médico a ser comparado
int Cons_verifica_medico (Consulta
                                        são recebidos como parâmetros. Retorna 1, se os
*p, char *med);
                                  nomes dos médicos são iguais, ou 0, em caso

<!-- página 5 -->

contrário.

 void Cons_libera_consulta (Consulta         Libera a área de uma consulta cujo endereço é
  *p);                                      recebido como parâmetro.

Tópico Pilha ou Fila ou Lista

7.  Escreva a funcão somaGrandesNumeros, cujo objetivo é unicamente ler do teclado dois números inteiros muito
    grandes e mostrar na tela o resultado da soma desses dois números. Com esse objetivo, um número inteiro muito,
   muito grande pode ser armazenado numa pilha, onde cada elemento da pilha guarda um dígito desse número.
Observando que a única coisa que a função deve fazer é mostrar na tela o resultado da soma (nem os números nem a
resposta precisam ser preservados), e pensando no algoritmo de soma (aquele dos seus 8 anos), escreva a função
somaGrandesNumeros descrita no enunciado, utilizando o TAD Pilha cuja interface encontra-se abaixo. Serão
necessárias 3 pilhas: 2 para os números lidos e uma para computar a resposta a ser posteriormente exibida.
Novamente: nem os números nem a resposta precisam ser preservados.
obs: só iremos trabalhar com números positivos. A entrada de um número será feita teclando-se, de cada vez, um
dígito do número seguido de enter. O final da entrada de cada número será indicado por um valor negativo.
   Considere disponível o TAD Pilha, (de inteiros) com a seguinte interface:
   /* pilha.h */
   typedef struct pilha Pilha;
   /* funcao que cria uma pilha de inteiros (digitos) e retorna o seu endereço */
   Pilha *piCria (void)
   /* funcao que recebe o endereço da pilha e um novo digito, inserindo-o*/
   void piPush (Pilha *p, int digito);
   /* funcao que recebe o end. da pilha e faz a retirada do elemento do topo da pilha, retornando-o */
    int piPop (Pilha *p);
   /* funcao que recebe o endereço da pilha e retorna 1, se vazia, ou 0, em caso contrário */
    int piEhVazia (Pilha *p);
   /* função que recebe o endereço de uma pilha e libera o espaço por ela utilizado */
   void piLibera (Pilha *p);

8.  Considere a existência de um tipo abstrato Fila de números inteiros, cuja interface está definida no arquivo fila.h da
    seguinte forma:
       typedef struct fila Fila;
        Fila* cria(void);
       void insere (Fila* f, int v);
         int retira (Fila* f);
         int vazia (Fila* f);
       void libera (Fila* f);

Sem conhecer a representação interna desse tipo abstrato Fila e usando apenas as funções declaradas no arquivo fila.h,
implemente uma função que receba tres filas, f, f_impares e f_pares, e separe todos os valores guardados em f de tal
forma que os valores pares são movidos para a fila f_pares e os valores ımpares para f_impares.

Note que, ao final dessa função, a fila f vai estar vazia. Essa função deve obedecer o protótipo:
void separa_filas (Fila* f, Fila* f_pares, Fila* f_impares);

9.  Considere o tipo estruturado Livro (aqui não é TAD) abaixo, que representa um livro de uma editora:
typedef struct livro Livro;
struct livro
{       char    autor[51];
       char    titulo[31];
         int     qtdVendida; /* quantidade de exemplares vendidos */
};

<!-- página 6 -->

Considere também a existência de uma lista simplesmente encadeada de livros, ordenada crescentemente  por
  qtdVendida (quantidade de exemplares vendidos), cujo nó da lista encadeada é representado pelo tipo estruturado No
  abaixo:
  typedef struct no No;
  struct no{
          Livro *info;
       No   *prox;
   };

edu             ana               leo                bia               leo             edu               vik                leo              ana               leo              vik                                                                                                                                                       dias                 dor                                                                                                                                                    trêsmar                                céu                                                      gaia                                                                       rua                                                                                           god                                                                                             luz                                                                                                                          600                        sal620            voz700               200                                                                                                           550200                              350                                             380                                                            400                                                                                           500                                                                           400

  Escreva a função não recursiva exibeOsXLivrosMaisVendidosDeUmAutor que:
       - recebe a lista de livros, ou seja, o endereço do primeiro nó da lista de livros, um autor e um número X;
         - exibe na tela o título dos X livros mais vendidos desse autor, ordenados DECRESCENTEMENTE por quantidade
  vendida.
  A sua função deverá obrigatoriamente usar uma estrutura auxiliar (pilha ou fila), que deve ser a mais apropriada para
  resolver o problema. Esta estrtura deve ser criada e manipulada dentro da função, considerando a existência do TAD
  PilhaDeLivros e do TAD FilaDeLivros.

  TAD PilhaDeLivros: guarda endereços de livros e a interface é apresentada a seguir:
  typedef struct pilhaDeLivros PilhaDeLivros;

  função:                                    descrição:
  PilhaDeLivros *piCria(void);                  cria e retorna (um ponteiro para) uma pilha de livros

  void piPush(PilhaDeLivros *pi, Livro *liv);  recebe (um ponteiro para) uma pilha de livros e o endereço de um
                                                     livro criado, inserindo o endereço desse livro na pilha

  Livro * piPop (PilhaDeLivros *pi);          recebe (um ponteiro para) uma pilha de livros e retira o endereço
                                    do livro que está no topo da pilha, retornando-o

   int piVazia (PilhaDeLivros *pi);            recebe (um ponteiro para) uma pilha de livros e retorna 1, se a
                                               pilha está vazia, ou 0, em caso contrário

  void piLibera (PilhaDeLivros *pi);          recebe (um ponteiro para) uma pilha de livros e libera o espaço
                             em memoria ocupado pela pilha

  TAD FilaDeLivros: guarda endereços de livros e a interface é apresentada a seguir:
  typedef struct filaDeLivros FilaDeLivros;

  função:                                    descrição:
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

<!-- página 7 -->

Obs2: pode não haver X livros desse autor na lista. Se houver menos do que X livros do autor, todos os livros serão
exibidos. Se não houver nenhum livro do autor, deve ser exibida na tela uma mensagem "Nao ha livros do autor
{fulano} na lista", onde fulano é o nome do autor recebido como parâmetro.
Por exemplo, para a lista da figura, se o autor fosse leo e o número pedido (X) fosse 3, teríamos:
{leo, sal, 620}, {leo, três, 550} e {leo, rua, 400}

10. Uma instituição de ensino deseja separar as diversas assinaturas de revistas nacionais e importadas. Serão lidas as
    seguintes informações de cada revista: código, classificação (1 – técnica, 2 –não técnica ), tipo (1 – importada, 2 –
    nacional), valor (em reais para as nacionais e em dólar para as importadas) e a nota (valor inteiro de 1 a 10). Este
   programa deverá mostrar em primeiro lugar as revistas cujo tipo ocorre em maior quantidade.( nacionais antes das
    importadas ou vice-versa).
       Defina o TAD REVISTA. ( a struct, o typedef e a interface)
        Qual(is) estrutura(s)- pilha, fila ou lista - você escolheria para resolver o problema.Justifique
       Considere os TADs das estruturas escolhidas existentes, faça a main para resolver o problema

11. A coordenação da Informática deseja controlar os pedidos de 2ª chamada de seus alunos nas disciplinas ofertadas
   no semestre. Como houve um número excessivo de pedidos seu programa deverá montar uma estrutura para
    ajudar este gerenciamento. Um pedido é considerado válido, quando realizado dentro do prazo. O prazo  para
    pedir uma segunda chamada é de até 7 dias após a realização da prova. Os pedidos válidos podem ser indeferidos
    pois apenas 2 pedidos sem justificativa podem ser realizados.
       Defina o TAD Segunda Chamada. ( a struct, o typedef e a interface)
        Qual(is) estrutura(s)- pilha, fila ou lista - você escolheria para resolver o problema. Justifique
       Você escolheria outra estrutura caso não houvesse a restrição: "Os pedidos  válidos podem ser indeferidos
        pois apenas 2 pedidos sem justificativa podem ser realizados"? Justifique

12. Crie o TAD Lista Linear de TINF ( int codigo e TOUTRAS outras) ordenado por código, com as seguintes operações:
        Criar lista
         Incluir um elemento
        Retirar um elemento
       Buscar um Elemento dado Código
        Liberar a lista
Opcionais:
       Copiar uma Lista L1 para uma outra L2. Use necessariamente a função insere.
       Copiar uma Lista L1 para uma Lista L2 eliminando elementos repetidos. Use a função insere.
        Inverter uma lista L1

13. Uma montadora de automóveis produz carros sob encomenda. Um carro tem modelo, chassi,  relação de
   componentes e de clientes que estão aguardando. Um cliente possui nome e endereço e são atendidos em ordem
   de chegada do pedido. Os componentes possuem características como: nome, quantidade, cor, preço e fornecedor.
   Qualquer componente pode ser consultado ou ter seu preço modificado. Um fornecedor da montadora tem CNPJ e
    razão social.
       Defina o TAD Carro. ( a struct, o typedef e a interface). Justifique suas escolhas.
        Qual(is) estrutura(s)- pilha, fila ou lista - você escolheria para a montadora? .Justifique
       Considere os TADs das estruturas escolhidas existentes - com suas operações básicas:
        faça a inclusão de um novo carro
       entregue um carro cujo modelo é recebido

14. Os conceitos de pilhas e  filas são valiosos como ferramentas para a concreta solução de problemas em
   computação. Esses conceitos são aplicações de outra estrutura, a  lista, cujas variantes são  utilizadas na
   implementação de pilhas e filas. Considerando tais conceitos, julgue os itens abaixo. ( V ou F)
     (     ) Considerando apenas a memória alocada, pilhas sequenciais, implementadas por vetores, são mais eficientes
      que aquelas implementadas com listas encadeadas.
     (       ) Listas duplamente encadeadas permitem a busca de elementos apenas em um sentido.
     (       ) Uma fila FIFO implementada como uma lista circular duplamente encadeada exige, pelo menos, dois
    ponteiros, um para o início e o fim da lista
     (       ) Após a inserção dos elementos A, B, C e D, nesta ordem, em uma lista do tipo LIFO (last-in,,first-out), o
   elemento obtido na segunda retirada será o B.

<!-- página 8 -->

15. (ENADE 2011) No desenvolvimento de um software que analisa bases de DNA, representadas pelas letras A, C, G, T,
    utilizou-se as estruturas de dados: pilha e fila. Considere que, se uma sequência representa uma pilha, o topo é o
   elemento mais à esquerda; e se uma sequência representa uma fila, a sua frente é o elemento mais à esquerda.
    Analise o seguinte cenário: “a sequência inicial ficou armazenada na primeira estrutura de dados na seguinte
   ordem: (A,G,T,C,A,G,T,T). Cada elemento foi retirado da primeira estrutura de dados e inserido na segunda
    estrutura de dados, e a sequência ficou armazenada na seguinte ordem: (T,T,G,A,C,T,G,A). Finalmente, cada
   elemento foi retirado da segunda estrutura de dados e inserido na terceira estrutura de dados e a sequência ficou
   armazenada na seguinte ordem: (T,T,G,A,C,T,G,A)”. Qual a única sequência de estruturas de dados apresentadas a
    seguir pode ter sido usada no cenário descrito acima?
    a.   Fila - Pilha - Fila.
    b.   Fila - Fila - Pilha.
     c.   Fila - Pilha - Pilha.
    d.   Pilha - Fila - Pilha.
    e.   Pilha - Pilha - Pilha.

16. Considere a existência de um tipo abstrato Fila de TInfo, cuja interface está definida no arquivo fila.h da seguinte
    forma:

        Fila* fila_cria(void);      /* cria uma fila vazia */
       void fila_insere(Fila* f,TInfo v); /* insere um elemento */
        TInfo fila_retira(Fila* f);    /* retira um elemento */
         int fila_libera(Fila* f);    /* destrói a fila */
         int fila_vazia(Fila* f);    /* retorna 1 se vazia ou 0 caso contrário */

Escreva uma implementação para essa fila, que deve estar no arquivo fila.c, deve ser circular, sequencial ( com vetor) e
SEM o contador de elementos, isto é, apenas como INI e FIM

                                                 X       X   X   X
    X    x     x     x                                Após E- E- I - I

                                                                           2        0
         0        3                                                                            Ini      Fim
            Ini      Fim

17. Uma empresa de mala direta utiliza um sistema informatizado para auxiliar no gerenciamento de informações
    sobre os destinatários de suas malas diretas. Essa empresa trabalha apenas com pessoas físicas. Nesse sistema, as
    informações referentes aos destinatários são armazenadas em uma pilha para o TAD Destinatario, incluídos em
   ordem cronológica de mes/dia de nascimento, isto é, os primeiros incluídos foram os nascidos em 01 Jan, depois
    os nascidos em 02 de Jan e assim sucessivamente

 Considere O TAD Destinatario tem a seguinte interface (arquivo Destinatario.h):

 typedef struct destinatario Destinatario;

              Função                                            Objetivo
  Destinatario *dest_cria_destinatario   Aloca dinamicamente área para o tipo Destinatario,  inicializa
                                       essa
  (char *nome, int d, int m, int a,
                                      área com os valores recebidos como parâmetros e retorna o
                                       ponteiro para essa área (ou NULL se não foi possível alocá-
                                                      la).

                                     Retorna a data de nascimento de determinada  pessoa. O
                                       ponteiro
 TData dest_consulta_nascimento
                                                                                   d
                                      para o  destinatário é recebido como parâmetro. A   data e
  (Destinatario *d);
                                    nascimento é do tipo TData.
 char *dest_consulta_nome           Retorna o nome de determinada pessoa. O  ponteiropara o

<!-- página 9 -->

destinatário  é  recebido  como  parâmetro. O  nome  do
                                          destinatário
  (Destinatario *d);
                                   é   representado  como  uma   cadeia  de
                                          caracteres.
                                     Copia um   destinatário, ou    seja,  aloca  uma nova área
  Destinatario *dest_copia_destinatario
                                    dinamicamente, inicializa essa área com valores idênticos aos
                                 do
  (Destinatario *d);
                                          destinatário recebido como parâmetro e retorna o ponteiro
                                      para a
                                   nova  área  (ou  NULL  se  não  foi  possível
                                              alocá-la).

                                 Compara uma data, fornecida como parâmetro, com a data
                                  de
                                    nascimento  de  um   destinatário.  O   ponteiro  para  o
  int dest_verifica_aniversariante         destinatário e o
                                          dia  e  mês  a  serem  comparados  são  recebidos  como
  (Destinatario *d, int d, int m);       parâmetros.
                                     Retorna 1, se as datas são iguais, ou -1 se a data é
                                          anterior ao nascimento e +1 se a data é posterior ao
                                    nascimento
                                         Libera a área de um destinatário cujo endereço é recebido
 void dest_libera_destinatario        como
  (Destinatario *d);                    parâmetro.

 onde TData corresponde ao tipo estruturado descrito abaixo.

          struct tdata {
                     int dia, mes, ano;
             }  ;
         typedef struct tdata TData;

 onde TEndereco corresponde ao tipo estruturado descrito abaixo.

          struct tendereco {

                  char logradouro [101]; char numero[11];
                 char complemento [51]; char CEP[9];
             }  ;
         typedef struct tendereco TEndereco;

e o TAD PilhaDeDestinarios: guarda endereços de Destinatárioslivros e a interface é apresentada a seguir:
typedef struct pilhaDeDestinatários PilhaDeDestinatários;

função:                                    descrição:
PilhaDeDestinatários *piCria(void);          cria e retorna (um ponteiro para) uma pilha de destinatários

void  piPush(PilhaDeDestinatários   *pi,  recebe (um ponteiro  para) uma  pilha de  destinatários e o
Destinatario *d);                       endereço de um  destinatário  criado,  inserindo o endereço
                                       desse destinatário na pilha
Destinatário         *          piPop  recebe (um ponteiro para) uma pilha de destinatários e retira
(PilhaDeDestinatários *pi);             o  endereço  do  destinatário  que  está  no  topo  da  pilha,
                                       retornando-o
int piVazia (PilhaDeDestinatários *pi);    recebe (um  ponteiro  para) uma  pilha  de  destinatários  e
                                         retorna 1, se a pilha está vazia, ou 0, em caso contrário

void    piLibera    (PilhaDeDestinatários  recebe (um  ponteiro  para) uma  pilha  de  destinatários  e
*pi);                                          libera o espaço em memória ocupado pela pilha

Faça uma função que, utilizando os TADs acima, receba uma pilha de destinatários e uma data e mostre os
aniversariantes do dia.

<!-- página 10 -->

18. Uma empresa vai oferecer no mês de dezembro um curso de gerência a seus funcionários. As inscrições
    serão abertas em junho e encerradas em outubro. Entretanto, ainda não se conhece o número de vagas
    disponíveis. Mais ainda, não se sabe também qual será o  critério exato na hora do preenchimento das
    vagas. Esse  critério só será definido em novembro, pois depende de uma decisão  política da  diretoria,
   havendo duas possibilidades:
critério 1: as vagas serão preenchidas dando prioridade a quem se inscreveu mais cedo; ou
critério 2: as vagas serão distribuídas igualmente entre os dois sexos, dando prioridade, dentro de cada sexo,
a quem se inscreveu mais cedo.

A estrutura de dados sendo  utilizada para armazenar as inscrições dos funcionários é uma  fila especial, de
acordo com a descrição abaixo:
typedef struct t_no
{ char nome[20];
 int idade;
 char cargo[15];
 char sexo;
 float preco;
 struct t_no * prox; /* aponta próximo na fila */
 struct t_no * prox_sexo; /* aponta próximo da fila do mesmo sexo */
}TNO;

typedef struct {
 TNO* F,*R; /* indicam primeiro e último da fila */
 TNO* FH, *RH; /* indicam primeiro homem e último homem da fila */
 TNO* FM, *RM; /* indicam primeira mulher e última mulher da fila */
}T_FILA_ESPECIAL;

18.A)  Escreva  as  funções  básicas  para  essa   fila   especial:   e_vazia(),   inicializa(),   insere(),   retira()  e
retira_por_sexo(). A retira_por_sexo() deve receber, entre outras coisas, o sexo considerado naquela retirada.
Na hora de desenvolver as funçõres de retirada, observe que só uma das funções de retirada será utilizada,
na hora de montar a turma, dependendo do critério adotado, e que o critério desprezado jamais será utilizado
depois  disso, não havendo  necessidade de manutenção da  estrutura com  relação ao  critério  desprezado.
Observe também que as  inscrições se encerram antes do estabelecimento do  critério e da montagem da
turma. Essa  fila especial é uma estrutura específica para resolver o problema de gerar essa única turma e são
permitidas algumas situações que normalmente não seriam aceitas.

18.B) Escreva agora duas funções, que devem  utilizar as funções específicas de 1.A, não devendo manipular
diretamente  as  variáveis da  estrutura: a  inclui_inscricao_func(), que  le os dados do  funcionário e  faz sua
inscrição; e a gera_turma(  ) que  recebe, entre outros dados, o  critério  (1 ou  2) e o número da  vagas,
apresentando na  tela os dados dos funcionários selecionados  (isso é considerado a geração da turma, não
precisa fazer uma lista).

19. Considernado o TAD Quadrado , que serve para representar um quadrado no plano, cuja interface encontra-se
    abaixo:
typedef struct quadrado Quadrado;

/* quaCria: recebe as coordenadas x e y do vértice superior direito, a medida do lado e o nome que identifica um quadrado, criando um
novo quadrado com os dados recebidos e retornando o seu endereço*/
Quadrado * quaCria(float x, float y, float lado, char *nome);
/* quaobtemXdeVSD: recebe um ponteiro para um quadrado e retorna a coordenada x do vértice superior direito*/
float quaObtemXdeVSD(Quadrado *q);
/* quaVSDobtemYdeVSD: recebe um ponteiro para um quadrado e retorna a coordenada y do vértice superior direito */
float quaObtemYdeVSD (Quadrado *q);
/* quaObtemLado: recebe um ponteiro para um quadrado e retorna a medida do lado do quadrado */
float quaObtemLado (Quadrado *q);
/* quaObtemNome: recebe um ponteiro para um quadrado e retorna o nome do quadrado */
char * quaObtemNome (Quadrado *q);
/* quaLibera: recebe um ponteiro para um quadrado e libera o espaço ocupado pelo quadrado*/
void quaLibera (Quadrado *q);

<!-- página 11 -->

19.b) Considere um lista simplesmente encadeada em que cada nó tem como informação um ponteiro para TAD
Quadrado. O nó da lista é representado pelo tipo No abaixo:
typedef struct no No;
struct no
{
  Quadrado *info;
  No    *prox;
};
Escreva a função contaPrimeiroQuadrante que
    - recebe (o endereço do primeiro nó de) uma lista simplesmente encadeada de ponteiros para Quadrado
    - retorna o número de quadrados totalmente contidos no primeiro quadrante. Um quadrado é
    considerado totalmente contido no primeiro quadrante quando todo o quadrado encontra-se acima do
    eixo x e à direita do eixo y.

19.c) Considere um vetor de ponteiros para o TAD Quadrado definido anteriormente, ordenado crescentemente pela
coordenada x do  vértice superior  direito  (VSD). Para um mesmo  x, os quadrados encontram-se ordenados
crescentemente pela coordenada y do VSD.
Escreva a função buscaBinaria, que
     - recebe um vetor de ponteiros para o TAD Quadrado ordenado como descrito, o número de quadrados,
    e as coordenadas x e y de vértice superior direito de um quadrado
     - retorna o nome de algum quadrado que tenha vértice superior direito com as coordenadas x e y
     recebidas. Caso não exista nenhum quadrado cujo vértice superior direito tenha essas coordenadas, a
    função retorna NULL.
Obviamente a função buscaBinaria implementa o método de busca de mesmo nome.

    20. Considere o tipo estruturado Evento (não é TAD) que representa um evento de uma empresa de eventos:
typedef struct data Data;
struct data
{         int dia,mes,ano;
};
typedef struct evento Evento;
struct evento
{        char nomeEvento[81];
        char produtor[51];
        Data dt; /* data do evento */
};
Para fazer o controle de seus eventos, a empresa os deixa armazenados em uma pilha de eventos, de modo que o evento mais
recente fique no topo da pilha.
Entretanto, foi resolvido que todos os eventos de um determinado produtor devem ser removidos dessa pilha. E, ao serem
removidos, devem ser devolvidos para uma nova estrutura de dados (pilha ou  fila, a que for mais adequada). Você, como
programador, deve decidir qual a estrutura de dados mais adequada a ser utilizada, de maneira a permitir que, mais tarde, os
eventos daquele produtor sejam recuperados do mais recente para o mais antigo.
Escreva a função extraiEventosDeUmProdutor, que:
            - recebe um ponteiro para uma pilha de eventos com os eventos da empresa e o nome de um produtor;
            - retira dessa pilha somente os eventos desse produtor, incluindo-os numa nova estrutura (pilha ou fila, a ser criada e
        construída por você), conforme descrito acima. Observe que os demais eventos devem ser mantidos na pilha original. A
        função retorna um ponteiro para essa nova estrutura.
Apresente, no espaço para a solução, apenas a função pedida (não precisa incluir diretivas #include). Obviamente, para manipular
pilha e/ou fila de Eventos só podem ser utilizadas as funções das interfaces do TAD PilhaDeEventos e do TAD FilaDeEventos
descritas abaixo:

OBS: você pode usar quantas estruturas auxiliares julgar conveniente (pilhas e/ou filas), mas não pode usar nenhum vetor extra.

/* TAD PilhaDeEventos: guarda endereços de eventos e a interface é apresentada a seguir: */
typedef struct pilhaDeEventos PilhaDeEventos;
função:                                                         descrição:

PilhaDeEventos *piCria(void);                                       cria e retorna (um ponteiro para) uma pilha de eventos
void piPush(PilhaDeEventos *pi, Evento *ev);                  recebe (um ponteiro para) uma pilha de eventos e o endereço de um evento
                                                                  criado, inserindo o endereço desse evento na pilha
Evento * piPop (PilhaDeEventos *pi);                        recebe (um ponteiro para) uma pilha de eventos e retira o endereço do evento
                                                   que está no topo da pilha, retornando-o
int piVazia (PilhaDeEventos *pi);                             recebe (um ponteiro para) uma pilha de eventos e retorna 1, se a pilha está vazia,

<!-- página 12 -->

ou 0, em caso contrário
int piLibera(PilhaDeEventos *pi);                            recebe (um ponteiro para) uma pilha de eventos e libera o espaço ocupado por
                                                            essa pilha

/* TAD FilaDeEventos: guarda endereços de eventos e a interface é apresentada a seguir: */
typedef struct filaDeEventos FilaDeEventos;
função:                                                         descrição:

FilaDeEventos *fiCria(void);                                         cria e retorna (um ponteiro para) uma fila de eventos

void fiInsere(FilaDeEventos *fi, Evento *ev);                  recebe (um ponteiro para) uma fila de eventos e o endereço de um evento criado,
                                                              inserindo o endereço desse evento na fila

Evento * fiRetira (FilaDeEventos * fi);                        recebe (um ponteiro para) uma fila de eventos e retira o endereço do evento que
                                                              está na frente da fila, retornando-o

int fiVazia (FilaDeEventos * fi);                              recebe (um ponteiro para) uma fila de eventos e retorna 1, se a fila está vazia, ou 0,
                                         em caso contrário

void fiLibera (FilaDeEventos * fi);                            recebe (um ponteiro para) uma fila de eventos e libera o espaço em memoria
                                                    ocupado pela fila
