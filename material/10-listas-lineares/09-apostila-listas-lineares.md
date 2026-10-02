# APOSTILA_LISTAS_LINEARES_Ccl

> Fonte: `base/APOSTILA_LISTAS_LINEARES_Ccl.pdf`

<!-- página 1 -->

'
         LISTAS LINEARES EM C

                           Lista Sequencial • LSE sem cabeça • LSE com cabeça/sentinela

                            LDE convencional • LDE circular com cabeça

<!-- página 2 -->

Apostila: Listas Lineares em C

O Método Desta Apostila

Esta apostila NÃO começa pelo código. Todo capítulo de implementação segue sempre a mesma ordem de
raciocínio, porque é assim que se resolve um problema de estrutura de dados de verdade — primeiro
entender, depois desenhar, só depois programar.

  A ORDEM QUE VOCÊ VAI ENCONTRAR EM CADA CAPÍTULO
1. O que queremos fazer? (o objetivo em português)
2. Como a lista está desenhada? (a representação na memória)
3. Qual é o caso normal? (a situação mais simples)
4. Qual caso quebra a regra? (o caso de fronteira / exceção)
5. Qual problema aparece? (o que dá errado se ignorarmos a exceção)
6. Que ideia resolve o problema? (a solução estrutural)
7. Simulação passo a passo (antes/depois, com desenho)
8. Só então: código.

Legenda dos Ícones e Símbolos

Ícone         Significado                     Quando aparece

              Objetivos do capítulo             No início de cada capítulo.

              Dica                    Um atalho ou forma mais clara de pensar sobre algo.

                               Um detalhe fácil de esquecer que causa
            Atenção
                                           comportamento inesperado.

                               Um erro típico, mostrado de propósito para você
              Erro comum
                                                   reconhecê-lo.

            Boa prática                      Forma correta e recomendada.

            Grave isso                         Regra fundamental para memorizar de verdade.

             Quiz relâmpago                      Autoavaliação — responda antes de ver o gabarito.

               Exercício prático                      Atividade para fazer sozinho ou em grupo.

             Para discutir em sala                Pergunta aberta para o encontro presencial.

Notação usada nos desenhos

Símbolo                                   Significado

C                             Nó cabeça (sentinela estrutural, não é um dado real do cliente).

[valor]                     Um nó real da lista contendo aquele valor/chave.

→                                      Ponteiro prox (aponta para o próximo nó).

↔ ou <->                              Par de ponteiros ant/prox entre dois nós vizinhos (lista dupla).

NULL                                    Ponteiro que não aponta para nenhum nó.

ant, atual, suc / A, X, S                     Variáveis auxiliares: anterior, nó em questão, sucessor.

X (sob um nó)                       Marca o nó que será removido na simulação.

↑ (seta apontando para cima, sob um
                                            Indica qual variável aponta para aquele nó.
nome)

                                                       Página 2 de

<!-- página 3 -->

Apostila: Listas Lineares em C

Sumário
O Método Desta Apostila                                                                  2

Legenda dos Ícones e Símbolos                                                             2

  Notação usada nos desenhos                                                             2

Capítulo 1 — O que é uma Lista Linear?                                                      4

  Nem toda coleção precisa lembrar a ordem                                                 4

  Exemplos de relações lineares do dia a dia                                                  4

  1.1 Definição posicional                                                                 4

  1.2 As cinco propriedades de uma lista linear                                                4

  1.3 As quatro operações básicas                                                          5

  1.4 Inserção em termos puramente posicionais                                              5

  1.5 Retirada em termos puramente posicionais                                              5

  1.6 Classificação e repetição: as DUAS políticas da lista                                        6

    Exemplo — lista classificada por código                                                  6

    Exemplo — lista sem repetição                                                         6

Capítulo 2 — O TAD Lista: uma Interface, Várias Representações                                  8

 O que é um TAD, na prática?                                                             8

  2.1 Por que a lista precisa ser genérica?                                                    8

  2.2 O comparador de chaves                                                             8

  2.3 A interface pública, comentada linha por linha                                            9

    Dissecando o contrato, função por função                                               12

Capítulo 3 — Lista Sequencialmente Alocada: Conceito e Representação                           14

  3.1 Imagem mental                                                                   14

  3.2 Quando usar a lista sequencial                                                       14

  3.3 O problema central: abrir e fechar espaço                                              14

  3.4 Representação interna (struct lista, privada)                                            15

  3.5 Localizando o elemento de índice i                                                    15

  3.6 memcpy vs. memmove: qual usar?                                                    16

Capítulo 4 — Lista Sequencial: Inclusão (Todos os Casos)                                       17

  Caso A — Lista vazia                                                                   17

  Caso B — Inserir no final                                                               17

  Caso C — Inserir antes do primeiro                                                       17

  Caso D — Inserir no meio (lista classificada)                                                18

  Caso E — Classificada e SEM repetição: chave já existe                                       18

  As quatro políticas de inclusão, resumidas                                                 19

  Código completo da inclusão sequencial                                                   19

    Dissecando o código                                                                 20

Capítulo 5 — Lista Sequencial: Exclusão (Todos os Casos)                                       22

  Caso A — Excluir o único elemento                                                       22

                                                       Página 3 de

<!-- página 4 -->

Apostila: Listas Lineares em C

  Caso B — Excluir o primeiro                                                             22

  Caso C — Excluir no meio                                                               22

  Caso D — Excluir o último                                                              22

  Caso E — Repetição + lista CLASSIFICADA                                                  23

  Caso F — Repetição + lista NÃO classificada                                                23

  Código completo da exclusão sequencial                                                  23

    Dissecando o código                                                                 25

Capítulo 6 — LSE Sem Cabeça: os Problemas que Motivam a Solução                              26

  6.1 Representação sem cabeça                                                          26

  6.2 Caso normal: inserir DEPOIS de um nó conhecido                                        26

  6.3 PROBLEMA 1 — inserir ANTES do primeiro                                              27

  6.4 PROBLEMA 2 — excluir o primeiro                                                    27

  6.5 PROBLEMA 3 — lista vazia                                                           28

  6.6 PROBLEMA 4 — busca sem sentinela                                                  28

  Resumo dos quatro problemas                                                          29

Capítulo 7 — A Solução: LSE com Cabeça e Sentinela                                           31

  7.1 A cabeça não é um dado real                                                         31

  7.2 Inserir antes do primeiro agora é um caso COMUM                                       31

  7.3 Excluir o primeiro agora também é um caso COMUM                                     31

  7.4 Ainda falta resolver a busca                                                          32

  7.5 Tornando a LSE circular                                                             32

  7.6 Cabeça e sentinela NÃO são exatamente a mesma ideia                                   32

    Busca sem sentinela em lista circular (ainda não ideal)                                     32

    Preparando a sentinela                                                              33

  7.7 Busca NÃO classificada com sentinela                                                  33

  7.8 Busca classificada com sentinela                                                      34

  7.9 A função de busca completa                                                         34

Capítulo 8 — LSE com Cabeça: Inclusão (Todos os Casos)                                        37

  Caso A — Inserção na lista vazia                                                         37

  Caso B — Inserção antes do primeiro                                                     37

  Caso C — Inserção no meio                                                             37

  Caso D — Inserção depois do último                                                      38

  Caso E — Repetição PROIBIDA                                                           38

  Caso F — Repetição PERMITIDA em lista classificada                                         38

  Código completo da inclusão LSE                                                         39

    Dissecando o código                                                                 40

Capítulo 9 — LSE com Cabeça: Exclusão (Todos os Casos)                                       42

  Caso A — Excluir da lista vazia                                                           42

  Caso B — Excluir o único elemento                                                       42

  Caso C — Excluir o primeiro de vários                                                     42

                                                       Página 4 de

<!-- página 5 -->

Apostila: Listas Lineares em C

  Caso D — Excluir no meio                                                               43

  Caso E — Excluir o último                                                               43

  Caso F — Excluir repetidos em lista CLASSIFICADA                                           43

  Caso G — Excluir repetidos em lista NÃO classificada                                        43

  Código completo da exclusão LSE                                                        44

    Dissecando o código                                                                 46

Capítulo 10 — LDE Sem Cabeça: os Problemas que Motivam a Solução                            47

  10.1 Representação convencional                                                        47

  10.2 Caso normal: inserir ENTRE dois nós                                                  47

  10.3 PROBLEMA 1 — inserir antes do primeiro                                              47

  10.4 PROBLEMA 2 — inserir depois do último                                              48

  10.5 PROBLEMA 3 — inserir o PRIMEIRO elemento (lista vazia)                                48

  10.6 PROBLEMA 4 — excluir o primeiro                                                   48

  10.7 PROBLEMA 5 — excluir o último                                                     49

  Resumo dos cinco problemas                                                           49

Capítulo 11 — A Solução: LDE Circular com Cabeça Sentinela                                    51

  11.1 Lista vazia                                                                       51

  11.2 Lista com elementos                                                               51

  11.3 Busca com sentinela na LDE                                                         51

    Simulação: buscar 25 (lista classificada)                                                 52

Capítulo 12 — LDE Circular: Inclusão (Todos os Casos)                                          54

  12.1 Regra universal: inserir N ANTES de S                                                 54

  Caso A — Inserir na lista vazia                                                           54

  Caso B — Inserir antes do primeiro                                                       54

  Caso C — Inserir no meio                                                               55

  Caso D — Inserir depois do último                                                        55

  Caso E — Repetição PROIBIDA                                                           56

  Código completo da inclusão LDE                                                        56

    Dissecando o código                                                                 57

Capítulo 13 — LDE Circular: Exclusão (Todos os Casos)                                         58

  13.1 Regra universal: retirar X                                                           58

  Caso A — Excluir o único elemento                                                       58

  Caso B — Excluir o primeiro                                                             58

  Caso C — Excluir no meio                                                               59

  Caso D — Excluir o último                                                              59

  Caso E — Repetidos em lista CLASSIFICADA                                                59

  Caso F — Repetidos em lista NÃO classificada                                              60

  13.2 Função auxiliar de remoção                                                         60

  Código completo da exclusão LDE                                                        60

    Dissecando o código                                                                 61

                                                       Página 5 de

<!-- página 6 -->

Apostila: Listas Lineares em C

Capítulo 14 — Como Organizar Cada Projeto TAD                                             63

  14.1 Criação de nó LSE genérico                                                          63

  14.2 Criação de nó LDE genérico                                                         64

  14.3 Por que a cabeça precisa de uma área info?                                            64

Capítulo 15 — Quando Usar Cada Implementação?                                            66

  15.1 Escolha guiada — pergunte nesta ordem                                              66

Capítulo 16 — Atlas de Erros Comuns                                                       68

Capítulo 17 — Checkpoints de Compreensão                                                 69

Capítulo 18 — Exercícios Graduados                                                        70

  Nível 1 — somente desenho                                                            70

  Nível 2 — rastreamento                                                                70

  Nível 3 — código                                                                      70

  Nível 4 — projeto                                                                     70

Capítulo 19 — Mapa Mental Final                                                          71

Glossário                                                                              72

Gabarito Geral                                                                         74

  Gabarito dos Quizzes Relâmpago                                                        74

  Gabarito resumido dos Checkpoints (Capítulo 17)                                           75

Apêndice A — Código de Apoio da Lista Sequencial                                            77

Apêndice B — Código de Apoio da LSE com Cabeça e Sentinela                                  80

Apêndice C — Código de Apoio da LDE Circular com Cabeça                                     84

Capítulo 1 — O que é uma Lista Linear?

  OBJETIVOS
Explicar por que a posição relativa dos elementos às vezes importa e às vezes não importa.
Enunciar de memória as cinco propriedades de uma lista linear.
Diferenciar ordem POSICIONAL de classificação por CHAVE.
Explicar o que significam classificada=1/0 e repetição=1/0.

Nem toda coleção precisa lembrar a ordem

Se você só precisa somar as notas de uma turma para calcular a média, a ORDEM em que os alunos foram
cadastrados não importa nada — poderia somar em qualquer sequência que o resultado seria o mesmo.

Mas agora pense na precipitação (chuva) medida todo santo dia de um mês. Se você embaralhar os valores,
ainda dá para calcular a média — só que você perde para sempre a informação de QUANDO choveu mais. A
ordem cronológica é parte do significado do dado.

  GRAVE ISSO

Toda vez que "o que vem antes" e "o que vem depois" faz parte do significado dos dados, você precisa de
uma estrutura que preserve essa relação. Essa estrutura é a LISTA LINEAR.

                                                       Página 6 de

<!-- página 7 -->

Apostila: Listas Lineares em C

Exemplos de relações lineares do dia a dia

  segunda  →  terça  →  quarta  →  quinta  →  sexta

  carta 1  →  carta 2  →  carta 3  →  ...

  aluno 1  →  aluno 2  →  aluno 3  →  ...

  1º campeão  →  2º campeão  →  3º campeão  →  ...

Em todos esses exemplos, existe a relação "vem antes de" / "vem depois de". É exatamente essa relação que
a lista linear representa formalmente.

1.1 Definição posicional

Considere uma lista com n elementos x1, x2, ..., xn. Quando n > 0, x1 é o primeiro elemento e xn é o último.
Para um elemento interno xk (não é o primeiro nem o último), existe sempre um antecessor xk-1 e um
sucessor xk+1. Quando n = 0, a lista está vazia — e isso É um estado válido, não um erro.

  Lista com 5 nós:

  posição:    1      2      3      4      5
             x1  →  x2  →  x3  →  x4  →  x5
                    ↑             ↑
                    │             │
         x1 é antecessor    x4 é sucessor
              de x2              de x3

1.2 As cinco propriedades de uma lista linear

Propriedade                           Tradução para o aluno

Pode ter 0 ou mais nós                        Lista vazia é um estado válido, não um erro.

Um nó pode ser incluído em qualquer
                                       Antes do primeiro, no meio, ou depois do último.
posição

Qualquer nó pode ser eliminado            Inclusive o primeiro, o último, ou o único nó.

Qualquer nó pode ser acessado           Por posição (índice) ou por conteúdo/chave.

Podemos visitar cada nó, na sua vez         Isso é o percurso (percorrer a lista do início ao fim).

   OBSERVE

Essas propriedades dizem O QUE uma lista deve permitir — não dizem COMO ela é implementada por dentro
(vetor? um ponteiro por nó? dois ponteiros por nó?). Essa separação entre "o que ela faz" e "como ela é feita
por dentro" é exatamente a diferença entre TAD (Tipo Abstrato de Dados) e IMPLEMENTAÇÃO — o assunto
do Capítulo 2.

                                                       Página 7 de

<!-- página 8 -->

Apostila: Listas Lineares em C

1.3 As quatro operações básicas

Operação                   Pergunta que ela responde         Exemplo

                      Como visitar todos os nós, em        Mostrar todos os alunos
Percurso
                         ordem?                              cadastrados.

Acesso / Busca             Onde está um nó específico?         Achar o aluno de matrícula 123.

Inserção                Como incorporar um novo nó?        Matricular um novo aluno.

Retirada / Exclusão         Como retirar um nó?              Remover um aluno que desistiu.

1.4 Inserção em termos puramente posicionais

  ANTES:
  x1 → x2 → x3 → x4

  Queremos inserir N na posição 3.

  DEPOIS:
  x1 → x2 → N → x3 → x4
                ↑
        o antigo x3 agora ocupa a posição 4

Regra geral: tudo que estava na posição 3 em diante "empurra" uma posição para frente. A quantidade n
aumenta em 1.

1.5 Retirada em termos puramente posicionais

  ANTES:
  x1 → x2 → x3 → x4 → x5
             X
       (retirar x3)

  DEPOIS:
  x1 → x2 → x4 → x5

O sucessor de x3 (que era x4) passa a suceder x2 diretamente. A quantidade n diminui em 1.

1.6 Classificação e repetição: as DUAS políticas da lista

Além da posição, muitas listas possuem uma CHAVE — um campo usado para identificar ou ordenar o
elemento (matrícula, código, CPF, número de processo). A lista pode ser configurada com duas políticas
independentes:

Propriedade            Se = 0                             Se = 1

                   A ordem das chaves NÃO precisa ser   A inclusão precisa preservar a ordem
