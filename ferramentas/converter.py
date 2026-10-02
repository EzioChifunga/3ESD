"""
Conversor de materiais de estudo para Markdown.

Percorre a pasta `base/` e converte PDF, DOCX e TXT em arquivos .md dentro de
`convertido/`, mantendo a mesma estrutura de pastas. Arquivos .c e .h viram
.md com o código em bloco ```c``` (o original continua intacto em base/).

Uso:
    python ferramentas/converter.py            # converte tudo
    python ferramentas/converter.py --forcar   # reconverte mesmo se já existir

Dependência: PyMuPDF (pip install pymupdf). DOCX e TXT usam só a stdlib.
"""

import argparse
import re
import sys
import zipfile
import xml.etree.ElementTree as ET
from pathlib import Path

RAIZ = Path(__file__).resolve().parent.parent
ORIGEM = RAIZ / "base"
DESTINO = RAIZ / "convertido"

W = "{http://schemas.openxmlformats.org/wordprocessingml/2006/main}"


def ler_texto(caminho: Path) -> str:
    dados = caminho.read_bytes()
    for codificacao in ("utf-8-sig", "cp1252", "latin-1"):
        try:
            return dados.decode(codificacao)
        except UnicodeDecodeError:
            continue
    return dados.decode("utf-8", errors="replace")


def limpar(texto: str) -> str:
    texto = texto.replace("\r\n", "\n").replace("\r", "\n")
    texto = texto.replace(" ", " ").replace("​", "").replace("", "•").replace("\x0c", "")
    texto = re.sub(r"[ \t]+\n", "\n", texto)
    texto = re.sub(r"\n{3,}", "\n\n", texto)
    return texto.strip()


def pdf_para_md(caminho: Path) -> str:
    import pymupdf

    partes = []
    with pymupdf.open(caminho) as doc:
        for numero, pagina in enumerate(doc, start=1):
            texto = limpar(pagina.get_text("text", sort=True))
            if texto:
                partes.append(f"<!-- página {numero} -->\n\n{texto}")
    if not partes:
        return "> **Aviso:** nenhum texto extraído (PDF possivelmente escaneado/imagem).\n"
    return "\n\n".join(partes)


def docx_para_md(caminho: Path) -> str:
    with zipfile.ZipFile(caminho) as z:
        raiz = ET.fromstring(z.read("word/document.xml"))
    corpo = raiz.find(f"{W}body")
    linhas = []

    def texto_paragrafo(p):
        trechos = []
        for no in p.iter():
            if no.tag == f"{W}t" and no.text:
                trechos.append(no.text)
            elif no.tag == f"{W}tab":
                trechos.append("\t")
            elif no.tag in (f"{W}br", f"{W}cr"):
                trechos.append("\n")
        return "".join(trechos)

    for bloco in corpo:
        if bloco.tag == f"{W}p":
            texto = texto_paragrafo(bloco)
            estilo = bloco.find(f"{W}pPr/{W}pStyle")
            nome = estilo.get(f"{W}val", "") if estilo is not None else ""
            m = re.match(r"(?:Heading|Ttulo|Titulo|Título)(\d)", nome)
            if m and texto.strip():
                texto = "#" * min(int(m.group(1)) + 1, 6) + " " + texto.strip()
            elif bloco.find(f"{W}pPr/{W}numPr") is not None and texto.strip():
                texto = "- " + texto.strip()
            linhas.append(texto)
        elif bloco.tag == f"{W}tbl":
            linhas.append("")
            for i, tr in enumerate(bloco.iter(f"{W}tr")):
                celulas = [
                    " ".join(texto_paragrafo(p) for p in tc.iter(f"{W}p")).replace("|", "\\|").strip()
                    for tc in tr.iter(f"{W}tc")
                ]
                linhas.append("| " + " | ".join(celulas) + " |")
                if i == 0:
                    linhas.append("|" + "---|" * len(celulas))
            linhas.append("")
    return limpar("\n\n".join(linhas))


def txt_para_md(caminho: Path) -> str:
    return limpar(ler_texto(caminho))


def codigo_para_md(caminho: Path) -> str:
    return f"```c\n{ler_texto(caminho).rstrip()}\n```"


CONVERSORES = {
    ".pdf": pdf_para_md,
    ".docx": docx_para_md,
    ".txt": txt_para_md,
    ".c": codigo_para_md,
    ".h": codigo_para_md,
}


def destino_de(arquivo: Path) -> Path:
    relativo = arquivo.relative_to(ORIGEM)
    # mantém a extensão original no nome para evitar colisão (ex.: x.pdf e x.docx)
    return DESTINO / relativo.parent / f"{relativo.name}.md"


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--forcar", action="store_true", help="reconverte arquivos já convertidos")
    args = parser.parse_args()

    if not ORIGEM.is_dir():
        print(f"Pasta de origem não encontrada: {ORIGEM}")
        return 1

    convertidos = pulados = falhas = 0
    for arquivo in sorted(ORIGEM.rglob("*")):
        if not arquivo.is_file():
            continue
        conversor = CONVERSORES.get(arquivo.suffix.lower())
        if conversor is None:
            if arquivo.suffix.lower() == ".doc":
                print(f"[ignorado] {arquivo.relative_to(RAIZ)} (.doc antigo: salve como .docx)")
            continue
        saida = destino_de(arquivo)
        if saida.exists() and not args.forcar and saida.stat().st_mtime >= arquivo.stat().st_mtime:
            pulados += 1
            continue
        try:
            conteudo = conversor(arquivo)
        except Exception as erro:  # segue convertendo os demais
            print(f"[falha] {arquivo.relative_to(RAIZ)}: {erro}")
            falhas += 1
            continue
        saida.parent.mkdir(parents=True, exist_ok=True)
        cabecalho = f"# {arquivo.stem}\n\n> Fonte: `{arquivo.relative_to(RAIZ).as_posix()}`\n\n"
        saida.write_text(cabecalho + conteudo + "\n", encoding="utf-8")
        print(f"[ok] {saida.relative_to(RAIZ)}")
        convertidos += 1

    print(f"\nConvertidos: {convertidos} | Já atualizados: {pulados} | Falhas: {falhas}")
    return 1 if falhas else 0


if __name__ == "__main__":
    sys.exit(main())
