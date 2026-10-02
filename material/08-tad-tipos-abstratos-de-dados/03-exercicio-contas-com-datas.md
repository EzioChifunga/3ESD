# ExercícioTAD262

> Fonte: `base/Introdução TAD/ExercícioTAD262.pdf`

<!-- página 1 -->

Exercício: Processamento de Contas com campos da struct não sendo do tipo Data

Considere a estrutura tConta definida a seguir, contendo a descrição da conta, o valor base e duas structs
internas que representam as datas de vencimento e pagamento (cada uma com dia, mes e ano):

typedef struct {
    int dia;
    int mes;
    int ano;
} DataSimples;

typedef struct {
    char descricao[50];
    float valor_original;
    DataSimples dt_vencimento;
    DataSimples dt_pagamento;
} tConta;

Tarefa:

A partir do programa base fornecido, implemente a lógica para calcular e exibir o valor final a pagar de
cada conta do vetor de contas a pagar fornecido na main

Regras de Negócio:
   ●  Desconto de 10%: Pagamento realizado com pelo menos 10 dias de antecedência em relação ao
       vencimento.
   ●  Sem Desconto / Sem Juros: Pagamento efetuado no prazo (no dia do vencimento ou antecipado em
      menos de 10 dias).
   ●  Juros de 0,1% ao dia: Pagamento efetuado com atraso (cobrado sobre o valor original para cada dia
      de atraso).

Dicas:

    1.  Converta as datas internas (DataSimples) para ponteiros do tipo tData utilizando dta_cria.
    2.  Calcule a diferença em dias usando a função dta_diferenca(dt_pagamento, dt_vencimento).
    3.  Exiba para cada conta: descrição, datas formatadas (usando dta_exibe ou dta_getData), valor
        original e valor final ajustado.
    4.  Libere a memória alocada para todas as variáveis tData criadas.

Código Base Fornecido:

#include <stdio.h>
#include <stdlib.h>
#include "Data.h"

typedef struct {
    int dia;
    int mes;
    int ano;
} DataSimples;

typedef struct {
    char descricao[50];
    float valor_original;
    DataSimples dt_vencimento;
    DataSimples dt_pagamento;
} tConta;

<!-- página 2 -->

int main() {
    // Declaração das 7 contas
    tConta c1 = {"Conta de Luz",      200.0, {20, 9, 2026}, {5, 9, 2026}};   // 15 dias antes (Desconto 10%)
    tConta c2 = {"Internet",          100.0, {15, 9, 2026}, {15, 9, 2026}};  // No dia (Valor normal)
    tConta c3 = {"Cartao Credito",    500.0, {10, 9, 2026}, {15, 9, 2026}};  // 5 dias de atraso (Juros)
    tConta c4 = {"Condominio",       800.0, {10, 10, 2026}, {1, 10, 2026}};  // 9 dias antes (Sem desconto)
    tConta c5 = {"Plano de Saude",    350.0, {30, 9, 2026}, {20, 9, 2026}};  // 10 dias antes (Desconto 10%)
    tConta c6 = {"IPVA",             1200.0, {05, 8, 2026}, {25, 8, 2026}};  // 20 dias de atraso (Juros)
    tConta c7 = {"Academia",         120.0, {01, 9, 2026}, {01, 9, 2026}};  // No dia (Valor normal)

    // Vetor de ponteiros para struct tConta preenchido
    tConta* contas[7] = {&c1, &c2, &c3, &c4, &c5, &c6, &c7};
    int total_contas = 7;

    // TODO: Escreva a partir daqui a lógica para calcular o valor final de cada conta
    // utilizando as funções do TAD Data (dta_cria, dta_diferenca, dta_exibe, dta_libera, etc.)

    return 0;
}

++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Aqui está a proposta atualizada com a modificação solicitada: a estrutura tConta agora possui ponteiros
para tData como campos, permitindo instanciar o TAD diretamente com dta_cria, mantendo o vetor de
ponteiros para as contas preenchido no código base.

Exercício: Processamento de Contas com o campo da struct sendo do tipo Data

Considere a estrutura tConta definida a seguir, contendo a descrição da conta, o valor base e ponteiros
para o TAD tData representando as datas de vencimento e pagamento:

typedef struct {
    char descricao[50];
    float valor_original;
    tData* dt_vencimento;
    tData* dt_pagamento;
} tConta;

Tarefa:

A partir do programa base fornecido abaixo, implemente a lógica no main para percorrer o vetor de contas,
calcular e exibir o valor final a pagar de cada conta.

Regras de Negócio:

   ●  Desconto de 10%: Pagamento realizado com pelo menos 10 dias de antecedência em relação ao
       vencimento.
   ●  Sem Desconto / Sem Juros: Pagamento efetuado no prazo (no dia do vencimento ou antecipado em
      menos de 10 dias).
   ●  Juros de 0,1% ao dia: Pagamento efetuado com atraso (cobrado sobre o valor original para cada dia
      de atraso).

