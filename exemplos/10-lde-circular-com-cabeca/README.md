# LDE circular com nó cabeça

**Modo:** 🐞 Depurar

## O que este exemplo mostra
A estrutura final da apostila do módulo 10:
- **Nó cabeça** (sentinela): sempre existe, não guarda dado. Lista vazia = a cabeça aponta para ela mesma.
- **Duplamente encadeada**: cada nó tem `ant` e `prox` — anda nos dois sentidos.
- **Circular**: o último aponta para a cabeça, e a cabeça para o último.
- Resultado: inserir no início, no meio, no fim ou em lista vazia é **o mesmo código**. Sem `if` especial.
- Busca com **sentinela**: coloca o valor procurado na cabeça para o laço não precisar testar o fim.

## Como usar
1. Marque `PARE AQUI` (F9) e **🐞 Depurar**.
2. Em **Observar**: `*l->cab`, `*l->cab->prox`, `l->cab->ant->info` (o último).
3. Entre (F11) em `lde_insere_ordenado` e veja as 4 atribuições ligando o novo nó.
