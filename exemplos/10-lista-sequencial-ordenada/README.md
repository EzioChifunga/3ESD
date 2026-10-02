# Lista sequencial ordenada sem repetição

**Modo:** ▶ Executar

## O que este exemplo mostra
O "kit lista" do material: **vetor + contador**, agrupados numa struct.
- **Busca**: como a lista é ordenada, usa busca binária e devolve também *onde o valor deveria estar*.
- **Inclusão**: desloca os elementos para a direita para **abrir espaço** (e recusa repetidos).
- **Exclusão**: desloca para a esquerda para **fechar o buraco**.

## Como usar
**▶ Executar** e use o menu: `1 30` inclui 30, `2 10` exclui 10, `3 25` busca 25, `0` sai.

## Experimente
No **🐞 Depurar**, pare em `lst_inclui` e observe `l->v[0]@l->n` antes e depois do deslocamento.
