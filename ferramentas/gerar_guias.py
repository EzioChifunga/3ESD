"""
Gera os README.md (guias) dos módulos de material/ com estrutura padronizada:
objetivo, ordem de leitura, exemplos práticos (lidos de exemplos/*/exemplo.json),
o que saber antes de avançar e navegação.

Uso: python ferramentas/gerar_guias.py
"""
import json, os

RAIZ = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
MAT = os.path.join(RAIZ, "material")
EX = os.path.join(RAIZ, "exemplos")

MODOS = {"Executar": "▶ Executar", "Depurar": "🐞 Depurar", "CorrigirErros": "🩹 Corrigir erros"}

# carrega exemplos por módulo
exemplos = {}
for pasta in sorted(os.listdir(EX)):
    j = os.path.join(EX, pasta, "exemplo.json")
    if os.path.exists(j):
        d = json.load(open(j, encoding="utf-8"))
        exemplos.setdefault(d["modulo"], []).append((pasta, d))

M = [
    dict(n="01", pasta="01-revisao-ponteiros-e-funcoes", titulo="Revisão: funções e ponteiros",
         objetivo="relembrar a estrutura de um programa C, funções, passagem de parâmetros e ponteiros — a base de tudo que vem depois (TAD e listas encadeadas usam ponteiros o tempo todo).",
         leitura=[("01-revisao-funcoes-e-ponteiros.md", "Revisão compacta de funções e ponteiros",
                   "estrutura de um programa, funções, endereços, `*` e `&`, passagem por referência.")],
         saber=["Diferenciar **passagem por valor** e **por referência**.",
                "Ler e escrever `int *p = &x; *p = 10;` sem hesitar.",
                "Explicar o que `&x` e `*p` significam em cada linha."]),
    dict(n="02", pasta="02-revisao-vetores-e-matrizes", titulo="Revisão: vetores e matrizes",
         objetivo="armazenar coleções de dados (vetores, vetores paralelos, matrizes) e passá-los para funções.",
         leitura=[("01-revisao-vetores-e-matrizes.md", "Revisão compacta de vetores e matrizes",
                   "problema motivador (notas de alunos), vetores, vetores paralelos, matrizes e funções.")],
         saber=["Passar **vetor** e **matriz** como parâmetro (`int v[]`, `int m[][COL]`).",
                "Usar **vetores paralelos** — base da *Etapa 1* do trabalho avaliativo.",
                "Percorrer uma matriz por linha e por coluna."]),
    dict(n="03", pasta="03-revisao-strings", titulo="Revisão: cadeias de caracteres (strings)",
         objetivo="entender o tipo `char`, a tabela ASCII, leitura e manipulação de strings, funções recursivas sobre strings e vetor de strings.",
         leitura=[("01-revisao-strings.md", "Revisão de strings", "caracteres, cadeias, leitura, funções da `string.h`, recursão e vetor de strings.")],
         saber=["Por que toda string termina em `'\\0'`.",
                "Usar `strcpy`, `strcmp`, `strlen` — e escrever suas próprias versões.",
                "Ler uma linha inteira com `fgets` e remover o `'\\n'` do final."]),
    dict(n="04", pasta="04-revisao-alocacao-dinamica", titulo="Revisão: alocação dinâmica de memória",
         objetivo="usar `malloc`/`free`, entender o ciclo de vida da memória (global, local, heap) e alocar vetores e matrizes em tempo de execução.",
         leitura=[("01-revisao-alocacao-de-memoria.md", "Revisão de alocação de memória", "uso da memória, vetores locais e funções."),
                  ("02-alocacao-dinamica-vetores-e-matrizes.md", "Alocação dinâmica: vetores e matrizes", "versão completa: inclui vetor e matriz alocados dinamicamente."),
                  ("03-exemplo-matriz-dinamica.md", "Exemplo: matriz dinâmica", "programa completo (`criaMat2d`, `liberaMat2d`, soma de linha/coluna).")],
         nota="O arquivo 2 repete o conteúdo do 1 e acrescenta vetores e matrizes dinâmicas.",
         saber=["Sempre testar se `malloc` devolveu `NULL`.",
                "Alocar e liberar uma matriz `int**` linha por linha — usado na *Etapa 6* do trabalho e no TAD Matriz.",
                "Por que uma função pode devolver memória do `malloc`, mas **não** um vetor local."]),
    dict(n="05", pasta="05-tipos-estruturados", titulo="Tipos estruturados (struct)",
         objetivo="agrupar dados heterogêneos com `struct`/`typedef`, usar ponteiro para struct (`->`), vetores de structs e vetores de ponteiros para structs.",
         leitura=[("01-tipos-estruturados-introducao.md", "Tipos estruturados — introdução (aula 1)", "o que é uma estrutura, declaração e acesso aos campos."),
                  ("02-revisao-structs.md", "Revisão de structs", "structs aninhadas, funções e ponteiros para struct."),
                  ("03-vetor-de-structs.md", "Vetor de tipos estruturados", "vetor de structs estático e com `malloc`."),
                  ("04-vetor-de-ponteiros-para-structs.md", "Vetor de ponteiros para tipos estruturados", "cada elemento alocado separadamente.")],
         saber=["`p->campo` é o mesmo que `(*p).campo`.",
                "A diferença entre `tPessoa v[n]` e `tPessoa *v[n]`.",
                "Esse é o modelo das *Etapas 7, 8 e 9* do trabalho e a porta de entrada para TAD."]),
    dict(n="06", pasta="06-busca-e-ordenacao", titulo="Busca e ordenação em vetores",
         objetivo="busca linear e binária; conceitos e algoritmos de ordenação interna.",
         leitura=[("01-metodos-de-busca.md", "Métodos de busca", "busca linear, linear em vetor ordenado e busca binária."),
                  ("02-ordenacao.md", "Ordenação", "definição, ordenação interna/externa, estabilidade e algoritmos.")],
         nota="Em `base/` o PDF de ordenação está na pasta *Introdução TAD*; aqui ele foi agrupado com busca por ser o mesmo tema.",
         saber=["Quando a **busca binária** pode ser usada (vetor ordenado) e por que é mais rápida.",
                "A ideia de cada ordenação: bolha, seleção e inserção.",
                "Busca aparece na *Etapa 1* do trabalho e em toda lista ordenada (módulo 10)."]),
    dict(n="07", pasta="07-trabalho-avaliativo", titulo="Trabalho avaliativo: Sistema de Recomendação por Afinidade",
         objetivo="aplicar de forma integrada os módulos 01–06. O mesmo problema é resolvido em várias versões; muda só a organização dos dados na memória.",
         leitura=[("01-enunciado-sistema-de-recomendacao.md", "Enunciado completo", "contexto, etapas, requisitos, entregas e critérios."),
                  ("02-regras-de-entrega.md", "Regras de entrega", "nome dos arquivos e cabeçalho com os membros do grupo.")],
         extra="""## Etapas × módulo de revisão

| Etapa do trabalho | Conteúdo | Revisar |
|---|---|---|
| 1 — Matriz e estruturas paralelas | cadastro, exibição, busca | [02](../02-revisao-vetores-e-matrizes/README.md), [06](../06-busca-e-ordenacao/README.md) |
| 2 — Distância entre duas pessoas | distância euclidiana, funções | [01](../01-revisao-ponteiros-e-funcoes/README.md) |
| 3 — Pessoa mais semelhante | percurso / menor distância | [02](../02-revisao-vetores-e-matrizes/README.md) |
| 4 — Ranking de afinidade | ordenação | [06](../06-busca-e-ordenacao/README.md) |
| 5 — Análise das preferências | percursos na matriz | [02](../02-revisao-vetores-e-matrizes/README.md) |
| 6 — Alocação dinâmica da matriz | `malloc` de matriz | [04](../04-revisao-alocacao-dinamica/README.md) |
| 7 — `struct Pessoa` | struct | [05](../05-tipos-estruturados/README.md) |
| 8 — Vetor dinâmico de estruturas | vetor de struct com `malloc` | [05](../05-tipos-estruturados/README.md) |
| 9 — Vetor de ponteiros para `struct` | vetor de ponteiros | [05](../05-tipos-estruturados/README.md) |
""",
         saber=["Ler o enunciado inteiro antes de começar: cada etapa reaproveita a anterior.",
                "Seguir à risca o nome dos arquivos e o cabeçalho pedidos nas regras de entrega."]),
    dict(n="08", pasta="08-tad-tipos-abstratos-de-dados", titulo="Tipos Abstratos de Dados (TAD)",
         objetivo="separar interface (`.h`) de implementação (`.c`), usar tipo opaco, encapsulamento e invariantes; criar, usar e liberar TADs; compor um TAD dentro de outro.",
         leitura=[("01-tad-slides-aula.md", "Slides da aula — TAD", "tipo de dado × estrutura de dado × TAD, interface/API, exemplos."),
                  ("02-apostila-tad.md", "Apostila de TAD em C", "material de referência completo (leia os capítulos 1 a 4 antes da aula)."),
                  ("03-exercicio-contas-com-datas.md", "Exercício: contas a pagar com datas", "struct com structs internas (`DataSimples`); prepara o uso do TAD Data.")],
         extra="""## Método da apostila

> **Não decore código.** Desenhe o que acontece na memória, cubra a solução e tente prever cada linha antes de comparar. Use os *checkpoints* como teste de domínio.

Roteiro da apostila: problema → conceito → `.h`/`.c` → tipo opaco → TAD Ponto → memória → composição → erros → projetar um TAD novo.
""",
         saber=["Escrever do zero um TAD com **cria**, **libera**, acesso e operações.",
                "Explicar por que o cliente não pode acessar os campos de um tipo opaco.",
                "Montar um projeto com `main.c`, `tad.h` e `tad.c`."]),
    dict(n="09", pasta="09-exercicios-tad", titulo="Exercícios de TAD",
         objetivo="praticar o uso e a construção de TADs com os TADs-base fornecidos (Conjunto, Data, Horário, Instante, Matriz, Altura).",
         leitura=[("01-lista-de-exercicios-tad.md", "Lista de exercícios de TAD", "começa pelo problema das férias com o TAD Conjunto."),
                  ("02-enunciado-tad-conjunto-ferias.md", "Enunciado: TAD Conjunto (férias da família)", "**caiu no simulado da AV1 (Questão 2)**."),
                  ("03-lista-didatica-v3.md", "Lista didática V3", "contratos dos TADs-base + 4 módulos de exercícios (simples, compostos, aplicações, Conjunto e Matriz)."),
                  ("04-aplicacao-tad-instante-agenda.md", "Aplicação do TAD Instante", "validador de conflito de agenda.")],
         extra="""## Código fornecido

Cópias dos arquivos originais de `base/Lista de TAD/`. Abra um deles na trilha e use **▶ Abrir como projeto**: o Estudo C copia o TAD e tudo que ele inclui (ex.: `instante.h` traz `data` e `horario`).

| TAD | Interface | Implementação |
|---|---|---|
| Conjunto | [conj.h](codigo/conj.h) | [conj.c](codigo/conj.c) |
| Data | [data.h](codigo/data.h) | [data.c](codigo/data.c) |
| Horário | [horario.h](codigo/horario.h) | [horario.c](codigo/horario.c) |
| Instante (Data + Horário) | [instante.h](codigo/instante.h) | [instante.c](codigo/instante.c) |
| Matriz | [matriz1.h](codigo/matriz1.h) | [matriz1.c](codigo/matriz1.c) |
| Altura | [TAD_Altura.h](codigo/TAD_Altura.h) | [TAD_Altura.c](codigo/TAD_Altura.c) |

> **Prova:** um TAD Matriz aparece no simulado (Questão 3 da segunda prova).
""",
         exemplos_de="08",
         saber=["Usar um TAD **só pela interface** (`.h`), sem olhar o `.c`.",
                "Compor TADs (ex.: Instante = Data + Horário).",
                "Resolver um problema com o TAD Conjunto (união, interseção, diferença)."]),
    dict(n="10", pasta="10-listas-lineares", titulo="Listas lineares",
         objetivo="lista sequencial (vetor) e listas encadeadas — simples (LSE), duplamente encadeada (LDE) e LDE circular com nó cabeça — com inclusão, exclusão e busca em todos os casos.",
         leitura=[("00-roteiro-dos-topicos.md", "Roteiro dos tópicos", "a ordem dos assuntos definida pelo professor."),
                  ("01-listas-lineares-o-que-sao.md", "Listas lineares — o que são", "definição, propriedades e operações."),
                  ("02-lista-sequencial-ordenada-sem-repeticao.md", "Lista sequencial ordenada sem repetição", "o “kit lista”: vetor + contador."),
                  ("03-tad-lista-sequencial.md", "TAD lista sequencial", "classificada/não classificada, com/sem repetição."),
                  ("04-lista-simplesmente-encadeada.md", "Lista simplesmente encadeada — introdução", "nós, `malloc`, encadeamento."),
                  ("05-exercicio-fixacao-lista-encadeada.md", "Exercício de fixação", "lista encadeada de alunos."),
                  ("06-lista-duplamente-encadeada.md", "Lista duplamente encadeada — introdução", "ponteiros `ant` e `suc`."),
                  ("07-resumo-lde.md", "Resumo: LDE (folha de código)", "funções principais da LDE."),
                  ("08-resumo-lde-circular-com-cabeca.md", "Resumo: LDE circular com cabeça (folha de código)", "funções principais com nó cabeça."),
                  ("09-apostila-listas-lineares.md", "Apostila: Listas Lineares em C", "referência completa, cobre tudo acima.")],
         nota="Os arquivos 7 e 8 eram folhas de uma página em colunas; o texto saiu com as colunas misturadas. Para eles, prefira o PDF original em `base/Listas/` (`LDE.pdf` e `ldeccab.pdf`).",
         extra="""## Método da apostila (capítulos 3–13)

Cada operação é estudada sempre na mesma ordem:

1. O que queremos fazer?
2. Como a lista está desenhada?
3. Qual é o caso normal?
4. Qual caso quebra a regra?
5. Qual problema aparece?
6. Que ideia resolve o problema?
7. Simulação passo a passo (desenho antes/depois)
8. **Só então:** código.

## Mapa da apostila

| Capítulos | Tema |
|---|---|
| 1–2 | O que é lista linear; o TAD Lista com várias representações |
| 3–5 | Lista sequencial: representação, inclusão, exclusão |
| 6–9 | LSE sem cabeça (problemas) → LSE com cabeça e sentinela: inclusão e exclusão |
| 10–13 | LDE sem cabeça (problemas) → LDE circular com cabeça: inclusão e exclusão |
| 14–15 | Organizar cada projeto TAD; quando usar cada implementação |
| 16–19 | Atlas de erros, checkpoints, exercícios graduados, mapa mental |
""",
         saber=["Inserir e remover em lista **sequencial** (abrir e fechar espaço no vetor).",
                "Tratar os casos especiais de uma LSE **sem cabeça** (inserir/remover o primeiro).",
                "Explicar por que o **nó cabeça** elimina esses casos especiais.",
                "Percorrer uma LDE nos dois sentidos."]),
    dict(n="11", pasta="11-pilha-e-fila", titulo="Pilha e fila",
         objetivo="pilha (LIFO) e fila (FIFO) como casos particulares de lista; implementação sequencial e escolha da estrutura adequada para cada problema.",
         leitura=[("01-pilha-sequencial.md", "Pilha sequencial", "definição, quando usar e operações."),
                  ("02-fila-sequencial.md", "Fila sequencial", "fila linear e suas operações."),
                  ("03-exercicios-pilha.md", "Exercícios: pilha", "reconciliação de cheques e outros."),
                  ("04-exercicios-lista-pilha-e-fila.md", "Exercícios: lista, pilha e fila sequencial", "cadastros e aplicações.")],
         saber=["**LIFO** × **FIFO** e exemplos do dia a dia de cada um.",
                "Identificar pelo enunciado se o problema pede pilha, fila ou lista (caiu no simulado: Questão 1 e “Biblioteca Digital”).",
                "Implementar fila **circular** com `% MAX`."]),
    dict(n="12", pasta="12-prova-av1", titulo="Preparação para a AV1",
         objetivo="treinar no formato da prova. Faça **depois** dos módulos 08–11.",
         leitura=[("01-lista-mista-materia-av1.md", "Lista mista — matéria da AV1", "20 questões: “Tópico TAD” e “Tópico Pilha ou Fila ou Lista”."),
                  ("02-simulado-av1.md", "Simulado AV1", "contém duas provas (a segunda começa na página 10).")],
         nota="`base/Prova/SimuladoAV1_ESD.docx.pdf` tem o mesmo texto de `SimuladoAV1_ESD.pdf`, por isso só um foi incluído.",
         extra="""## Questões × módulo

### Lista mista

| Questões | Tema | Módulo |
|---|---|---|
| 1–6 (Produto, Fração, DVD, funcionários, Quadrado, clínica) | Projetar/usar TAD | [08](../08-tad-tipos-abstratos-de-dados/README.md), [09](../09-exercicios-tad/README.md) |
| 7–20 (grandes números, `fila.h`, Livro, revistas, 2ª chamada, lista linear, montadora, DNA/ENADE, mala direta, curso, Evento…) | Pilha, fila ou lista | [10](../10-listas-lineares/README.md), [11](../11-pilha-e-fila/README.md) |

### Simulado — prova 1

| Questão | Tema | Módulo |
|---|---|---|
| 1 (3,0) | Teoria: pilha, fila, lista | [10](../10-listas-lineares/README.md), [11](../11-pilha-e-fila/README.md) |
| 2 (3,0) | TAD Conjunto — férias da família | [09](../09-exercicios-tad/README.md) |
| 3 (3,0) | Estruturas encadeadas com `Livro` (mesma da lista mista, q. 9) | [10](../10-listas-lineares/README.md) |

### Simulado — prova 2 (a partir da página 10)

| Questão | Tema | Módulo |
|---|---|---|
| 2 (2,0) | Biblioteca digital: escolher pilha/fila/lista | [11](../11-pilha-e-fila/README.md) |
| 3 (2,0) | TAD Matriz (`Tad_Matriz.h`) | [09](../09-exercicios-tad/README.md) |
| 4 (3,0) | TAD lista simplesmente encadeada sem nó cabeça | [10](../10-listas-lineares/README.md) |
""",
         saber=["Resolver as questões **sem consultar** e só depois conferir no material.",
                "Treinar a escrita do código à mão (a prova é no papel)."]),
]


