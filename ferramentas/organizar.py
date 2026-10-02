"""
Monta a pasta `material/` (trilha de estudo) a partir dos arquivos convertidos.

Copia cada .md de `convertido/` para o módulo correspondente em `material/`,
com prefixo numérico indicando a ordem de leitura. Os códigos .c/.h originais
de `base/` são copiados para `codigo/` dentro do módulo. O conteúdo NÃO é
alterado — só organizado. Os README.md de cada módulo são escritos à mão e
nunca são sobrescritos por este script.

Uso (rode o converter antes):
    python ferramentas/converter.py
    python ferramentas/organizar.py
"""

import shutil
import sys
from pathlib import Path

RAIZ = Path(__file__).resolve().parent.parent
BASE = RAIZ / "base"
CONVERTIDO = RAIZ / "convertido"
MATERIAL = RAIZ / "material"

# módulo -> lista de (arquivo em convertido/ ou base/, nome no destino)
TRILHA = {
    "01-revisao-ponteiros-e-funcoes": [
        ("Revisão de Ponteiros/RevisãoCompactaFuncoesPonteiro.pdf.md", "01-revisao-funcoes-e-ponteiros.md"),
    ],
    "02-revisao-vetores-e-matrizes": [
        ("Revisão Vetores e Matrizes/RevisãoCompactaVetorMatriz.pdf.md", "01-revisao-vetores-e-matrizes.md"),
    ],
    "03-revisao-strings": [
        ("Revisão Strings/RevisãoStrings.pdf.md", "01-revisao-strings.md"),
    ],
    "04-revisao-alocacao-dinamica": [
        ("Revisão Alocação Dinâmica de Memória/RevisãoAlocaçãodeMemória.pdf.md", "01-revisao-alocacao-de-memoria.md"),
        ("Revisão Alocação Dinâmica de Memória/AlocaçãoDinamicadeMemóriaVF.pdf.md", "02-alocacao-dinamica-vetores-e-matrizes.md"),
        ("Revisão Alocação Dinâmica de Memória/matrizDinAloc.pdf.md", "03-exemplo-matriz-dinamica.md"),
    ],
    "05-tipos-estruturados": [
        ("Revisão Tipos Estruturados/TiposEstruturadosIntroducao_Aula1.pdf.md", "01-tipos-estruturados-introducao.md"),
        ("Revisão Tipos Estruturados/RevisãoStructs.pdf.md", "02-revisao-structs.md"),
        ("Revisão Tipos Estruturados/VetorDeTiposEstruturadosVFod.pdf.md", "03-vetor-de-structs.md"),
        ("Revisão Tipos Estruturados/VetorDePonteirosParaTiposEstruturadosVFod.pdf.md", "04-vetor-de-ponteiros-para-structs.md"),
    ],
    "06-busca-e-ordenacao": [
        ("MetodosBusca.pdf.md", "01-metodos-de-busca.md"),
        ("Introdução TAD/Ordenacao.pdf.md", "02-ordenacao.md"),
    ],
    "07-trabalho-avaliativo": [
        ("material/Trabalho_Avaliativo_Sistema_de_Recomendacao_por_Afinidade_EXATO.docx.md", "01-enunciado-sistema-de-recomendacao.md"),
        ("material/Trabalho Avaliativo — Sistema de Recomendação por Afinidade.txt.md", "02-regras-de-entrega.md"),
    ],
    "08-tad-tipos-abstratos-de-dados": [
        ("Introdução TAD/TAD_FAETERJ.pdf.md", "01-tad-slides-aula.md"),
        ("Introdução TAD/Apostila_TAD_C_Revisada_Comentarios_e_Projeto.pdf.md", "02-apostila-tad.md"),
        ("Introdução TAD/ExercícioTAD262.pdf.md", "03-exercicio-contas-com-datas.md"),
    ],
    "09-exercicios-tad": [
        ("Lista de TAD/ListaExerciciosTAD.pdf.md", "01-lista-de-exercicios-tad.md"),
        ("Lista de TAD/enunciado.txt.md", "02-enunciado-tad-conjunto-ferias.md"),
        ("Lista de TAD/Exercicios_TAD_C_Lista_Didatica_V3 (1).pdf.md", "03-lista-didatica-v3.md"),
        ("Lista de TAD/aplicacaoTADInstante.pdf.md", "04-aplicacao-tad-instante-agenda.md"),
        ("base:Lista de TAD/conj.h", "codigo/conj.h"),
        ("base:Lista de TAD/conj.c", "codigo/conj.c"),
        ("base:Lista de TAD/data.h", "codigo/data.h"),
        ("base:Lista de TAD/data.c", "codigo/data.c"),
        ("base:Lista de TAD/horario.h", "codigo/horario.h"),
        ("base:Lista de TAD/horario.c", "codigo/horario.c"),
        ("base:Lista de TAD/instante.h", "codigo/instante.h"),
        ("base:Lista de TAD/instante.c", "codigo/instante.c"),
        ("base:Lista de TAD/matriz1.h", "codigo/matriz1.h"),
        ("base:Lista de TAD/matriz1.c", "codigo/matriz1.c"),
        ("base:Lista de TAD/TAD_Altura.h", "codigo/TAD_Altura.h"),
        ("base:Lista de TAD/TAD_Altura.c", "codigo/TAD_Altura.c"),
    ],
    "10-listas-lineares": [
        ("Listas/texto.txt.md", "00-roteiro-dos-topicos.md"),
        ("Listas/1ListasLineares_OqueSao.pdf.md", "01-listas-lineares-o-que-sao.md"),
        ("Listas/2ListasLineares_OrdSemRepetição.pdf.md", "02-lista-sequencial-ordenada-sem-repeticao.md"),
        ("Listas/3ListasSequenciaisLineares (1).pdf.md", "03-tad-lista-sequencial.md"),
        ("Listas/ListaSimplesmenteEncadeadaIntrod.pdf.md", "04-lista-simplesmente-encadeada.md"),
        ("Listas/ExercFIxacaolista encadeada.pdf.md", "05-exercicio-fixacao-lista-encadeada.md"),
        ("Listas/EstruturasListaDuplaEncadeadaIntroducao.pdf.md", "06-lista-duplamente-encadeada.md"),
        ("Listas/LDE.pdf.md", "07-resumo-lde.md"),
        ("Listas/ldeccab.pdf.md", "08-resumo-lde-circular-com-cabeca.md"),
        ("APOSTILA_LISTAS_LINEARES_Ccl.pdf.md", "09-apostila-listas-lineares.md"),
    ],
    "11-pilha-e-fila": [
        ("Pilha e Fila/PilhaSequencial.pdf.md", "01-pilha-sequencial.md"),
        ("Pilha e Fila/FIlaSequencial.pdf.md", "02-fila-sequencial.md"),
        ("Pilha e Fila/Lista de exercíciosPilhaSIM.pdf.md", "03-exercicios-pilha.md"),
        ("Pilha e Fila/ListaAplicacaoPilhaOuFila.pdf.md", "04-exercicios-lista-pilha-e-fila.md"),
    ],
    "12-prova-av1": [
        ("Prova/Lista MIsta _ Materia Av1.pdf.md", "01-lista-mista-materia-av1.md"),
        # SimuladoAV1_ESD.docx.pdf tem o mesmo texto deste arquivo, por isso não é copiado
        ("Prova/SimuladoAV1_ESD.pdf.md", "02-simulado-av1.md"),
    ],
}


def main() -> int:
    faltando = 0
    for modulo, itens in TRILHA.items():
        for origem, destino in itens:
            if origem.startswith("base:"):
                fonte = BASE / origem[len("base:"):]
            else:
                fonte = CONVERTIDO / origem
            if not fonte.exists():
                print(f"[faltando] {fonte.relative_to(RAIZ)}")
                faltando += 1
                continue
            alvo = MATERIAL / modulo / destino
            alvo.parent.mkdir(parents=True, exist_ok=True)
            shutil.copy2(fonte, alvo)
            print(f"[ok] {alvo.relative_to(RAIZ)}")

    # avisa sobre arquivos de base/ que ainda não estão na trilha
    mapeados = {o[len("base:"):] if o.startswith("base:") else o for itens in TRILHA.values() for o, _ in itens}
    mapeados.add("Prova/SimuladoAV1_ESD.docx.pdf.md")
    for md in sorted(CONVERTIDO.rglob("*.md")):
        rel = md.relative_to(CONVERTIDO).as_posix()
        if rel not in mapeados and rel[:-3] not in mapeados:
            print(f"[fora da trilha] {rel} — adicione em TRILHA")

    return 1 if faltando else 0


if __name__ == "__main__":
    sys.exit(main())