classificada              preservada — inserir sempre no fim    das chaves — o elemento entra no
                      é válido.                                lugar certo.

                      Duas chaves iguais são PROIBIDAS —
                                                       Duas ou mais chaves iguais podem
repeticao                 tentar inserir uma chave existente é
                                                                              existir na lista.
                            erro.

                                                       Página 8 de

<!-- página 9 -->

Apostila: Listas Lineares em C

  AS QUATRO COMBINAÇÕES POSSÍVEIS

classificada=0, repeticao=1 → "pilha de fichas qualquer jeito, aceita repetidas" — a mais simples: sempre
insere no fim.

classificada=0, repeticao=0 → livre na ordem, mas cada chave só pode aparecer uma vez — precisa
checar duplicidade antes de inserir.

classificada=1, repeticao=1 → mantém ordem crescente pela chave, mas aceita repetidas — precisa achar
a posição certa.

classificada=1, repeticao=0 → mantém ordem crescente E rejeita chave repetida — a mais rigorosa das
quatro.

Exemplo — lista classificada por código

  [10] [20] [30] [40]        (classificada = 1)

  Inserir 25 NÃO pode significar "colocar no final":
  [10] [20] [30] [40] [25]     ERRADO — quebra a ordem

  CORRETO — 25 entra no lugar que preserva a ordem:
  [10] [20] [25] [30] [40]

Exemplo — lista sem repetição

  [10] [20] [30]             (repeticao = 0)

  Tentar inserir a chave 20:
  ERRO → a chave já existe. A lista NÃO muda.

  Quiz Relâmpago:

1. Uma lista com n=0 elementos está em um estado:

      a) Inválido — toda lista precisa ter ao menos 1 elemento

     b) Válido — lista vazia é permitido pela definição

      c) De erro de compilação

     d) Impossível de representar em C

2. Em uma lista com classificada=1 e repeticao=0, o que acontece ao tentar inserir uma chave já existente?

      a) É inserida no final normalmente

     b) É inserida na posição ordenada, permitindo duplicidade

      c) A operação falha e a lista não muda

     d) O programa trava obrigatoriamente

   Para discutir em sala:

Dê um exemplo do seu curso (não de programação) em que a ORDEM dos itens é parte essencial da informação, e outro em que a ordem é
irrelevante. O que muda na forma de pensar sobre os dois casos?

                                                       Página 9 de

<!-- página 10 -->

Apostila: Listas Lineares em C

Capítulo 2 — O TAD Lista: uma Interface, Várias Representações

  OBJETIVOS
Explicar por que o cliente da lista não deveria conhecer ponteiros internos.
Explicar por que a lista precisa ser genérica (independente do tipo armazenado).
Ler e entender cada função da interface pública lista.h.
Explicar o papel do comparador de chaves (tListaCompara).

O que é um TAD, na prática?

TAD significa Tipo Abstrato de Dados. A ideia central é simples: quem USA a lista (o "cliente", ou seja, o
código que chama lista_inclui, lista_busca etc.) deveria pensar apenas em OPERAÇÕES e REGRAS — nunca
em como os ponteiros estão amarrados por dentro.

  GRAVE ISSO

Separação de responsabilidades: o cliente pergunta "o que a lista faz?". A implementação responde
"como ela faz por dentro?". Essas duas perguntas NUNCA devem se misturar no mesmo arquivo.

2.1 Por que a lista precisa ser genérica?

Uma lista não deveria existir apenas para guardar int. O mesmo TAD precisa poder guardar Produto, Aluno,
Livro, ou qualquer outro tipo de registro que o cliente inventar — sem que a lista precise ser reescrita a cada
novo tipo.

 /*
  * Exemplo de registro do programa CLIENTE.
  * Não é o TAD Lista; é apenas o tipo de dado que será armazenado.
  */
 struct produto {
     int codigo;      /* chave */
     char nome[40];
     float preco;
 };

O TAD não deve conhecer 'codigo', 'nome' ou 'preco'. Ele conhece apenas um BLOCO DE BYTES de tamanho
fixo (tamElem) e recebe do cliente uma função que sabe comparar as chaves — assim a lista consegue
ordenar e buscar sem nunca precisar entender o que está guardando.

  Do ponto de vista do TAD Lista, cada elemento é apenas isto:

     ┌─────────────────────────────┐
     │  ??? ??? ??? ??? ??? ???            │   ← tamElem bytes, sem nome nem tipo
     └─────────────────────────────┘

  O SIGNIFICADO desses bytes só existe na cabeça do cliente.

2.2 O comparador de chaves

Como a lista não sabe o que está guardando, ela precisa que o CLIENTE forneça uma função capaz de
comparar dois elementos pela chave. Essa função segue sempre a mesma convenção matemática usada por
qsort e bsearch da biblioteca padrão.

                                                      Página 10 de

<!-- página 11 -->

Apostila: Listas Lineares em C

 typedef int (*tListaCompara)(const void *a, const void *b);

 /*
  * Convenção:
  *  < 0  :  chave(a) < chave(b)
  *  = 0  :  chaves iguais
  *  > 0  :  chave(a) > chave(b)
  */

 int comparaProduto(const void *a, const void *b)
 {
     const struct produto *pa = a;
     const struct produto *pb = b;

     if (pa->codigo < pb->codigo) return -1;
     if (pa->codigo > pb->codigo) return 1;
     return 0;
 }

  DICA

Repare que a lista nunca chama pa->codigo diretamente — quem faz isso é comparaProduto, escrita
pelo CLIENTE. A lista só chama L->compara(a, b) e confia no resultado (negativo, zero ou positivo). Essa é
a mágica que permite reaproveitar o mesmo código de lista para qualquer tipo.

2.3 A interface pública, comentada linha por linha

O arquivo lista.h é o CONTRATO entre o cliente e a lista. Ele diz o que a lista faz, sem revelar como.

 /* lista.h */
 #ifndef LISTA_H
 #define LISTA_H

 #include <stddef.h>

 /*
  * TIPO OPACO DO TAD.
  *
  * O typedef fica na INTERFACE (.h).
  * Aqui apenas informamos que existe uma struct chamada "lista"
  * e damos a ela o nome público tLista.
  *
  * IMPORTANTE:
  * os campos de struct lista NÃO aparecem neste arquivo.
  */
 typedef struct lista tLista;

 O QUE É UM "TIPO OPACO"?

typedef struct lista tLista; declara que tLista EXISTE, mas não diz do que ela é feita por dentro. O
cliente só pode manipular ponteiros para tLista (tLista *L), nunca a struct em si — porque o compilador
não sabe o tamanho nem os campos dela fora do arquivo .c da implementação.

Vantagem prática: você pode TROCAR a implementação inteira (de sequencial para encadeada, por
exemplo) sem que o código do cliente precise mudar UMA LINHA. Isso é o poder do TAD.

 /*

                                                      Página 11 de

<!-- página 12 -->

Apostila: Listas Lineares em C

 * Tipo da função de comparação de chaves.
 * Todo nome criado com typedef nesta apostila começa com "t".
 */
typedef int (*tListaCompara)(const void *a, const void *b);

/*
 * Cria uma lista vazia.
 *
 * tamElem:   tamanho, em bytes, de cada elemento.
 *
 * classificada:   1 = preservar ordem pela chave;  0 = ordem livre.
 *
 * repeticao:   1 = chaves iguais são permitidas;  0 = chaves iguais são proibidas.
 *
 * compara:
 *   função usada para comparar chaves.
 */
tLista *lista_cria(size_t tamElem,
                    int classificada,
                    int repeticao,
                    tListaCompara compara);

/* Libera toda a memória pertencente à lista. */
void lista_destroi(tLista *L);

/* Retorna 1 se a lista não possui elementos reais. */
int lista_vazia(const tLista *L);

/* Retorna a quantidade de elementos reais. */
size_t lista_quantidade(const tLista *L);

/*
 * Inclui uma CÓPIA de elem.
 *  1 = sucesso
 *  0 = falha de memória/capacidade
 * -1 = repetição proibida
 */
int lista_inclui(tLista *L, const void *elem);

/*
 * Exclui elementos cuja chave é igual à de elem.
 * Quando repetição é permitida, remove TODAS as ocorrências.
 * Retorna a quantidade removida.
 */
size_t lista_exclui(tLista *L, const void *elem);

/*
 * Busca a primeira ocorrência e copia o registro para saida.
 * Retorna 1 se encontrou e 0 se não encontrou.
 */
int lista_busca(const tLista *L,
                 const void *chave,
                 void *saida);

#endif

                                                     Página 12 de

<!-- página 13 -->

Apostila: Listas Lineares em C

Dissecando o contrato, função por função

Função             O que ela promete ao cliente

                          Devolve uma lista pronta para uso, já configurada com as políticas escolhidas
lista_cria
                               (classificada/repetição) e o comparador.

                          Devolve toda a memória ao sistema — nenhum vazamento, seja qual for a
lista_destroi
                         implementação por dentro.

lista_vazia /
                          Perguntas de consulta; nunca alteram a lista (por isso recebem const tLista *).
lista_quantidade

                       Sempre copia o elemento (nunca guarda o ponteiro do cliente) — a lista é
lista_inclui
                       dona da sua própria cópia dos dados.

                          Retorna QUANTOS elementos foram removidos, porque com repetição pode
lista_exclui
                             ser mais de um.

                       Nunca devolve um ponteiro para dentro da lista — copia o resultado para
lista_busca
                               saida, protegendo a estrutura interna do cliente.

  ERRO COMUM

Um erro comum é achar que lista_busca deveria devolver um ponteiro direto para o nó interno.
Isso quebraria a opacidade do TAD: o cliente passaria a depender da estrutura interna (struct
no_lse, struct no_lde etc.), que pode mudar entre implementações.

   REGRA DIDÁTICA

Antes de estudar qualquer implementação (sequencial, LSE, LDE), releia este contrato. As três
implementações desta apostila devem continuar representando a MESMA abstração — mesmas
funções, mesmo comportamento  visível —  ainda que por dentro sejam completamente
diferentes.

  Quiz Relâmpago:

3. Por que lista.h declara apenas 'typedef struct lista tLista;' sem os campos da struct?

      a) Por esquecimento do programador

     b) Para que o cliente não dependa da representação interna (tipo opaco)

      c) Porque C não permite structs com campos em arquivos .h

     d) Porque isso deixa o programa mais rápido

4. O que a convenção do comparador (tListaCompara) exige que a função devolva quando chave(a) > chave(b)?

      a) Sempre 1

     b) Qualquer valor negativo

      c) Qualquer valor positivo

     d) Sempre 0

  Exercício prático:

Escreva a função comparaAluno para uma struct Aluno { int matricula; char nome[50]; float media; }, usando matricula como chave. Depois, escreva
uma segunda função comparaAlunoPorNome, usando strcmp sobre o campo nome como critério. Isso mostra que o mesmo TAD pode ser
reconfigurado só trocando o comparador — sem tocar em nenhuma outra parte do código da lista.

                                                      Página 13 de

<!-- página 14 -->

Apostila: Listas Lineares em C

Capítulo 3 — Lista Sequencialmente Alocada: Conceito e
Representação

  OBJETIVOS
Explicar a imagem mental da lista sequencial (vetor contíguo).
Reconhecer quando essa implementação é a melhor escolha.
Entender por que inserir/excluir no meio exige deslocar elementos.
Diferenciar quando usar memcpy e quando usar memmove.

3.1 Imagem mental

A lista sequencial guarda todos os elementos em uma ÚNICA faixa contínua de memória — como uma fileira
de gavetas numeradas, uma colada na outra.

  índice:     0        1        2        3        4
           ┌────────┬────────┬────────┬────────┬────────┐
           │ Elem 0 │ Elem 1 │ Elem 2 │ LIVRE  │ LIVRE  │
           └────────┴────────┴────────┴────────┴────────┘

           qtd = 3          capacidade = 5

Todos os elementos ficam lado a lado, sem nenhum "salto" na memória. Essa proximidade torna o acesso
por posição extremamente simples e rápido (basta calcular um endereço), mas cria o problema do
DESLOCAMENTO, que vamos ver já no Capítulo 4.

3.2 Quando usar a lista sequencial

  •  Acesso por posição (índice) é frequente — ex.: "me dê o 5º elemento".

  • A quantidade máxima de elementos é conhecida ou pode ser bem estimada.

  •  Inserções e exclusões no MEIO da lista não dominam o uso do programa.

  •  Queremos boa localidade de memória (elementos vizinhos na RAM = cache mais eficiente).

  •  Desejamos uma representação simples, sem ponteiros entre os elementos.

3.3 O problema central: abrir e fechar espaço

Diferente de uma lista encadeada, aqui NÃO existe "espaço entre duas gavetas" — para inserir no meio, é
preciso literalmente empurrar as gavetas seguintes.

  INSERIR NO MEIO:

  [10] [20] [30] [40] [  ]
              ↑
        quer inserir 25 aqui

  Não existe 'espaço entre 20 e 30'.
  Precisamos EMPURRAR 30 e 40 para a direita antes de poder escrever 25.

                                                      Página 14 de

<!-- página 15 -->

Apostila: Listas Lineares em C

  EXCLUIR NO MEIO:

  [10] [20] [30] [40]
              X

  Ao retirar 30, fica um BURACO no meio.
  Precisamos PUXAR 40 para a esquerda para fechar o buraco.

3.4 Representação interna (struct lista, privada)

 /* lista_seq.c */

 /*
  * Definição COMPLETA do TAD.
  * Ela pertence ao arquivo .c e fica INVISÍVEL ao cliente.
  */
 struct lista {
     unsigned char *dados;   /* vetor de bytes brutos */
     size_t qtd;
     size_t capacidade;
     size_t tamElem;
     int classificada;
     int repeticao;
     tListaCompara compara;
 };

  DICA

dados é tratado como um vetor de bytes (unsigned char*) porque o TAD não conhece o tipo real
armazenado — ele só sabe que cada elemento ocupa tamElem bytes.

3.5 Localizando o elemento de índice i

Toda a lista sequencial depende de UMA fórmula: para achar o elemento  i, ande  i posições de tamElem
bytes a partir do início do vetor.

 static void *elem_em(tLista *L, size_t i)
 {
     return L->dados + i * L->tamElem;
 }

  dados (endereço base) = 1000,  tamElem = 20 bytes

  elem_em(L, 0) = 1000 + 0*20 = 1000
  elem_em(L, 1) = 1000 + 1*20 = 1020
  elem_em(L, 2) = 1000 + 2*20 = 1040

  ┌──────────┬──────────┬──────────┐
  │ índice 0 │ índice 1 │ índice 2 │
  │  @1000   │  @1020   │  @1040   │
  └──────────┴──────────┴──────────┘

                                                      Página 15 de

<!-- página 16 -->

Apostila: Listas Lineares em C

  GRAVE ISSO

Essa é a razão pela qual o acesso por índice em uma lista sequencial é O(1): não é necessário
percorrer nó por nó, basta uma multiplicação e uma soma.

3.6 memcpy vs. memmove: qual usar?

Quando copiamos o NOVO elemento para dentro da lista, a origem (variável do cliente) e o destino (dentro
do vetor da lista) são blocos de memória INDEPENDENTES — não se sobrepõem. Nesse caso, memcpy é
adequado e mais rápido.

Mas quando DESLOCAMOS elementos dentro do próprio vetor (empurrando para a direita ou puxando para
a esquerda), origem e destino podem se SOBREPOR — porque são a mesma área de memória, só que em
posições diferentes. Nesse caso, é obrigatório usar memmove.

 /* Copiar o NOVO registro (origem e destino são independentes): */
 memcpy(destino, elem, L->tamElem);

 /* Deslocar registros dentro do próprio vetor (podem se sobrepor): */
 memmove(destino, origem, quantidade_de_bytes);

  ERRO CLÁSSICO

Usar memcpy para deslocar [30,40] uma posição à direita: como a área de origem e a de destino se
sobrepõem parcialmente, memcpy tem comportamento INDEFINIDO nesse caso (pode corromper os
dados de forma silenciosa). A função projetada especificamente para isso é memmove — ela sabe copiar
corretamente mesmo quando as áreas se sobrepõem.

  Quiz Relâmpago:

