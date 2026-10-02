# Vetor de ponteiros para structs

**Modo:** 🐞 Depurar

## O que este exemplo mostra
- `tPessoa *v[MAX]`: o vetor guarda **endereços**; cada pessoa é criada com `malloc` quando necessário.
- Acesso com `v[i]->campo` (o mesmo que `(*v[i]).campo`).
- Para **ordenar**, basta trocar os ponteiros — as structs não saem do lugar na memória.
- Cada `malloc` precisa do seu `free`.

## Como usar
1. Marque `PARE AQUI` (F9) e **🐞 Depurar**.
2. Em **Observar**: `v[0]` (um endereço), `*v[0]` (a struct inteira) e `v[0]->nome`.
3. Avance com F10 até depois da ordenação e observe de novo: `v[0]` agora tem **outro endereço** (o ponteiro foi trocado).
