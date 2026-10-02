# Exercicios_TAD_C_Lista_Didatica_V3 (1)

> Fonte: `base/Lista de TAD/Exercicios_TAD_C_Lista_Didatica_V3 (1).pdf`

<!-- página 1 -->

Exercícios de TAD em C — Lista Didática V3 (Com
Dados Testes Fornecidos na Main)

Esta versão atualizou os exercícios de aplicação do Módulo 4: a questão de matriz agora
aborda análise de vendas de produtos por mês, e todas as aplicações incluem
explicitamente o bloco main.c pronto com dados testes de entrada para que os alunos resolvam
o problema manipulando as funções do TAD.

CONTRATOS E ESPECIFICAÇÕES DOS TADS BASE FORNECIDOS

1. TAD Data (data.h)

  /* Aloca dinamicamente uma Data validando dia, mês e ano. */
  Data* dta_cria(int d, int m, int a);

  /* Libera a memória alocada para a estrutura Data. */
  void dta_libera(Data *d);

  /* Retorna uma string alocada dinamicamente no formato "DD/MM/AAAA". */
  char* dta_paraString(const Data *d);

  /* Retorna a diferença em dias entre d1 e d2 (d1 - d2). */
  int dta_diferenca(const Data *d1, const Data *d2);

  /* Retorna negativo se d1 < d2, zero se d1 == d2, ou positivo se d1 > d2. */
  int dta_compara(const Data *d1, const Data *d2);

2. TAD Horario (horario.h)

  /* Aloca dinamicamente um Horario validando horas (0-23), minutos (0-59) e segundos (0-59). */
  Horario* hor_cria(int h, int m, int s);

  /* Libera a memória alocada para a estrutura Horario. */
  void hor_libera(Horario *h);

  /* Retorna uma string alocada dinamicamente no formato "HH:MM:SS". */
  char* hor_paraString(const Horario *h);

  /* Converte e retorna o valor total do horário traduzido em segundos. */
  int hor_paraSegundos(const Horario *h);

  /* Retorna negativo se h1 < h2, zero se h1 == h2, ou positivo se h1 > h2. */
  int hor_compara(const Horario *h1, const Horario *h2);

<!-- página 2 -->

3. TAD Conjunto (conjunto.h)

   /* Aloca dinamicamente um Conjunto com capacidade máxima especificada. */
  Conjunto* conj_cria(int capacidade);

   /* Libera a memória do conjunto e seus elementos internos. */
   void conj_libera(Conjunto *c);

   /* Retorna uma string alocada dinamicamente no formato "{e1, e2, ...}". */
  char* conj_paraString(const Conjunto *c);

   /* Insere um elemento no conjunto (sem duplicadas). Retorna 1 se sucesso, 0 se falha. */
   int conj_insere(Conjunto *c, int elem);

   /* Verifica se o elemento pertence ao conjunto. Retorna 1 se pertence, 0 caso contrário. */
   int conj_pertence(const Conjunto *c, int elem);

   /* Cria e retorna um novo Conjunto contendo a união entre c1 e c2. */
  Conjunto* conj_uniao(const Conjunto *c1, const Conjunto *c2);

 4. TAD Matriz (matriz.h)

   /* Aloca dinamicamente uma Matriz de valores numéricos de dimensões (lin x col). */
   Matriz* mat_cria(int lin, int col);

   /* Libera a memória da matriz de dados e de sua estrutura receptora. */
   void mat_libera(Matriz *m);

   /* Retorna uma string alocada dinamicamente com a visualização completa da matriz. */
  char* mat_paraString(const Matriz *m);

   /* Escreve um valor double na célula localizada na linha 'l' e coluna 'c'. */
   void mat_atribui(Matriz *m, int l, int c, double val);

   /* Retorna o valor double armazenado na célula localizada na linha 'l' e coluna 'c'. */
  double mat_acessa(const Matriz *m, int l, int c);

MÓDULO 1: CONSTRUÇÃO DE TADS SIMPLES (Nível Básico)

 Exercício 1: TAD Altura

●  Contexto: Em sistemas biométricos e médico-esportivos, a medição de altura é tratada
   separadamente em metros e centímetros para evitar erros de arredondamento em ponto
    flutuante.
