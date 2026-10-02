namespace EstudoC.Core.Projetos;

public enum ModeloProjeto { OlaMundo, Tad }

/// <summary>Projetos do aluno em Documentos\EstudoC\projetos.</summary>
public sealed class Workspace(string raiz)
{
    public string Raiz { get; } = raiz;

    public static Workspace Padrao() => new(Caminhos.Projetos);

    public IReadOnlyList<Projeto> Listar()
    {
        if (!Directory.Exists(Raiz))
            return [];
        return Directory.GetDirectories(Raiz)
            .Select(p => new Projeto(p))
            .OrderByDescending(p => Directory.GetLastWriteTimeUtc(p.Pasta))
            .ToList();
    }

    public Projeto Criar(string nome, ModeloProjeto modelo)
    {
        var projeto = new Projeto(Path.Combine(Raiz, NomeLivre(nome)));
        Directory.CreateDirectory(projeto.Pasta);
        foreach (var (arquivo, conteudo) in Modelos.Para(modelo))
            File.WriteAllText(Path.Combine(projeto.Pasta, arquivo), conteudo);
        return projeto;
    }

    /// <summary>Cria um projeto a partir de arquivos de material (cópia — o original não é alterado).</summary>
    public Projeto CriarDeArquivos(string nome, IEnumerable<string> arquivos)
    {
        var projeto = new Projeto(Path.Combine(Raiz, NomeLivre(nome)));
        Directory.CreateDirectory(projeto.Pasta);
        foreach (var arquivo in arquivos)
            File.Copy(arquivo, Path.Combine(projeto.Pasta, Path.GetFileName(arquivo)));
        var temMain = projeto.ArquivosFonte().Any(f => File.ReadAllText(f).Contains("main("));
        if (!temMain)
            File.WriteAllText(Path.Combine(projeto.Pasta, "main.c"), Modelos.MainUsando(arquivos.Where(a => a.EndsWith(".h")).Select(a => Path.GetFileName(a))));
        return projeto;
    }

    /// <summary>
    /// Cria um projeto a partir de um .c/.h do material, levando junto o par (x.h + x.c)
    /// e tudo que ele inclui com #include "…" (ex.: instante.h → data e horario).
    /// </summary>
    public Projeto CriarDeMaterial(string arquivo) =>
        CriarDeArquivos(Path.GetFileNameWithoutExtension(arquivo), Dependencias(arquivo));

    public static IReadOnlyList<string> Dependencias(string arquivo)
    {
        var pasta = Path.GetDirectoryName(Path.GetFullPath(arquivo))!;
        var resultado = new List<string>();
        var pendentes = new Queue<string>([Path.GetFileNameWithoutExtension(arquivo)]);
        var vistos = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        while (pendentes.TryDequeue(out var nome))
        {
            if (!vistos.Add(nome))
                continue;
            foreach (var ext in new[] { ".h", ".c" })
            {
                var caminho = Path.Combine(pasta, nome + ext);
                if (!File.Exists(caminho))
                    continue;
                resultado.Add(caminho);
                foreach (var linha in File.ReadLines(caminho))
                {
                    var m = System.Text.RegularExpressions.Regex.Match(linha, "^\\s*#\\s*include\\s*\"([^\"]+)\\.h\"");
                    if (m.Success)
                        pendentes.Enqueue(m.Groups[1].Value);
                }
            }
        }
        return resultado;
    }

    /// <summary>Cria um projeto com uma cópia dos arquivos do exemplo.</summary>
    public Projeto CriarDeExemplo(Exemplos.Exemplo exemplo) =>
        CriarDeArquivos(exemplo.Id, exemplo.Arquivos());

    // ---------------- sandbox ----------------

    /// <summary>A sandbox: um único projeto livre para testes rápidos. Criada com "Olá mundo" se não existir.</summary>
    public static Projeto Sandbox(string? pasta = null)
    {
        var projeto = new Projeto(pasta ?? Caminhos.Sandbox);
        if (!Directory.Exists(projeto.Pasta) || projeto.ArquivosFonte().Count == 0)
            SubstituirConteudo(projeto, Modelos.Para(ModeloProjeto.OlaMundo).Select(m => (m.Item1, m.Item2)));
        return projeto;
    }

