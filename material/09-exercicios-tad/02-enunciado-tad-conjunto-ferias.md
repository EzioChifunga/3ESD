# enunciado

> Fonte: `base/Lista de TAD/enunciado.txt`

Usando o TAD conjunto fornecido resolva o seguinte problema:
José Carlos e Marlene são os pais de Valéria. A família quer viajar nas férias de julho. José Carlos conseguiu tirar suas férias na fábrica do dia 2 ao dia 28. Marlene obteve licença no escritório de 5 a 30. As férias de Valéria na escola vão de 1 a 25. Durante quais dias a família poderá viajar sem faltar as suas obrigações?

link com o tad conjunto implementado:

https://onlinegdb.com/1KCiBCYQ9d

/* TAD conjunto*/
/* Elementos do conjunto são acessados através do endereço de um de seus elementos */
/* Tipo elemento de conjunto*/
typedef struct elem ElemConjunto;
/* Cria um conjunto de inteiros vazio */
ElemConjunto* conj_Cria(void);
/*Libera um conjunto de inteiros */
void  conj_Libera(ElemConjunto*A);
/* Inclui um novo nº no Conjunto caso ele ainda não pertenca ao conjunto */
ElemConjunto* conj_Insere(ElemConjunto *C, int x);
/* Verifica se um nº pertence ao Conjunto - 1 se pertence ou 0 caso contrário */
int conj_Ehmembro(ElemConjunto*A, int x);
/* Retira um nº do Conjunto caso ele  pertenca ao conjunto */
ElemConjunto* conj_Remove(int x, ElemConjunto *C);

/* Uniao de dois conjuntos retornando o conjunto Uniao */
ElemConjunto* conj_Uniao(ElemConjunto *A, ElemConjunto *B);

/* Intersecao de dois conjuntos retornando o conjunto Intersecao */
ElemConjunto* conj_Intersecao(ElemConjunto* A, ElemConjunto* B);

/* Diferencao entre dois conjuntos retornando o conjunto Diferenca */
ElemConjunto* conj_Diferenca(ElemConjunto*A, ElemConjunto*B);

/* Mostra na tela os elementos de um conjunto */
void conj_exibe(ElemConjunto*A);
/* Libera   um conjunto */
void conj_Libera (ElemConjunto*A);