●  Enunciado Técnico: Projetar a interface (altura.h) e a implementação (altura.c) do tipo
    abstrato Altura. A estrutura oculta deve guardar m (metros) e cm (centímetros) como
    inteiros.

<!-- página 3 -->

●  Requisitos do TAD:
   1.  alt_cria(m, cm): Aloca dinamicamente o TAD. Lida com o ajuste caso os centímetros
      informados sejam superiores ou iguais a 100 (ex: 1m e 120cm torna-se 2m e 20cm).
   2.  alt_libera(p): Libera a memória alocada.
   3.  alt_paraString(p): (Obrigatório) Aloca dinamicamente e retorna uma string formatada no
      padrão X,XXm (ex: "1,75m").
   4.  alt_paraCentimetros(p): Converte o valor total para centímetros inteiros.
   5.  alt_compara(p1, p2): Retorna um número negativo se p1 < p2, zero se iguais ou positivo
      se p1 > p2.

 Exercício 2: TAD Massa

●  Contexto: Em sistemas de logística e nutrição, pesos são divididos em quilogramas e
   gramas para garantir exatidão em operações numéricas inteiras.
●  Enunciado Técnico: Construa a interface (massa.h) e a implementação (massa.c) do tipo
    abstrato Massa. O tipo oculta dois inteiros: quilogramas e gramas.
●  Requisitos do TAD:
   1.  mas_cria(kg, g) e mas_libera(m): Criação com ajuste automático de transbordo (1000g =
      1kg) e liberação de memória.
   2.  mas_paraString(m): (Obrigatório) Aloca dinamicamente e retorna uma string formatada
     no padrão X,XXXkg (ex: "82,450kg").
   3. mas_paraGramas(m): Converte a massa total para gramas inteiras.
   4. mas_soma(m1, m2): Retorna uma nova Massa alocada dinamicamente correspondente à
     soma dos dois operandos.

MÓDULO 2: CONSTRUÇÃO DE TADS COMPOSTOS (Nível
Intermediário)

 Exercício 3: TAD Instante (Composição de Data e Horario)

●  Contexto: Registros de auditoria e carimbos de data/hora (timestamps) em bancos de
   dados corporativos exigem a unificação de uma data e um horário específico.
●  Enunciado Técnico: Projete e implemente o TAD Instante (instante.h e instante.c). A
    estrutura opaca Instante deve conter internamente dois ponteiros para os TADs fornecidos:
   Data *data e Horario *horario.
●  Requisitos do TAD:
   1.  inst_cria(d, m, a, h, min, s): Instancia dinamicamente o objeto alocando internamente as
       estruturas Data e Horario.
   2.  inst_libera(i): Libera sequencialmente os ponteiros data e horario antes de liberar a
       estrutura receptora Instante.
   3.  inst_paraString(i): (Obrigatório) Retorna uma string alocada no formato "DD/MM/AAAA
     HH:MM:SS". Deve invocar obrigatoriamente dta_paraString e hor_paraString, concatenar

<!-- página 4 -->

seus resultados e desalocar as strings intermediárias.
   4.  inst_compara(i1, i2): Compara dois instantes temporais, retornando um valor negativo se
       i1 < i2, zero se idênticos ou positivo se i1 > i2.

 Exercício 4: TAD RegistroTemperatura (Composição de Matriz e Data)

●  Contexto: Estações meteorológicas automatizadas colhem múltiplas medições diárias de
   temperatura e precisam organizar esses dados indexados cronologicamente.
●  Enunciado Técnico: Projete o TAD RegistroTemp (registro_temp.h e registro_temp.c). A
    estrutura oculta deve guardar um ponteiro Data *data_inicio e um ponteiro Matriz *medicoes
   do TAD fornecido (onde as linhas representam os dias e as colunas representam os
    horários das medições).
●  Requisitos do TAD:
   1.  reg_cria(data_inicio, num_dias, leituras_por_dia): Inicializa a data base e aloca a Matriz
     com as dimensões especificadas.
   2.  reg_libera(r): Executa a liberação da memória da Data, da Matriz interna e da estrutura
       receptora.
   3.  reg_adicionarLeitura(r, dia_offset, leitura_idx, temp): Grava a temperatura na célula
      correspondente usando mat_atribui.
   4.  reg_paraString(r): (Obrigatório) Concatena o texto com a data base e o resultado de
      mat_paraString, retornando um relatório completo em string.

 Exercício 5: TAD EscalaTrabalho (Composição de Conjunto e Horario)

