# Apostila_TAD_C_Revisada_Comentarios_e_Projeto

> Fonte: `base/Introdução TAD/Apostila_TAD_C_Revisada_Comentarios_e_Projeto.pdf`

<!-- página 1 -->

TIPOS ABSTRATOS DE DADOS EM C

Apostila de Referência para Aprendizagem Autônoma e Sala de Aula
                                Invertida

  Do “o que é?” ao “como implementar?” — com modelos mentais, código, memória, erros
                                 típicos e exercícios comentados.

Como usar esta apostila
Leia os capítulos 1 a 4 antes da aula. Não tente decorar o código: desenhe o que acontece. Nos capítulos
práticos, cubra a solução, tente prever cada linha e só depois compare. Use os checkpoints como teste de
domínio.

                                Tipos Abstratos de Dados em C  • Apostila de referência  • Página 1

<!-- página 2 -->

Roteiro de aprendizagem
Esta apostila foi organizada para reduzir a carga de abstração. A mesma ideia aparece várias vezes em
linguagens diferentes: analogia do cotidiano, desenho, código e visão de memória. O aluno não deve
avançar apenas porque “leu”: deve avançar quando consegue explicar com suas próprias palavras e prever
o comportamento do código.

1.  Entender por que tipos primitivos e structs, sozinhos, não resolvem todos os problemas de organização.
2.  Distinguir tipo, estrutura, Tipo Abstrato de Dados, interface e implementação.
3.  Entender o papel de arquivos .h e .c, aprender a criar fisicamente um projeto de TAD com esses
    arquivos e compreender por que separar “o que pode ser feito” de “como é feito”.
4.  Entender tipo opaco, encapsulamento e invariantes.
5.  Construir um TAD Ponto completo, com criação, consulta, alteração controlada, operações e liberação.
6.  Visualizar malloc, ponteiros, heap e ciclo de vida dos objetos.
7.  Usar um TAD dentro de outro (composição).
8.  Identificar erros de compilação, ligação, execução e projeto.
9.  Projetar um novo TAD a partir de um problema.

 Meta de domínio
 Ao final, você deve ser capaz de receber um problema como “representar uma conta bancária” e decidir:
 quais dados ficam escondidos, quais operações são públicas, quais regras precisam ser protegidas e
 como dividir os arquivos.

1. Antes do TAD: qual problema estamos tentando resolver?

1.1 Tipos primitivos: ótimos, mas pequenos demais
Em C, tipos como int, float, double e char representam valores básicos. Eles são essenciais, mas descrevem
apenas peças pequenas do mundo. Um sistema real trabalha com entidades: aluno, produto, data, ponto,
conta, pedido, livro, paciente, jogo, fila de atendimento.

   int idade = 19;
   float nota = 8.5f;
   char turma = 'B';

Essas três variáveis são válidas, porém não dizem ao compilador que idade, nota e turma pertencem ao
mesmo aluno. A relação existe apenas na cabeça do programador.

1.2 O primeiro salto: agrupar dados com struct
   typedef struct {
       int idade;
       float nota;
       char turma;
   } Aluno;

Agora existe um tipo composto chamado Aluno. Uma variável desse tipo reúne os campos. Isso melhora
muito a organização dos dados.

 Atenção conceitual
 Uma struct é uma forma de agrupar dados. Um TAD é uma ideia mais ampla: define quais valores
 representam o objeto, quais operações são permitidas e quais regras devem permanecer verdadeiras.
 Uma struct pode ser usada para implementar um TAD, mas não é sinônimo de TAD.

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 2

<!-- página 3 -->

1.3 O problema dos dados expostos
   Aluno a;
   a.idade = -500;      /* compila, mas faz sentido? */
   a.nota = 900.0f;     /* compila, mas viola a regra do domínio */

O compilador sabe que idade é int e nota é float. Ele não sabe que idade negativa ou nota 900 são inválidas
no nosso domínio. Se qualquer parte do programa puder escrever diretamente nos campos, cada trecho de
código passa a ser responsável por lembrar as regras.

 Pergunta de previsão
 Se dez partes diferentes do programa alteram diretamente a nota de um aluno, em quantos lugares você
 precisará verificar a regra “nota entre 0 e 10”? Resposta: potencialmente em todos eles. O TAD concentra
 essa responsabilidade.

2. O que é um Tipo Abstrato de Dados?
Um Tipo Abstrato de Dados descreve um conjunto de valores possíveis e um conjunto de operações
permitidas sobre esses valores. A palavra “abstrato” indica que quem usa o tipo pode trabalhar com ele sem
depender dos detalhes internos da representação.

                   Figura 1 — O cliente conversa com a interface; os detalhes internos ficam na implementação.

2.1 A analogia do controle remoto
Pense em uma televisão. Para aumentar o volume, você usa um botão. Você não altera diretamente a
tensão em um componente eletrônico. O botão é a interface. O circuito é a implementação. A regra
importante é: você pede uma operação; o equipamento decide como realizá-la.

 Modelo mental
 TAD = “o que este objeto significa” + “o que pode ser feito com ele” + “quais regras devem ser
 preservadas”.

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 3

<!-- página 4 -->

2.2 Três palavras que precisam ficar separadas
 Termo                  Pergunta que responde   Exemplo                 Ideia-chave

 Representação        Como os dados estão     dois float: x e y           Detalhe interno
                        guardados?

 Interface           O que o cliente pode        criar, obter x, distância    Contrato público
                           pedir?

 Implementação        Como cada operação      malloc, fórmula, acesso   Código interno
                          funciona?              a campos

2.3 Encapsulamento não significa “segurança contra hackers”
Neste contexto, encapsular significa impedir que o código cliente dependa diretamente da representação
interna. O principal ganho é controlar dependências, preservar regras e permitir mudanças locais. Não é
uma barreira de segurança contra código malicioso.

