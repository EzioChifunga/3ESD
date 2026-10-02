# Módulo 04 — Revisão: alocação dinâmica de memória

> **Objetivo:** usar `malloc`/`free`, entender o ciclo de vida da memória (global, local, heap) e alocar vetores e matrizes em tempo de execução.

## Ordem de leitura

1. [Revisão de alocação de memória](01-revisao-alocacao-de-memoria.md) — uso da memória, vetores locais e funções.
2. [Alocação dinâmica: vetores e matrizes](02-alocacao-dinamica-vetores-e-matrizes.md) — versão completa: inclui vetor e matriz alocados dinamicamente.
3. [Exemplo: matriz dinâmica](03-exemplo-matriz-dinamica.md) — programa completo (`criaMat2d`, `liberaMat2d`, soma de linha/coluna).

> **Observação:** O arquivo 2 repete o conteúdo do 1 e acrescenta vetores e matrizes dinâmicas.

## Exemplos práticos

Clique para abrir o guia do exemplo; de lá, use **🧪 Abrir na sandbox** ou **📁 Abrir como projeto**.

- [Matriz alocada dinamicamente (int**)](../../exemplos/04-matriz-dinamica/README.md) — 🐞 Depurar. Cria a matriz linha por linha com malloc, preenche, soma linhas e colunas e libera na ordem certa.
- [Alocação dinâmica: vetor do tamanho que o usuário pedir](../../exemplos/04-vetor-dinamico/README.md) — ▶ Executar. malloc de um vetor com n lido do teclado, realloc para crescer e free no final — sempre testando o retorno.

## Antes de avançar, você deve saber

- Sempre testar se `malloc` devolveu `NULL`.
- Alocar e liberar uma matriz `int**` linha por linha — usado na *Etapa 6* do trabalho e no TAD Matriz.
- Por que uma função pode devolver memória do `malloc`, mas **não** um vetor local.

---

⬅️ [03 — Revisão: cadeias de caracteres (strings)](../03-revisao-strings/README.md) · 🏠 [Início](../README.md) · ➡️ [05 — Tipos estruturados (struct)](../05-tipos-estruturados/README.md)
