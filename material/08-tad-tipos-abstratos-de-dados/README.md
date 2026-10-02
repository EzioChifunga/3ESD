# Módulo 08 — Tipos Abstratos de Dados (TAD)

> **Objetivo:** separar interface (`.h`) de implementação (`.c`), usar tipo opaco, encapsulamento e invariantes; criar, usar e liberar TADs; compor um TAD dentro de outro.

## Ordem de leitura

1. [Slides da aula — TAD](01-tad-slides-aula.md) — tipo de dado × estrutura de dado × TAD, interface/API, exemplos.
2. [Apostila de TAD em C](02-apostila-tad.md) — material de referência completo (leia os capítulos 1 a 4 antes da aula).
3. [Exercício: contas a pagar com datas](03-exercicio-contas-com-datas.md) — struct com structs internas (`DataSimples`); prepara o uso do TAD Data.

## Método da apostila

> **Não decore código.** Desenhe o que acontece na memória, cubra a solução e tente prever cada linha antes de comparar. Use os *checkpoints* como teste de domínio.

Roteiro da apostila: problema → conceito → `.h`/`.c` → tipo opaco → TAD Ponto → memória → composição → erros → projetar um TAD novo.

## Exemplos práticos

Clique para abrir o guia do exemplo; de lá, use **🧪 Abrir na sandbox** ou **📁 Abrir como projeto**.

- [TAD Ponto: interface (.h) × implementação (.c)](../../exemplos/08-tad-ponto/README.md) — ▶ Executar. O TAD clássico da apostila: tipo opaco, cria/libera, acesso controlado e uma operação (distância).
- [TAD Retângulo usando o TAD Ponto (composição)](../../exemplos/08-tad-retangulo-composicao/README.md) — 🐞 Depurar. Um TAD que guarda dois Pontos: o Retângulo só usa a interface do Ponto, nunca seus campos.

## Antes de avançar, você deve saber

- Escrever do zero um TAD com **cria**, **libera**, acesso e operações.
- Explicar por que o cliente não pode acessar os campos de um tipo opaco.
- Montar um projeto com `main.c`, `tad.h` e `tad.c`.

---

⬅️ [07 — Trabalho avaliativo: Sistema de Recomendação por Afinidade](../07-trabalho-avaliativo/README.md) · 🏠 [Início](../README.md) · ➡️ [09 — Exercícios de TAD](../09-exercicios-tad/README.md)
