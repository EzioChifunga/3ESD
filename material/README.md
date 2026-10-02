# Trilha de estudo — Estrutura de Dados em C (ESD)

Todo o material de `base/` convertido para Markdown e organizado na ordem de estudo, do início até a prova. O conteúdo **não foi alterado** — só convertido e reorganizado. Cada arquivo indica no topo de onde veio (`> Fonte: base/...`).

## Como estudar
1. Siga os módulos **na ordem**. Cada pasta tem um `README.md` com objetivo, ordem de leitura e o que você precisa dominar antes de avançar.
2. Método dos próprios materiais: **entender → desenhar a memória → só então codificar**. Não decore código; cubra a solução e tente prever cada linha.
3. Nos exercícios, escreva o código antes de olhar qualquer solução.

## Trilha

| # | Módulo | Conteúdo |
|---|---|---|
| **Parte 1 — Revisão de C** | | |
| 01 | [Funções e ponteiros](01-revisao-ponteiros-e-funcoes/README.md) | estrutura de programa, funções, `*` e `&` |
| 02 | [Vetores e matrizes](02-revisao-vetores-e-matrizes/README.md) | vetores, vetores paralelos, matrizes |
| 03 | [Strings](03-revisao-strings/README.md) | `char`, ASCII, cadeias, vetor de strings |
| 04 | [Alocação dinâmica](04-revisao-alocacao-dinamica/README.md) | `malloc`/`free`, vetor e matriz dinâmicos |
| 05 | [Tipos estruturados](05-tipos-estruturados/README.md) | `struct`, vetor de structs, vetor de ponteiros |
| 06 | [Busca e ordenação](06-busca-e-ordenacao/README.md) | busca linear/binária, ordenação |
| 07 | [Trabalho avaliativo](07-trabalho-avaliativo/README.md) | sistema de recomendação (integra 01–06) |
| **Parte 2 — Tipos Abstratos de Dados** | | |
| 08 | [TAD](08-tad-tipos-abstratos-de-dados/README.md) | interface `.h` × implementação `.c`, tipo opaco |
| 09 | [Exercícios de TAD](09-exercicios-tad/README.md) | Conjunto, Data, Horário, Instante, Matriz + código |
| **Parte 3 — Estruturas lineares** | | |
| 10 | [Listas lineares](10-listas-lineares/README.md) | sequencial, LSE, LDE, LDE circular com cabeça |
| 11 | [Pilha e fila](11-pilha-e-fila/README.md) | LIFO, FIFO, implementação sequencial |
| **Parte 4 — Avaliação** | | |
| 12 | [Prova AV1](12-prova-av1/README.md) | lista mista + simulado, com mapa questão × módulo |

## O que cai na AV1 (pelos simulados)
- **TAD:** projetar um TAD (`.h` + `.c`) e usar TAD pronto (Conjunto, Matriz) → módulos 08–09.
- **Pilha / fila / lista:** teoria (LIFO/FIFO), escolher a estrutura certa e manipular lista encadeada sem nó cabeça → módulos 10–11.

## Como esta pasta é gerada
```
python ferramentas/converter.py   # base/  -> convertido/ (PDF, DOCX, TXT, .c/.h -> .md)
python ferramentas/organizar.py   # convertido/ -> material/ (copia na ordem da trilha)
```
Os `README.md` dos módulos são escritos à mão e não são sobrescritos. Ao adicionar um arquivo novo em `base/`, rode os dois scripts e inclua-o no dicionário `TRILHA` de `ferramentas/organizar.py` (o script avisa o que ficou de fora).
