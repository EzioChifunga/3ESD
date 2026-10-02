#ifndef MATRIZ_H
#define MATRIZ_H

#include <stddef.h> // size_t

// Tipo opaco
typedef struct matriz Matriz;

// Cria e libera
Matriz* criaMatriz(int linhas, int colunas);
void liberaMatriz(Matriz* m);

// Dimensões
int linhasMatriz(const Matriz* m);
int colunasMatriz(const Matriz* m);

// Atribui e acessa
void atribuiMatriz(Matriz* m, int linha, int coluna, int valor);

// Versão simples: retorna 0 se fora do intervalo
int acessaMatriz(const Matriz* m, int linha, int coluna);

// Versão segura: retorna 1 (ok) ou 0 (falha); valor sai em *out
int acessaMatrizSafe(const Matriz* m, int linha, int coluna, int* out);

// Impressão
void imprimeMatriz(const Matriz* m);

// --------- Operações pedidas ---------

// Soma de uma linha específica (0-based). Retorna 1 (ok) ou 0 (falha)
int somaLinhaMatriz(const Matriz* m, int linha, long long* soma_out);

// Soma de uma coluna específica (0-based). Retorna 1 (ok) ou 0 (falha)
int somaColunaMatriz(const Matriz* m, int coluna, long long* soma_out);

// Soma de matrizes (resultado = A + B). Retorna nova matriz ou NULL se dimensões incompatíveis.
Matriz* somaMatrizes(const Matriz* A, const Matriz* B);

#endif