5. Por que o acesso por índice na lista sequencial é considerado rápido (O(1))?

      a) Porque o computador guarda um índice para cada elemento

     b) Porque o endereço de qualquer elemento é calculado direto por uma fórmula, sem percorrer nós

      c) Porque a lista sequencial nunca tem mais de 10 elementos

     d) Não é rápido; é o mais lento de todos

6. Quando devemos usar memmove em vez de memcpy?

      a) Sempre, memmove é mais rápido em qualquer situação

     b) Quando a origem e o destino podem se sobrepor, como ao deslocar elementos dentro do mesmo vetor

      c) Apenas quando o elemento é maior que 100 bytes

     d) memmove e memcpy são sempre intercambiáveis

   Para discutir em sala:

Se você soubesse, de antemão, que o programa vai fazer 10.000 buscas por índice e apenas 3 inserções no meio da lista durante toda a execução, a
lista sequencial seria uma boa escolha? Justifique com base no que este capítulo explicou sobre deslocamento.

                                                      Página 16 de

<!-- página 17 -->

Apostila: Listas Lineares em C

Capítulo 4 — Lista Sequencial: Inclusão (Todos os Casos)

  OBJETIVOS
Simular visualmente TODOS os casos de inclusão na lista sequencial.
Explicar as quatro políticas de inclusão (classificada × repetição).
Ler e entender o código completo de lista_seq_inclui.

Vamos simular, um por um, TODOS os casos possíveis de inclusão nesta implementação:

          lista vazia, inserir no final, inserir antes do primeiro, inserir no meio, e
       a tentativa de inserir uma chave repetida quando isso é proibido.

Caso A — Lista vazia

  ANTES:
  [  ] [  ] [  ] [  ]        qtd = 0

  Inserir 20.  →  pos = 0.  Não há nada para deslocar.

  DEPOIS:
  [20] [  ] [  ] [  ]        qtd = 1

Caso B — Inserir no final

  ANTES:
  [10] [20] [30] [  ] [  ]   qtd = 3

  Inserir 40 em lista NÃO classificada, com repetição permitida.
  pos = qtd = 3

  DEPOIS:
  [10] [20] [30] [40] [  ]   qtd = 4

💡 DICA Este é o caso mais barato de todos: não precisa deslocar NADA, porque a posição de destino já
está livre logo depois do último elemento.

                                                      Página 17 de

<!-- página 18 -->

Apostila: Listas Lineares em C

Caso C — Inserir antes do primeiro

  ANTES:índice: 0    1    2    3
              [10] [20] [30] [  ]

  Inserir 5 na posição 0.

  PASSO 1 — deslocar TUDO uma posição para a direita
  (sempre da direita para a esquerda, para não sobrescrever!):

  [10] [20] [30] [  ]
    │     │     │
    └──  └──  └──

  Com memmove:
  [ ? ] [10] [20] [30]

  PASSO 2 — copiar 5 para a posição 0:
  [ 5 ] [10] [20] [30]

  Novo qtd = 4

   ATENÇÃO

O deslocamento SEMPRE acontece do fim para o começo (do maior índice para o menor). Se você
deslocasse na ordem contrária, sobrescreveria um elemento antes de copiá-lo — é exatamente por
isso que memmove existe: ela cuida dessa direção correta internamente.

Caso D — Inserir no meio (lista classificada)

  ANTES (classificada):
  [10] [20] [30] [40] [  ]

  Inserir 25.

  BUSCA (binária, pois é classificada):
  10 < 25 → segue
  20 < 25 → segue
  30 >= 25 → PARA aqui → pos = 2

  PASSO 1 — mover [30,40] uma posição para a direita:
  [10] [20] [30] [40] [  ]
           └───┴───┘
                    move-se este bloco

  Depois do deslocamento:
  [10] [20] [ ? ] [30] [40]

  PASSO 2 — copiar 25 no buraco:
  [10] [20] [25] [30] [40]

                                                      Página 18 de

<!-- página 19 -->

Apostila: Listas Lineares em C

Caso E — Classificada e SEM repetição: chave já existe

  ANTES:
  [10] [20] [30]

  Tentar inserir 20 (repeticao = 0).

  Busca binária encontra 20.
  Como repeticao = 0:
     NÃO desloca.
     NÃO copia.
     Retorna -1.

  A LISTA PERMANECE EXATAMENTE IGUAL:
  [10] [20] [30]

  ERRO COMUM

Um erro comum é esquecer de checar existe antes de prosseguir com o deslocamento — isso faria a
lista inserir a chave repetida mesmo com repeticao=0, quebrando a política prometida ao cliente pelo
TAD.

As quatro políticas de inclusão, resumidas

Classificada         Repetição         Precisa buscar?                 Onde insere

0                 1              Não                            Sempre no final.

                                  Sim (linear, para checar           No final, se a chave não
0                 0
                                         duplicidade)                               existir ainda.

1                 1               Sim (binária, para achar a posição)     Posição ordenada.

                                                                           Posição ordenada, somente
1                 0               Sim (binária)
                                                                          se a chave não existir.

                                                      Página 19 de

<!-- página 20 -->

Apostila: Listas Lineares em C

Código completo da inclusão sequencial

 int lista_seq_inclui(tLista *L, const void *elem)
 {
     if (L == NULL || elem == NULL)
         return 0;

     if (L->qtd == L->capacidade)
         return 0;

     size_t pos;
     int existe = 0;

     if (!L->classificada && L->repeticao) {
         /* Caso mais simples: inserir no final. */
         pos = L->qtd;
     }
     else if (L->classificada) {
         /*
          * Busca binária devolve:
          *  - se existe;
          *  - a primeira posição >= chave.
          */
         existe = busca_binaria(L, elem, &pos);

         if (existe && !L->repeticao)
             return -1;
     }
     else {
         /* Não classificada e sem repetição. */
         existe = busca_linear(L, elem, &pos);

         if (existe)
             return -1;

         pos = L->qtd;
     }

     /*
      * Se não é o final, abre um espaço.
      * memmove é usado porque os blocos podem se sobrepor.
      */
     if (pos < L->qtd) {
         memmove(elem_em(L, pos + 1),
                 elem_em(L, pos),
                 (L->qtd - pos) * L->tamElem);
     }

     /* Copia o novo registro para o espaço definitivo. */
     memcpy(elem_em(L, pos), elem, L->tamElem);

     L->qtd++;
     return 1;
 }

                                                      Página 20 de

<!-- página 21 -->

Apostila: Listas Lineares em C

Dissecando o código

Trecho                             Corresponde a qual caso da simulação?

if (L->qtd == L->capacidade) return 0;    Proteção extra: lista cheia, nem tenta inserir.

!classificada && repeticao → pos = L-
                                   Caso B (inserir no final), sem busca nenhuma.
>qtd;

classificada → busca_binaria(...)         Casos C e D (posição calculada pela busca ordenada).

existe && !repeticao → return -1;       Caso E (chave repetida proibida).

if (pos < L->qtd) memmove(...)       O deslocamento dos casos C e D.

memcpy(elem_em(L, pos), elem, ...)    A cópia final do novo elemento, em todos os casos.

  Quiz Relâmpago:

7. Ao inserir no final de uma lista sequencial não classificada com repetição, quantos elementos precisam ser deslocados?

      a) Todos os elementos da lista

     b) Nenhum — pos já é a primeira posição livre

      c) Metade da lista, em média

     d) Depende do valor inserido

8. Na inclusão de uma lista classificada e sem repetição, o que acontece se busca_binaria encontrar a chave?

      a) A função insere mesmo assim, no final

     b) A função retorna -1 e não modifica a lista

      c) A função substitui o elemento antigo

     d) Ocorre um erro de segmentação

  Exercício prático:

Desenhe (no papel) a lista sequencial [10, 30, 50] classificada e sem repetição. Simule, uma de cada vez, as inserções de: 5, 40, 30 (repetida), 60.
Desenhe o ANTES e o DEPOIS de cada uma, indicando pos e quais elementos foram deslocados.

                                                      Página 21 de

<!-- página 22 -->

Apostila: Listas Lineares em C

Capítulo 5 — Lista Sequencial: Exclusão (Todos os Casos)

  OBJETIVOS
Simular visualmente TODOS os casos de exclusão na lista sequencial.
Entender por que excluir com repetição exige tratamento diferente conforme classificada ou não.
Ler e entender o código completo de lista_seq_exclui.

Caso A — Excluir o único elemento

  ANTES:
  [20] [  ] [  ]        qtd = 1

  Excluir 20.
  Não há sucessores para mover.

  DEPOIS:
  [  ] [  ] [  ]        qtd = 0

Caso B — Excluir o primeiro

  ANTES:
  [10] [20] [30] [40]

  Excluir 10.
  O buraco fica na posição 0:

  [ ? ] [20] [30] [40]

  Mover [20,30,40] para a esquerda:

  [20] [30] [40] [  ]

  qtd: 4 → 3

Caso C — Excluir no meio

                                                      Página 22 de

<!-- página 23 -->

Apostila: Listas Lineares em C

  ANTES:
  [10] [20] [30] [40]
              X
  Excluir 30.

  Buraco:
  [10] [20] [ ? ] [40]

  Mover 40 para a esquerda:
  [10] [20] [40] [  ]

Caso D — Excluir o último

  ANTES:
  [10] [20] [30]

  Excluir 30.
  Não há nada à direita para deslocar.

  DEPOIS:
  [10] [20] [  ]

💡 DICA: Assim como "inserir no final", "excluir o último" é o caso mais barato: basta diminuir qtd,
sem mover nenhum byte.

Caso E — Repetição + lista CLASSIFICADA

Como a lista é classificada, chaves iguais ficam sempre JUNTAS, formando um bloco contíguo — isso
simplifica bastante a remoção de todas as ocorrências.

  ANTES:
  [10] [20] [20] [20] [30] [40]
        └──── bloco de três 20 ────┘

  Excluir chave 20.

  Como está classificada, os repetidos estão JUNTOS.
  Mover [30,40] para a posição do primeiro 20:

  DEPOIS:
  [10] [30] [40] [  ] [  ] [  ]

Caso F — Repetição + lista NÃO classificada

Sem ordem, as ocorrências repetidas podem estar espalhadas — não existe mais um bloco único, então é
preciso examinar TODOS os elementos.

                                                      Página 23 de

<!-- página 24 -->

Apostila: Listas Lineares em C

  ANTES:
  [20] [10] [20] [30] [20] [40]

  Excluir todos os 20.

  Leitura : 20  10  20  30  20  40
  Gravação:     10      30      40
               (compactando em uma única passada)

  Final:
  [10] [30] [40] [  ] [  ] [  ]

  A DIFERENÇA ENTRE OS CASOS E E F
Classificada: repetidos formam um BLOCO ÚNICO → basta achar o início e o fim do bloco e mover o restante uma única vez.
Não classificada: repetidos podem estar espalhados → é preciso VARRER a lista inteira, compactando (mantendo) os elementos
que não batem com a chave.

Código completo da exclusão sequencial

 size_t lista_seq_exclui(tLista *L, const void *elem)
 {
     if (L == NULL || elem == NULL || L->qtd == 0)
         return 0;

     size_t pos;
     int achou = L->classificada
                 ? busca_binaria(L, elem, &pos)
                 : busca_linear(L, elem, &pos);

     if (!achou)
         return 0;

     size_t removidos = 0;

     if (!L->repeticao) {
         removidos = 1;
     }
     else if (L->classificada) {
         size_t fim = pos;

         while (fim < L->qtd &&
                L->compara(elem_em(L, fim), elem) == 0) {
             fim++;
         }

         removidos = fim - pos;
     }
     else {
         /*
          * Não classificada e com repetição:
          * compacta a lista em uma única passagem.
          */
         size_t grava = 0;

         for (size_t le = 0; le < L->qtd; le++) {

                                                      Página 24 de

<!-- página 25 -->

Apostila: Listas Lineares em C

             if (L->compara(elem_em(L, le), elem) == 0) {
                 removidos++;
             }
             else {
                 if (grava != le) {
                     memmove(elem_em(L, grava),
                             elem_em(L, le),
                             L->tamElem);
                 }
                 grava++;
             }
         }

         L->qtd -= removidos;
         return removidos;
     }

     size_t origem = pos + removidos;

     if (origem < L->qtd) {
         memmove(elem_em(L, pos),
                 elem_em(L, origem),
                 (L->qtd - origem) * L->tamElem);
     }

     L->qtd -= removidos;
     return removidos;
 }

Dissecando o código

Trecho                               Corresponde a qual caso da simulação?

!L->repeticao → removidos = 1;           Casos A, B, C, D — sempre remove exatamente 1 ocorrência.

L->classificada → while (...) fim++;         Caso E — encontra o FIM do bloco de repetidos, todos juntos.

else (não classificada) → laço de
                                     Caso F — varre tudo, mantendo o que não bate com a chave.
compactação

memmove(elem_em(L,pos),
                           O deslocamento final que fecha o buraco, comum a A–E.
elem_em(L,origem), ...)

  ERRO COMUM

Esquecer de tratar removidos == 0 corretamente (ou seja, achou == 0). Sem a checagem inicial if
(!achou) return 0;, o código tentaria calcular um deslocamento a partir de uma posição inválida.

  Quiz Relâmpago:

9. Por que, em uma lista classificada, os elementos repetidos SEMPRE aparecem juntos?

      a) Por coincidência

     b) Porque a política de inserção sempre preserva a ordem crescente da chave

      c) Porque repetição só é permitida em listas classificadas

     d) Não é verdade, podem estar espalhados

                                                      Página 25 de

<!-- página 26 -->

Apostila: Listas Lineares em C

10. Na exclusão de repetidos em lista NÃO classificada, por que é necessário percorrer TODA a lista?

      a) Porque a lista sequencial sempre percorre tudo, mesmo sem repetição

     b) Porque as ocorrências repetidas podem estar espalhadas em qualquer posição

      c) Porque memmove exige percorrer a lista inteira

     d) Não é necessário; basta olhar a primeira posição

  Exercício prático:

Desenhe a lista [20, 10, 20, 30, 20] (NÃO classificada, com repetição). Simule, passo a passo (leitura/gravação), a exclusão de todos os 20, exatamente
como no Caso F. Confira se o resultado final é [10, 30].

   Para discutir em sala:

Por que faria sentido a lista sequencial oferecer uma versão "remove só a primeira ocorrência" e outra "remove todas as ocorrências", em vez de
forçar sempre um dos dois comportamentos?

                                                      Página 26 de

<!-- página 27 -->

Apostila: Listas Lineares em C

Capítulo 6 — LSE Sem Cabeça: os Problemas que Motivam a Solução

  OBJETIVOS
Construir a representação mais óbvia de uma lista simplesmente encadeada (LSE).
Identificar, um por um, os quatro problemas que essa representação "ingênua" cria.
Entender por que esses problemas motivam a criação do nó cabeça (Capítulo 7).

Antes de introduzir a cabeça, vamos construir a representação mais óbvia da LSE. Isso é importante porque a
cabeça só faz sentido quando você PERCEBE, na prática, os problemas que ela resolve — decorar a solução
sem entender o problema não gruda.

6.1 Representação sem cabeça

  prim
   │
   ▼
  [10|●]─→[20|●]─→[30|NULL]

 /* lista_lse.c */

 /*
  * Nó é detalhe INTERNO da implementação.
  * Como não faz parte da interface pública, não precisamos
  * criar um typedef para ele no .h.
  */
 struct no_lse {
     void *info;
     struct no_lse *prox;
 };

 /* Definição privada do TAD tLista. */
 struct lista {
     struct no_lse *prim;
     size_t qtd;
     size_t tamElem;
     int classificada;
     int repeticao;
     tListaCompara compara;
 };

                                                      Página 27 de

<!-- página 28 -->

Apostila: Listas Lineares em C

6.2 Caso normal: inserir DEPOIS de um nó conhecido

  ANTES:
  prim
   │
   ▼
  [10]─→[20]─→[30]─→NULL

  Inserir 25 depois de 20.
  ant = [20]     suc = [30]

  PASSO 1: novo->prox = ant->prox
  [25]─→[30]

  PASSO 2: ant->prox = novo
  [20]─→[25]─→[30]

  RESULTADO:
  [10]─→[20]─→[25]─→[30]─→NULL

