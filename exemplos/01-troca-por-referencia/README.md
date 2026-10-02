# Ponteiros: passagem por valor × por referência

**Modo:** 🐞 Depurar

## O que este exemplo mostra
- Em C, os parâmetros são **cópias**. `trocaValor` troca as cópias — a `main` não percebe.
- Passando o **endereço** (`&x`), a função mexe na variável original através do ponteiro (`*a`).
- Uma função pode "devolver" dois resultados usando parâmetros ponteiro (`minMax`).

## Como usar
1. Marque (F9 ou clique na margem) a linha com `PARE AQUI` e clique em **🐞 Depurar**.
2. Use **↓ Entrar** (F11) para entrar em `trocaValor`. Em **Observar**, adicione `&a` e compare com o `&x` da `main` (clique em `main` na pilha de chamadas): são endereços **diferentes**.
3. Continue até `trocaRef`. Agora observe `a` (um ponteiro: guarda o endereço de `x`) e `*a` (o valor de `x`).
4. Depois de **↑ Sair**, veja `x` e `y` trocados na `main`.
