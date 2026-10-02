/* TAD: Conjunto de números inteiros: elementos do conjunto e seu tamanho n */
/* Tipo exportado */
typedef struct tconjunto TConjunto;
/* Funções exportadas */
/* Função cria um conjunto vazio
** 
*/
TConjunto* criaTC();
/* Função que le n dados de um conjunto e retorna o conjunto com 
os dados lidos*/
TConjunto* lerTC(TConjunto* c, int n);
/* Função que faz a união dos dados de um conjunto.
** Retorna o novo conjunto
*/
TConjunto* uniaoTC(TConjunto* c1, TConjunto* c2);
/* Função que faz a intersecção de dois conjuntos
** Retorna o novo conjunto.
*/
TConjunto* interecTC(TConjunto* c1, TConjunto* c2);
/* Função que verifica se dois conjuntos são iguais.
** Retorna 1 caso positivo e 0 caso contrário
*/
int eqTC(TConjunto* c1, TConjunto* c2);
/* Procedimento que imprime um conjunto 
*/
void printTC(TConjunto* c1);