Aqui a regra é simples porque existe um nó ANTERIOR real — o algoritmo só precisa religar dois ponteiros.

6.3 PROBLEMA 1 — inserir ANTES do primeiro

  ANTES:
  prim
   │
   ▼
  [10]─→[20]─→[30]─→NULL

  Queremos inserir 5 antes de 10.  Não existe um nó anterior a 10!

  Não podemos fazer:
     ant->prox = novo
  porque ant NÃO EXISTE.

  SOLUÇÃO ESPECIAL (sem cabeça):

  novo->prox = prim;
  prim = novo;

  DEPOIS:
  prim
   │
   ▼
  [5]─→[10]─→[20]─→[30]─→NULL

 O PROBLEMA

A função de inclusão agora precisa de DOIS formatos: um para inserir antes do primeiro (mexendo na
variável prim, que fica FORA de qualquer nó) e outro para inserir depois de um nó já existente
(mexendo em ant->prox, que fica DENTRO de um nó). Esse "if especial" aumenta a dificuldade e a
chance de erro — é fácil esquecer um dos dois casos.

                                                      Página 28 de

<!-- página 29 -->

Apostila: Listas Lineares em C

6.4 PROBLEMA 2 — excluir o primeiro

  CASO NORMAL — excluir nó do meio:

  [10]─→[20]─→[30]
          X

  ant = [10]     atual = [20]

  ant->prox = atual->prox
  free(atual)

  MAS para excluir o PRIMEIRO:

  prim
   │
   ▼
  [10]─→[20]─→[30]
   X

  Não existe ant!

  Precisamos de OUTRO código:

  atual = prim;
  prim = prim->prox;
  free(atual);

   CONCLUSÃO

O primeiro nó é diferente de todos os outros porque o ponteiro que o referencia está DENTRO DA
LISTA (a variável prim), e não dentro de outro nó (algum ant->prox). Isso obriga cada função (inclusão,
exclusão) a ter um caminho de código exclusivo só para o primeiro nó.

6.5 PROBLEMA 3 — lista vazia

  Lista vazia sem cabeça:

  prim = NULL

  Inserir o primeiro nó:
    - precisa testar prim == NULL?
    - novo->prox = NULL
    - prim = novo

  Excluir:
    - precisa testar prim == NULL ANTES de acessar prim->...

A ausência de um nó estrutural permanente faz o algoritmo alternar constantemente entre "há nó" e "não
há nó" — mais um teste especial espalhado pelo código.

                                                      Página 29 de

<!-- página 30 -->

Apostila: Listas Lineares em C

6.6 PROBLEMA 4 — busca sem sentinela

 struct no_lse *p = L->prim;

 while (p != NULL &&
        L->compara(p->info, chave) != 0) {
     p = p->prox;
 }

A cada passo do laço, o computador precisa perguntar DUAS coisas ao mesmo tempo:

   1. p ainda existe (p != NULL)?

   2. p contém a chave procurada?

  Se a chave não existe na lista:

  [10]─→[20]─→[30]─→NULL
                       ↑
              a busca termina aqui, por NULL

  POR QUE MOSTRAR ESSA VERSÃO "RUIM"?

Porque agora existe uma razão CONCRETA para inventar a cabeça e a sentinela — assunto do próximo
capítulo. Sem antes enxergar esses quatro problemas reais, o nó cabeça parece apenas "mais um
ponteiro desnecessário" que alguém decidiu adicionar por capricho.

Resumo dos quatro problemas

Nº     Problema                         Por que acontece

         Inserir antes do primeiro precisa de   Não existe nó anterior real — o ponteiro afetado é prim,
1
        código especial                        fora de qualquer nó.

         Excluir o primeiro precisa de código   Mesmo motivo: quem aponta para o primeiro nó é a
2
         especial                                variável prim, não um ant->prox.

                                   Não existe nó estrutural permanente; prim pode virar
3        Lista vazia exige checagens extras
                                         NULL.

       Busca precisa checar duas          Não existe um "marcador de fim" fácil de comparar — é
4
        condições por passo                  preciso testar NULL toda hora.

  Quiz Relâmpago:

11. Por que inserir antes do primeiro nó exige um código diferente do caso geral, na LSE sem cabeça?

      a) Porque o primeiro nó não pode ser removido

     b) Porque não existe um nó anterior real; é a variável prim que precisa mudar

      c) Porque o primeiro nó ocupa mais memória

     d) Não exige, o código é sempre o mesmo

12. Na busca sem sentinela, quantas condições o laço while precisa checar a cada passo?

      a) Uma: apenas se a chave bate

     b) Duas: se p ainda existe E se a chave bate

      c) Três

     d) Nenhuma, o laço para sozinho

                                                      Página 30 de

<!-- página 31 -->

Apostila: Listas Lineares em C

Capítulo 7 — A Solução: LSE com Cabeça e Sentinela

  OBJETIVOS
Entender por que um nó cabeça permanente resolve os quatro problemas do Capítulo 6.
Diferenciar CABEÇA (estrutura permanente) de SENTINELA (papel temporário durante a busca).
Simular a busca com sentinela em lista classificada e não classificada.
Ler e entender a função busca_lse completa.

7.1 A cabeça não é um dado real

  Lista vazia:
  C ─→ NULL

  Lista com elementos:
  C ─→ [10] ─→ [20] ─→ [30] ─→ NULL

  C = CABEÇA
  C NÃO conta em qtd.
  C NÃO é uma struct produto/Aluno real — é só um nó estrutural.

Mesmo sem circularidade ainda, a cabeça já resolve o problema do primeiro nó: agora o primeiro elemento
REAL sempre possui um anterior de verdade — a própria cabeça.

7.2 Inserir antes do primeiro agora é um caso COMUM

  ANTES:
  C ─→ [10] ─→ [20] ─→ [30] ─→ NULL

  Inserir 5 antes de 10.  ant = C

  Mesmas duas linhas usadas em QUALQUER inserção no meio:

  PASSO 1: novo->prox = ant->prox     →  [5]─→[10]
  PASSO 2: ant->prox = novo           →  C ─→ [5]

  RESULTADO:
  C ─→ [5] ─→ [10] ─→ [20] ─→ [30] ─→ NULL

 O GANHO

Agora "inserir antes do primeiro" é apenas "inserir depois da cabeça" — o MESMO código genérico
que insere em qualquer outro ponto da  lista. O algoritmo geral nunca mais precisa alterar
diretamente uma variável prim fora dos nós.

                                                      Página 31 de

<!-- página 32 -->

Apostila: Listas Lineares em C

7.3 Excluir o primeiro agora também é um caso COMUM

  ANTES:
  C ─→ [10] ─→ [20] ─→ [30] ─→ NULL
        X

  ant = C           atual = [10]

  ant->prox = atual->prox;
  free(atual);

  DEPOIS:
  C ─→ [20] ─→ [30] ─→ NULL

7.4 Ainda falta resolver a busca

A cabeça já resolveu os casos de inserção/exclusão no primeiro nó. Mas se o último nó ainda aponta para
NULL, a busca continua precisando testar o fim (o Problema 4 do capítulo anterior continua vivo):

 C ─→ [10] ─→ [20] ─→ [30] ─→ NULL

 while (p != NULL && chave(p) != procurada)

7.5 Tornando a LSE circular

Em vez de terminar em NULL, fazemos o último nó apontar de volta para a própria cabeça:

  Em vez de:
  C ─→ [10] ─→ [20] ─→ [30] ─→ NULL

  Fazemos:
  C ─→ [10] ─→ [20] ─→ [30]
  ▲                       │
  └───────────────────────┘

  O último aponta para C.
  Lista vazia:  C ─→ C

  GRAVE ISSO

Agora não existe mais NULL no encadeamento. A própria cabeça marca o fim do percurso — sabemos
que "acabou de dar a volta" quando o ponteiro corrente volta a ser C.

7.6 Cabeça e sentinela NÃO são exatamente a mesma ideia

   ATENÇÃO

A CABEÇA é um nó estrutural PERMANENTE — ela existe do início ao fim da vida da lista.

A SENTINELA é um papel TEMPORÁRIO que a cabeça assume durante a busca: copiamos a chave
procurada para a área de dados da cabeça, para garantir que a comparação sempre vai encontrar
uma chave igual em algum ponto — mesmo que a chave real não exista na lista.

                                                      Página 32 de

<!-- página 33 -->

Apostila: Listas Lineares em C

Busca sem sentinela em lista circular (ainda não ideal)

 struct no_lse *p = L->cabeca->prox;

 while (p != L->cabeca &&
        L->compara(p->info, chave) != 0) {
     p = p->prox;
 }

Melhoramos o fim (não há mais NULL), mas ainda restam DUAS condições sendo checadas a cada passo do
laço.

Preparando a sentinela

  Buscar chave 25.

  ANTES:
  C(?) ─→ [10] ─→ [20] ─→ [30] ─→ C(?)

  Copiamos 25 para C:
  C(25) ─→ [10] ─→ [20] ─→ [30] ─→ C(25)

  Agora, mesmo que 25 NÃO esteja em nenhum nó real,
  a comparação obrigatoriamente vai ENCONTRAR 25 em C.

7.7 Busca NÃO classificada com sentinela

 memcpy(L->cabeca->info, chave, L->tamElem);

 struct no_lse *ant = L->cabeca;
 struct no_lse *p = L->cabeca->prox;

 while (L->compara(p->info, chave) != 0) {
     ant = p;
     p = p->prox;
 }

  Exemplo: buscar 25

  C(25) ─→ [10] ─→ [20] ─→ [30] ─→ C(25)

  p=10: 10≠25? sim → avança
  p=20: 20≠25? sim → avança
  p=30: 30≠25? sim → avança
  p=C : C≠25?  NÃO (C também tem 25!) → PARA

  Como p == C:
  25 NÃO existe entre os elementos reais.

 O GANHO REAL

Repare: dentro do while agora existe UMA condição só — L->compara(p->info, chave) != 0. Não
precisamos mais checar p != L->cabeca a cada passo, porque a PRÓPRIA sentinela garante que o laço vai
parar em algum momento.

                                                      Página 33 de

<!-- página 34 -->

Apostila: Listas Lineares em C

7.8 Busca classificada com sentinela

 while (L->compara(p->info, chave) < 0) {
     ant = p;
     p = p->prox;
 }

  Lista classificada:
  C(25) ─→ [10] ─→ [20] ─→ [30] ─→ C(25)

  Buscar 25:
  10 < 25 → avança
  20 < 25 → avança
  30 < 25 → FALSO → PARA

  ant = [20]     p = [30]

  Não encontrou 25 como elemento real,
  mas encontrou o LUGAR onde ele entraria:
  20 → 25 → 30

  Agora buscar 50 (maior que todos):
  C(50) ─→ [10] ─→ [20] ─→ [30] ─→ C(50)

  10 < 50 → avança
  20 < 50 → avança
  30 < 50 → avança
  C(50) < 50? FALSO → PARA (comparando a sentinela com ela mesma)

  ant = [30]     p = C

  Inserir antes de C significa:
  inserir DEPOIS do último elemento real.

                                                      Página 34 de

<!-- página 35 -->

Apostila: Listas Lineares em C

7.9 A função de busca completa

 static int busca_lse(tLista *L, const void *chave, tno_lse **pAnt, tno_lse **pAtual)
 {
     /*
      * A cabeça recebe temporariamente uma CÓPIA da chave.
      * Por isso sua área info deve ter tamElem bytes.
      */
     memcpy(L->cabeca->info, chave, L->tamElem);

     struct no_lse *ant = L->cabeca;
     struct no_lse *p = L->cabeca->prox;

     if (L->classificada) {
         while (L->compara(p->info, chave) < 0) {
             ant = p;
             p = p->prox;
         }
     }
     else {
         while (L->compara(p->info, chave) != 0) {
             ant = p;
             p = p->prox;
         }
     }

     *pAnt = ant;
     *pAtual = p;

     return p != L->cabeca &&  L->compara(p->info, chave) == 0;
 }

 O QUE A BUSCA ENTREGA

pAnt = nó anterior ao ponto onde a busca parou.

pAtual = nó onde a busca efetivamente parou.

Esse par (ant, atual) serve tanto para a INCLUSÃO quanto para a EXCLUSÃO — os próximos dois
capítulos usam exatamente esse resultado.

  Quiz Relâmpago:

13. Qual é a diferença entre a cabeça e a sentinela?

      a) São exatamente a mesma coisa

     b) A cabeça é permanente; a sentinela é o uso temporário da cabeça durante a busca

      c) A sentinela é um nó extra diferente da cabeça

     d) A cabeça só existe em listas classificadas

14. Depois de busca_lse retornar, como sabemos se a chave foi realmente encontrada?

      a) Sempre foi encontrada se a função não travou

     b) Verificando se pAtual aponta para a cabeça ou para um nó real com chave igual

      c) Checando apenas se pAnt é diferente de NULL

     d) Não é possível saber

  Exercício prático:

Desenhe C(?) → [10] → [30] → [50] → C(?), lista classificada. Simule a busca com sentinela (copiando a chave para C) para os valores 5, 30 e 60. Para
cada uma, indique ant, atual e se a chave foi encontrada.

                                                      Página 35 de

<!-- página 36 -->

Apostila: Listas Lineares em C

Capítulo 8 — LSE com Cabeça: Inclusão (Todos os Casos)

  OBJETIVOS
Simular visualmente TODOS os casos de inclusão na LSE circular com cabeça.
Perceber que todos os casos usam a MESMA regra de duas linhas.
Ler e entender o código completo de lista_lse_inclui.

  A REGRA UNIVERSAL DA INCLUSÃO NA LSE

novo->prox = ant->prox;

ant->prox = novo;

Essas DUAS linhas resolvem TODOS os casos abaixo — inclusive lista vazia e inserir antes do primeiro
— porque a cabeça garante que ant SEMPRE existe.

Caso A — Inserção na lista vazia

  ANTES:
  C ─→ C

  Inserir 20.   ant = C

  novo:  [20] ─→ ?

  1) novo->prox = ant->prox     →  [20] ─→ C
  2) ant->prox = novo           →  C ─→ [20]

  DEPOIS:
  C ─→ [20] ─→ C

Caso B — Inserção antes do primeiro

  ANTES:
  C ─→ [20] ─→ [40] ─→ C

  Inserir 10 (classificada).

  Busca:  ant = C     p = [20]

  1) [10]->prox = [20]
  2) C->prox = [10]

  DEPOIS:
  C ─→ [10] ─→ [20] ─→ [40] ─→ C

                                                      Página 36 de

<!-- página 37 -->

Apostila: Listas Lineares em C

Caso C — Inserção no meio

  ANTES:
  C ─→ [10] ─→ [20] ─→ [40] ─→ C

  Inserir 30.

  Busca:  ant = [20]     p = [40]

  1) novo->prox = ant->prox    →  [30] ─→ [40]
  2) ant->prox = novo          →  [20] ─→ [30]

  DEPOIS:
  C ─→ [10] ─→ [20] ─→ [30] ─→ [40] ─→ C

Caso D — Inserção depois do último

  ANTES:
  C ─→ [10] ─→ [20] ─→ C

  Inserir 30.

  Busca:  ant = [20]     p = C

  1) [30]->prox = C
  2) [20]->prox = [30]

  DEPOIS:
  C ─→ [10] ─→ [20] ─→ [30] ─→ C

  DICA

Observe como este caso é IDÊNTICO aos anteriores em termos de código — a única diferença é que p
parou na própria cabeça (C), e não em um nó real. É exatamente esse comportamento que a sentinela
garante.

Caso E — Repetição PROIBIDA

  ANTES:
  C ─→ [10] ─→ [20] ─→ [30] ─→ C

  Inserir chave 20.   repeticao = 0

  Busca para em [20].
  p != C  e  compara(p,20)==0

  NÃO cria nó.  Retorna -1.

  A LISTA NÃO MUDA.

                                                      Página 37 de

<!-- página 38 -->

Apostila: Listas Lineares em C

