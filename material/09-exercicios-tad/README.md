# Módulo 09 — Exercícios de TAD

> **Objetivo:** praticar o uso e a construção de TADs com os TADs-base fornecidos (Conjunto, Data, Horário, Instante, Matriz, Altura).

## Ordem de leitura

1. [Lista de exercícios de TAD](01-lista-de-exercicios-tad.md) — começa pelo problema das férias com o TAD Conjunto.
2. [Enunciado: TAD Conjunto (férias da família)](02-enunciado-tad-conjunto-ferias.md) — **caiu no simulado da AV1 (Questão 2)**.
3. [Lista didática V3](03-lista-didatica-v3.md) — contratos dos TADs-base + 4 módulos de exercícios (simples, compostos, aplicações, Conjunto e Matriz).
4. [Aplicação do TAD Instante](04-aplicacao-tad-instante-agenda.md) — validador de conflito de agenda.

## Código fornecido

Cópias dos arquivos originais de `base/Lista de TAD/`. Abra um deles na trilha e use **▶ Abrir como projeto**: o Estudo C copia o TAD e tudo que ele inclui (ex.: `instante.h` traz `data` e `horario`).

| TAD | Interface | Implementação |
|---|---|---|
| Conjunto | [conj.h](codigo/conj.h) | [conj.c](codigo/conj.c) |
| Data | [data.h](codigo/data.h) | [data.c](codigo/data.c) |
| Horário | [horario.h](codigo/horario.h) | [horario.c](codigo/horario.c) |
| Instante (Data + Horário) | [instante.h](codigo/instante.h) | [instante.c](codigo/instante.c) |
| Matriz | [matriz1.h](codigo/matriz1.h) | [matriz1.c](codigo/matriz1.c) |
| Altura | [TAD_Altura.h](codigo/TAD_Altura.h) | [TAD_Altura.c](codigo/TAD_Altura.c) |

> **Prova:** um TAD Matriz aparece no simulado (Questão 3 da segunda prova).

## Exemplos práticos (TADs do módulo 08)

Clique para abrir o guia do exemplo; de lá, use **🧪 Abrir na sandbox** ou **📁 Abrir como projeto**.

- [TAD Ponto: interface (.h) × implementação (.c)](../../exemplos/08-tad-ponto/README.md) — ▶ Executar. O TAD clássico da apostila: tipo opaco, cria/libera, acesso controlado e uma operação (distância).
- [TAD Retângulo usando o TAD Ponto (composição)](../../exemplos/08-tad-retangulo-composicao/README.md) — 🐞 Depurar. Um TAD que guarda dois Pontos: o Retângulo só usa a interface do Ponto, nunca seus campos.

## Antes de avançar, você deve saber

- Usar um TAD **só pela interface** (`.h`), sem olhar o `.c`.
- Compor TADs (ex.: Instante = Data + Horário).
- Resolver um problema com o TAD Conjunto (união, interseção, diferença).

---

⬅️ [08 — Tipos Abstratos de Dados (TAD)](../08-tad-tipos-abstratos-de-dados/README.md) · 🏠 [Início](../README.md) · ➡️ [10 — Listas lineares](../10-listas-lineares/README.md)
