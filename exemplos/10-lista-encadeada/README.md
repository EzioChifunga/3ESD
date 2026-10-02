# Lista simplesmente encadeada (sem cabeça)

**Modo:** 🐞 Depurar

## O que este exemplo mostra
- Cada nó é alocado com `malloc` e aponta para o próximo (`prox`); a lista é só o ponteiro para o **primeiro** nó (`NULL` = vazia).
- Por que as funções **devolvem a lista atualizada** (`lst = lst_insere(lst, x)`): inserir ou remover o primeiro nó muda o início.
- O caso especial da remoção do primeiro nó (sem cabeça é preciso tratar à parte — a apostila do módulo 10 mostra como o nó cabeça elimina esse caso).
- Liberar a lista inteira guardando o próximo **antes** do `free`.

## Como usar
1. Marque `PARE AQUI` (F9) e **🐞 Depurar**.
2. Em **Observar**: `*lst`, `*lst->prox`, `lst->prox->prox->info`.
3. Entre (F11) em `lst_retira` e acompanhe `ant` e `p` andando pela lista.