Caso F — Repetição PERMITIDA em lista classificada

  ANTES:
  C ─→ [10] ─→ [20] ─→ [20] ─→ [30] ─→ C

  Inserir outro 20.

  Política escolhida nesta implementação:
  inserir ANTES da primeira ocorrência igual.

  Busca classificada para no primeiro >= 20:
  ant = [10]     p = primeiro [20]

  DEPOIS:
  C ─→ [10] ─→ [20 NOVO] ─→ [20] ─→ [20] ─→ [30] ─→ C

   OBSERVAÇÃO

Outra política igualmente válida seria inserir DEPOIS do bloco de iguais, em vez de antes. O TAD deve
DOCUMENTAR  claramente  qual  política  adotou — o  cliente  precisa  poder  confiar em um
comportamento previsível.

Código completo da inclusão LSE

 int lista_lse_inclui(tLista *L, const void *elem)
 {
     if (L == NULL || elem == NULL)
         return 0;

     struct no_lse *ant;
     struct no_lse *atual;
     int existe = 0;

     if (!L->classificada && L->repeticao) {
         /*
          * Ordem livre e repetição permitida:
          * não é necessário buscar.
          * Inserimos logo após a cabeça.
          */
         ant = L->cabeca;
     }
     else {
         existe = busca_lse(L, elem, &ant, &atual);

         if (existe && !L->repeticao)
             return -1;
     }

     struct no_lse *novo = cria_no_lse(L, elem);
     if (novo == NULL)
         return 0;

     /*
      * Regra universal da inclusão após ant.
      */
     novo->prox = ant->prox;

                                                      Página 38 de

<!-- página 39 -->

Apostila: Listas Lineares em C

     ant->prox = novo;

     L->qtd++;
     return 1;
 }

Dissecando o código

Trecho                                 Corresponde a qual caso?

!classificada && repeticao → ant = L-       Não precisa buscar — insere sempre logo no início (caso
>cabeca;                                mais barato).

                                         Casos A, B, C, D, E, F — a busca acha o ponto de inclusão em
busca_lse(...) → existe, ant, atual
                                         todos eles.

existe && !repeticao → return -1;           Caso E (repetição proibida).

novo->prox = ant->prox; ant->prox = novo;   A REGRA UNIVERSAL que fecha todos os casos A–D e F.

  Quiz Relâmpago:

15. Por que a regra 'novo->prox = ant->prox; ant->prox = novo;' funciona até para a lista vazia?

      a) Porque a lista vazia é tratada com um if separado

     b) Porque a cabeça garante que ant sempre existe, mesmo quando não há elementos reais

      c) Na verdade não funciona, é preciso tratamento especial

     d) Porque novo->prox nunca é usado nesse caso

16. No caso 'inserir depois do último', onde a busca faz p parar?

      a) Em NULL

     b) No próprio nó cabeça (C)

      c) No primeiro nó da lista

     d) A busca nunca para nesse caso

  Exercício prático:

Desenhe C → [10] → [30] → C (classificada, com repetição). Simule a inclusão, em sequência, de: 5, 20, 30 (repetido), 40. Desenhe o ANTES e o
DEPOIS de cada uma, identificando ant e atual.

                                                      Página 39 de

<!-- página 40 -->

Apostila: Listas Lineares em C

Capítulo 9 — LSE com Cabeça: Exclusão (Todos os Casos)

  OBJETIVOS
Simular visualmente TODOS os casos de exclusão na LSE circular com cabeça.
Entender a regra universal de remoção de um nó a partir de (ant, atual).
Evitar o erro clássico de avançar ant no momento errado.
Ler e entender o código completo de lista_lse_exclui.

  A REGRA UNIVERSAL DA EXCLUSÃO NA LSE

ant->prox = atual->prox;

free(atual->info); free(atual);

Assim como na inclusão, essas linhas resolvem TODOS os casos, porque a cabeça garante que sempre
existe um ant, mesmo para o primeiro nó.

Caso A — Excluir da lista vazia

  C ─→ C

  qtd = 0

  Não há elemento real.
  Retorna 0. Nenhum ponteiro muda.

Caso B — Excluir o único elemento

  ANTES:
  C ─→ [20] ─→ C
        X

  Busca:  ant = C     atual = [20]

  1) ant->prox = atual->prox    →  C->prox = C
  2) libera info de [20]
  3) libera nó [20]

  DEPOIS:
  C ─→ C

Caso C — Excluir o primeiro de vários

                                                      Página 40 de

<!-- página 41 -->

Apostila: Listas Lineares em C

  ANTES:
  C ─→ [10] ─→ [20] ─→ [30] ─→ C
        X

  ant = C     atual = [10]

  C->prox = [20]
  free([10])

  DEPOIS:
  C ─→ [20] ─→ [30] ─→ C

Caso D — Excluir no meio

  ANTES:
  C ─→ [10] ─→ [20] ─→ [30] ─→ C
               X

  ant = [10]     atual = [20]

  [10]->prox = [30]
  free([20])

  DEPOIS:
  C ─→ [10] ─→ [30] ─→ C

Caso E — Excluir o último

  ANTES:
  C ─→ [10] ─→ [20] ─→ [30] ─→ C
                        X

  ant = [20]     atual = [30]

  [20]->prox = C
  free([30])

  DEPOIS:
  C ─→ [10] ─→ [20] ─→ C

  DICA

Repare: os casos B, C, D e E usam TODOS o mesmo código — a regra universal. A única coisa que muda
de um caso para o outro é QUAL nó ant e atual apontam, e isso é decidido pela busca, não por um if
especial.

Caso F — Excluir repetidos em lista CLASSIFICADA

                                                      Página 41 de

<!-- página 42 -->

Apostila: Listas Lineares em C

  ANTES:
  C ─→ [10] ─→ [20] ─→ [20] ─→ [20] ─→ [30] ─→ C
               ▲ primeiro igual
  ant=[10]

  Como está classificada, os iguais formam um BLOCO.

  Enquanto atual for 20:
    - guardar suc
    - liberar atual
    - atual = suc

  ant permanece [10] o TEMPO TODO.

  Ao terminar:  atual = [30]
  ant->prox = [30]

  DEPOIS:
  C ─→ [10] ─→ [30] ─→ C

Caso G — Excluir repetidos em lista NÃO classificada

  ANTES:
  C ─→ [20] ─→ [10] ─→ [20] ─→ [30] ─→ [20] ─→ C

  Objetivo: remover TODOS os 20.

  Passo 1: ant=C, atual=20 → REMOVE.  ant continua C.
  C ─→ [10] ─→ [20] ─→ [30] ─→ [20] ─→ C

  Passo 2: atual=10 → MANTÉM.  ant passa a ser 10.

  Passo 3: atual=20 → REMOVE.  ant continua 10.
  C ─→ [10] ─→ [30] ─→ [20] ─→ C

  Passo 4: atual=30 → MANTÉM.  ant passa a ser 30.

  Passo 5: atual=20 → REMOVE.

  FINAL:
  C ─→ [10] ─→ [30] ─→ C

  ERRO MUITO COMUM

Depois de remover atual, NÃO avance ant! O anterior deve continuar sendo o último nó que
PERMANECEU na lista — se você avançar ant mesmo quando acabou de remover algo, o próximo
religamento vai apontar para um nó que já foi liberado (free), causando comportamento indefinido
(geralmente uma falha de segmentação).

Código completo da exclusão LSE

 size_t lista_lse_exclui(tLista *L, const void *elem)
 {
     if (L == NULL || elem == NULL || L->qtd == 0)
         return 0;

     struct no_lse *ant;

                                                      Página 42 de

<!-- página 43 -->

Apostila: Listas Lineares em C

    struct no_lse *atual;

    if (!busca_lse(L, elem, &ant, &atual))
        return 0;

    size_t removidos = 0;

    if (!L->repeticao) {
        ant->prox = atual->prox;

        free(atual->info);
        free(atual);

        L->qtd--;
        return 1;
    }

    if (L->classificada) {
        while (atual != L->cabeca &&
               L->compara(atual->info, elem) == 0) {

            struct no_lse *suc = atual->prox;

            free(atual->info);
            free(atual);

            atual = suc;
            removidos++;
        }

        ant->prox = atual;
    }
    else {
        while (atual != L->cabeca) {

            if (L->compara(atual->info, elem) == 0) {
                struct no_lse *suc = atual->prox;

                ant->prox = suc;

                free(atual->info);
                free(atual);

                atual = suc;
                removidos++;
            }
            else {
                ant = atual;
                atual = atual->prox;
            }
        }
    }

    L->qtd -= removidos;
    return removidos;
}

                                                     Página 43 de

<!-- página 44 -->

Apostila: Listas Lineares em C

Dissecando o código

Trecho                                 Corresponde a qual caso?

if (!busca_lse(...)) return 0;                 Caso A (nada encontrado / lista vazia).

!L->repeticao → ant->prox = atual->prox;
                                         Casos B, C, D, E — a regra universal, uma única remoção.
free...

classificada → while (...) atual = suc;         Caso F — remove o bloco inteiro sem NUNCA mover ant.

não classificada → else { ant = atual; ... }      Caso G — ant só avança quando o nó atual é MANTIDO.

  Quiz Relâmpago:

17. Na exclusão de repetidos em lista NÃO classificada, quando a variável ant deve avançar?

      a) Sempre, a cada passo do laço

     b) Apenas quando o nó atual NÃO é removido (é mantido)

      c) Apenas quando o nó atual é removido

     d) ant nunca muda nessa função

18. Por que é necessário guardar suc = atual->prox ANTES de dar free(atual)?

      a) Não é necessário, é só um estilo de código

     b) Porque depois do free, ler atual->prox seria acessar memória já liberada

      c) Porque suc precisa ser calculado antes do laço começar

     d) Porque free() apaga o valor de prox automaticamente

  Exercício prático:

Desenhe C → [20] → [10] → [20] → [30] → [20] → C (NÃO classificada, com repetição). Simule passo a passo, como no Caso G, a exclusão de todos os
20 — anote, a cada passo, os valores de ant e atual.

   Para discutir em sala:

Compare os Capítulos 4/5 (lista sequencial) com os Capítulos 8/9 (LSE). Em qual das duas implementações o código de inclusão/exclusão precisa de
MENOS casos especiais escritos manualmente? Por quê?

                                                      Página 44 de

<!-- página 45 -->

Apostila: Listas Lineares em C

Capítulo 10 — LDE Sem Cabeça: os Problemas que Motivam a
Solução

  OBJETIVOS
Entender por que a lista duplamente encadeada (LDE) acrescenta um ponteiro ant.
Identificar os cinco problemas de fronteira da LDE convencional (com prim e ult).
Perceber que a LDE resolve um problema da LSE, mas cria outros novos nas extremidades.

A lista duplamente encadeada (LDE) acrescenta, em cada nó, um ponteiro para o ANTECESSOR além do
ponteiro para o sucessor. Isso resolve um problema real da LSE: se já conhecemos o nó a excluir, ele mesmo
sabe quem vem antes dele — não é preciso percorrer a lista de novo procurando o anterior. Mas, como
veremos, surgem novos cuidados nas extremidades.

10.1 Representação convencional

  prim                              ult
   │                                 │
   ▼                                 ▼
  NULL ←[10]↔[20]↔[30]→ NULL

 /* lista_lde.c */

 struct no_lde {
     void *info;
     struct no_lde *ant;
     struct no_lde *prox;
 };

 struct lista {
     struct no_lde *prim;
     struct no_lde *ult;
     /* demais campos privados */
 };

10.2 Caso normal: inserir ENTRE dois nós

  ANTES:
  A ↔ S

  Inserir N entre A e S.

  1) N->ant = A
  2) N->prox = S
  3) A->prox = N
  4) S->ant = N

  DEPOIS:
  A ↔ N ↔ S

Repare: agora são QUATRO ligações a ajustar (em vez de duas, como na LSE) — porque cada nó tem dois
vizinhos para acertar, não só um.

                                                      Página 45 de

<!-- página 46 -->

Apostila: Listas Lineares em C

10.3 PROBLEMA 1 — inserir antes do primeiro

  ANTES:
  prim
   │
   ▼
  [10] ↔ [20] ↔ [30]
   ▲
   não há antecessor real

  Para inserir 5 antes de 10:

  novo->ant = NULL
  novo->prox = prim
  prim->ant = novo
  prim = novo

  Isso é DIFERENTE do caso do meio (não existe A para religar).

10.4 PROBLEMA 2 — inserir depois do último

  ANTES:
  [10] ↔ [20] ↔ [30]
                  ▲
                 ult

  Inserir 40 depois de 30:

  novo->prox = NULL
  novo->ant = ult
  ult->prox = novo
  ult = novo

  Novamente: um caso especial diferente do meio.

10.5 PROBLEMA 3 — inserir o PRIMEIRO elemento (lista vazia)

  Lista vazia:
  prim = NULL
  ult = NULL

  Inserir 10:

  novo->ant = NULL
  novo->prox = NULL
  prim = novo
  ult = novo

  Aqui precisamos atualizar DUAS referências externas ao mesmo tempo.

10.6 PROBLEMA 4 — excluir o primeiro

                                                      Página 46 de

<!-- página 47 -->

Apostila: Listas Lineares em C

  ANTES:
  prim
   │
   ▼
  [10] ↔ [20] ↔ [30]

  Excluir 10:

  prim = 20
  20->ant = NULL
  free(10)

  MAS se 10 for o ÚNICO nó:
  prim deve virar NULL
  ult também deve virar NULL

10.7 PROBLEMA 5 — excluir o último

  ANTES:
  [10] ↔ [20] ↔ [30]
                  ▲
                 ult

  Excluir 30:

  ult = 20
  20->prox = NULL
  free(30)

  De novo, um caso diferente do meio.

  O QUE APRENDEMOS

Na LDE convencional, possuir prim e ult separados é útil, mas cria MUITOS estados de fronteira: lista
vazia, único nó, primeiro, meio e último — cada um com seu próprio bloco de código.

Queremos uma representação em que TODOS esses estados usem exatamente a MESMA lógica de
ligação, sem nenhum if especial. É o assunto do próximo capítulo.

Resumo dos cinco problemas

Nº     Problema                             Por que acontece

                                       Não existe A (anterior); prim precisa mudar
1        Inserir antes do primeiro
                                                diretamente.

                                       Não existe S (sucessor); ult precisa mudar
2        Inserir depois do último
                                                diretamente.

3        Inserir o primeiro elemento (lista vazia)    prim e ult precisam ser atualizados ao mesmo tempo.

4        Excluir o primeiro                       prim muda, e se for o único nó, ult também muda.

5        Excluir o último                                ult muda; caso especial diferente do meio.

  Quiz Relâmpago:

19. Quantas ligações de ponteiro (aproximadamente) precisam ser ajustadas para inserir um nó no MEIO de uma LDE, comparado com a LSE?

                                                      Página 47 de

<!-- página 48 -->

Apostila: Listas Lineares em C

      a) O mesmo número: duas em ambas

     b) O dobro: quatro na LDE contra duas na LSE

      c) A LDE precisa de menos ligações

     d) Depende do tamanho da lista

20. Por que excluir o ÚNICO nó de uma LDE convencional é um caso ainda mais especial que excluir o primeiro de vários?

      a) Porque um nó único não pode ser excluído

     b) Porque tanto prim quanto ult precisam ser zerados ao mesmo tempo

      c) Porque a memória de um nó único nunca é liberada

     d) Não é mais especial, é exatamente igual

   Para discutir em sala:

A LSE resolveu seus problemas de fronteira com UM nó cabeça. Você acha que a LDE também poderia ser resolvida com um único nó cabeça, ou ela
precisaria de dois (um para cada extremidade)? Justifique antes de ler o Capítulo 11.

                                                      Página 48 de

<!-- página 49 -->

Apostila: Listas Lineares em C

Capítulo 11 — A Solução: LDE Circular com Cabeça Sentinela

  OBJETIVOS
Entender como UMA única cabeça, usada nos dois sentidos, resolve os cinco problemas do Capítulo 10.
Explicar por que não precisamos mais de prim e ult separados.
Simular a busca com sentinela na LDE.

11.1 Lista vazia

        ┌─────────────┐
        │                 │
        ▼                 │
      [ C ]                │
        │                 ▲
        └─────────────┘

  C->prox = C
  C->ant  = C

