# Depurando passo a passo

**Modo:** 🐞 Depurar

## O que este exemplo mostra
Como o depurador deixa você **ver o programa por dentro**: o valor de cada variável a cada linha, e o que acontece quando uma função é chamada.

## Como usar
1. Clique na **margem à esquerda** do número da linha marcada com `PARE AQUI` (ou ponha o cursor nela e aperte **F9**). Aparece uma bolinha vermelha ●.
2. Clique em **🐞 Depurar** (F5). O programa para na linha marcada (fundo amarelo).
3. Abra a aba **Depuração** embaixo: veja `n`, `i` e `resultado`.
4. Use **↷ Próxima linha** (F10) várias vezes e acompanhe `i` e `resultado` mudando a cada volta do laço.
5. Na linha que chama `fatorial`, use **↓ Entrar** (F11) para entrar na função. A **pilha de chamadas** mostra `fatorial` em cima de `main`.
6. **↑ Sair** (Shift+F11) termina a função e volta para a `main`.
7. Em **Observar**, digite `resultado * 2` e aperte Enter: o depurador calcula a expressão.

> A saída do programa aparece numa janela de console separada durante a depuração.
