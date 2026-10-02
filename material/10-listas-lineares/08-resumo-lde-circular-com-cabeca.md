# ldeccab

> Fonte: `base/Listas/ldeccab.pdf`

<!-- página 1 -->

Duplamente Encadeada Circular com Cabeça

Int cria_lista (Tlista * pL)
{
     TNODE * p = (TNODE *) malloc (sizeof (TNODE));
             If (p!=NULL)
           {
            psuc = pant = p;
            pLprim = p;
                 return 1;
        return 0;
}

Inclui Lista Duplamente Encadeada Circular Cabeça Ordenada
Int INCLUI LDEOCCab ( constTlista *p L, Tinf inf)
{
     TNODE * p, * pno;
             If (busca (pL, inf.chv, &pno))
                 return –1;
      p = cria_no (inf);
             If (p ==NULL)
              Return 0;
      pprox = pno;
      pant = pnoant;
      pnoantprox = p;
      pnoant = p;
        return 1;
}

int EXCLUI (const Tlista * pL, tipo chv, Tinf * inf)
{     TNODE * p, * pno;
              if   if (lista_vazia (L))
                 return 0;
              if (!busca (*pL, chv, &Pno)
                 return –1;
          (*inf) – pnoinf;
      pnoantprox = pnoprox;
      pnoproxant = pnoant;

        free (pno)
        return 1;
}

int Lista_Vazia (const Tlista *p L)
{
        return (pL.prim == pL.primsuc)
}

Int BUSCA_SENTINELA_ou_Rapida ( const Tlista *pL, tipo chv, TNODE ** pno)
{
     TNODE *p;
       pL.priminf.chv = chv; /* Coloca chv na cabeça */
         for (p = pL.primsuc; pinf.chv < chv; p = psuc);
        (*pno) = p;
              if ((p!=pL.prim) && (pinf.chv == chv))
                 return 1;
        return 0;
}
