# Módulo 08 — Tipos Abstratos de Dados (TAD)

**Objetivo:** separar interface (`.h`) de implementação (`.c`), usar tipo opaco, encapsulamento e invariantes; criar, usar e liberar TADs; compor um TAD dentro de outro.

## Ordem de leitura
1. [Slides da aula — TAD](01-tad-slides-aula.md) — tipo de dado × estrutura de dado × TAD, interface/API, exemplos.
2. [Apostila de TAD em C](02-apostila-tad.md) — material de referência. A própria apostila sugere ler os **capítulos 1 a 4 antes da aula** e seguir o roteiro: problema → conceito → `.h`/`.c` → tipo opaco → TAD Ponto → memória → composição → erros → projetar um TAD novo.
3. [Exercício: contas a pagar com datas](03-exercicio-contas-com-datas.md) — struct com structs internas (`DataSimples`); prepara o uso do TAD Data.

## Método da apostila
Não decorar código: **desenhar** o que acontece na memória, cobrir a solução e tentar prever cada linha antes de comparar; usar os *checkpoints* como teste de domínio.

## Antes de avançar, você deve saber
- Escrever do zero um TAD com `cria`, `libera`, acesso e operações (ex.: Produto, Fração — caem na lista da AV1).

⬅️ [07 — Trabalho](../07-trabalho-avaliativo/README.md) · ➡️ [09 — Exercícios de TAD](../09-exercicios-tad/README.md)