●  Contexto: Sistemas de recursos humanos precisam registrar os dias da semana em que
  um colaborador trabalha juntamente com sua jornada de entrada e saída.
●  Enunciado Técnico: Crie o TAD EscalaTrabalho (escala.h e escala.c). A estrutura interna
   deve possuir um ponteiro Conjunto *dias_semana (para guardar inteiros de 1 a 7
   representando Segunda a Domingo) e dois ponteiros para o TAD Horario: entrada e saida.
●  Requisitos do TAD:
   1.  esc_cria(entrada, saida): Inicializa o conjunto de dias com capacidade 7 e associa os
       horários informados.
   2.  esc_libera(e): Desaloque o conjunto, os dois horários e a própria estrutura.
   3.  esc_adicionarDia(e, dia_semana): Adiciona o dia da semana ao conjunto interno via
       conj_insere.
   4.  esc_paraString(e): (Obrigatório) Retorna uma string alocada dinamicamente unindo o
       resultado de conj_paraString e hor_paraString no formato "Dias: {1, 3, 5} | Entrada:
      08:00:00 | Saida: 17:00:00".

 Exercício 6: TAD Bagagem (Composição de Altura e Massa)

●  Contexto: Companhias aéreas lidam com dados tridimensionais (altura, largura,
   comprimento) combinados ao peso para gerenciar o porão das aeronaves.

<!-- página 5 -->

●  Enunciado Técnico: Projete e implemente o TAD_Bagagem (bagagem.h e bagagem.c)
    utilizando os TADs criados nos exercícios 1 e 2. A estrutura Bagagem deve conter
   internamente três ponteiros do tipo Altura (altura, largura e comprimento) e um ponteiro do
    tipo Massa (peso).
