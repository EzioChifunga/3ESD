# Alocação dinâmica: vetor do tamanho que o usuário pedir

**Modo:** ▶ Executar

## O que este exemplo mostra
- Um vetor local precisa de tamanho fixo **na compilação**. Com `malloc`, o tamanho é decidido **durante a execução**.
- `malloc` pode falhar: sempre teste se devolveu `NULL`.
- `realloc` aumenta o vetor mantendo os valores; `free` devolve a memória.
- Uma função que **cria e devolve** um vetor (a memória continua existindo depois que a função termina — diferente de um vetor local!).

## Como usar
**▶ Executar** e informe: quantos números, os números, quantos a mais e os novos números.

## Experimente
- No **🐞 Depurar**, em **Observar** use `v[0]@n` para ver os `n` elementos do vetor dinâmico.
- Remova o `free(v)` — o programa continua funcionando, mas isso é um **vazamento de memória**.