2.4 Invariantes: as regras que nunca deveriam ser quebradas
Uma invariante é uma condição que deve permanecer verdadeira enquanto o objeto for válido. Em uma
Conta, saldo e limite podem obedecer a uma regra. Em uma Data, mês deve estar entre 1 e 12. Em um
Círculo, o raio pode ser obrigado a ser positivo. Um bom TAD protege essas regras por meio de suas
operações.

 Exemplo
 Se o raio de um círculo não pode ser negativo, evite entregar ao cliente um campo raio público. Faça
 circ_cria e circ_define_raio validarem o valor.

3. Como C implementa um TAD: .h, .c e código cliente

                                 Figura 2 — Separação entre cliente, interface e implementação.

3.1 O arquivo .h é o contrato

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 4

<!-- página 5 -->

O arquivo de cabeçalho (.h) é o contrato público do TAD. Ele apresenta ao programador cliente quais tipos
existem e quais operações podem ser utilizadas, sem revelar como essas operações foram implementadas.
Por isso, o .h deve ser legível mesmo para uma pessoa que nunca abriu o arquivo .c. Em uma equipe, é
perfeitamente possível que um programador use o TAD conhecendo apenas o .h.

   #ifndef PONTO_H
   #define PONTO_H

   /*
    * TAD Ponto
    * ----------
    * Representa um ponto no plano cartesiano.
    *
    * IMPORTANTE:
    * Este arquivo é a INTERFACE do TAD.
    * Quem usa o TAD deve precisar ler apenas este arquivo
    * para descobrir quais operações estão disponíveis.
    */

   /*
    * Declaração do tipo opaco.
    * O cliente sabe que existe um tipo chamado Ponto,
    * mas NÃO conhece os campos internos da struct.
    */
   typedef struct ponto Ponto;

   /*
    * pto_cria
    * Cria um novo ponto com as coordenadas informadas.
    *
    * Parâmetros:
    *   x - coordenada no eixo X.
    *   y - coordenada no eixo Y.
    *
    * Retorno:
    *   endereço do novo Ponto, se a criação funcionar;
    *   NULL, se não houver memória disponível.
    */
   Ponto* pto_cria(float x, float y);

   /*
    * pto_libera
    * Libera a memória ocupada por um Ponto criado por pto_cria.
    *
    * Parâmetro:
    *   p - endereço do Ponto que será liberado.
    */
   void pto_libera(Ponto* p);

   /*
    * pto_distancia
    * Calcula a distância euclidiana entre dois pontos.
    *
    * Parâmetros:
    *   a, b - pontos usados no cálculo.
    *
    * Retorno:
    *   distância entre a e b;
    *   valor negativo se algum ponteiro for inválido.
    *
    * const indica que esta função NÃO altera os pontos.
    */
   float pto_distancia(const Ponto* a, const Ponto* b);

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 5

<!-- página 6 -->

#endif

3.1.1 Por que os comentários são especialmente importantes no arquivo .h?
Comentários existem em qualquer parte do programa, mas no arquivo .h eles têm uma importância especial:
o .h é a documentação mais próxima do contrato que o cliente realmente utiliza. Um protótipo como int
conta_saca(Conta* c, double valor); informa os tipos envolvidos, mas não explica sozinho quais valores são
aceitos, o que significa retornar 0 ou 1, se a função altera o objeto ou quais situações representam erro.

Assim, um bom comentário de interface deve responder, sempre que necessário, a quatro perguntas: (1) o
que a função faz? (2) o que significa cada parâmetro? (3) o que ela devolve? e (4) quais regras, limites ou
situações de erro o cliente precisa conhecer? O comentário não deve descrever linha por linha como a
função foi programada; isso pertence à implementação.

Regra prática para esta disciplina: ao terminar um .h, imagine que outro aluno recebeu somente esse
arquivo. Ele conseguiria usar corretamente o TAD sem abrir o .c? Se a resposta for não, a interface ou seus
comentários ainda precisam melhorar.

Exemplo: comentário útil x comentário pouco útil
   /* POUCO ÚTIL: repete apenas o nome da função. */
   /* Faz saque. */
   int conta_saca(Conta* c, double valor);

   /* MELHOR: explica o contrato que o cliente precisa conhecer. */
   /*
    * Retira um valor da conta.
    * valor deve ser maior que zero e não pode ultrapassar o saldo.
    * Retorna 1 em caso de sucesso e 0 se a operação não puder ser realizada.
    */
   int conta_saca(Conta* c, double valor);

3.2 O que significa “struct incompleta” ou “tipo opaco”?
A linha typedef struct ponto Ponto; avisa que existe uma estrutura cujo nome de tag é struct ponto e cria o
apelido Ponto. Mas não revela os campos. Como o tamanho do objeto ainda é desconhecido fora de
ponto.c, o cliente normalmente manipula Ponto por meio de ponteiros.

 Teste mental
 No main.c, Ponto* p; é possível porque o tamanho de um ponteiro é conhecido. Já Ponto p; não é possível
 com esse header opaco, porque o compilador não sabe quantos bytes um Ponto ocupa.

3.3 O arquivo .c revela a representação
   #include "ponto.h"
   #include <stdlib.h>
   #include <math.h>

   struct ponto {
       float x;
       float y;
   };

Somente aqui o compilador conhece os campos x e y. Por isso, somente as funções implementadas neste
módulo podem acessar p->x e p->y diretamente.

3.4 O cliente usa, mas não invade
   #include "ponto.h"
   #include <stdio.h>

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 6

<!-- página 7 -->

int main(void) {
       Ponto* p = pto_cria(2.0f, 3.0f);
       if (p == NULL) {
           printf("Falha ao criar ponto.\n");
           return 1;
       }

       /* p->x = 99;  ERRO: representação escondida */

       pto_libera(p);
       return 0;
   }