●  Requisitos do TAD:
   1.  bag_cria(alt, larg, comp, peso): Recebe as instâncias dos TADs Altura e Massa e monta
     a estrutura.
   2.  bag_libera(b): Libera sequencialmente os TADs de altura, largura, comprimento e massa
      antes de liberar a estrutura receptora Bagagem.
   3.  bag_paraString(b): (Obrigatório) Gera e retorna uma string alocada unindo
       alt_paraString para cada dimensão e mas_paraString para o peso (ex: "Dimensoes:
      [1,20m x 0,40m x 0,50m] - Peso: 23,500kg").
   4.  bag_calculaVolumeM3(b): Converte as alturas para metros e retorna o volume total em
      m3.

MÓDULO 3: APLICAÇÕES PRÁTICAS COM TADS (Com Main e Dados
Fornecidos)

 Exercício 7: Sistema de Avaliação Física de Atletas

●  Enunciado: Utilize a estrutura fornecida na main.c e complete as funções auxiliares para
    calcular o IMC e emitir os relatórios em string utilizando as funções dos TADs Altura e
   Massa.

   #include <stdio.h>
   #include <stdlib.h>
   #include "altura.h"
   #include "massa.h"

  typedef struct {
     char nome[50];
     Altura *altura;
    Massa *massa;
   } Atleta;

  char* atleta_paraString(const Atleta *at) {
     char *s_alt = alt_paraString(at->altura);
     char *s_mas = mas_paraString(at->massa);
     char *res = (char*) malloc(200 * sizeof(char));
      sprintf(res, "Atleta: %s | Altura: %s | Peso: %s", at->nome, s_alt, s_mas);
      free(s_alt);
     free(s_mas);
     return res;
   }

<!-- página 6 -->

int main() {
       /* DADOS TESTES FORNECIDOS */
     Atleta equipe[3] = {
        {"Carlos Silva", alt_cria(1, 85), mas_cria(82, 500)},
       {"Mariana Souza", alt_cria(1, 68), mas_cria(58, 200)},
       {"Lucas Lima", alt_cria(2, 05), mas_cria(110, 0)}
       };

     printf("=== RELATÓRIO DE ATLETAS ===\n");
      for (int i = 0; i < 3; i++) {
       char *str_atleta = atleta_paraString(&equipe[i]);

            // CÁLCULO DE IMC CONSUMINDO O TAD
       double h_m = alt_paraCentimetros(equipe[i].altura) / 100.0;
       double p_kg = mas_paraGramas(equipe[i].massa) / 1000.0;
       double imc = p_kg / (h_m * h_m);

        printf("%s | IMC: %.2f\n", str_atleta, imc);
         free(str_atleta);

            // Liberação dos TADs
         alt_libera(equipe[i].altura);
       mas_libera(equipe[i].massa);
       }

     return 0;
   }

 Exercício 8: Otimização de Carga e Frete Logístico

●  Enunciado: Dado o vetor de bagagens pré-cadastrado na main.c, escreva a rotina de
    controle de embarque acumulando peso e volume através das funções dos TADs.

   #include <stdio.h>
   #include <stdlib.h>
   #include "bagagem.h"

   int main() {
       /* DADOS TESTES FORNECIDOS */
    Bagagem* cargas[4];
     cargas[0] = bag_cria(alt_cria(0,50), alt_cria(0,40), alt_cria(0,60), mas_cria(15, 0));
     cargas[1] = bag_cria(alt_cria(1,00), alt_cria(0,80), alt_cria(1,20), mas_cria(45, 500));
     cargas[2] = bag_cria(alt_cria(0,30), alt_cria(0,30), alt_cria(0,40), mas_cria(8, 200));
     cargas[3] = bag_cria(alt_cria(0,80), alt_cria(0,60), alt_cria(0,90), mas_cria(30, 0));

     double limite_peso_kg = 80.0;
     double peso_acumulado_kg = 0.0;

     printf("=== PROCESSAMENTO DE EMBARQUE DE CARGA ===\n");

<!-- página 7 -->

for (int i = 0; i < 4; i++) {
       char *info = bag_paraString(cargas[i]);
       double vol = bag_calculaVolumeM3(cargas[i]);

        printf("Item %d: %s | Vol: %.3fm3\n", i + 1, info, vol);
         free(info);

            // Desalocação
        bag_libera(cargas[i]);
       }

     return 0;
   }

 Exercício 9: Controle Financeiro de Contas a Pagar (TAD Data)

●  Enunciado: Com os dados de títulos a pagar fornecidos na main.c, aplique as regras de
   negócio de desconto/juros consumindo a função dta_diferenca.

   #include <stdio.h>
   #include <stdlib.h>
   #include "data.h"

  typedef struct {
     char descricao[40];
     double valor_original;
    Data *vencimento;
    Data *pagamento;
   } Titulo;

   int main() {
       /* DADOS TESTES FORNECIDOS */
      Titulo contas[3] = {
        {"Aluguel do Galpão", 5000.00, dta_cria(20, 10, 2026), dta_cria(05, 10, 2026)}, // 15 dias antecedencia (10%
  desc)
       {"Fornecedor de Matéria-Prima", 2500.00, dta_cria(15, 10, 2026), dta_cria(15, 10, 2026)}, // No dia
  (Nominal)
        {"Serviço de Manutenção", 1200.00, dta_cria(10, 10, 2026), dta_cria(15, 10, 2026)}  // 5 dias atraso (Juros)
       };

     printf("=== LIQUIDAÇÃO DE TÍTULOS FINANCEIROS ===\n");
      for (int i = 0; i < 3; i++) {
          int dias_diff = dta_diferenca(contas[i].vencimento, contas[i].pagamento);
       double valor_final = contas[i].valor_original;

              if (dias_diff >= 10) {
           valor_final *= 0.90; // 10% desconto
          } else if (dias_diff < 0) {
             int dias_atraso = -dias_diff;

<!-- página 8 -->

valor_final += contas[i].valor_original * (0.001 * dias_atraso); // 0.1% ao dia
          }

       char *str_venc = dta_paraString(contas[i].vencimento);
       char *str_pag = dta_paraString(contas[i].pagamento);

         printf("Título: %s\n Vencimento: %s | Pagamento: %s\n Valor Original: R$ %.2f | Valor Pago: R$ %.2f\n\n",
            contas[i].descricao, str_venc, str_pag, contas[i].valor_original, valor_final);

        free(str_venc);
        free(str_pag);
        dta_libera(contas[i].vencimento);
        dta_libera(contas[i].pagamento);
       }

     return 0;
   }

 Exercício 10: Painel de Ocupação do Pátio Logístico (TAD Matriz e TAD Conjunto)