def exemplos_md(mod):
    lista = exemplos.get(mod, [])
    if not lista:
        return ""
    linhas = ["## Exemplos práticos", "",
              "Clique para abrir o guia do exemplo; de lá, use **🧪 Abrir na sandbox** ou **📁 Abrir como projeto**.", ""]
    for pasta, d in lista:
        linhas.append(f"- [{d['titulo']}](../../exemplos/{pasta}/README.md) — {MODOS[d['modo']]}. {d['descricao']}")
    return "\n".join(linhas) + "\n"


for i, m in enumerate(M):
    partes = [f"# Módulo {m['n']} — {m['titulo']}", "",
              f"> **Objetivo:** {m['objetivo']}", "",
              "## Ordem de leitura", ""]
    for j, (arq, tit, desc) in enumerate(m["leitura"], start=1 if not m["leitura"][0][0].startswith("00") else 0):
        partes.append(f"{j}. [{tit}]({arq}) — {desc}")
    partes.append("")
    if m.get("nota"):
        partes += [f"> **Observação:** {m['nota']}", ""]
    if m.get("extra"):
        partes.append(m["extra"].rstrip() + "\n")
    ex = exemplos_md(m.get("exemplos_de", m["n"]))
    if ex:
        if m.get("exemplos_de"):
            ex = ex.replace("## Exemplos práticos", "## Exemplos práticos (TADs do módulo 08)")
        partes.append(ex)
    partes += ["## Antes de avançar, você deve saber", ""]
    partes += [f"- {s}" for s in m["saber"]]
    partes.append("")
    nav = []
    if i > 0:
        p = M[i - 1]
        nav.append(f"⬅️ [{p['n']} — {p['titulo']}](../{p['pasta']}/README.md)")
    nav.append("🏠 [Início](../README.md)")
    if i + 1 < len(M):
        q = M[i + 1]
        nav.append(f"➡️ [{q['n']} — {q['titulo']}](../{q['pasta']}/README.md)")
    partes += ["---", "", " · ".join(nav), ""]
    with open(os.path.join(MAT, m["pasta"], "README.md"), "w", encoding="utf-8", newline="\n") as f:
        f.write("\n".join(partes))
print("ok", len(M))
