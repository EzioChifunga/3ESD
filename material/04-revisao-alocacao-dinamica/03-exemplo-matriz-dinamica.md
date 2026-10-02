# matrizDinAloc

> Fonte: `base/Revisão Alocação Dinâmica de Memória/matrizDinAloc.pdf`

<!-- página 1 -->

/* Faça um programa que pergunte ao usuário o número de linhas e colunas de uma matriz.
Aloque-a dinâmicamente, preecha-a com números aleatórios entre 0 e 20 e, a seguir, exiba-a.
Construa funções para: a) mostrar a soma dos elementos de uma linha, cujo número é recebido
b) mostrar a soma dos elementos de uma coluna, cujo número é recebido */ #include #include
int** criaMat2d(int nLinhas, int nColunas); void liberaMat2d(int** matriz, int nLinhas); void
imprimeMat2d(int** matriz, int nLinhas, int nColunas); int main(void) { int nLin; // número de
linhas da matriz int nCol; // número de colunas da matriz int** matriz; //Pergunta ao usuário a
quantidade de linhas e colunas printf("Quantas linhas? "); scanf("%d",&nLin); printf("Quantas
colunas? "); scanf("%d",&nCol); // cria a matriz nLin x nCol matriz = criaMat2d(nLin, nCol);
imprimeMat2d(matriz, nLin, nCol); // libera a matriz 4x5 liberaMat2d(matriz, nLin); return 0; } /* *
Função criaMat2d * Parâmetros: * - número de linhas * - número de colunas * Retorna: * -
sucesso: endereço matriz * - erro: aborta o programa */ int **criaMat2d(int nLinhas, int
nColunas) { int** pMat; int *pAux; // criando a matriz pMat = (int**)malloc(nLinhas * sizeof(int*));
if (!pMat){ exit(1); } for (int i = 0; i < nLinhas; i++) { *(pMat+i) = (int *)malloc(nColunas *
sizeof(int)); //pMat[i]=... if(!pMat[i]){ liberaMat2d(pMat,i); // libera linhas já alocadas exit(1); } } //
preenchendo a matriz for (int linha = 0; linha < nLinhas; linha++) { for (int coluna = 0; coluna <
nColunas; coluna++) { // usando o endereço da linha pAux = *(pMat+linha); *(pAux + coluna) =
rand()%21; // usando indexação // pMat[linha][coluna] = rand()%21; // *(pMat[linha] +coluna) =
rand()%21 } } return pMat; } /* * Função libera * Parâmetros * - endereço da matriz * - números
de linhas * Retorna: * - nada */ void liberaMat2d(int** matriz, int nLinhas) { for (int linha = 0; linha
< nLinhas; linha++) { free(*(matriz + linha)); //free(matriz[linha]); } free(matriz); } /* * Função
imprime * Parâmetros: * - a matriz * - número de linhas * - número de colunas * Retorna: * -
nada */ void imprimeMat2d(int** matriz, int nLinhas, int nColunas) { for (int linha = 0; linha <
nLinhas; linha++) { for (int coluna = 0; coluna < nColunas; coluna++) { printf("%2d ", matriz[linha]
[coluna]); } printf("\n"); //putchar('\n'); } }