●  Enunciado: A main.c inicializa a matriz do pátio e o conjunto de linhas bloqueadas.
   Implemente a contagem de vagas operacionais e a formatação do relatório.

   #include <stdio.h>
   #include <stdlib.h>
   #include "matriz.h"
   #include "conjunto.h"

   int main() {
       /* DADOS TESTES FORNECIDOS: Pátio 4x5 */
     Matriz *patio = mat_cria(4, 5);
     Conjunto *zonas_interditadas = conj_cria(4);

        // Bloqueia as zonas (linhas) 1 e 3 para manutenção
     conj_insere(zonas_interditadas, 1);
     conj_insere(zonas_interditadas, 3);

        // Ocupa algumas vagas nas zonas liberadas (0: livre, 1: ocupada)
     mat_atribui(patio, 0, 1, 1.0);
     mat_atribui(patio, 0, 3, 1.0);
     mat_atribui(patio, 2, 0, 1.0);

     printf("=== MAPA DE OCUPAÇÃO DO PÁTIO ===\n");
     char *str_mat = mat_paraString(patio);
     char *str_conj = conj_paraString(zonas_interditadas);

      printf("Matriz do Pátio:\n%s\nZonas Interditadas: %s\n", str_mat, str_conj);

        // Contagem de vagas livres em zonas ativas

<!-- página 9 -->

int vagas_livres = 0;
      for (int l = 0; l < 4; l++) {
              if (!conj_pertence(zonas_interditadas, l)) {
            for (int c = 0; c < 5; c++) {
                      if (mat_acessa(patio, l, c) == 0.0) {
              vagas_livres++;
                 }
             }
          }
       }

      printf("Total de Vagas Disponíveis para Uso: %d\n", vagas_livres);

     free(str_mat);
      free(str_conj);
     mat_libera(patio);
     conj_libera(zonas_interditadas);

     return 0;
   }

MÓDULO 4: APLICAÇÕES EXCLUSIVAS COM TAD CONJUNTO E TAD
MATRIZ (Com Main e Dados Fornecidos)

 Exercício 11: Recomendador de Filmes e Compatibilidade de Usuários (TAD
Conjunto)

●  Contexto: Plataformas de streaming calculam a afinidade cultural entre dois usuários
   comparando o conjunto de IDs dos filmes que cada um assistiu na plataforma.
●  Enunciado Técnico: A main.c cria dois conjuntos de histórico de filmes. O objetivo do aluno
   é implementar a função de contagem de interseção e a análise de similaridade utilizando
   exclusivamente o TAD Conjunto.

   #include <stdio.h>
   #include <stdlib.h>
   #include "conjunto.h"

   /* Retorna a quantidade de elementos presentes simultaneamente em c1 e c2 */
   int conj_intersecao_qtd(const Conjunto *c1, const Conjunto *c2) {
      int qtd = 0;
        // Testa IDs de filmes típicos cadastrados
      for (int id = 100; id <= 120; id++) {
              if (conj_pertence(c1, id) && conj_pertence(c2, id)) {
          qtd++;
          }
       }
     return qtd;

<!-- página 10 -->

}

   int main() {
       /* DADOS TESTES FORNECIDOS */
     Conjunto *usrA = conj_cria(10);
     Conjunto *usrB = conj_cria(10);

        // Histórico de filmes assistidos (IDs)
     conj_insere(usrA, 101);
     conj_insere(usrA, 102);
     conj_insere(usrA, 105);
     conj_insere(usrA, 108);

     conj_insere(usrB, 102);
     conj_insere(usrB, 105);
     conj_insere(usrB, 110);
     conj_insere(usrB, 112);
     conj_insere(usrB, 115);

     printf("=== ANÁLISE DE AFINIDADE DE USUÁRIOS (STREAMING) ===\n");
     char *strA = conj_paraString(usrA);
     char *strB = conj_paraString(usrB);

     printf("Usuário A: %s\n", strA);
     printf("Usuário B: %s\n", strB);

     Conjunto *uniao = conj_uniao(usrA, usrB);
     char *str_uniao = conj_paraString(uniao);

      int qtd_intersecao = conj_intersecao_qtd(usrA, usrB);

     printf("União dos Filmes: %s\n", str_uniao);
     printf("Filmes em Comum: %d\n", qtd_intersecao);

      free(strA);
      free(strB);
     free(str_uniao);
     conj_libera(usrA);
     conj_libera(usrB);
     conj_libera(uniao);

     return 0;
   }

 Exercício 12: Análise de Vendas Mensais por Produto e Unidade (TAD Matriz)

