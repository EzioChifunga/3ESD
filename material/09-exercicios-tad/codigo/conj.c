#include <stdlib.h> /* malloc, free, exit */
#include <stdio.h> /* printf */
#include "conj.h"

struct  tconjunto{
  int conj[20];
  int size;
};


TConjunto* criaTC(){
   printf("\n Criando um conjunto");  
   TConjunto *c = (TConjunto*) malloc(sizeof(TConjunto));
   c->size = 0;
   return c;
}


TConjunto* lerTC(TConjunto* c, int n){
   int i;
   c->size=n;
   printf("\n Lendo um conjunto de %d Elementos",n);  
   for(i=0;i<n;i++){
      printf("\n Digite um Elemento: ");
      scanf("%d",&c->conj[i]);
   }
   return c;
}



void printTC(TConjunto* c1){
   int i;
   printf("\n Imprimindo  um conjunto de %d elementos!", c1->size);  
   for(i=0; i < c1->size; i++){
     printf("\n Elemento %d=%d\n",i+1,c1->conj[i]);
   }
 
}
