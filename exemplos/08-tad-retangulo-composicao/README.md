# TAD Retângulo usando o TAD Ponto (composição)

**Modo:** 🐞 Depurar

## O que este exemplo mostra
- **Composição de TADs**: `Retangulo` guarda dois `Ponto*` (canto inferior esquerdo e superior direito).
- O `retangulo.c` **inclui `ponto.h`** e usa só as funções do Ponto — ele também não enxerga `x` e `y`.
- Quem cria, libera: `ret_libera` libera os pontos que `ret_cria` criou.

## Como usar
1. Marque `PARE AQUI` (F9) e **🐞 Depurar**.
2. Use **↓ Entrar** (F11) em `ret_area` e depois em `pto_acessa`: a **pilha de chamadas** mostra `main → ret_area → pto_acessa`, passando por três arquivos.