Dicas:

    1.  Utilize a função dta_diferenca(dt_pagamento, dt_vencimento) para obter o número de dias de
       diferença entre as datas.
    2.  Exiba para cada conta: a descrição, as datas (formatadas via dta_exibe ou dta_getData), o valor
        original e o valor final recalculado.

<!-- página 3 -->

3. Ao final da execução, libere toda a memória alocada dinamicamente: primeiro os ponteiros de
      tData internos de cada conta (usando dta_libera) e, em seguida, os ponteiros das próprias contas
       (usando free).

Código Base Fornecido :

#include <stdio.h>
#include <stdlib.h>
#include "Data.h"

typedef struct {
    char descricao[50];
    float valor_original;
    tData* dt_vencimento;
    tData* dt_pagamento;
} tConta;

// Função auxiliar para simplificar a criação das contas nos testes
tConta* criar_conta(const char* desc, float valor, int diaV, int mesV, int anoV, int diaP, int mesP, int
anoP) {
    tConta* c = (tConta*) malloc(sizeof(tConta));
    sprintf(c->descricao, "%s", desc);
    c->valor_original = valor;
    c->dt_vencimento = dta_cria(diaV, mesV, anoV);
    c->dt_pagamento = dta_cria(diaP, mesP, anoP);
    return c;
}

int main() {
    // Vetor de ponteiros para struct tConta alocado e preenchido
    tConta* contas[7];
    contas[0] = criar_conta("Conta de Luz",   200.0,  20, 9, 2026,   5, 9, 2026);  // 15 dias antes (Desconto
10%)
    contas[1] = criar_conta("Internet",       100.0,  15, 9, 2026,  15, 9, 2026);  // No dia (Valor normal)
    contas[2] = criar_conta("Cartao Credito", 500.0,  10, 9, 2026,  15, 9, 2026);  // 5 dias de atraso
(Juros)
    contas[3] = criar_conta("Condominio",     800.0,  10, 10, 2026,  1, 10, 2026); // 9 dias antes (Sem
desconto)
    contas[4] = criar_conta("Plano de Saude", 350.0,  30, 9, 2026,  20, 9, 2026);  //  10 dias antes
(Desconto 10%)
    contas[5] = criar_conta("IPVA",          1200.0,   5, 8, 2026,  25, 8, 2026);  // 20 dias de atraso
(Juros)
    contas[6] = criar_conta("Academia",      120.0,    1, 9, 2026,   1, 9, 2026);  // No dia (Valor normal)

    int total_contas = 7;

    // TODO: Escreva a partir daqui a lógica para calcular o valor final de cada conta
    // utilizando as funções do TAD Data (dta_diferenca, dta_exibe/dta_getData, dta_libera, etc.)

    return 0;
}

+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Exercício: Gestão de Projetos e Controle de Prazos de Entregáveis

Uma empresa de software gerencia a entrega de módulos de um projeto. Cada entregável possui uma data
limite de conclusão (prazo final) e uma data em que o desenvolvimento foi efetivamente concluído.

Considere a estrutura tEntregavel, que contém a descrição do módulo, a estimativa de horas de trabalho e
ponteiros para o TAD tData representando a data do prazo e a data da entrega:

typedef struct {
    char modulo[50];
    int horas_estimadas;
    tData* dt_prazo;
    tData* dt_entrega;

<!-- página 4 -->

} tEntregavel;

Tarefa:

A partir do código base fornecido abaixo, implemente a lógica no main para percorrer o vetor de ponteiros
para entregáveis, avaliar a situação de cada entrega e calcular o indicador de desempenho final
(pontuação) do entregável.

Regras de Negócio (Pontuação do Entregável):

   ●  Entrega Antecipada (no mínimo 5 dias antes do prazo): Ganha 20% de bônus sobre as horas
       estimadas .
   ●  Entrega no Prazo (no dia ou antecipada em até 4 dias): Recebe exatamente as horas estimadas
   ●  Entrega com Atraso: Sofre uma penalidade de 5% das horas estimadas por dia de atraso (a
       pontuação final não pode ser menor que zero).

Dicas:

    1.  Utilize a função dta_diferenca(dt_entrega, dt_prazo) para determinar a diferença em dias
        entre a entrega e o prazo.
    2.  Exiba para cada entregável: o nome do módulo, as datas formatadas (dta_exibe ou dta_getData),
       os dias de antecedência/atraso e a pontuação final calculada.
    3. Ao final da execução, libere toda a memória alocada dinamicamente: primeiro os ponteiros tData
       dentro de cada struct e depois a struct do próprio entregável.

Código Base Fornecido:

#include <stdio.h>
#include <stdlib.h>
#include "Data.h"

typedef struct {
    char modulo[50];
    int horas_estimadas;
    tData* dt_prazo;
    tData* dt_entrega;
} tEntregavel;

