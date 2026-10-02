# ListaAplicacaoPilhaOuFila

> Fonte: `base/Pilha e Fila/ListaAplicacaoPilhaOuFila.pdf`

<!-- página 1 -->

Lista de exercícios: Lista, Pilha e Fila Sequencial
Questão 1 :
Uma loja de roupas quer fazer um cadastro de seus clientes. Para cada cliente deseja-se
guardar:
   -  nome(string 20),
   -   identidade(int),
   -   idade(int),
   -  sexo(char) e
   -   telefone(string 9).
1.a) Defina o tipo t_cli, para cliente.
1.b) Defina o tipo t_lista_cli, para lista seqüencial de clientes.
1.c) Escreva as funções inic_ord(  ), é_vazia_ord(  ), busca_ord( ) e ins_ord( ) para uma lista
ordenada de clientes do tipo t_lista_cli definido anteriormente. A ordenação da lista é pelo
campo identidade. A busca_ord( ) deve levar em conta a ordenação, assim como, obviamente, a
ins_ord( ) deve preservar a ordenação. Essas duas funções devem retornar 1 quando realizadas
com sucesso e 0 em caso contrário. Caso o elemento já esteja na lista, a ins_ord( ) deve retornar
(-1).
1.d) Escreva a função lê_dados_de_um_cliente( ) que lê do teclado os dados de um cliente do
tipo t_cli acima, retornando-os. A função retorna 1 se for feita uma leitura válida, ou 0, quando
uma identidade menor ou igual a 0 for lida.
1.e) Escreva a função constrói_lista_clientes( ) que, utilizando as funções dos itens 1.c e 1.d,
constrói uma lista ordenada de clientes, retornando-a. A função deve retornar 1 se realizada
com sucesso e 0 em caso contrário.
1.f) Escreva a função merge_especial( ) que recebe duas listas do tipo t_lista_cli ordenadas por
identidade e faz um merge especial resultando em duas novas listas ordenadas: na primeira lista
ficam os elementos que só aparecem em uma das listas originais e na segunda lista ficam
aqueles que aparecem nas duas listas, ou seja, que têm duas ocorrências. A função deve
retornar as duas listas resultantes. Atenção: essa função deve ser a mais eficiente possível.

Questão 2 :
Duas crianças brincam de passar mensagens em código escrevendo as seqüências de não vogais
de trás para frente.
Exemplo:  PROVA DE ESTRUTURAS DE DADOS  vira  RPOVAD E ERTSUTURAD SED ADOS
2.1) Escreva os tipos e funções básicas para pilha seqüencial, com elemento do tipo char.
2.2) Escreva os tipos e funções básicas para fila seqüencial simples, inicialização F=0, R=-1, com
elemento do tipo char.
2.3) Escreva a função decifra(  ), para ler uma mensagem codificada do teclado, caracter a
caracter, e depois apresentar a mensagem original na tela. Sua função retorna 1 se realizada
com sucesso ou 0 em caso contrário.
ATENÇÃO: Não use vetores (ou strings) soltos. Utilize apenas estruturas dos tipos dos itens 2.1 e
2.2, assim como só as manipule utilizando as suas funções específicas.
Obs: Considere apenas letras maiúsculas.

Questão 3 :
Um restaurante grande colocou um computador na entrada para controlar a ocupação das suas
mesas. A idéia é que quando um cliente chega e não tem mesa vazia, seu nome fica armazenado
numa estrutura X. Quando uma mesa é liberada, um nome é retirado da estrutura X e o cliente
entra.

<!-- página 2 -->

3.1) O controle deve ser feito de forma a ser dada prioridade a quem chegou primeiro.
Considere isso escolher o tipo da estrutura X. Apresente tipos e funções básicas para essa
estrutura.  Observe também  as  características  do  funcionamento  de  X  e  escolha  a
implementação mais adequada.
3.2) Escreva a função controle( ) que lê e trata as opções:
G – guarda nome na estrutura X;
M – lê um número de mesa (m) e pega um nome (N) na estrutura X, imprimindo a seguinte
mensagem: “Sr.(a) N , favor encaminhar-se a mesa m.”;
F – fim;