Não precisamos mais de prim e ult separados. A cabeça é permanente e resolve as duas pontas ao mesmo
tempo.

11.2 Lista com elementos

       primeiro
       │
       ▼
  C ↔ [10] ↔ [20] ↔ [30] ↔ C
  ▲                          ▲
  │                           │
  └─────────────────────┘
              último = C->ant

  C->prox = primeiro elemento real
  C->ant  = último elemento real

  IDEIA-CHAVE

Na circularidade, a cabeça representa as DUAS fronteiras ao mesmo tempo: antes do primeiro E depois
do último. Inserir ANTES de C significa inserir no FINAL da lista. Inserir DEPOIS de C significa inserir no
INÍCIO da lista.

11.3 Busca com sentinela na LDE

A busca usa a mesma técnica da LSE (Capítulo 7). A única diferença é que aqui não precisamos retornar o
anterior explicitamente, porque o próprio nó já sabe quem vem antes dele através de p->ant.

 static int busca_lde(tLista *L,
                       const void *chave,
                       struct no_lde **pAtual)
 {
     memcpy(L->cabeca->info, chave, L->tamElem);

                                                      Página 49 de

<!-- página 50 -->

Apostila: Listas Lineares em C

     struct no_lde *p = L->cabeca->prox;

     if (L->classificada) {
         while (L->compara(p->info, chave) < 0)
             p = p->prox;
     }
     else {
         while (L->compara(p->info, chave) != 0)
             p = p->prox;
     }

     *pAtual = p;

     return p != L->cabeca &&
            L->compara(p->info, chave) == 0;
 }

Simulação: buscar 25 (lista classificada)

  C(25) ↔ [10] ↔ [20] ↔ [30] ↔ C(25)

  10 < 25 → avança
  20 < 25 → avança
  30 < 25 → FALSO → PARA

  p = [30]
  p->ant = [20]     ← já sabemos o anterior sem precisar retornar 'ant'!

  25 não existe como elemento real,
  mas o lugar de inclusão é entre [20] e [30].

  DICA

Essa é a grande vantagem estrutural da LDE sobre a LSE: como cada nó já conhece seu antecessor via -
>ant, a função de busca fica mais simples — não precisa carregar um segundo ponteiro de saída (pAnt)
por todo o percurso.

  Quiz Relâmpago:

21. Por que a LDE circular com cabeça não precisa mais das variáveis prim e ult?

      a) Porque a LDE não tem mais primeiro nem último elemento

     b) Porque C->prox aponta para o primeiro e C->ant aponta para o último, sempre

      c) Porque prim e ult foram renomeados, mas continuam existindo

     d) Não é verdade, a LDE circular também precisa de prim e ult

22. Na função busca_lde, por que não é necessário devolver um ponteiro 'ant' separado?

      a) Porque a busca nunca precisa do nó anterior

     b) Porque cada nó já guarda seu antecessor em p->ant

      c) Porque a LDE não usa sentinela

     d) Porque ant é sempre igual à cabeça

   Para discutir em sala:

Releia os cinco problemas do Capítulo 10. Para cada um, explique em uma frase como a cabeça circular da LDE o resolve.

                                                      Página 50 de

<!-- página 51 -->

Apostila: Listas Lineares em C

Capítulo 12 — LDE Circular: Inclusão (Todos os Casos)

  OBJETIVOS
Simular visualmente TODOS os casos de inclusão na LDE circular com cabeça.
Aplicar a regra universal de quatro passos em cada caso, incluindo lista vazia.
Ler e entender o código completo de lista_lde_inclui.

12.1 Regra universal: inserir N ANTES de S

  ANTES:
  A ↔ S

  Queremos inserir N antes de S.

  PASSO 1: N->ant = A
  PASSO 2: N->prox = S
  PASSO 3: A->prox = N
  PASSO 4: S->ant = N

  DEPOIS:
  A ↔ N ↔ S

  GRAVE ISSO

Onde A = S->ant (o antecessor de S, seja lá quem for). Essa é a ÚNICA regra que vamos usar em
TODOS os casos abaixo — inclusive na lista vazia, porque a cabeça garante que S sempre tem um -
>ant válido.

Caso A — Inserir na lista vazia

  ANTES:
  C ↔ C

  suc = C     ant = suc->ant = C

  Novo N = 20.

  1) N->ant = C
  2) N->prox = C
  3) C->prox = N
  4) C->ant = N

  DEPOIS:
  C ↔ [20] ↔ C

  Observe: o MESMO C é antecessor e sucessor de N ao mesmo tempo.

Caso B — Inserir antes do primeiro

                                                      Página 51 de

<!-- página 52 -->

Apostila: Listas Lineares em C

  ANTES:
  C ↔ [20] ↔ [40] ↔ C

  Inserir 10.  Busca classificada: suc = [20]

  ant = suc->ant = C

  1) [10]->ant = C
  2) [10]->prox = [20]
  3) C->prox = [10]
  4) [20]->ant = [10]

  DEPOIS:
  C ↔ [10] ↔ [20] ↔ [40] ↔ C

Caso C — Inserir no meio

  ANTES:
  C ↔ [10] ↔ [20] ↔ [40] ↔ C

  Inserir 30.  suc = [40]     ant = [20]

  ANTES do religamento:
  [20] ↔ [40]

  NOVO:
  [30]

  DEPOIS dos quatro ajustes:
  [20] ↔ [30] ↔ [40]

  LISTA FINAL:
  C ↔ [10] ↔ [20] ↔ [30] ↔ [40] ↔ C

Caso D — Inserir depois do último

  ANTES:
  C ↔ [10] ↔ [20] ↔ C

  Inserir 30.  Busca chega a C.

  suc = C     ant = C->ant = [20]

  Aplicamos a MESMA regra:

  1) [30]->ant = [20]
  2) [30]->prox = C
  3) [20]->prox = [30]
  4) C->ant = [30]

  DEPOIS:
  C ↔ [10] ↔ [20] ↔ [30] ↔ C

                                                      Página 52 de

<!-- página 53 -->

Apostila: Listas Lineares em C

 O QUE DESAPARECEU

Não  existe mais um código  especial  "atualize  ult".  Inserir antes da cabeça  atualiza C->ant
naturalmente, através da MESMA regra universal usada em todos os outros casos.

Caso E — Repetição PROIBIDA

  ANTES:
  C ↔ [10] ↔ [20] ↔ [30] ↔ C

  Inserir 20.  repeticao = 0

  Busca retorna p = [20].
  Como é elemento real e chave igual:
  NÃO aloca nó.  Retorna -1.

Código completo da inclusão LDE

 int lista_lde_inclui(tLista *L, const void *elem)
 {
     if (L == NULL || elem == NULL)
         return 0;

     struct no_lde *suc;
     int existe = 0;

     if (!L->classificada && L->repeticao) {
         /*
          * Ordem livre:
          * inserir no início = inserir antes do primeiro real.
          */
         suc = L->cabeca->prox;
     }
     else {
         existe = busca_lde(L, elem, &suc);

         if (existe && !L->repeticao)
             return -1;
     }

     struct no_lde *novo = cria_no_lde(L, elem);
     if (novo == NULL)
         return 0;

     struct no_lde *ant = suc->ant;

     novo->ant = ant;
     novo->prox = suc;
     ant->prox = novo;
     suc->ant = novo;

     L->qtd++;
     return 1;
 }

                                                      Página 53 de

<!-- página 54 -->

Apostila: Listas Lineares em C

Dissecando o código

Trecho                                 Corresponde a qual caso?

!classificada && repeticao → suc = L-
                                             Insere sempre logo no início, sem precisar buscar.
>cabeca->prox;

busca_lde(...) → existe, suc                 Casos A, B, C, D, E — a busca acha o ponto de inclusão.

existe && !repeticao → return -1;           Caso E (repetição proibida).

                                              Calcula A automaticamente — funciona até quando suc é a
struct no_lde *ant = suc->ant;
                                           cabeça.

                                 A REGRA UNIVERSAL de 4 passos, igual em todos os casos
novo->ant/prox; ant->prox; suc->ant;
                                       A–D.

  Quiz Relâmpago:

23. Na inclusão da LDE, de onde vem o valor da variável ant?

      a) É sempre a cabeça C

     b) É calculado como suc->ant, o antecessor de quem vai ficar depois do novo nó

      c) É sempre o último nó inserido

     d) Precisa ser buscado separadamente com outra função

24. Por que a inclusão na lista vazia (Caso A) não precisa de nenhum código especial na LDE circular?

      a) Porque listas vazias não podem receber inclusão

     b) Porque suc->ant, quando suc é a própria cabeça vazia, aponta para a própria cabeça, fazendo a regra universal funcionar sozinha

      c) Porque a lista vazia é tratada fora da função de inclusão

     d) Não é verdade, precisa sim de tratamento especial

  Exercício prático:

Desenhe C ↔ [20] ↔ [40] ↔ C (classificada). Simule a inclusão, em sequência, de: 10, 30, 50. Para cada uma, escreva os quatro passos da regra
universal com os valores concretos de N, A e S.

                                                      Página 54 de

<!-- página 55 -->

Apostila: Listas Lineares em C

Capítulo 13 — LDE Circular: Exclusão (Todos os Casos)

  OBJETIVOS
Simular visualmente TODOS os casos de exclusão na LDE circular com cabeça.
Aplicar a regra universal de remoção, incluindo o caso do único elemento.
Ler e entender o código completo de lista_lde_exclui.

13.1 Regra universal: retirar X

  ANTES:
  A ↔ X ↔ S

  Queremos que A e S deixem de apontar para X.

  1) A->prox = S
  2) S->ant = A

  DEPOIS:
  A ↔───────↔ S

  Então:
  free(X->info)
  free(X)

  GRAVE ISSO

Apenas DUAS linhas religam os vizinhos entre si, não importa quem sejam A e S — mesmo que A ou S
seja a própria cabeça. Essa é a regra universal usada em TODOS os casos abaixo.

Caso A — Excluir o único elemento

  ANTES:
  C ↔ [20] ↔ C
        X

  Para X=[20]:  A = X->ant = C     S = X->prox = C

  1) C->prox = C
  2) C->ant = C

  free(20)

  DEPOIS:
  C ↔ C

  NENHUM if especial foi necessário!

Caso B — Excluir o primeiro

                                                      Página 55 de

<!-- página 56 -->

Apostila: Listas Lineares em C

  ANTES:
  C ↔ [10] ↔ [20] ↔ [30] ↔ C
        X

  X=[10]     A=C     S=[20]

  1) C->prox = [20]
  2) [20]->ant = C

  DEPOIS:
  C ↔ [20] ↔ [30] ↔ C

Caso C — Excluir no meio

  ANTES:
  C ↔ [10] ↔ [20] ↔ [30] ↔ C
               X

  X=[20]     A=[10]     S=[30]

  1) [10]->prox = [30]
  2) [30]->ant = [10]

  DEPOIS:
  C ↔ [10] ↔ [30] ↔ C

Caso D — Excluir o último

  ANTES:
  C ↔ [10] ↔ [20] ↔ [30] ↔ C
                        X

  X=[30]     A=[20]     S=C

  1) [20]->prox = C
  2) C->ant = [20]

  DEPOIS:
  C ↔ [10] ↔ [20] ↔ C

  Mesmo código do caso do meio!

  BOA PRÁTICA

Repare que os Casos A, B, C e D usam LITERALMENTE o mesmo código — a única diferença entre eles
é para qual nó A e S apontam (podendo ser a própria cabeça). Não existe nenhum "if especial" para
nenhuma extremidade.

Caso E — Repetidos em lista CLASSIFICADA

                                                      Página 56 de

<!-- página 57 -->

Apostila: Listas Lineares em C

  ANTES:
  C ↔ [10] ↔ [20] ↔ [20] ↔ [20] ↔ [30] ↔ C

  Busca retorna o primeiro [20].

  Enquanto chave(p) == 20:
    - suc = p->prox
    - remover p com a REGRA UNIVERSAL
    - p = suc

  DEPOIS:
  C ↔ [10] ↔ [30] ↔ C

Caso F — Repetidos em lista NÃO classificada

  ANTES:
  C ↔ [20] ↔ [10] ↔ [20] ↔ [30] ↔ [20] ↔ C

  Percorrer TODOS os nós reais.

  Quando encontrar 20:
    - guardar p->prox ANTES do free
    - remover p
    - continuar pelo sucessor guardado

  DEPOIS:
  C ↔ [10] ↔ [30] ↔ C

  DICA

Diferente da LSE (Capítulo  9), aqui NÃO precisamos gerenciar manualmente uma variável ant
separada durante a varredura — cada nó já sabe seu antecessor via ->ant, então a regra universal de
remoção funciona sozinha a cada passo.

13.2 Função auxiliar de remoção

 static void remove_no_lde(tLista *L, struct no_lde *p)
 {
     /*
      * Os vizinhos passam a apontar um para o outro.
      */
     p->ant->prox = p->prox;
     p->prox->ant = p->ant;

     free(p->info);
     free(p);

     L->qtd--;
 }

Código completo da exclusão LDE

 size_t lista_lde_exclui(tLista *L, const void *elem)
 {
     if (L == NULL || elem == NULL || L->qtd == 0)

                                                      Página 57 de

<!-- página 58 -->

Apostila: Listas Lineares em C

         return 0;

     struct no_lde *p;

     if (!busca_lde(L, elem, &p))
         return 0;

     if (!L->repeticao) {
         remove_no_lde(L, p);
         return 1;
     }

     size_t removidos = 0;

     if (L->classificada) {
         while (p != L->cabeca &&
                L->compara(p->info, elem) == 0) {

             struct no_lde *suc = p->prox;
             remove_no_lde(L, p);
             removidos++;
             p = suc;
         }
     }
     else {
         while (p != L->cabeca) {
             struct no_lde *suc = p->prox;

             if (L->compara(p->info, elem) == 0) {
                 remove_no_lde(L, p);
                 removidos++;
             }

             p = suc;
         }
     }

     return removidos;
 }

Dissecando o código

Trecho                                 Corresponde a qual caso?

if (!busca_lde(...)) return 0;               Nada encontrado (lista vazia ou chave ausente).

!L->repeticao → remove_no_lde(L, p);        Casos A, B, C, D — a regra universal, uma única remoção.

classificada → while (...) p = suc;            Caso E — remove o bloco de repetidos inteiro.

não classificada → guarda suc antes de       Caso F — varre tudo sem precisar gerenciar ant
comparar                              manualmente.

                                                      Página 58 de

<!-- página 59 -->

Apostila: Listas Lineares em C

  ERRO CLÁSSICO (VALE PARA AS DUAS LISTAS)

Chamar p->prox DEPOIS de já ter feito free(p). Assim como na LSE, é obrigatório guardar o sucessor
em uma variável (suc = p->prox;) ANTES de liberar o nó atual.

  Quiz Relâmpago:

25. Por que a exclusão do único elemento na LDE circular NÃO precisa de tratamento especial?

      a) Porque um único elemento não pode ser excluído

     b) Porque A e S apontam ambos para a cabeça, e a regra universal reconecta a cabeça com ela mesma

      c) Porque o único elemento nunca tem antecessor nem sucessor

     d) Precisa sim de tratamento especial, assim como na LDE sem cabeça

26. Na exclusão de repetidos não classificados na LDE, por que não precisamos de uma variável 'ant' separada como fizemos na LSE?

      a) Porque a LDE não permite repetição

     b) Porque cada nó já conhece seu antecessor via p->ant, dispensando o rastreamento manual

      c) Porque a busca sempre retorna todos os nós de uma vez

     d) Não há diferença nenhuma em relação à LSE

  Exercício prático:

Desenhe C ↔ [20] ↔ [10] ↔ [20] ↔ [30] ↔ [20] ↔ C (NÃO classificada, com repetição). Simule a remoção de todos os 20, indicando a cada
passo quem são A, X e S antes de cada aplicação da regra universal.

   Para discutir em sala:

Compare o número total de "casos especiais" (blocos de código exclusivos para uma situação de fronteira) entre: lista sequencial, LSE sem cabeça, LSE
com cabeça, LDE sem cabeça e LDE circular com cabeça. O que essa comparação revela sobre o valor de uma boa escolha estrutural?

                                                      Página 59 de