3.5 Include guard: por que existe?
O bloco #ifndef / #define / #endif impede que o mesmo conteúdo do header seja processado repetidamente
na mesma unidade de tradução. Sem essa proteção, headers incluídos por caminhos diferentes podem
causar redefinições.

3.6 Como criar, de verdade, um projeto com TAD (.h + .c + main.c)
Até aqui vimos os papéis de cada arquivo. Agora vamos montar o projeto como o aluno fará no computador.
Um TAD não é criado em um único arquivo: normalmente teremos pelo menos um arquivo de interface (.h),
um arquivo de implementação (.c) e um programa cliente (.c).

Estrutura mínima do projeto
   ProjetoPonto/
   |
   |-- main.c      -> programa cliente: usa o TAD
   |-- ponto.h     -> interface: declara o tipo e as funções públicas
   `-- ponto.c     -> implementação: define a struct e implementa as funções

Passo 1 — Crie uma pasta ou um novo projeto C. Dê a ele um nome significativo, por exemplo ProjetoPonto.
Se estiver usando uma IDE, escolha um projeto do tipo C, e não C++.

Passo 2 — Crie o arquivo ponto.h. Nele escreva o include guard, a declaração do tipo opaco e os protótipos
das operações públicas. Acrescente comentários que expliquem o contrato de cada operação.

Passo 3 — Crie o arquivo ponto.c. A primeira inclusão deve ser #include "ponto.h". Depois inclua as
bibliotecas necessárias, defina struct ponto e escreva o corpo das funções prometidas no .h.

Passo 4 — Crie ou edite main.c. O cliente inclui #include "ponto.h" e chama as funções públicas. Ele NÃO
deve escrever #include "ponto.c" e NÃO deve acessar diretamente campos como p->x.

Passo 5 — Garanta que todos os arquivos .c façam parte do projeto. Em uma IDE, adicione main.c e ponto.c
ao projeto. O ponto.h pode ser adicionado para organização e edição, mas quem gera código objeto são os
arquivos .c.

Passo 6 — Compile e ligue o projeto. O compilador trata main.c e ponto.c separadamente e, depois, o linker
junta os módulos. Por isso, esquecer ponto.c na compilação pode produzir mensagens como “undefined
reference to pto_cria”.

O que o compilador faz?
   main.c  --compila-->  main.o
   ponto.c --compila-->  ponto.o
                            |
                            v
                    linker junta os dois
                            |
                            v
                       programa executável

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 7

<!-- página 8 -->

Perceba a diferença: #include "ponto.h" faz o compilador conhecer as DECLARAÇÕES. Isso não coloca
automaticamente o código de ponto.c dentro de main.c. A implementação precisa participar da
compilação/ligação.

Erro muito comum: incluir o arquivo .c
   /* ERRADO */
   #include "ponto.c"

   /* CERTO */
   #include "ponto.h"

O arquivo .h é feito para ser incluído. O .c é uma unidade de implementação que deve ser compilada
separadamente. Incluir um .c pode provocar definições duplicadas, confundir a organização do projeto e
destruir a separação entre interface e implementação.

3.7 Projeto mínimo completo antes de avançar
Antes de estudar funções mais complexas, faça este teste: crie os três arquivos, compile e execute uma
versão mínima. Se isso funcionar, você já entendeu a arquitetura física do TAD.

   /* main.c */
   #include "ponto.h"
   #include <stdio.h>

   int main(void) {
       Ponto* p = pto_cria(1.0f, 2.0f);

       if (p == NULL) {
           printf("Nao foi possivel criar o ponto.\n");
           return 1;
       }

       pto_libera(p);
       return 0;
   }

4. Ponteiros e memória: o pedaço que costuma travar o
aluno
TAD opaco em C frequentemente aparece junto de ponteiros e alocação dinâmica. Por isso, antes de
construir o Ponto completo, vamos separar as ideias.

4.1 Ponteiro não é o objeto
Um ponteiro guarda um endereço. Se p tem tipo Ponto*, então p guarda o endereço onde existe um Ponto.
Imagine um papel com o número de uma casa: o papel não é a casa; ele permite encontrá-la.

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 8

<!-- página 9 -->

Figura 3 — p1 guarda um endereço; o objeto Ponto está em outra região de memória.

4.2 malloc: pedir um bloco de memória
   Ponto* novo = malloc(sizeof(Ponto));

10. sizeof(Ponto) calcula quantos bytes a representação interna ocupa. Isso só funciona em ponto.c, onde
    struct ponto já foi definida.
11. malloc solicita essa quantidade de bytes no heap.
12. Se der certo, devolve o endereço inicial do bloco.
13. Se não puder alocar, devolve NULL.
14. Em C, não é necessário converter o retorno de malloc com cast.

 Erro frequente
 Escrever Ponto* novo = (Ponto*) malloc(sizeof(Ponto)); não é necessário em C. O código pode compilar,
 mas o cast não ensina nada útil e pode mascarar a ausência de #include <stdlib.h> em
 compiladores/configurações permissivas.

4.3 free: devolver o bloco

                                  Tipos Abstratos de Dados em C  • Apostila de referência  • Página 9

<!-- página 10 -->

Figura 4 — Todo objeto alocado precisa de um ciclo de vida claro.

   void pto_libera(Ponto* p) {
       free(p);
   }

free libera o bloco apontado por p. A variável do cliente ainda contém o antigo endereço, que agora não
deve ser usado. Esse é um ponteiro pendente (dangling pointer).

   pto_libera(p);
   p = NULL;  /* feito pelo cliente: agora p não aponta para o bloco liberado */

 Ponto importante
 A função pto_libera(Ponto* p) não consegue colocar automaticamente a variável p do chamador em
 NULL, porque recebe uma cópia do endereço. Para alterar o ponteiro do chamador, seria preciso receber
 Ponto**.

4.4 Quatro falhas de memória que você precisa reconhecer
 Falha                         Exemplo mental                 Consequência

 memory leak                     alocou e perdeu o endereço sem   memória fica ocupada até o
                                       free                            processo terminar

 use after free                   usou p depois de free(p)          comportamento indefinido

 double free                          free no mesmo bloco duas vezes   comportamento
                                                                         indefinido/corrupção

 NULL dereference               acessou p->x quando p == NULL   comportamento indefinido

5. Projeto guiado: construindo o TAD Ponto do zero

5.1 Passo 0 — antes de programar, escreva o contrato em português
 Contrato do Ponto
 Um Ponto representa uma posição bidimensional. Deve ser possível criar, destruir, consultar

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 10

<!-- página 11 -->

coordenadas, alterar coordenadas de forma controlada, calcular a distância entre dois pontos e exibir um
 ponto.

Observe a ordem: primeiro decidimos o que o tipo deve oferecer. Só depois escolhemos como guardar os
dados.

5.2 Passo 1 — interface ponto.h
   #ifndef PONTO_H
   #define PONTO_H

   /*
    * TAD Ponto
    * Interface pública do módulo ponto.
    *
    * Um Ponto representa uma posição (x, y) no plano.
    * Os campos internos ficam escondidos em ponto.c.
    */
   typedef struct ponto Ponto;

   /*
    * Cria um Ponto.
    * Retorna o endereço do objeto criado ou NULL em caso de falha.
    */
   Ponto* pto_cria(float x, float y);

   /*
    * Libera um Ponto criado anteriormente por pto_cria.
    */
   void pto_libera(Ponto* p);

   /*
    * Copia a coordenada x para a variável apontada por x.
    * Retorna 1 em caso de sucesso e 0 se algum ponteiro for NULL.
    */
   int pto_obtem_x(const Ponto* p, float* x);

   /*
    * Copia a coordenada y para a variável apontada por y.
    * Retorna 1 em caso de sucesso e 0 se algum ponteiro for NULL.
    */
   int pto_obtem_y(const Ponto* p, float* y);

   /*
    * Altera as duas coordenadas do ponto.
    * Retorna 1 se a alteração for realizada e 0 se p for NULL.
    */
   int pto_define(Ponto* p, float x, float y);

   /*
    * Calcula a distância euclidiana entre a e b.
    * Não altera os pontos, por isso os parâmetros usam const.
    * Retorna valor negativo se algum ponteiro for NULL.
    */
   float pto_distancia(const Ponto* a, const Ponto* b);

   /*
    * Exibe o ponto no formato (x, y).
    * Se p for NULL, exibe uma mensagem apropriada.
    */
   void pto_exibe(const Ponto* p);

   #endif

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 11

<!-- página 12 -->

Aqui aparece const em funções que não devem modificar o objeto. Isso comunica intenção ao leitor e
permite ao compilador bloquear alterações acidentais por meio daquele parâmetro.

5.3 Passo 2 — representação interna em ponto.c
   #include "ponto.h"
   #include <math.h>
   #include <stdio.h>
   #include <stdlib.h>

   struct ponto {
       float x;
       float y;
   };

5.4 Passo 3 — função de criação
   Ponto* pto_cria(float x, float y) {
       Ponto* novo = malloc(sizeof(Ponto));

       if (novo == NULL) {
           return NULL;
       }

       novo->x = x;
       novo->y = y;
       return novo;
   }

15. Ponto* novo declara um ponteiro que futuramente guardará o endereço do novo objeto.
16. malloc(sizeof(Ponto)) reserva exatamente o espaço necessário.
17. if (novo == NULL) trata a possibilidade de falha.
18. novo->x acessa o campo x do objeto apontado por novo.
19. return novo entrega ao cliente o endereço do objeto pronto.

 Por que retornar NULL em vez de exit(1)?
 Em uma biblioteca/TAD reutilizável, geralmente é melhor devolver a falha ao cliente para que ele decida o
 que fazer. Encerrar todo o programa dentro do TAD reduz a reutilização. Em exercícios simples, exit pode
 aparecer, mas é importante entender a diferença de projeto.

5.5 Passo 4 — funções de consulta
   int pto_obtem_x(const Ponto* p, float* x) {
       if (p == NULL || x == NULL) {
           return 0;
       }
       *x = p->x;
       return 1;
   }

   int pto_obtem_y(const Ponto* p, float* y) {
       if (p == NULL || y == NULL) {
           return 0;
       }
       *y = p->y;
       return 1;
   }

Por que usar um parâmetro de saída? Assim conseguimos retornar 0/1 para indicar falha/sucesso e, ao
mesmo tempo, entregar o valor consultado. Há outros projetos possíveis; o importante é que o contrato seja
consistente.

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 12

<!-- página 13 -->

5.6 Passo 5 — alteração controlada
   int pto_define(Ponto* p, float x, float y) {
       if (p == NULL) {
           return 0;
       }
       p->x = x;
       p->y = y;
       return 1;
   }

No Ponto, qualquer float pode ser aceito, então a validação é simples. Em um TAD Data, Conta ou Produto,
esta função seria o lugar natural para proteger invariantes.

5.7 Passo 6 — distância
   float pto_distancia(const Ponto* a, const Ponto* b) {
       if (a == NULL || b == NULL) {
           return -1.0f;
       }

       float dx = b->x - a->x;
       float dy = b->y - a->y;
       return sqrtf(dx * dx + dy * dy);
   }

Para os pontos (2,3) e (5,7): dx = 3, dy = 4, então distância = √(9+16) = 5. Esse exemplo é ótimo para testar
a implementação porque o resultado é exato e conhecido.

5.8 Passo 7 — exibição e liberação
   void pto_exibe(const Ponto* p) {
       if (p == NULL) {
           printf("(ponto inexistente)");
           return;
       }
       printf("(%.2f, %.2f)", p->x, p->y);
   }

   void pto_libera(Ponto* p) {
       free(p);
   }

5.9 Passo 8 — cliente completo
   #include "ponto.h"
   #include <stdio.h>

   int main(void) {
       Ponto* p1 = pto_cria(2.0f, 3.0f);
       Ponto* p2 = pto_cria(5.0f, 7.0f);

       if (p1 == NULL || p2 == NULL) {
           printf("Erro de alocacao.\n");
           pto_libera(p1);
           pto_libera(p2);
           return 1;
       }

       printf("p1 = ");
       pto_exibe(p1);
       printf("\n");

       printf("p2 = ");

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 13

<!-- página 14 -->

pto_exibe(p2);
       printf("\n");

       printf("distancia = %.2f\n", pto_distancia(p1, p2));

       pto_libera(p1);
       pto_libera(p2);
       p1 = NULL;
       p2 = NULL;
       return 0;
   }

 Saída esperada
 p1 = (2.00, 3.00)
 p2 = (5.00, 7.00)
 distancia = 5.00

5.10 Como compilar
   gcc -Wall -Wextra -pedantic main.c ponto.c -lm -o programa
   ./programa

A opção -lm é necessária em vários ambientes Unix/Linux para ligar a biblioteca matemática usada por sqrtf.
Em algumas plataformas ou IDEs isso pode ser tratado automaticamente.

6. Aprendendo a ler um TAD sem se perder
Alunos costumam tentar compreender todos os arquivos simultaneamente. Uma estratégia melhor é ler em
camadas.

20. Leia primeiro o .h e responda: que tipo existe? quais operações são públicas?

Enquanto lê o .h, leia também os comentários como parte do contrato: eles devem explicar pré-condições,
retorno, erros e efeitos importantes. Não trate comentário de interface como enfeite.

21. Ignore o corpo das funções e escreva uma frase para cada operação.
22. Abra o .c e identifique a struct interna.
23. Leia cria/libera para entender o ciclo de vida.
24. Leia consultas e alterações.
25. Só então leia operações mais complexas.
26. Por último, leia main.c e verifique se o cliente respeita o contrato.

 Técnica do papel
 Ao estudar, desenhe três caixas: CLIENTE, .h e .c. Para cada variável ou campo, pergunte “quem
 consegue ver isto?”. Essa pergunta resolve grande parte das dúvidas sobre encapsulamento.

7. Composição: construindo o TAD Círculo usando o TAD
Ponto

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 14

<!-- página 15 -->

Figura 5 — O Círculo pode delegar ao TAD Ponto as operações relacionadas ao centro.

7.1 Interface circulo.h
   #ifndef CIRCULO_H
   #define CIRCULO_H

   #include "ponto.h"

   /*
    * TAD Circulo
    * ----------
    * Representa um círculo por meio de um centro e um raio.
    * A implementação reutiliza o TAD Ponto para representar o centro.
    */
   typedef struct circulo Circulo;

   /*
    * Cria um círculo de centro (x, y) e raio informado.
    * O raio deve ser maior que zero.
    * Retorna o novo círculo ou NULL se o raio for inválido
    * ou se ocorrer falha de alocação.
    */
   Circulo* circ_cria(float x, float y, float raio);

   /*
    * Libera o círculo e os recursos internos que pertencem a ele.
    */
   void circ_libera(Circulo* c);

   /*
    * Calcula a área do círculo.
    * Retorna valor negativo se c for NULL.
    */
   float circ_area(const Circulo* c);

   /*
    * Verifica se o ponto p está dentro ou sobre a borda do círculo.
    * Retorna 1 se estiver contido e 0 caso contrário.
    */
   int circ_contem(const Circulo* c, const Ponto* p);

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 15

<!-- página 16 -->

#endif

7.2 Implementação circulo.c
   #include "circulo.h"
   #include <stdlib.h>

   #define PI 3.14159265f

   struct circulo {
       Ponto* centro;
       float raio;
   };

   Circulo* circ_cria(float x, float y, float raio) {
       if (raio <= 0.0f) {
           return NULL;
       }

       Circulo* c = malloc(sizeof(Circulo));
       if (c == NULL) {
           return NULL;
       }

       c->centro = pto_cria(x, y);
       if (c->centro == NULL) {
           free(c);
           return NULL;
       }

       c->raio = raio;
       return c;
   }

   void circ_libera(Circulo* c) {
       if (c == NULL) return;
       pto_libera(c->centro);
       free(c);
   }

   float circ_area(const Circulo* c) {
       if (c == NULL) return -1.0f;
       return PI * c->raio * c->raio;
   }

   int circ_contem(const Circulo* c, const Ponto* p) {
       if (c == NULL || p == NULL) return 0;
       float d = pto_distancia(c->centro, p);
       return d >= 0.0f && d <= c->raio;
   }

 Erro sutil
 Ao criar um objeto composto, não basta verificar se malloc do Círculo funcionou. A criação do Ponto
 interno também pode falhar. Nesse caso, é necessário liberar o Círculo já alocado antes de retornar
 NULL.

7.3 Ordem correta de destruição
Se o Círculo é dono do Ponto centro que ele criou, deve destruí-lo antes de destruir a si mesmo. Isso é uma
questão de propriedade da memória. Pergunte sempre: quem criou este objeto? quem é responsável por
liberá-lo?
                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 16

<!-- página 17 -->

pto_libera(c->centro);  /* primeiro o objeto interno */
   free(c);                 /* depois o objeto externo */

8. Como projetar uma boa interface

8.1 Nomes devem mostrar intenção
Prefira nomes previsíveis: aluno_cria, aluno_libera, aluno_obtem_nome, aluno_define_nota, aluno_media.
Em C não há namespaces de linguagem, então prefixos reduzem colisões entre módulos.

8.2 Não exponha operação só porque “é fácil”
Cada função pública aumenta o contrato que você precisará manter. Exponha o que o cliente precisa, não
todo detalhe que a implementação sabe fazer.

8.3 const é documentação executável
   float pto_distancia(const Ponto* a, const Ponto* b);

A assinatura informa que a função recebe os pontos apenas para leitura. Se o código tentar escrever em a-
>x por esse ponteiro, o compilador poderá acusar erro.

8.4 Retornos de erro precisam ser decididos
Uma interface deve definir como sinaliza falhas: NULL, 0/1, enum de erro, valor sentinela, parâmetro de
saída etc. Não existe uma única escolha universal. Existe escolha coerente com o problema.

9. Atlas de erros: sintoma → causa → correção
 Sintoma                      Causa provável            O que verificar

 “invalid use of incomplete      O cliente não conhece os           Criar/usar uma operação pública
 typedef” ao escrever p->x no      campos do tipo opaco.            para consultar ou modificar o
 main                                                            dado.

 “unknown type name Ponto”       Header não foi incluído ou            Incluir ponto.h e verificar a
                                    typedef não está visível.          ordem/nomes.

 “undefined reference to pto_cria”  A declaração foi vista, mas a        Compilar/ligar também ponto.c ou
                                implementação não foi ligada.      ponto.o.

 Programa compila e cai ao         Possível NULL, use-after-free ou    Validar ponteiros e rastrear ciclo
 executar                       acesso inválido.                de vida.

 Memória cresce continuamente    Objetos criados não estão sendo   Mapear cada malloc e seu free
                                      liberados.                         correspondente.

 double free or corruption        Mesmo bloco foi liberado mais de   Definir propriedade clara; zerar
                          uma vez.                          ponteiros do cliente quando útil.

 Resultado matemático errado      Fórmula, tipos ou caso de teste     Testar com valores cujo resultado
                                       incorretos.                         seja conhecido, como triângulo 3-
                                                                            4-5.

9.1 Erro de compilação x erro de ligação x erro de execução
27. Compilação: cada arquivo .c é verificado e transformado em código objeto. Erros de tipo, sintaxe e
   acesso a struct incompleta aparecem aqui.
                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 17

<!-- página 18 -->

28. Ligação (link): o linker tenta encontrar as implementações das funções usadas. “undefined reference” é
    típico desta fase.
29. Execução: o programa já foi construído, mas pode falhar por ponteiro inválido, divisão por zero, lógica
    incorreta etc.

10. Segundo exemplo completo: TAD Conta Bancária
O objetivo deste exemplo é mostrar por que encapsulamento é mais importante quando existem regras de
domínio.

10.1 Regras do problema
•  Cada conta possui número e saldo.
•  O número não deve mudar depois da criação.
•   Depósito deve ser positivo.
•  Saque deve ser positivo e não pode ultrapassar o saldo.
•  O cliente pode consultar número e saldo, mas não alterá-los diretamente.

10.2 conta.h
   #ifndef CONTA_H
   #define CONTA_H

   /*
    * TAD Conta
    * ---------
    * Representa uma conta bancária simples.
    *
    * Regras principais:
    * - o número da conta deve ser positivo;
    * - o número não muda depois da criação;
    * - depósitos devem ser positivos;
    * - saques devem ser positivos e não podem ultrapassar o saldo.
    */
   typedef struct conta Conta;

   /*
    * Cria uma conta com saldo inicial igual a zero.
    * numero deve ser maior que zero.
    * Retorna a nova conta ou NULL em caso de erro.
    */
   Conta* conta_cria(int numero);

   /*
    * Libera a memória da conta.
    */
   void conta_libera(Conta* c);

   /*
    * Deposita valor na conta.
    * Retorna 1 se o depósito for realizado e 0 se os dados forem inválidos.
    */
   int conta_deposita(Conta* c, double valor);

   /*
    * Saca valor da conta.
    * O valor deve ser positivo e não pode ultrapassar o saldo.
    * Retorna 1 em caso de sucesso e 0 caso contrário.
    */
   int conta_saca(Conta* c, double valor);

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 18

<!-- página 19 -->

/*
    * Copia o número da conta para *numero.
    * Retorna 1 em caso de sucesso e 0 se algum ponteiro for NULL.
    */
   int conta_numero(const Conta* c, int* numero);

   /*
    * Copia o saldo da conta para *saldo.
    * Retorna 1 em caso de sucesso e 0 se algum ponteiro for NULL.
    */
   int conta_saldo(const Conta* c, double* saldo);

   #endif

10.3 conta.c
   #include "conta.h"
   #include <stdlib.h>

   struct conta {
       int numero;
       double saldo;
   };

   Conta* conta_cria(int numero) {
       if (numero <= 0) return NULL;

       Conta* c = malloc(sizeof(Conta));
       if (c == NULL) return NULL;

       c->numero = numero;
       c->saldo = 0.0;
       return c;
   }

   void conta_libera(Conta* c) {
       free(c);
   }

   int conta_deposita(Conta* c, double valor) {
       if (c == NULL || valor <= 0.0) return 0;
       c->saldo += valor;
       return 1;
   }

   int conta_saca(Conta* c, double valor) {
       if (c == NULL || valor <= 0.0 || valor > c->saldo) return 0;
       c->saldo -= valor;
       return 1;
   }

   int conta_numero(const Conta* c, int* numero) {
       if (c == NULL || numero == NULL) return 0;
       *numero = c->numero;
       return 1;
   }

   int conta_saldo(const Conta* c, double* saldo) {
       if (c == NULL || saldo == NULL) return 0;
       *saldo = c->saldo;
       return 1;
   }

 Por que isso é melhor que uma struct pública?
 Nenhum cliente consegue fazer c->saldo = -100000 diretamente, pois nem conhece o campo. As únicas
                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 19

<!-- página 20 -->

portas de entrada para alterar o saldo são deposita e saca, que concentram as regras.

12. Checkpoints de compreensão
1. Uma struct é sempre um TAD? Explique.

2. O que o cliente precisa saber para usar um TAD opaco?

3. Por que Ponto* pode aparecer no main mesmo quando os campos estão escondidos?

4. Por que Ponto p; não funciona no main com um tipo opaco?

5. Qual a diferença entre ponto.h e ponto.c?

6. O que malloc devolve quando falha?

7. Depois de free(p), é seguro acessar p->x?

8. Quem deve decidir a política de erro: a interface ou cada cliente de forma improvisada?

9. Para que serve const Ponto*?

10. Se ponto.c mudar internamente, o main precisa mudar obrigatoriamente? Em que situação precisaria?

 Não consulte o gabarito ainda
 Se você não consegue explicar uma resposta sem usar a palavra “porque sim”, marque a questão e volte
 ao capítulo correspondente.

13. Exercícios graduados

Nível 1 — reconhecer conceitos
13.1 Classifique como REPRESENTAÇÃO, INTERFACE ou IMPLEMENTAÇÃO: “float raio;”, “circ_area”,
“código da fórmula pi*r*r”.

13.2 Explique por que main.c não deveria incluir ponto.c.

13.3 Diga se cada item é obrigatório em todo TAD C: malloc, struct, .h, funções públicas. Justifique; algumas
respostas dependem da estratégia de implementação.

13.4 Dê um exemplo de invariante para Data, Produto e Aluno.

Nível 2 — prever o código
   Ponto* p = pto_cria(1, 2);
   pto_libera(p);
   pto_exibe(p);

13.5 O código acima é válido? Diferencie “compila” de “é seguro executar”.

   Ponto* p = NULL;
   float x;
   int ok = pto_obtem_x(p, &x);

13.6 Qual deve ser o valor de ok na implementação proposta? x recebe um valor válido?

Nível 3 — corrigir bugs
   Circulo* circ_cria(float x, float y, float r) {

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 20

<!-- página 21 -->

Circulo* c = malloc(sizeof(Circulo));
       c->centro = pto_cria(x, y);
       c->raio = r;
       return c;
   }

13.7 Encontre pelo menos três problemas de robustez no código.

Nível 4 — construir
13.8 Projete um TAD Retângulo com largura e altura positivas. Defina interface, invariantes e operações
para criar, liberar, calcular área e perímetro.

13.9 Projete um TAD Produto com código imutável, nome e preço não negativo. O cliente deve conseguir
alterar o preço apenas se o novo preço for válido.

13.10 Desafio: crie um TAD Segmento composto por dois Pontos. A função seg_comprimento deve reutilizar
pto_distancia.

14. Projeto de síntese: do problema para o TAD
Problema: um aplicativo simples precisa representar avaliações de filmes. Cada Avaliacao possui código do
usuário, código do filme e nota de 0 a 10. Depois de criada, a identificação não pode mudar; a nota pode ser
atualizada, desde que continue no intervalo válido.

30. Liste os dados internos.
31. Escreva as invariantes.
32. Escolha quais informações podem ser consultadas.
33. Defina as funções públicas no arquivo avaliacao.h.
34. Implemente avaliacao.c usando tipo opaco.
35. Crie main.c que tente fazer duas atualizações válidas e duas inválidas.
36. Explique por que o cliente não consegue violar diretamente a faixa da nota.

 Critério de avaliação
 Uma boa solução não é a que tem mais funções. É a que apresenta um contrato simples, protege as
 regras e deixa clara a responsabilidade sobre memória e erros.

15. Gabarito comentado dos checkpoints
1. Não. struct agrupa dados; TAD acrescenta uma especificação de operações e regras de uso. Uma struct
pública pode participar da implementação de um TAD, mas não basta por si só.

2. Precisa conhecer o tipo pelo nome/ponteiro e as operações públicas declaradas no header. Não precisa
conhecer os campos internos.

3. Porque o compilador conhece o tamanho de um ponteiro mesmo sem conhecer o tamanho da struct
apontada.

4. Para reservar uma variável Ponto por valor, o compilador precisa conhecer sizeof(Ponto), o que a
declaração incompleta não informa.

5. .h publica o contrato; .c contém a representação completa e os corpos das funções.

6. NULL.

7. Não. O bloco já foi liberado; acessar por aquele endereço é use-after-free e causa comportamento
indefinido.

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 21

<!-- página 22 -->

8. A interface deve estabelecer uma política coerente de sinalização; o cliente decide como reagir ao erro
informado.

9. Indica que a função não deve modificar o objeto por meio daquele ponteiro.

10. Não, se o contrato do header permanecer compatível. O cliente precisa mudar quando a interface
pública muda ou quando sua própria lógica precisa usar novas operações.

15.1 Pistas para os exercícios
•   13.5: depois do free, o valor do ponteiro fica pendente; o fato de uma chamada compilar não a torna
    correta em execução.
•   13.6: ok deve ser 0; x não deve ser usado como resultado válido.
•   13.7: validar raio, verificar malloc, verificar pto_cria e liberar c se a criação do centro falhar.
•   13.10: Segmento pode possuir Ponto* a e Ponto* b; é preciso decidir se ele cria cópias próprias ou
   apenas referencia pontos de outro dono. Essa decisão muda quem libera a memória.

16. Mapa mental final

•  TAD não é “uma struct bonita”: é uma abstração com valores, operações e regras.
•   Interface responde O QUE pode ser feito.
•  Implementação responde COMO é feito.
•   Tipo opaco reduz dependência do cliente em relação aos campos internos.
•   malloc cria uma responsabilidade de free.
•   Ponteiro guarda endereço; não confunda ponteiro com objeto.
•  Encapsulamento concentra validações e protege invariantes.
•  Composição permite construir abstrações maiores reutilizando menores.
•   Erros devem ser analisados por fase: compilação, ligação ou execução.
•  Um bom programador de TAD começa pelo contrato, não pela struct.

 Pergunta final
 Se amanhã a representação de Ponto mudar de dois float para outra forma interna, quantos clientes
 deveriam precisar saber? Idealmente, nenhum, desde que a interface pública e seu comportamento
 permaneçam compatíveis.

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 22

<!-- página 23 -->

Apêndice A — Código completo do TAD Ponto

   /* =========================
      ponto.h - INTERFACE DO TAD
      ========================= */

   #ifndef PONTO_H
   #define PONTO_H

   /*
    * TAD Ponto
    * Representa uma posição bidimensional.
    *
    * O cliente conhece somente este contrato.
    * A definição completa de struct ponto fica em ponto.c.
    */
   typedef struct ponto Ponto;

   /*
    * Cria um ponto com coordenadas x e y.
    * Retorna Ponto* em caso de sucesso ou NULL em caso de falha.
    */
   Ponto* pto_cria(float x, float y);

   /* Libera um objeto criado por pto_cria. */
   void pto_libera(Ponto* p);

   /*
    * Obtém a coordenada x.
    * Retorna 1 em caso de sucesso; 0 se p ou x forem NULL.
    */
   int pto_obtem_x(const Ponto* p, float* x);

   /*
    * Obtém a coordenada y.
    * Retorna 1 em caso de sucesso; 0 se p ou y forem NULL.
    */
   int pto_obtem_y(const Ponto* p, float* y);

   /*
    * Define novas coordenadas.
    * Retorna 1 se a alteração ocorrer; 0 se p for NULL.
    */
   int pto_define(Ponto* p, float x, float y);

   /*
    * Calcula a distância euclidiana.
    * Os parâmetros são const porque não são modificados.
    * Retorna valor negativo se algum ponto for NULL.
    */
   float pto_distancia(const Ponto* a, const Ponto* b);

   /*
    * Exibe o ponto.
    * Trata p == NULL sem tentar acessar campos inválidos.
    */
   void pto_exibe(const Ponto* p);

   #endif

   /* ===============================
                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 23

<!-- página 24 -->

ponto.c - IMPLEMENTAÇÃO DO TAD
   =============================== */

#include "ponto.h"
#include <math.h>
#include <stdio.h>
#include <stdlib.h>

/* Representação privada: o cliente não enxerga estes campos. */
struct ponto {
    float x;
    float y;
};

Ponto* pto_cria(float x, float y) {
    Ponto* novo = malloc(sizeof(Ponto));
    if (novo == NULL) return NULL;

    novo->x = x;
    novo->y = y;
    return novo;
}

void pto_libera(Ponto* p) {
    free(p);
}

int pto_obtem_x(const Ponto* p, float* x) {
    if (p == NULL || x == NULL) return 0;
    *x = p->x;
    return 1;
}

int pto_obtem_y(const Ponto* p, float* y) {
    if (p == NULL || y == NULL) return 0;
    *y = p->y;
    return 1;
}

int pto_define(Ponto* p, float x, float y) {
    if (p == NULL) return 0;
    p->x = x;
    p->y = y;
    return 1;
}

float pto_distancia(const Ponto* a, const Ponto* b) {
    if (a == NULL || b == NULL) return -1.0f;

    float dx = b->x - a->x;
    float dy = b->y - a->y;
    return sqrtf(dx * dx + dy * dy);
}

void pto_exibe(const Ponto* p) {
    if (p == NULL) {
        printf("(ponto inexistente)");
        return;
    }
    printf("(%.2f, %.2f)", p->x, p->y);
}

/* ponto.c */
#include "ponto.h"
#include <math.h>

                              Tipos Abstratos de Dados em C  • Apostila de referência  • Página 24

<!-- página 25 -->

#include <stdio.h>
   #include <stdlib.h>

   struct ponto {
       float x;
       float y;
   };

   Ponto* pto_cria(float x, float y) {
       Ponto* novo = malloc(sizeof(Ponto));
       if (novo == NULL) return NULL;
       novo->x = x;
       novo->y = y;
       return novo;
   }

   void pto_libera(Ponto* p) { free(p); }

   int pto_obtem_x(const Ponto* p, float* x) {
       if (p == NULL || x == NULL) return 0;
       *x = p->x;
       return 1;
   }

   int pto_obtem_y(const Ponto* p, float* y) {
       if (p == NULL || y == NULL) return 0;
       *y = p->y;
       return 1;
   }

   int pto_define(Ponto* p, float x, float y) {
       if (p == NULL) return 0;
       p->x = x;
       p->y = y;
       return 1;
   }

   float pto_distancia(const Ponto* a, const Ponto* b) {
       if (a == NULL || b == NULL) return -1.0f;
       float dx = b->x - a->x;
       float dy = b->y - a->y;
       return sqrtf(dx * dx + dy * dy);
   }

   void pto_exibe(const Ponto* p) {
       if (p == NULL) {
           printf("(ponto inexistente)");
           return;
       }
       printf("(%.2f, %.2f)", p->x, p->y);
   }

Apêndice B — Checklist para criar qualquer TAD
•    Consigo explicar em uma frase o que o tipo representa?
•      Listei as invariantes?
•     Separei operações essenciais de detalhes internos?
•    Meu .h expõe apenas o necessário?
•    Se o tipo é opaco, a struct completa está apenas no .c?
•    Cada malloc tem uma estratégia clara de free?
•    As funções tratam NULL quando isso faz parte do contrato?

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 25

<!-- página 26 -->

•     Usei const onde a função apenas consulta?
•     Defini como erros são comunicados?
•    Tenho testes com casos normais, limites e erros?
•   O código cliente consegue usar o TAD sem conhecer a representação?
•    Cada função pública do .h tem comentário suficiente para que outro programador saiba usá-la sem
    abrir o .c?

                                 Tipos Abstratos de Dados em C  • Apostila de referência  • Página 26
