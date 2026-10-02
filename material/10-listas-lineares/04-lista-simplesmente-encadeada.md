# ListaSimplesmenteEncadeadaIntrod

> Fonte: `base/Listas/ListaSimplesmenteEncadeadaIntrod.pdf`

<!-- página 1 -->

ESTRUTURA DE DADOS

                LISTAS LINEARES
              ENCADEADAS

Baseado no material de prof. Dr. Daniel Caetano

<!-- página 2 -->

Discussão sobre Listas Encadeadas

•  Lista Encadeada: lista de elementos interligados, como em uma
   corrente
   – O primeiro elemento aponta para o segundo
   – O segundo aponta para o terceiro
   – O terceiro aponta para o quarto
   – ...

                                   2

                                                             4

          1

                       3

<!-- página 3 -->

Discussão sobre Listas Encadeadas

•  Lista Encadeada: lista de elementos interligados, como em uma
   corrente
•  Para acrescentar outro elemento no fim?
    •  Criar o novo elemento
    •  Associá-lo à lista (encadeá-lo)

                                  2
                                                            4

         1

                                              5                      3

<!-- página 4 -->

Como criar um novo nó?

Alocação Dinâmica de Memória
 – Reservar no momento necessário: malloc
 – Liberar quando não for mais necessário: free

<!-- página 5 -->

Representando Listas Encadeadas

• Como vimos na dinâmica:
  – Lista encadeada é composta por nós
• Um nó tem a função de...                  Valor 1
  – Guardar um elemento da lista
• Que outra informação tem o nó?

                                                       Valor 2  – Um ponteiro que indique o próximo nó
• E quando não há um próximo nó?
  – Ajustamos o ponteiro para valer NULL

                                                      Valor 3• Onde começa uma lista encadeada?
  – Precisamos de um ponteiro para isso!    NULL

<!-- página 6 -->

Representando Listas Encadeadas

Valor 1           Cada elemento da
                       lista encadeada é
               uma struct

Valor 2

                 Criaremos uma
                    struct chamada
Valor 3
                  Elemento
 NULL

<!-- página 7 -->

Representando Listas Encadeadas

       struct elemento {
          int info;
          ...struct elemento* prox;
       };
       typedef struct elemento Elemento;Valor 1

                     Cada “Elemento” é
                     composto de uma
Valor 2
                         informação

Valor 3                 E de um ponteiro
 NULL                 para outro “Elemento"

<!-- página 8 -->

Representando Listas Encadeadas

          struct lista {
             Elemento * prim;
          };
          typedef struct lista tLista;
Valor 1

                 A lista é
Valor 2
                  representada por
           um ponteiro para o
                  primeiro Elemento
Valor 3

 NULL

<!-- página 9 -->

Representando Listas Encadeadas

       struct elemento {
Valor 1      int info;
          struct elemento* prox;
       };
       typedef struct elemento Elemento;

Valor 2

          tLista * lst;

Valor 3

 NULL

<!-- página 10 -->

INICIALIZANDO
LISTAS ENCADEADAS E
     INSERINDO NÓS

<!-- página 11 -->

Listas Encadeadas: Inicializando

lista    prim=NULL
             Quando criamos uma
                         lista, ela está vazia
                    tLista lista;
                    lista_cria(&lista);

 void lista_cria(tLista * lst) {
                  lst->prim=NUL
   }paacrescentar um
              outro elemento?

<!-- página 12 -->

Listas Encadeadas: Inserção

lista    NULL         Como criamos um
                      elemento?

       1
tmp
              Elemento *tmp;     NULL
            tmp = (Elemento * ) malloc( sizeof (Elemento));
                          if (tmp!=NULL)
                      {
                      tmp->valor = 1;      //preencher infs
                    tmp-> prox = NULL    // sucessor
                      }

<!-- página 13 -->

Listas Encadeadas: Inserção

lista    NULL        Como colocar o
               elemento na lista?

       1
