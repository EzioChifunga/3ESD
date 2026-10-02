# TAD Ponto: interface × implementação

**Modo:** ▶ Executar

## O que este exemplo mostra
- **`ponto.h`** é o *contrato*: o nome do tipo e o que dá para fazer com ele.
- **`ponto.c`** é a *implementação*: só ele conhece os campos `x` e `y`.
- **`main.c`** é o *cliente*: usa só as funções do `.h`.
- Os três arquivos são compilados juntos automaticamente (todo `.c` do projeto entra na compilação).

## Experimente (e veja o erro!)
Na `main`, tente acessar o campo direto: `printf("%f", p1->x);`. O compilador recusa (*tipo incompleto*): é o **encapsulamento** funcionando. A dica do Estudo C explica o que fazer.

## Próximo passo
O exemplo *TAD Retângulo* mostra um TAD que usa outro TAD (composição).
