# LDE

> Fonte: `base/Listas/LDE.pdf`

<!-- página 1 -->

LISTA

                                   void CriaLista(TLISTA *l)                int ListaVazia (TLISTA * l)
                                                      {                                                      {
                                                                                          l  prim = l  ult = NULL;              return (l->prim == NULL);      Percorre do Primeiro ao Último
DUPLAMENTE                                 }                                                      }
ENCADEADA                                                                                                     void PercursoPrimUlt (TLISTA *pl)
                                                                                                                                                              {
                                           int BuscaLDEOrd (TLISTADE  *l, tipo chv, TNODE **pno)               TNO *p;
                                                    {
                           TNO *p;                                                                                  for (p = pl->prim; p != NULL; p = p 
     Definição do tipo                                                    /* Se chv a lista, pno contém o endereço do nó que contém a chave         suc)
                              Se chv a lista, pno contém o endereço do nó que conteria a chave                 exibe (p  inf);
     typedef struct tno {                                                    */                                        }
        TINF inf;
           struct tno *ant, *suc;                                                  for (p = lprim; (p != NULL) && (p  inf.chv < chv); p = p  suc);
       } TNODE;                                                                                         Percorre do Último ao Primeiro                                               (*pno) = p;
     typedef struct {                                                                                               void PercursoTrasFrente (TLISTA *pl)                                            return ((p != NULL) && (p  inf.chv == chv));
      TNODE *prim, *ult;                                                                                                                       {                                                    }
       } TLISTA;                                                                    TNO *p;

                                                                                                                                             for (p = pl->ult; p != NULL; p = p  ant)
                                                                                                                      exibe (p  inf);
                                                             }

 int IncluiLDEOrd (TLISTA *l, TINF * inf)                                             int IncluiAntesDoNoComEndK (TLISTA *l, TINF* inf, TNO **k)
 {                                                                                                            {
   TNO *p, *pno;                                           TNO *p = CriaNode(inf);
         if (ListaVazia( l )) /* Se lista vazia p é criado e é o prim e o ult */                               If ( p == NULL)
       {                                                                                        return 0;
        p = CriaNo(inf);                                                  p  suc = k;
                   l  prim = l  ult = p;                                            p  ant = k  ant;
       }                                                                 p  suc  ant = p;
     else                  /* Se chave não  a lista */                                                                   if (k  ant != NULL)  /* Não é o primeiro ? */
       {                                                                     p  ant  suc = p;
                 if (infchv < l  prim  inf.chv)  /* Chave antes do primeiro */              else
             {                                                                 lprim = p;
           p = CriaNo(inf);                                                           return 1;
                         if ( p == NULL)                                                                        }
                   return 0;
           p  suc = l  prim;
           p  suc  ant = p;                                                     int Excluir (TLISTA *l, tipo chv, TINF *pinf)
                            l  prim = p;                 /* Nó é o primeiro */                               {
             }                                                 TNO *p, *pno;
         else
             {                                                                                                                                         if (ListaVazia(l))
                        /* Chave depois do último */                                               return 0;
                                if (infchv > l  ult  inf.chv)                                                                   if (! Busca (l, chv, &pno))
                        {                                                                          return –1;
                  p = CriaNo(inf);                                                                                        if (l  prim == l  ult)           /* Se chave  1o nó da lista */
                                        if ( p == NULL)                                                             CriaLista(l);              /* Exclusão do único nó da lista */
                            return 0;                                                  else
                  p  ant = l  ult;                                                              {
                  p  ant  suc = p;                                                                                       if (pno == l  prim)         /* Exclusão do primeiro nó */
                                             l  ult = p;        /* Nó é o último */                                             {
                        }                                                                                                                                                           l  prim = pno  suc;    /* Atualiza o primeiro nó */
                  else                        /* Chave no meio */                                                                         l  prim  ant = NULL;
                        {                                                                                                 }
                                        if (Busca( l, infchv, &pno))                                       else
                              {                                                                                           {
                            return -1;                                                                                                           if (pno == l  ult)      /* Exclusão do último nó */
                              }                                                                                                 {
                  p = CriaNo(inf);                                                                                                                             l  ult = pno  ant; /* Atualiza o último nó */
                                        if ( p == NULL)                                                                                                                              l  ult  suc = NULL;
                            return 0;                                                                                 }
                  p  ant = pno  ant;                                                  else            /* Exclusão do meio */
                  p  suc = pno;                                                                             {
                  p  ant  suc = p;                                              pno  ant  suc = pno  suc;
                  p  suc  ant = p;                                              pno  suc  ant = pno  ant;
                  }                                                                                                            }
             }                                                                                                            }
          return 1;                                                                                             }
 }                                                                                                    (*pinf) = pno  inf;
                                                                                          free(pno);
                                                                                         return 1;
                                                         }
