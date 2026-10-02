# Matriz alocada dinamicamente (int**)

**Modo:** 🐞 Depurar (ou ▶ Executar)

## O que este exemplo mostra
Mesmo problema do material `matrizDinAloc` do módulo 04:
- A matriz é um **vetor de ponteiros** (`int**`): cada posição aponta para uma linha alocada separadamente.
- Para liberar, primeiro cada linha, depois o vetor de ponteiros (ordem inversa da criação).
- `srand(0)` para gerar sempre os mesmos números "aleatórios" (bom para testar).

## Como usar
1. Marque `PARE AQUI` (F9) e **🐞 Depurar**. Digite `3 4` na janela do programa.
2. Em **Observar**: `matriz` (endereço do vetor de linhas), `matriz[0]` (endereço da linha 0) e `matriz[0][0]@4` (a linha 0 inteira).