    /// <summary>Troca TODO o código da sandbox pelos arquivos dados (ex.: de um exemplo).</summary>
    public static void SubstituirSandbox(Projeto sandbox, IEnumerable<string> arquivos) =>
        SubstituirConteudo(sandbox, arquivos.Select(a => (Path.GetFileName(a), File.ReadAllText(a))));

    /// <summary>Volta a sandbox para o "Olá mundo".</summary>
    public static void RecomecarSandbox(Projeto sandbox) =>
        SubstituirConteudo(sandbox, Modelos.Para(ModeloProjeto.OlaMundo).Select(m => (m.Item1, m.Item2)));

    private static void SubstituirConteudo(Projeto projeto, IEnumerable<(string Nome, string Conteudo)> arquivos)
    {
        Directory.CreateDirectory(projeto.Pasta);
        foreach (var antigo in projeto.Arquivos())
            File.Delete(antigo);
        if (Directory.Exists(projeto.PastaBuild))
            try { Directory.Delete(projeto.PastaBuild, true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
        foreach (var (nome, conteudo) in arquivos)
            File.WriteAllText(Path.Combine(projeto.Pasta, nome), conteudo);
    }

    /// <summary>Evita sobrescrever: "lista", "lista-2", "lista-3"…</summary>
    public string NomeLivre(string nome)
    {
        var limpo = string.Concat(nome.Trim().Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '-' : c));
        if (limpo.Length == 0)
            limpo = "projeto";
        var candidato = limpo;
        for (var i = 2; Directory.Exists(Path.Combine(Raiz, candidato)); i++)
            candidato = $"{limpo}-{i}";
        return candidato;
    }
}

internal static class Modelos
{
    public static IEnumerable<(string, string)> Para(ModeloProjeto modelo) => modelo switch
    {
        ModeloProjeto.Tad =>
        [
            ("main.c", """
                #include <stdio.h>
                #include "ponto.h"

                int main(void)
                {
                    Ponto *p = pto_cria(1.0f, 2.0f);
                    float x, y;

                    pto_acessa(p, &x, &y);
                    printf("Ponto: (%.1f, %.1f)\n", x, y);

                    pto_libera(p);
                    return 0;
                }

                """),
            ("ponto.h", """
                #ifndef PONTO_H
                #define PONTO_H

                /* Tipo opaco: o cliente só conhece o nome, não os campos. */
                typedef struct ponto Ponto;

                Ponto *pto_cria(float x, float y);
                void pto_libera(Ponto *p);
                void pto_acessa(const Ponto *p, float *x, float *y);
                void pto_atribui(Ponto *p, float x, float y);

                #endif

                """),
            ("ponto.c", """
                #include <stdlib.h>
                #include "ponto.h"

                struct ponto {
                    float x;
                    float y;
                };

                Ponto *pto_cria(float x, float y)
                {
                    Ponto *p = (Ponto *) malloc(sizeof(Ponto));
                    if (p != NULL) {
                        p->x = x;
                        p->y = y;
                    }
                    return p;
                }

                void pto_libera(Ponto *p)
                {
                    free(p);
                }

                void pto_acessa(const Ponto *p, float *x, float *y)
                {
                    *x = p->x;
                    *y = p->y;
                }

                void pto_atribui(Ponto *p, float x, float y)
                {
                    p->x = x;
                    p->y = y;
                }

                """),
        ],
        _ =>
        [
            ("main.c", """
                #include <stdio.h>

                int main(void)
                {
                    char nome[50];

                    printf("Qual é o seu nome? ");
                    scanf("%49s", nome);
                    printf("Olá, %s! Bons estudos de C.\n", nome);

                    return 0;
                }

                """),
        ],
    };

    public static string MainUsando(IEnumerable<string> cabecalhos)
    {
        var includes = string.Join("\n", cabecalhos.Select(h => $"#include \"{h}\""));
        return $$"""
            #include <stdio.h>
            #include <stdlib.h>
            {{includes}}

            int main(void)
            {
                /* Escreva aqui o programa que usa o TAD. */

                return 0;
            }

            """;
    }
}