<!-- página 60 -->

Apostila: Listas Lineares em C

 Capítulo 14 — Como Organizar Cada Projeto TAD

   OBJETIVOS
 Organizar um projeto em C separando interface (.h) de implementação (.c).
 Explicar por que a área info da cabeça precisa existir mesmo sem dado real.
 Reconhecer a criação de nó genérico (LSE e LDE).

 Cada uma das três implementações desta apostila deve morar em seu próprio projeto, com a MESMA
 divisão de responsabilidades:

   ProjetoListaSeq/
     lista_seq.h
     lista_seq.c
     main.c

   ProjetoLSE/
     lista_lse.h
     lista_lse.c
     main.c

   ProjetoLDE/
     lista_lde.h
     lista_lde.c
     main.c

Arquivo              O que contém

                    O contrato: comentários, protótipos, e o typedef público typedef struct
.h (interface)
                                       lista tLista;

                      A definição COMPLETA struct lista { ... };, as structs internas dos nós,
.c (implementação)
                            funções auxiliares (static) e todas as implementações.

                     NÃO deve conhecer struct lista, struct no_lse, struct no_lde, prox, ant,
main.c (cliente)
                              cabeca, ou o vetor interno dados.

  ERRO COMUM

 Se o main.c precisar escrever L->cabeca->prox ou L->dados[i] diretamente, a opacidade do TAD foi
 quebrada. Isso é sinal de que uma função está faltando na interface pública.

 14.1 Criação de nó LSE genérico

  static struct no_lse *cria_no_lse(const tLista *L, const void *elem)
  {
      struct no_lse *novo = malloc(sizeof(struct no_lse));
      if (novo == NULL)
          return NULL;

      novo->info = malloc(L->tamElem);
      if (novo->info == NULL) {
          free(novo);
          return NULL;

                                                       Página 60 de

<!-- página 61 -->

Apostila: Listas Lineares em C

     }

     memcpy(novo->info, elem, L->tamElem);
     novo->prox = NULL;

     return novo;
 }

14.2 Criação de nó LDE genérico

 static struct no_lde *cria_no_lde(const tLista *L, const void *elem)
 {
     struct no_lde *novo = malloc(sizeof(struct no_lde));
     if (novo == NULL)
         return NULL;

     novo->info = malloc(L->tamElem);
     if (novo->info == NULL) {
         free(novo);
         return NULL;
     }

     memcpy(novo->info, elem, L->tamElem);
     novo->ant = NULL;
     novo->prox = NULL;

     return novo;
 }

  DICA

Repare que cada nó guarda uma CÓPIA independente do elemento (malloc(L->tamElem) + memcpy), não
um ponteiro para a variável original do cliente. Isso protege a lista contra o cliente alterar ou destruir a
variável original depois da inclusão.

14.3 Por que a cabeça precisa de uma área info?

Porque, durante toda busca com sentinela (Capítulos 7 e 11):

 memcpy(L->cabeca->info, chave, L->tamElem);

A cabeça precisa temporariamente armazenar uma CÓPIA do mesmo tipo de elemento procurado — mesmo
nunca guardando um elemento "de verdade" do cliente. Por isso, na criação da lista, a área info da cabeça é
alocada com malloc(tamElem), exatamente como qualquer outro nó.

                                                      Página 61 de

<!-- página 62 -->

Apostila: Listas Lineares em C

Capítulo 15 — Quando Usar Cada Implementação?

  OBJETIVOS
Comparar as três implementações em critérios objetivos de uso.
Aplicar um roteiro de perguntas para escolher a implementação certa.

Situação                     Sequencial              LSE                   LDE circular

Acesso por índice muito
                      MELHOR                ruim                   ruim
frequente

Inserção após posição já
                         pode deslocar            O(1)                     O(1)
conhecida

Remoção de nó conhecido    pode deslocar             precisa do anterior       O(1)

Percorrer para trás           por índices             não                    sim

Boa localidade de cache       excelente             menor               menor

Memória extra por
                              baixa                 1 ponteiro             2 ponteiros
elemento

                                                                                      circularidade + cabeça
Casos de fronteira            deslocamentos          cabeça simplifica
                                                                                   simplificam

                          normalmente não
Sentinela de busca                                                  útil                             útil
                               necessária

15.1 Escolha guiada — pergunte nesta ordem

  1. Preciso acessar MUITO por posição (índice)?
     SIM → sequencial é forte candidata.

  2. Preciso inserir/remover SEM deslocar elementos?
     SIM → encadeada (LSE ou LDE).

  3. Preciso voltar para o nó ANTERIOR com frequência?
     SIM → LDE.

  4. Preciso economizar 1 ponteiro por nó e só ando para frente?
     SIM → LSE.

                                                      Página 62 de

<!-- página 63 -->

Apostila: Listas Lineares em C

  REGRA PRÁTICA RESUMIDA

Muito acesso por índice + poucas mudanças de tamanho → Sequencial.

Muitas inserções/remoções, só percorre para frente → LSE com cabeça.

Muitas inserções/remoções, precisa andar nos dois sentidos ou remover um nó já em mãos
rapidamente → LDE circular com cabeça.

  Quiz Relâmpago:

27. Para um programa que faz milhares de buscas por posição e raras inserções, qual implementação tende a ser melhor?

      a) LSE sem cabeça

     b) Lista sequencial

      c) LDE circular

     d) Todas têm o mesmo desempenho

28. Se o programa precisa remover, com frequência, um nó do qual já se tem o ponteiro (sem precisar buscar), qual estrutura evita percorrer a
lista para achar o anterior?

      a) Lista sequencial

     b) LSE (sem qualquer ajuda extra)

      c) LDE, porque cada nó já conhece seu ->ant

     d) Nenhuma delas evita esse percurso

   Para discutir em sala:

Pense em um sistema real (ex.: histórico do navegador, fila de impressão, playlist de música). Qual das três implementações você escolheria para cada
um, e por quê?

                                                      Página 63 de

<!-- página 64 -->

Apostila: Listas Lineares em C

Capítulo 16 — Atlas de Erros Comuns

  OBJETIVOS
Reconhecer os erros mais frequentes cometidos por quem está aprendendo listas.
Associar cada erro ao raciocínio incorreto que costuma causá-lo.
Usar esta tabela como checklist antes de entregar qualquer implementação.

Cada linha desta tabela representa um erro que aparece repetidamente entre alunos. A segunda coluna
mostra o raciocínio (errado) que costuma levar até ele; a terceira mostra a correção mental que evita cair na
armadilha.

                       O que o aluno
Erro                                                        Correção mental
                                provavelmente pensou

                                    "Copiar bytes é sempre
Usar memcpy em deslocamento                               Se origem e destino se sobrepõem →
                                           igual, não importa a
sobreposto                                         memmove, não memcpy.
                                    função."

                                "Todo nó da lista é um      A cabeça é estrutura, NÃO conta em
Tratar a cabeça como dado real
                                elemento do cliente."         qtd.

Na LSE sem cabeça, esquecer de     "Todo nó tem um anterior   O primeiro nó NÃO tem anterior real
atualizar prim                           real."           — por isso a cabeça existe.

Na LSE, remover e avançar ant        "Preciso sempre avançar os   ant só avança quando o nó atual É
sempre                              dois ponteiros juntos."      MANTIDO, nunca quando é removido.

Acessar atual->prox depois de      "O endereço ainda está      Guarde suc = atual->prox ANTES do
free(atual)                       guardado na variável."        free — depois é memória liberada.

Na LDE, esquecer de atualizar S-      "Só o caminho para frente    As DUAS direções (->ant e ->prox)
>ant                                  (->prox) importa."         devem continuar consistentes.

Na LDE convencional, esquecer de   "Remover o último é igual   Sem circularidade, ult PRECISA ser
atualizar ult                      a remover do meio."          atualizado manualmente.

Na busca com sentinela, não testar   "Se o laço parou, é porque  O laço pode ter parado NA PRÓPRIA
p == cabeça                        encontrou."                  sentinela — sempre confira.

Comparar a struct inteira em vez     "Igualdade significa todos   A política de repetição é baseada
da chave                          os campos iguais."        SOMENTE na chave, via L->compara.

                                                               malloc pode falhar; sempre trate o
Esquecer de verificar malloc ==
                                   "malloc sempre funciona."    retorno NULL antes de usar o
NULL
                                                                   ponteiro.

Confundir p e *p (ou p e p-           "É basicamente a mesma    p é o ENDEREÇO; *p ou p->campo é o
>campo)                               coisa."                CONTEÚDO apontado.

Esquecer o free(L->cabeca->info) e   "Só os nós reais precisam    A cabeça também foi alocada com
free(L->cabeca) ao destruir           ser liberados."               malloc — também precisa de free.

                                                      Página 64 de

<!-- página 65 -->

Apostila: Listas Lineares em C

  USE ESTA TABELA COMO CHECKLIST

Antes de considerar uma implementação pronta, releia as três colunas de cada linha e pergunte: "eu
cometi esse erro no meu código?". A maioria dos bugs em listas encadeadas se encaixa em uma
destas doze linhas.

Capítulo 17 — Checkpoints de Compreensão

Responda a cada pergunta COM SUAS PRÓPRIAS PALAVRAS, sem olhar o texto, antes de conferir no Gabarito
Geral (final da apostila). Se travar em alguma, volte ao capítulo correspondente antes de seguir.

1. Quais são as cinco propriedades fundamentais de uma lista linear apresentadas no Capítulo 1?

2. Qual a diferença entre ordem posicional e classificação por chave?

3. Por que a classificação deve ser preservada durante a inclusão?

4. Por que a não repetição deve ser verificada antes da inclusão?

5. Qual o problema da inclusão no meio da lista sequencial?

6. Por que usamos memmove e não memcpy para deslocar elementos dentro do vetor?

7. Na LSE sem cabeça, por que inserir antes do primeiro é um caso especial?

8. Como a cabeça transforma esse caso especial em um caso normal?

9. Cabeça e sentinela são exatamente a mesma coisa? Explique.

10. Como a sentinela elimina o teste de fim dentro da busca?

11. Como sabemos se a busca parou em um nó real ou na sentinela?

12. Na LSE, por que a busca costuma devolver o nó anterior (ant)?

13. Na LDE, por que a busca não precisa devolver o anterior?

14. Qual o problema de manter prim e ult separados numa LDE convencional?

15. Como a circularidade com cabeça representa o primeiro e o último elemento ao mesmo tempo?

16. Por que excluir o único elemento na LDE circular não exige nenhum caso especial?

                                                      Página 65 de

<!-- página 66 -->

Apostila: Listas Lineares em C

Capítulo 18 — Exercícios Graduados

Os exercícios avançam em dificuldade — do desenho puro até um projeto completo. Resolva na ordem.

Nível 1 — somente desenho

  •  18.1 Desenhe uma lista sequencial [10,20,40] e simule a inclusão de 30.

  •  18.2 Desenhe uma LSE sem cabeça e mostre por que inserir antes do primeiro exige mudar prim.

  •  18.3 Repita o exercício anterior usando cabeça.

  •  18.4 Desenhe uma LDE convencional com prim e ult e remova o único elemento.

  •  18.5 Repita em LDE circular com cabeça e compare o número de casos especiais.

Nível 2 — rastreamento

  •  18.6 Em C→[10]→[20]→[30]→C, simule a busca com sentinela por 25 em lista NÃO classificada.

  •  18.7 Na mesma lista, agora classificada, simule a busca por 25 e indique ant/atual.

  •  18.8 Em C↔[10]↔[20]↔[30]↔C, indique A, X e S para remover 20.

  •  18.9 Em C↔[10]↔[20]↔C, indique ant e suc para inserir 30 no fim.

Nível 3 — código

  •  18.10 Implemente lista_busca na lista sequencial, copiando a resposta com memcpy.

  •  18.11 Altere a LSE classificada com repetição para inserir DEPOIS do bloco de chaves iguais (em vez de
      antes).

  •  18.12 Implemente percurso reverso na LDE circular.

  •  18.13 Implemente uma função genérica de percurso que receba void (*visita)(const void *).

Nível 4 — projeto

18.14 Use uma struct Produto com codigo como chave. Crie as TRÊS implementações do TAD (sequencial,
LSE, LDE) e execute exatamente a sequência: inserir 30, 10, 20, 20; excluir 10; excluir 20. Faça isso para as
QUATRO combinações de classificação/repetição e desenhe o estado da lista após CADA operação, em cada
uma das três implementações — um total de 12 simulações completas.

  DICA

Este último exercício é o mais valioso da apostila: ele obriga você a aplicar, de memória, todas as
regras universais e todos os casos de fronteira estudados até aqui, nas três estruturas ao mesmo
tempo.

                                                      Página 66 de

<!-- página 67 -->

Apostila: Listas Lineares em C

Capítulo 19 — Mapa Mental Final

  LISTA = TAD
   │
   ├── propriedades:
   │    - 0 ou mais nós
   │    - inserir em qualquer posição
   │    - excluir qualquer nó
   │    - acessar
   │    - percorrer
   │
   ├── políticas:
   │    - classificada?
   │    - repetição?
   │
   └── implementações:
        │
        ├── SEQUENCIAL
        │    dados contíguos
        │    inserir/excluir → deslocar
        │    memcpy para o novo elemento
        │    memmove para o deslocamento
        │
        ├── LSE SEM CABEÇA
        │    primeiro é caso especial
        │    busca termina em NULL
        │
        ├── LSE COM CABEÇA CIRCULAR
        │    antes do primeiro = depois da cabeça
        │    último aponta para a cabeça
        │    cabeça funciona como SENTINELA
        │
        ├── LDE SEM CABEÇA
        │    prim + ult
        │    vários casos especiais nas extremidades
        │
        └── LDE CIRCULAR COM CABEÇA
             C->prox = primeiro
             C->ant  = último
             inserir antes de C = inserir no fim
             remover qualquer nó = mesma regra universal
             busca com sentinela

  FRASE PARA LEVAR PARA A PROVA

Primeiro desenhe a estrutura. Depois marque quem é o anterior, o atual e o sucessor. Se o desenho
estiver correto, o código vira apenas a tradução das setas.

                                                      Página 67 de

<!-- página 68 -->

Apostila: Listas Lineares em C

Glossário

Consulte sempre que encontrar um termo técnico que não lembra o significado.

Termo                           Significado

                                Tipo Abstrato de Dados: define O QUE uma estrutura faz, sem revelar
TAD
                     COMO ela é implementada por dentro.

                   Um tipo declarado (typedef struct lista tLista;) cujos campos internos
Tipo opaco
                               ficam escondidos do cliente.

                             Função fornecida pelo cliente que diz se uma chave é menor, igual ou
Comparador de chaves
                             maior que outra.

                        Campo usado para identificar ou ordenar um elemento (ex.: matrícula,
Chave
                                 código, CPF).

                                      Política que exige que a lista preserve a ordem das chaves durante a
Classificada
                                   inclusão.

Repetição                           Política que define se chaves iguais podem ou não coexistir na lista.

                               Bloco de memória que guarda um elemento de verdade do cliente, junto
Nó (nó real)
                        com ponteiro(s) de encadeamento.

                        Nó estrutural PERMANENTE, criado junto com a lista, que nunca guarda
Cabeça
                     um dado real do cliente.

                           Uso TEMPORÁRIO da cabeça durante a busca: copia-se a chave
Sentinela
                              procurada para ela, garantindo que o laço sempre encontre uma parada.

                                      Lista em que o último nó aponta de volta para o primeiro (ou para a
Lista circular
                                 cabeça), eliminando o NULL do encadeamento.

                             Função que copia bytes entre duas áreas de memória que NÃO se
memcpy
                             sobrepõem.

                             Função que copia bytes mesmo quando origem e destino podem se
memmove
                              sobrepor — usada em deslocamentos dentro do mesmo vetor.

                            Convenção de nomes para o nó anterior, o nó em questão e o sucessor
ant / atual / suc (ou A / X / S)
                              durante uma operação.

                                      Lista Simplesmente Encadeada: cada nó aponta apenas para o próximo (-
LSE
                                  >prox).

                                      Lista Duplamente Encadeada: cada nó aponta para o próximo e para o