tmp                    if (lista_vazia(&lista) {     NULL
                 lista->prim = tmp;
                }

<!-- página 14 -->

Listas Encadeadas: Inserção

lista              Como colocar o
                  elemento na lista?

       1
tmp                          if (lista_vazia(&lista) {     NULL
                     lista->prim = tmp;
                    }

<!-- página 15 -->

Listas Encadeadas: Inserção

lista                          sta?

                          if (lista_vazia(&lista) {
       1
                     lista->prim = tmp;     NULL
                    }

       2
tmp
     NULL
               E para acrescentar um
                   outro elemento?

<!-- página 16 -->

Listas Encadeadas: Inserção

lista                    if (lista_vazia(&lista) {
                  lista->prim = tmp;

       1

     NULL
                   } else {
                 tmp->prox = lista
                   }       2
tmp
               E para acrescentar um
                   outro elemento?

<!-- página 17 -->

Listas Encadeadas: Inserção

lista                     if (lista_vazia(&lista) {
                   lista->prim = tmp;

       1                    } else {
     NULL
               tmp->prox = lista
                   lista->prim = tmp;
                     }       2
tmp
               E para acrescentar um
                   outro elemento?

<!-- página 18 -->

Listas Encadeadas: Inserção

lista                       if (lista_vazia(&lista) {
       1              lista->prim = tmp;

     NULL                      } else {
                tmp->prox = lista       2
                    lista->prim = tmp;
                      }
       3
tmp
     NULL         E para acrescentar um
                    outro elemento?

<!-- página 19 -->

Listas Encadeadas: Inserção

lista                      if (lista_vazia(&lista) {
       1              lista->prim = tmp;

     NULL                     } else {
                tmp->prox = lista       2
                    lista->prim = tmp;
                     }
       3
tmp
                E para acrescentar um
                    outro elemento?

<!-- página 20 -->

Listas Encadeadas: Inserção

lista                     if (lista_vazia(&lista) {
       1             lista->prim = tmp;

     NULL                     } else {
                tmp->prox = lista       2
                  lista->prim = tmp;
                     }
       3
tmp
               E para acrescentar um
                   outro elemento?

<!-- página 21 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 22 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 23 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 24 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 25 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 26 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 27 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 28 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

     NULL
                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 29 -->

Listas Encadeadas: Listar/Percorrer

lista            Como percorrer uma
                           lista encadeada?       1

                     Basta seguir os
       2
                         ponteiros...

       3

<!-- página 30 -->

Listas Encadeadas: Remoção

lista              Como remover o
                    primeiro elemento?       1

     NULL                                 if (!lista_vazia(lista )) {
                  tmp = lista->prim;
       2                         lista->prim= tmp->prox;
                          free( tmp);
                            }

       3

<!-- página 31 -->

Listas Encadeadas: Remoção

lista             Como remover o
                  primeiro elemento?       1

     NULL                                  if (!lista_vazia(lista )) {
                  tmp = lista->prim;
       2                          lista->prim = tmp->prox;
                           free( tmp);
                             }

       3

<!-- página 32 -->

Listas Encadeadas: Remoção

lista             Como remover o
                  primeiro elemento?       1
     NULL                    if (!lista_vazia(lista )) {
                   tmp = lista->prim;
       2                           lista->prim = tmp->prox;
                            free( tmp);
                              }

       3
tmp

<!-- página 33 -->

Listas Encadeadas: Remoção

lista             Como remover o
                  primeiro elemento?       1
     NULL                    if (!lista_vazia(lista )) {
                   tmp = lista->prim;
       2                          lista->prim= tmp->prox
                            free( tmp);
                              }

       3
tmp

<!-- página 34 -->

Listas Encadeadas: Remoção

lista             Como remover o
                  primeiro elemento?       1
     NULL                    if (!lista_vazia(lista )) {
                   tmp = lista->prim;
       2                           lista->prim = tmp->prox;
                            free( tmp);
                              }

       3
tmp

<!-- página 35 -->

Listas Encadeadas: Remoção

lista             Como remover o
                  primeiro elemento?       1
     NULL                    if (!lista_vazia(lista )) {
                   tmp = lista->prim;
       2                           lista->prim= tmp->prox;
                           free( tmp);
                              }

       3
tmp

<!-- página 36 -->

Listas Encadeadas: Remoção

lista             Como remover o
                  primeiro elemento?       1
     NULL                    if (!lista_vazia(lista )) {
                   tmp = lista->prim;
       2                           lista->prim= tmp->prox;
                           free( tmp);
                              }

tmp

<!-- página 37 -->

Operações Com Listas Encadeadas
• Apresentadas
  – Inicialização
  – Inserção no Início
  – Listar / Percorrer
  – Remover
• A realizar:
  – Buscar
  – Inserir no final
  – Inserir no meio
  – Remover do meio
