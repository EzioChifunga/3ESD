# Vetor de structs

**Modo:** ▶ Executar

## O que este exemplo mostra
- `typedef struct` para criar o tipo `tPessoa`.
- Um **vetor de structs** alocado com `malloc(n * sizeof(tPessoa))`: todas as pessoas ficam lado a lado na memória.
- Acesso com `v[i].campo`, e passagem do vetor para funções.
- Uma função que devolve o **índice** do elemento encontrado (ou -1).

## Como usar
**▶ Executar**: informe quantas pessoas, depois `nome idade sexo` de cada uma (ex.: `Ana 21 F`) e por fim um nome para buscar.

## Compare com
O exemplo *Vetor de ponteiros para structs* resolve o mesmo problema guardando só **endereços** no vetor.
