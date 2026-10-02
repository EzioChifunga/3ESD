# Módulo 10 — Listas lineares

> **Objetivo:** lista sequencial (vetor) e listas encadeadas — simples (LSE), duplamente encadeada (LDE) e LDE circular com nó cabeça — com inclusão, exclusão e busca em todos os casos.

## Ordem de leitura

0. [Roteiro dos tópicos](00-roteiro-dos-topicos.md) — a ordem dos assuntos definida pelo professor.
1. [Listas lineares — o que são](01-listas-lineares-o-que-sao.md) — definição, propriedades e operações.
2. [Lista sequencial ordenada sem repetição](02-lista-sequencial-ordenada-sem-repeticao.md) — o “kit lista”: vetor + contador.
3. [TAD lista sequencial](03-tad-lista-sequencial.md) — classificada/não classificada, com/sem repetição.
4. [Lista simplesmente encadeada — introdução](04-lista-simplesmente-encadeada.md) — nós, `malloc`, encadeamento.
5. [Exercício de fixação](05-exercicio-fixacao-lista-encadeada.md) — lista encadeada de alunos.
6. [Lista duplamente encadeada — introdução](06-lista-duplamente-encadeada.md) — ponteiros `ant` e `suc`.
7. [Resumo: LDE (folha de código)](07-resumo-lde.md) — funções principais da LDE.
8. [Resumo: LDE circular com cabeça (folha de código)](08-resumo-lde-circular-com-cabeca.md) — funções principais com nó cabeça.
9. [Apostila: Listas Lineares em C](09-apostila-listas-lineares.md) — referência completa, cobre tudo acima.

> **Observação:** Os arquivos 7 e 8 eram folhas de uma página em colunas; o texto saiu com as colunas misturadas. Para eles, prefira o PDF original em `base/Listas/` (`LDE.pdf` e `ldeccab.pdf`).

## Método da apostila (capítulos 3–13)

Cada operação é estudada sempre na mesma ordem:

1. O que queremos fazer?
2. Como a lista está desenhada?
3. Qual é o caso normal?
4. Qual caso quebra a regra?
5. Qual problema aparece?
6. Que ideia resolve o problema?
7. Simulação passo a passo (desenho antes/depois)
8. **Só então:** código.

## Mapa da apostila

| Capítulos | Tema |
|---|---|
| 1–2 | O que é lista linear; o TAD Lista com várias representações |
| 3–5 | Lista sequencial: representação, inclusão, exclusão |
| 6–9 | LSE sem cabeça (problemas) → LSE com cabeça e sentinela: inclusão e exclusão |
| 10–13 | LDE sem cabeça (problemas) → LDE circular com cabeça: inclusão e exclusão |
| 14–15 | Organizar cada projeto TAD; quando usar cada implementação |
| 16–19 | Atlas de erros, checkpoints, exercícios graduados, mapa mental |

## Exemplos práticos

Clique para abrir o guia do exemplo; de lá, use **🧪 Abrir na sandbox** ou **📁 Abrir como projeto**.

- [Lista duplamente encadeada circular com nó cabeça](../../exemplos/10-lde-circular-com-cabeca/README.md) — 🐞 Depurar. Com o nó cabeça (sentinela) não existe caso especial: inserir e remover são sempre as mesmas 4 atribuições.
- [Lista simplesmente encadeada (sem cabeça)](../../exemplos/10-lista-encadeada/README.md) — 🐞 Depurar. Inserção no início e ordenada, busca, remoção e liberação — acompanhe os ponteiros prox no depurador.
- [Lista sequencial ordenada sem repetição](../../exemplos/10-lista-sequencial-ordenada/README.md) — ▶ Executar. Lista em vetor: busca binária da posição, inclusão abrindo espaço e exclusão fechando espaço.

## Antes de avançar, você deve saber

- Inserir e remover em lista **sequencial** (abrir e fechar espaço no vetor).
- Tratar os casos especiais de uma LSE **sem cabeça** (inserir/remover o primeiro).
- Explicar por que o **nó cabeça** elimina esses casos especiais.
- Percorrer uma LDE nos dois sentidos.

---

⬅️ [09 — Exercícios de TAD](../09-exercicios-tad/README.md) · 🏠 [Início](../README.md) · ➡️ [11 — Pilha e fila](../11-pilha-e-fila/README.md)
