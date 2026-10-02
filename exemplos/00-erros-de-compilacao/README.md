# Corrija os erros de compilação

**Modo:** 🩹 Corrigir erros

## O que este exemplo mostra
Os erros mais comuns de quem está começando — e como ler as mensagens do compilador.

## Como usar
1. Abra o `main.c`. Os erros já aparecem **sublinhados em vermelho** enquanto você olha o código.
2. Abra a aba **Problemas**: cada erro tem a linha e uma **💡 dica em português**. Clique nele para ir até a linha.
3. Corrija um erro de cada vez. A verificação roda sozinha enquanto você digita.
4. Quando não houver mais erros, **▶ Executar** e digite dois números (ex.: `4 7`).

## Os 5 erros (tente achar antes de ler!)
<details>
1. Falta `#include <stdlib.h>` para usar `malloc`/`free`.
2. Falta `;` no fim de uma linha (o gcc acusa a linha **seguinte**).
3. Variável usada sem ser declarada.
4. `=` (atribuição) usado no lugar de `==` (comparação) — é um aviso, mas é um erro de lógica!
5. Faltou o `&` no `scanf`.
</details>