●  Contexto: Uma rede varejista monitora o desempenho de vendas registradas em matrizes
    bidimensionais, onde cada linha representa uma Unidade/Loja e cada coluna representa
  um Mês do ano.

<!-- página 11 -->

●  Enunciado Técnico: A main.c fornece uma matriz $3 \times 4$ pré-preenchida com os
    totais de vendas (em milhares de reais) de 3 lojas ao longo do 1º quadrimestre (4 meses). O
   estudante deve implementar rotinas que utilizem exclusivamente a interface pública do TAD
    Matriz (mat_acessa, mat_atribui, mat_paraString) para:
   1.  Calcular o faturamento total acumulado por cada loja (soma da linha).
   2.  Identificar o mês de maior faturamento geral de toda a rede (soma da coluna).
   3.  Gerar e exibir o relatório consolidado final no terminal.

   #include <stdio.h>
   #include <stdlib.h>
   #include "matriz.h"

   int main() {
       /* DADOS TESTES FORNECIDOS: Matriz 3 Lojas (linhas) x 4 Meses (colunas)
      Valores em milhares de R$ */
      int num_lojas = 3;
      int num_meses = 4;
     Matriz *vendas = mat_cria(num_lojas, num_meses);

        // Loja 0: Centro
     mat_atribui(vendas, 0, 0, 45.5); // Jan
     mat_atribui(vendas, 0, 1, 50.0); // Fev
     mat_atribui(vendas, 0, 2, 52.8); // Mar
     mat_atribui(vendas, 0, 3, 60.1); // Abr

        // Loja 1: Shopping
     mat_atribui(vendas, 1, 0, 80.2); // Jan
     mat_atribui(vendas, 1, 1, 75.0); // Fev
     mat_atribui(vendas, 1, 2, 90.5); // Mar
     mat_atribui(vendas, 1, 3, 88.0); // Abr

        // Loja 2: Zona Sul
     mat_atribui(vendas, 2, 0, 30.0); // Jan
     mat_atribui(vendas, 2, 1, 32.5); // Fev
     mat_atribui(vendas, 2, 2, 28.9); // Mar
     mat_atribui(vendas, 2, 3, 35.4); // Abr

     printf("=== MATRIZ DE VENDAS (LOJAS x MESES) ===\n");
     char *str_vendas = mat_paraString(vendas);
      printf("%s\n", str_vendas);
     free(str_vendas);

      printf("--- FATURAMENTO TOTAL POR LOJA ---\n");
      for (int l = 0; l < num_lojas; l++) {
       double total_loja = 0.0;
         for (int c = 0; c < num_meses; c++) {
           total_loja += mat_acessa(vendas, l, c);
          }

<!-- página 12 -->

printf("Loja %d: R$ %.2fk\n", l + 1, total_loja);
       }

      printf("\n--- MÊS COM MAIOR FATURAMENTO REDE ---\n");
      int melhor_mes = 0;
     double maior_faturamento_mes = -1.0;

      for (int c = 0; c < num_meses; c++) {
       double total_mes = 0.0;
         for (int l = 0; l < num_lojas; l++) {
         total_mes += mat_acessa(vendas, l, c);
          }
        printf("Mês %d: Total R$ %.2fk\n", c + 1, total_mes);
              if (total_mes > maior_faturamento_mes) {
         maior_faturamento_mes = total_mes;
        melhor_mes = c + 1;
          }
       }

      printf("=> O Mês com maior faturamento foi o Mês %d (R$ %.2fk)\n", melhor_mes, maior_faturamento_mes);

        // Desalocação
     mat_libera(vendas);

     return 0;
   }

REGRAS DE IMPLEMENTAÇÃO E ENCAPSULAMENTO

1. Representação em String: Toda função que gera string (paraString) deve alocar
   dinamicamente a memória suficiente via malloc e retornar o ponteiro char*. O código
   chamador fica responsável por aplicar o free() na string recebida.
2. Encapsulamento Estrito: É expressamente proibido acessar os membros internos de
   qualquer TAD diretamente. Toda a manipulação deve ser feita exclusivamente através das
   funções públicas do arquivo de interface (.h).
