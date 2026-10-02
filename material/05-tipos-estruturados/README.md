# Módulo 05 — Tipos estruturados (struct)

> **Objetivo:** agrupar dados heterogêneos com `struct`/`typedef`, usar ponteiro para struct (`->`), vetores de structs e vetores de ponteiros para structs.

## Ordem de leitura

1. [Tipos estruturados — introdução (aula 1)](01-tipos-estruturados-introducao.md) — o que é uma estrutura, declaração e acesso aos campos.
2. [Revisão de structs](02-revisao-structs.md) — structs aninhadas, funções e ponteiros para struct.
3. [Vetor de tipos estruturados](03-vetor-de-structs.md) — vetor de structs estático e com `malloc`.
4. [Vetor de ponteiros para tipos estruturados](04-vetor-de-ponteiros-para-structs.md) — cada elemento alocado separadamente.

## Exemplos práticos

Clique para abrir o guia do exemplo; de lá, use **🧪 Abrir na sandbox** ou **📁 Abrir como projeto**.

- [Vetor de ponteiros para structs](../../exemplos/05-vetor-de-ponteiros-para-structs/README.md) — 🐞 Depurar. Cada pessoa é alocada separadamente e o vetor guarda só os endereços; ordenar troca ponteiros, não structs inteiras.
- [Vetor de structs (alocado dinamicamente)](../../exemplos/05-vetor-de-structs/README.md) — ▶ Executar. Cadastro de pessoas em um vetor de struct criado com malloc; busca pelo nome e pela maior idade.

## Antes de avançar, você deve saber

- `p->campo` é o mesmo que `(*p).campo`.
- A diferença entre `tPessoa v[n]` e `tPessoa *v[n]`.
- Esse é o modelo das *Etapas 7, 8 e 9* do trabalho e a porta de entrada para TAD.

---

⬅️ [04 — Revisão: alocação dinâmica de memória](../04-revisao-alocacao-dinamica/README.md) · 🏠 [Início](../README.md) · ➡️ [06 — Busca e ordenação em vetores](../06-busca-e-ordenacao/README.md)