LDE
                                  anterior (->prox e ->ant).

                             Implementação em que todos os elementos ficam lado a lado em um
Lista sequencialmente alocada
                                vetor contínuo.

                          Mover um bloco de elementos para abrir ou fechar espaço em uma lista
Deslocamento
                                  sequencial.

Busca linear                     Percorre elemento por elemento até encontrar (ou não) a chave.

                                 Divide o intervalo de busca ao meio repetidamente; só funciona em listas
Busca binária
                                     classificadas (ordenadas).

                                                      Página 68 de

<!-- página 69 -->

Apostila: Listas Lineares em C

                              novo->prox = ant->prox; ant->prox = novo; — funciona para todos os
Regra universal (inclusão)
                                casos de inclusão na LSE com cabeça.

                                   Religar os vizinhos de um nó entre si antes de liberar sua memória —
Regra universal (exclusão)
                                funciona para todos os casos em listas com cabeça.

                                                      Página 69 de

<!-- página 70 -->

Apostila: Listas Lineares em C

Gabarito Geral

Confira suas respostas SOMENTE depois de tentar responder sozinho (Passo 1 do ciclo de estudo).

Gabarito dos Quizzes Relâmpago

     Cap
Nº         Resposta                              Por quê
         .

                                                               Lista vazia (n=0) é um estado previsto
1    1      b) Válido
                                                      pela definição formal.

                                                           classificada=1 e repeticao=0 juntas
2    1       c) A operação falha e a lista não muda
                                                proíbem chave duplicada.

             b) Evita que o cliente dependa da
3    2                                          É a definição de tipo opaco.
            representação interna

                                               Convenção do comparador: > 0
4    2       c) Qualquer valor positivo
                                             quando chave(a) > chave(b).

             b) O endereço é calculado por fórmula,
5    3                                              elem_em(L,i) = dados + i*tamElem.
          sem percorrer nós

             b) Quando origem e destino podem se      Definição de uso correto de
6    3
            sobrepor                         memmove.

                                                 pos = qtd já é a primeira posição livre
7    4      b) Nenhum
                                             no final.

                                                               Política de repetição proibida com
8    4      b) Retorna -1 e não modifica a lista
                                                 chave já existente.

             b) Porque a inserção sempre preserva a    Consequência direta de
9    5
          ordem crescente                             classificada=1.

             b) Porque as ocorrências podem estar    Sem ordem, repetidos não formam
10   5
            espalhadas                              bloco único.

             b) Não existe nó anterior real; prim      O ponteiro que referencia o 1º nó
11   6
             precisa mudar                                  fica fora de qualquer nó.

                                        Sem sentinela, o laço checa NULL e a
12   6      b) Duas: se p existe E se a chave bate
                                                 chave a cada passo.

             b) Cabeça é permanente; sentinela é o
13   7                                                Definição central do capítulo.
           uso temporário dela

             b) Verificando se pAtual é a cabeça ou
14   7                                           Retorno de busca_lse.
        um nó real igual

             b) Porque a cabeça garante que ant
15   8                                 Mesmo na lista vazia, ant=C é válido.
           sempre existe

                                         A sentinela garante que a busca
16   8      b) No próprio nó cabeça (C)
                                              sempre para, mesmo sem achar.

             b) Apenas quando o nó atual NÃO é        Erro clássico: avançar ant após
17   9
           removido                           remoção quebra a lista.

                                                      Página 70 de

<!-- página 71 -->

Apostila: Listas Lineares em C

             b) Porque ler atual->prox após free seria
18   9                                           Guardar suc antes é obrigatório.
             acessar memória liberada

             b) O dobro: quatro na LDE contra duas     Cada nó da LDE tem dois vizinhos
19   10
           na LSE                                  para religar.

             b) Porque prim e ult precisam ser         Único nó = ambas as referências
20   10
            zerados ao mesmo tempo            mudam juntas.

             b) C->prox aponta para o 1º e C->ant      É a definição da cabeça circular na
21   11
            para o último, sempre                    LDE.

             b) Porque cada nó já guarda seu          Vantagem estrutural da LDE sobre a
22   11
            antecessor em p->ant                       LSE.

                                                     ant é sempre derivado de suc, nunca
23   12     b) É calculado como suc->ant
                                                buscado à parte.

             b) Porque suc->ant, mesmo na cabeça    A regra universal funciona sozinha,
24   12
              vazia, aponta pra ela mesma           sem if especial.

             b) Porque A e S apontam ambos para a    A regra universal reconecta a cabeça
25   13
            cabeça nesse caso                  com ela mesma.

             b) Porque cada nó já conhece seu          Dispensa o rastreamento manual de
26   13
            antecessor via p->ant                       ant, usado na LSE.

                                                  Acesso por índice é o ponto forte
27   15     b) Lista sequencial
                                                      dela (O(1)).

               c) LDE, porque cada nó já conhece seu -    Remoção O(1) sem precisar buscar o
28   15
            >ant                                         anterior.

Gabarito resumido dos Checkpoints (Capítulo 17)

1. Pode ter 0 ou mais nós; incluir em qualquer posição; eliminar qualquer nó; acessar qualquer nó; visitar os
nós em sequência.

2. Ordem posicional diz quem vem antes/depois na estrutura; classificação impõe ordem segundo uma
chave específica.

3. Porque a lista prometeu ao cliente manter seus elementos classificados — quebrar isso violaria o contrato
do TAD.

4. Porque a política do TAD proíbe duas chaves iguais; inserir sem checar quebraria essa promessa.

5. Não há espaço físico entre elementos contíguos: é necessário deslocar um bloco inteiro para abrir espaço.

6. Porque origem e destino podem se sobrepor durante o deslocamento; memcpy tem comportamento
indefinido nesse caso.

7. Porque não existe nó anterior real; é necessário alterar diretamente o ponteiro prim, que fica fora de
qualquer nó.

8. O primeiro nó real passa a ter a cabeça como seu anterior — deixando de ser um caso especial.

9. Não. A cabeça é uma estrutura permanente; a sentinela é o uso temporário da cabeça para garantir a
parada da busca.

10. A chave procurada é copiada para dentro da cabeça; assim, alguma comparação sempre vai encontrar
uma chave igual.

                                                      Página 71 de

<!-- página 72 -->

Apostila: Listas Lineares em C

11. Se o ponteiro onde a busca parou é a própria cabeça, não houve ocorrência real da chave na lista.

12. Porque a LSE precisa do nó anterior para religar o ponteiro prox durante inclusão/exclusão.

13. Porque, na LDE, o próprio nó encontrado já possui o campo ant, dispensando um retorno adicional.

14. Primeiro, último, vazio e único nó exigem atualizações especiais e separadas de prim e/ou ult.

15. cabeca->prox aponta para o primeiro elemento real, e cabeca->ant aponta para o último elemento real.

16. Porque o único nó tem a cabeça como antecessor E sucessor ao mesmo tempo; a regra universal
reconecta a cabeça com ela mesma automaticamente.

                                                      Página 72 de

<!-- página 73 -->

Apostila: Listas Lineares em C

Apêndice A — Código de Apoio da Lista Sequencial

Os trechos a seguir completam as rotinas de criação, busca e destruição usadas no capítulo da lista
sequencial. Eles aparecem depois das simulações de propósito, para que você primeiro compreenda o
MOVIMENTO dos elementos e só então leia a implementação completa.

 static void *elem_em(tLista *L, size_t i)
 {
     return L->dados + i * L->tamElem;
 }

 static const void *elem_em_const(const tLista *L, size_t i)
 {
     return L->dados + i * L->tamElem;
 }

 tLista *lista_seq_cria(size_t tamElem,
                        size_t capacidade,
                        int classificada,
                        int repeticao,
                        tListaCompara compara)
 {
     if (tamElem == 0 || capacidade == 0 || compara == NULL)
         return NULL;

     tLista *L = malloc(sizeof(tLista));
     if (L == NULL)
         return NULL;

     L->dados = malloc(capacidade * tamElem);
     if (L->dados == NULL) {
         free(L);
         return NULL;
     }

     L->qtd = 0;
     L->capacidade = capacidade;
     L->tamElem = tamElem;
     L->classificada = classificada != 0;
     L->repeticao = repeticao != 0;
     L->compara = compara;

     return L;
 }

 void lista_seq_destroi(tLista *L)
 {
     if (L == NULL) return;
     free(L->dados);
     free(L);
 }

 static int busca_linear(tLista *L,

                                                      Página 73 de

<!-- página 74 -->

Apostila: Listas Lineares em C

                         const void *chave,
                         size_t *pos)
{
    size_t i = 0;

    while (i < L->qtd &&
           L->compara(elem_em(L, i), chave) != 0) {
        i++;
    }

    *pos = i;
    return i < L->qtd;
}

static int busca_binaria(tLista *L,
                          const void *chave,
                          size_t *pos)
{
    size_t ini = 0;
    size_t fim = L->qtd;  /* intervalo [ini, fim) */

    while (ini < fim) {
        size_t meio = ini + (fim - ini) / 2;
        int c = L->compara(elem_em(L, meio), chave);

        if (c < 0)
            ini = meio + 1;
        else
            fim = meio;
    }

    *pos = ini;

    return ini < L->qtd &&
           L->compara(elem_em(L, ini), chave) == 0;
}

int lista_seq_busca(const tLista *L,
                     const void *chave,
                     void *saida)
{
    if (L == NULL || chave == NULL || saida == NULL)
        return 0;

    /*
     * Para simplificar, a versão pública faz busca linear.
     * A implementação pode escolher busca binária se classificada.
     */
    size_t i = 0;
    while (i < L->qtd &&
           L->compara(elem_em_const(L, i), chave) != 0) {
        i++;
    }

    if (i == L->qtd)
        return 0;

                                                     Página 74 de

<!-- página 75 -->

Apostila: Listas Lineares em C

     memcpy(saida, elem_em_const(L, i), L->tamElem);
     return 1;
 }

 COMO ESTUDAR ESTE APÊNDICE

Volte aos desenhos do Capítulo 3, 4 e 5 e associe cada memmove a um bloco que você desenhou
se deslocando. Não tente decorar os parâmetros sem relacioná-los ao desenho correspondente.

                                                      Página 75 de

<!-- página 76 -->

Apostila: Listas Lineares em C

Apêndice B — Código de Apoio da LSE com Cabeça e Sentinela

 /* lista_lse.c */

 struct no_lse {
     void *info;
     struct no_lse *prox;
 };

 struct lista {
     struct no_lse *cabeca;
     size_t qtd;
     size_t tamElem;
     int classificada;
     int repeticao;
     tListaCompara compara;
 };

 static struct no_lse *cria_no_lse(const tLista *L,
                                    const void *elem)
 {
     struct no_lse *novo = malloc(sizeof(struct no_lse));
     if (novo == NULL)
         return NULL;

     novo->info = malloc(L->tamElem);
     if (novo->info == NULL) {
         free(novo);
         return NULL;
     }

     memcpy(novo->info, elem, L->tamElem);
     novo->prox = NULL;
     return novo;
 }

 tLista *lista_lse_cria(size_t tamElem,
                        int classificada,
                        int repeticao,
                        tListaCompara compara)
 {
     if (tamElem == 0 || compara == NULL)
         return NULL;

     tLista *L = malloc(sizeof(tLista));
     if (L == NULL)
         return NULL;

     L->cabeca = malloc(sizeof(struct no_lse));
     if (L->cabeca == NULL) {
         free(L);
         return NULL;
     }

                                                      Página 76 de

<!-- página 77 -->

Apostila: Listas Lineares em C

    /* A cabeça precisa receber a chave-sentinela nas buscas. */
    L->cabeca->info = malloc(tamElem);
    if (L->cabeca->info == NULL) {
        free(L->cabeca);
        free(L);
        return NULL;
    }

    /* Lista vazia circular: a cabeça aponta para si mesma. */
    L->cabeca->prox = L->cabeca;

    L->qtd = 0;
    L->tamElem = tamElem;
    L->classificada = classificada != 0;
    L->repeticao = repeticao != 0;
    L->compara = compara;

    return L;
}

int lista_lse_busca(tLista *L,
                     const void *chave,
                     void *saida)
{
    if (L == NULL || chave == NULL || saida == NULL)
        return 0;

    struct no_lse *ant;
    struct no_lse *atual;

    if (!busca_lse(L, chave, &ant, &atual))
        return 0;

    memcpy(saida, atual->info, L->tamElem);
    return 1;
}

void lista_lse_destroi(tLista *L)
{
    if (L == NULL) return;

    struct no_lse *p = L->cabeca->prox;

    while (p != L->cabeca) {
        struct no_lse *suc = p->prox;
        free(p->info);
        free(p);
        p = suc;
    }

    free(L->cabeca->info);
    free(L->cabeca);
    free(L);
}

                                                     Página 77 de

<!-- página 78 -->

Apostila: Listas Lineares em C

   VERIFICAÇÃO VISUAL ANTES DE DESTRUIR

Desenhe C → A → B → C. A destruição precisa guardar B ANTES de liberar A; depois guardar C antes de
liberar B. Somente depois de todos os nós reais liberados, a própria cabeça é liberada.

                                                      Página 78 de

<!-- página 79 -->

Apostila: Listas Lineares em C

Apêndice C — Código de Apoio da LDE Circular com Cabeça

 /* lista_lde.c */

 struct no_lde {
     void *info;
     struct no_lde *ant;
     struct no_lde *prox;
 };

 struct lista {
     struct no_lde *cabeca;
     size_t qtd;
     size_t tamElem;
     int classificada;
     int repeticao;
     tListaCompara compara;
 };

 static struct no_lde *cria_no_lde(const tLista *L,
                                    const void *elem)
 {
     struct no_lde *novo = malloc(sizeof(struct no_lde));
     if (novo == NULL)
         return NULL;

     novo->info = malloc(L->tamElem);
     if (novo->info == NULL) {
         free(novo);
         return NULL;
     }

     memcpy(novo->info, elem, L->tamElem);
     novo->ant = NULL;
     novo->prox = NULL;
     return novo;
 }

 tLista *lista_lde_cria(size_t tamElem,
                        int classificada,
                        int repeticao,
                        tListaCompara compara)
 {
     if (tamElem == 0 || compara == NULL)
         return NULL;

     tLista *L = malloc(sizeof(tLista));
     if (L == NULL)
         return NULL;

     L->cabeca = malloc(sizeof(struct no_lde));
     if (L->cabeca == NULL) {
         free(L);
         return NULL;

                                                      Página 79 de

<!-- página 80 -->

Apostila: Listas Lineares em C

    }

    L->cabeca->info = malloc(tamElem);
    if (L->cabeca->info == NULL) {
        free(L->cabeca);
        free(L);
        return NULL;
    }

    /* Lista vazia circular nos dois sentidos. */
    L->cabeca->prox = L->cabeca;
    L->cabeca->ant = L->cabeca;

    L->qtd = 0;
    L->tamElem = tamElem;
    L->classificada = classificada != 0;
    L->repeticao = repeticao != 0;
    L->compara = compara;

    return L;
}

int lista_lde_busca(tLista *L,
                     const void *chave,
                     void *saida)
{
    if (L == NULL || chave == NULL || saida == NULL)
        return 0;

    struct no_lde *p;

    if (!busca_lde(L, chave, &p))
        return 0;

    memcpy(saida, p->info, L->tamElem);
    return 1;
}

void lista_lde_destroi(tLista *L)
{
    if (L == NULL) return;

    struct no_lde *p = L->cabeca->prox;

    while (p != L->cabeca) {
        struct no_lde *suc = p->prox;
        free(p->info);
        free(p);
        p = suc;
    }

    free(L->cabeca->info);
    free(L->cabeca);
    free(L);
}

                                                     Página 80 de

<!-- página 81 -->

Apostila: Listas Lineares em C

  FIM DA APOSTILA

Se você chegou até aqui tendo refeito cada simulação com lápis e papel, já tem o repertório
completo para implementar, explicar e depurar as três estruturas de lista linear mais usadas em C.
Volte ao Capítulo 18 (Exercícios Graduados) sempre que quiser testar sua compreensão de forma
mais exigente.

                                                      Página 81 de
