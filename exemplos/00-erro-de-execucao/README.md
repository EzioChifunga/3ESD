# Achando um erro de execução

**Modo:** 🐞 Depurar (depois de ver o erro com ▶ Executar)

## O que este exemplo mostra
Um programa que **compila** mas **quebra** ao rodar: falha de segmentação (acesso inválido à memória). É o erro mais comum com ponteiros e listas.

## Como usar
1. Clique em **▶ Executar**: o programa termina com erro e o terminal explica o que é uma falha de segmentação.
2. Agora clique em **🐞 Depurar** (sem pontos de parada). A execução **para sozinha na linha do erro**, destacada em vermelho.
3. Na aba **Depuração**, olhe as variáveis: qual ponteiro vale `0x0` (NULL)?
4. Na **pilha de chamadas**, clique em `main` para ver de onde a função foi chamada.
5. Corrija: a função `procura` devolve NULL quando não encontra — teste antes de usar o resultado.