// Função auxiliar para criar e instanciar um entregável
tEntregavel* criar_entregavel(const char* mod, int horas, int diaP, int mesP, int anoP, int
diaE, int mesE, int anoE) {
    tEntregavel* e = (tEntregavel*) malloc(sizeof(tEntregavel));
    sprintf(e->modulo, "%s", mod);
    e->horas_estimadas = horas;
    e->dt_prazo = dta_cria(diaP, mesP, anoP);
    e->dt_entrega = dta_cria(diaE, mesE, anoE);
    return e;
}

int main() {
    // Vetor de ponteiros para struct tEntregavel alocado e preenchido
    tEntregavel* projeto[6];
    projeto[0] = criar_entregavel("Modulo de Autenticacao", 40, 15, 10, 2026,  5, 10, 2026); // 10 dias antes
(Bônus 20%)
    projeto[1] = criar_entregavel("API de Pagamentos",     80, 20, 10, 2026, 20, 10, 2026); // No dia
(Pontuação normal)
    projeto[2] = criar_entregavel("Relatorios em PDF",     30, 10, 10, 2026, 14, 10, 2026); // 4 dias de
atraso (Penalidade)
    projeto[3] = criar_entregavel("Dashboard Analytics",   60, 25, 10, 2026, 22, 10, 2026); // 3 dias antes
(No prazo - sem bônus)
    projeto[4] = criar_entregavel("Modulo de Notificacao", 20, 05, 10, 2026, 30, 09, 2026); // 5 dias antes
(Bônus 20%)

<!-- página 5 -->

projeto[5] = criar_entregavel("Integracao Gateway",    50, 01, 11, 2026, 15, 11, 2026); // 14 dias de
atraso (Penalidade severa)

    int total_entregaveis = 6;

    // TODO: Escreva a partir daqui a lógica para calcular o desempenho de cada entregável
    // utilizando as funções do TAD Data (dta_diferenca, dta_exibe/dta_getData, dta_libera, etc.)

    return 0;
}

++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Exercício: Criação de um TAD para Sistema de Reservas de Hotel

Neste exercício, você deverá criar um Tipo Abstrato de Dados (TAD) chamado tReserva que gerencia
reservas de quartos de hotel e utiliza a interface do TAD Data para manipular as datas de check-in e check-
out.

1. Especificação da Interface (Reserva.h)

Você deve implementar as seguintes definições e funções exportadas:

#ifndef RESERVA_H
#ifndef RESERVA_H
#define RESERVA_H

#include "Data.h"

typedef struct reserva tReserva;

/* Função cria: Aloca dinamicamente uma reserva com hóspede, número do quarto,
   valor da diária e datas de check-in e check-out */
tReserva* res_cria(const char* hospede, int quarto, float diaria,
                   tData* dt_checkin, tData* dt_checkout);

/* Função libera: Desaloca a memória da reserva e das datas internas */
void res_libera(tReserva* r);

/* Retorna a quantidade de diárias (diferença em dias entre check-out e check-in) */
int res_calculaDiarias(tReserva* r);

/* Retorna o valor total da hospedagem (diárias * valor da diária) */
float res_calculaValorTotal(tReserva* r);

/* Imprime o resumo da reserva na tela com hóspede, quarto, datas e valor total */
void res_exibe(tReserva* r);

#endif

2. Implementação do TAD (Reserva.c)

Na implementação interna da estrutura:

   ●  Guarde o nome do hóspede (char hospede[50]), número do quarto (int quarto), valor da diária
      (float diaria) e dois ponteiros para tData (dt_checkin e dt_checkout).
   ●  Na função res_calculaDiarias, utilize a função dta_diferenca(r->dt_checkout, r-
      >dt_checkin).
   ●  Na função res_libera, garanta a liberação dos ponteiros tData usando dta_libera antes de liberar
      o ponteiro da própria reserva.

3. Programa Principal de Teste (main.c)

<!-- página 6 -->

Crie um programa que utilize seu novo TAD criando um vetor de ponteiros para tReserva:

#include <stdio.h>
#include <stdlib.h>
#include "Data.h"
#include "Reserva.h"

int main() {
    tReserva* reservas[3];

    // Criação de reservas utilizando o TAD Data para as datas
    reservas[0] = res_cria("Carlos Silva", 101, 150.0, dta_cria(10, 10, 2026), dta_cria(15, 10,
2026));
    reservas[1] = res_cria("Ana Souza",    204, 220.0, dta_cria(01, 11, 2026), dta_cria(08, 11,
2026));
    reservas[2] = res_cria("Marcos Lima",   305, 300.0, dta_cria(20, 12, 2026), dta_cria(25, 12,
2026));

    printf("=== RELATÓRIO DE RESERVAS ===\n\n");
    float faturamento_total = 0.0;

    for (int i = 0; i < 3; i++) {
        res_exibe(reservas[i]);
        faturamento_total += res_calculaValorTotal(reservas[i]);
        printf("-----------------------------------\n");
    }

    printf("Faturamento Total Previsto: R$ %.2f\n", faturamento_total);

    // Liberação de memória
    for (int i = 0; i < 3; i++) {
        res_libera(reservas[i]);
    }

    return 0;
}

++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
