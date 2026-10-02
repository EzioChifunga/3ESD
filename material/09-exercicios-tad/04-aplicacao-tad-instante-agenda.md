# aplicacaoTADInstante

> Fonte: `base/Lista de TAD/aplicacaoTADInstante.pdf`

<!-- página 1 -->

Aqui está uma sugestão de enunciado técnico para um exercício focado em validação de conflitos de agenda
usando um vetor de ponteiros e o TAD Instante:

Exercício: Validador de Conflito de Compromissos (Uso do TAD Instante)
Contexto: Em sistemas de gestão de agenda corporativa e calendários eletrônicos, um dos principais requisitos
é garantir que novos eventos não causem sobreposição de horários (conflitos) com compromissos previamente
agendados.
Enunciado Técnico: Projete a estrutura e a função de validação para um sistema de agenda baseado no TAD
Instante.
     1.  Estrutura Compromisso: Defina a estrutura Compromisso, contendo:
         o   char descricao[100]: Descrição do evento.
         o   Instante *inicio: Ponteiro para o instante de início do compromisso.
         o   int duracao_minutos: Duração do compromisso em minutos.
     2.  Função de Validação: Implemente a função compro_pode_inserir:
     C
          int compro_pode_inserir(Compromisso **agenda, int n, Compromisso *novo);
         o  Entrada:
                         ▪   agenda: Um vetor de ponteiros para compromissos já cadastrados (Compromisso*
                          agenda[]).
                         ▪    n: A quantidade atual de compromissos no vetor.
                         ▪   novo: Ponteiro para o novo compromisso que se deseja incluir.
         o  Regra de Negócio: Dois compromissos entram em conflito se o horário de início de um
                 ocorrer antes do término do outro.
         o   Retorno: Retorna 1 se o compromisso puder ser incluído (sem conflitos) ou 0 caso contrário.
Requisitos do Exercício:
   ●   Utilize as funções do TAD Instante (inst_compara, inst_paraString, etc.) para manipular os instantes
        de início.
   ●   Considere que o término de um compromisso é obtido calculando o instante de início acrescido da sua
        duração em minutos.

MAIN PARA O COMPROMISSO
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "instante.h"

// 1. Definição da Estrutura Compromisso
typedef struct {
  char descricao[100];
  Instante *inicio;
   int duracao_minutos;
} Compromisso;
// Protótipos das funções do Compromisso
Compromisso* compro_cria(const char *descricao, Instante *inicio, int duracao_minutos);
void compro_libera(Compromisso *c);
int compro_pode_inserir(Compromisso **agenda, int n, Compromisso *novo);

// Função auxiliar para instanciar um Compromisso
Compromisso* compro_cria(const char *descricao, Instante *inicio, int duracao_minutos) {
   if (inicio == NULL || duracao_minutos <= 0) return NULL;

  Compromisso *c = (Compromisso *) malloc(sizeof(Compromisso));
   if (c == NULL) return NULL;

  strncpy(c->descricao, descricao, 99);
  c->descricao[99] = '\0';

<!-- página 2 -->

c->inicio = inicio;
  c->duracao_minutos = duracao_minutos;

  return c;
}

// Função para liberar a memória do compromisso e seu instante
void compro_libera(Compromisso *c) {
   if (c != NULL) {
       if (c->inicio != NULL) {
       inst_libera(c->inicio);
      }
     free(c);
   }
}

// 2. Função de Validação utilizando inst_somaMinutos e inst_compara
int compro_pode_inserir(Compromisso **agenda, int n, Compromisso *novo) {
  // escreva seu código aqui
}

int main() {
  Compromisso *agenda[10];
   int n = 0;

  // 1. Inserir compromisso 1: 01/09/2026 das 14:00 às 15:30 (90 min)
  Instante *t1 = inst_cria(1, 9, 2026, 14, 0, 0);
  Compromisso *c1 = compro_cria("Reuniao de Planejamento", t1, 90);

   if (compro_pode_inserir(agenda, n, c1)) {
    agenda[n++] = c1;
    printf("Compromisso 1 inserido com sucesso!\n\n");
   } else {
    printf("ERRO: Compromisso 1 recusado!\n\n");
    compro_libera(c1);
   }

  // 2. Tentar inserir compromisso 2 (COM CONFLITO): 01/09/2026 das 15:00 às 16:00 (60 min)
  Instante *t2 = inst_cria(1, 9, 2026, 15, 0, 0);
  Compromisso *c2 = compro_cria("Apresentacao de Projeto", t2, 60);
  printf("Tentando inserir: '%s' (15:00h)...\n", c2->descricao);

   if (compro_pode_inserir(agenda, n, c2)) {
    agenda[n++] = c2;
     printf("-> Compromisso inserido com sucesso!\n\n");
   } else {
     printf("-> ERRO: Horario em conflito! Compromisso recusado.\n\n");
    compro_libera(c2); // Recusado, libera memória
   }

  // 3. Tentar inserir compromisso 3 (SEM CONFLITO): 01/09/2026 das 15:30 às 16:30 (60 min)
  Instante *t3 = inst_cria(1, 9, 2026, 15, 30, 0);
  Compromisso *c3 = compro_cria("Sessao de Code Review", t3, 60);
  printf("Tentando inserir: '%s' (15:30h)...\n", c3->descricao);

<!-- página 3 -->

if (compro_pode_inserir(agenda, n, c3)) {
    agenda[n++] = c3;
     printf("-> Compromisso inserido com sucesso!\n\n");
   } else {
     printf("-> ERRO: Horario em conflito! Compromisso recusado.\n\n");
    compro_libera(c3);
   }

  // Limpeza de memória final dos compromissos aceitos na agenda
  for (int i = 0; i < n; i++) {
    compro_libera(agenda[i]);
   }

  return 0;
}
