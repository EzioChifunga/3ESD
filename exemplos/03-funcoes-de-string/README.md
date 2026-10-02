# Strings: escrevendo as funções da string.h

**Modo:** ▶ Executar

## O que este exemplo mostra
- Uma string em C é um **vetor de char terminado em `'\0'`**. Toda função de string percorre até achar esse caractere.
- Versões próprias de `strlen`, `strcpy` e `strcmp` (e um tamanho **recursivo**).
- `fgets` para ler uma linha inteira (com espaços) e como tirar o `'\n'` do final.

## Experimente
- No modo **🐞 Depurar**, pare dentro de `meuStrlen` e observe `s[i]`.
- Em **Observar**, digite `frase` para ver o vetor inteiro.
