# Fila sequencial circular

**Modo:** 🐞 Depurar (ou ▶ Executar)

## O que este exemplo mostra
- **Fila (FIFO)**: entra no fim, sai do início — o primeiro a chegar é o primeiro atendido.
- Na fila sequencial simples, depois de algumas saídas sobra espaço no começo do vetor que não é reaproveitado. A **fila circular** resolve: os índices "dão a volta" com `% MAX`.
- Um contador `n` diferencia fila vazia de fila cheia.

## Como usar
1. Marque `PARE AQUI` (F9) e **🐞 Depurar**.
2. Em **Observar**: `f.ini`, `f.fim`, `f.n` e `f.itens`. Avance (F10) e veja `fim` voltar para 0 depois de passar do fim do vetor.
