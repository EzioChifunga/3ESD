
/* Tipo exportado */
/* cria uma medida de comprimento como em m e cm*/
typedef struct alt Altura;

/* Funções exportadas */
/* Função cria - Aloca e retorna uma altura (m e cm) */
Altura* alt_cria(int m, int cm);

/* Função libera - Libera a memória de uma altura previamente criado */
void alt_libera(Altura* p);

/* Função acessa - Retorna os valores dde uma altura */
void alt_acessa(Altura* p, int* m, int* cm);

/* Função atribui - Atribui novos valores às partes de uma altura */
void alt_atribui(Altura* p, int m, int cm);

/* Função exibe -Escreve na tela oa altura  no formato  
/
void alt_exibe(Altura* p);

/* Função getAltur- Retorna uma string com os valores dA ALTURA  no formato m,cm*/
char* alt_getAltura(Altura* p);

/*função compara negativo:se o al1 < alt2, 0 se alt1 == alt2, positivo se o alt1 > alt2  */
int alt_compara(Altura *p1, Altura *p2);