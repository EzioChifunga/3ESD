using System.Text.RegularExpressions;

namespace EstudoC.Core.Compilacao;

/// <summary>Explicações em português para as mensagens mais comuns do gcc.</summary>
public static class DicasErros
{
    private static readonly (Regex Padrao, string Dica)[] Dicas =
    [
        (R(@"expected '[;,]'.*before"), "Provavelmente faltou um ponto e vírgula (;) no fim da linha ANTERIOR."),
        (R(@"expected '\)'"), "Faltou fechar um parêntese ')'. Confira os pares de ( )."),
        (R(@"expected '\}'|expected declaration or statement at end of input"), "Faltou fechar uma chave '}'. Confira os pares de { }."),
        (R(@"expected '\]'"), "Faltou fechar um colchete ']'."),
        (R(@"'(\w+)' undeclared"), "Variável '$1' usada sem ser declarada. Declare antes de usar (ex.: int $1;) ou confira se o nome está escrito igual."),
        (R(@"implicit declaration of function '(printf|scanf|puts|gets|fgets|getchar|putchar|fopen|fclose|fprintf|fscanf|sprintf)'"), "Faltou #include <stdio.h> no início do arquivo."),
        (R(@"implicit declaration of function '(malloc|calloc|realloc|free|exit|rand|srand|atoi|atof|system|abs)'"), "Faltou #include <stdlib.h> no início do arquivo."),
        (R(@"implicit declaration of function '(strlen|strcpy|strncpy|strcmp|strncmp|strcat|strncat|strchr|strstr|memcpy|memset)'"), "Faltou #include <string.h> no início do arquivo."),
        (R(@"implicit declaration of function '(sqrt|pow|fabs|floor|ceil)'"), "Faltou #include <math.h> no início do arquivo."),
        (R(@"implicit declaration of function '(toupper|tolower|isdigit|isalpha|isspace|isupper|islower)'"), "Faltou #include <ctype.h> no início do arquivo."),
        (R(@"implicit declaration of function '(\w+)'"), "A função '$1' foi chamada antes de ser declarada. Declare o protótipo antes da main, inclua o .h correto ou confira o nome."),
        (R(@"undefined reference to 'WinMain'"), "O programa não tem função main (ou o nome está errado: deve ser 'main')."),
        (R(@"undefined reference to '(\w+)'"), "A função '$1' foi declarada mas não implementada. Se ela está em outro .c (ex.: um TAD), confira se o arquivo está no projeto e se o nome está igual."),
        (R(@"multiple definition of '(\w+)'"), "'$1' foi definido duas vezes. Em TAD, o .h deve ter só protótipos; o corpo da função fica no .c."),
        (R(@"redefinition of '(\w+)'|conflicting types for '(\w+)'"), "Nome definido duas vezes ou protótipo diferente da implementação. Compare o .h com o .c."),
        (R(@"No such file or directory"), "Arquivo de #include não encontrado. Para seus próprios .h use aspas: #include \"meu.h\" (e o .h deve estar no projeto)."),
        (R(@"makes pointer from integer without a cast|makes integer from pointer without a cast"), "Mistura de ponteiro com inteiro. Você quis usar & (endereço) ou * (conteúdo)?"),
        (R(@"incompatible pointer type"), "Tipos de ponteiro diferentes. Confira se está passando o endereço do tipo certo (ex.: int* × float*, Lista* × Lista**)."),
        (R(@"format '%(\w+)' expects argument of type '([^']+)'.*has type '([^']+)'"), "O formato %$1 espera '$2', mas recebeu '$3'. No scanf, lembre do & (exceto em strings)."),
        (R(@"too few arguments to function"), "A função foi chamada com menos argumentos do que o protótipo pede."),
        (R(@"too many arguments to function"), "A função foi chamada com mais argumentos do que o protótipo pede."),
        (R(@"dereferencing pointer to incomplete type|invalid use of undefined type"), "Acesso a campos de um tipo opaco (TAD). Fora do .c do TAD use apenas as funções da interface (.h)."),
        (R(@"request for member '(\w+)' in something not a structure"), "Uso de '.' em algo que não é struct. Se for ponteiro para struct, use '->' (p->$1)."),
        (R(@"'(\w+)' is a pointer; did you mean to use '->'"), "'$1' é ponteiro: use '->' em vez de '.'."),
        (R(@"has no member named '(\w+)'"), "A struct não tem o campo '$1'. Confira o nome na definição da struct."),
        (R(@"unused variable '(\w+)'"), "A variável '$1' foi declarada e nunca usada."),
        (R(@"'(\w+)' is used uninitialized|may be used uninitialized"), "Variável usada antes de receber valor: o conteúdo é lixo de memória."),
        (R(@"control reaches end of non-void function"), "A função deveria retornar um valor, mas há caminho que termina sem return."),
        (R(@"suggest parentheses around assignment used as truth value"), "Você usou '=' (atribuição) numa condição. Para comparar use '=='."),
        (R(@"assignment to expression with array type"), "Não dá para atribuir vetor/string com '='. Para strings use strcpy(destino, origem)."),
        (R(@"lvalue required"), "O lado esquerdo do '=' precisa ser uma variável."),
        (R(@"stray '\\\d+' in program"), "Caractere inválido no código (geralmente aspas “curvas” ou acentos copiados de PDF). Redigite as aspas/símbolos."),
        (R(@"missing terminating"), "Faltou fechar aspas (\" ou ')."),
        (R(@"return type of 'main' is not 'int'"), "Use int main(void) e termine com return 0;."),
        (R(@"array subscript .* is (above|below) array bounds"), "Acesso fora dos limites do vetor (lembre: índices vão de 0 a tamanho-1)."),
    ];

    private static Regex R(string padrao) => new(padrao, RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static string? Para(string mensagem)
    {
        foreach (var (padrao, dica) in Dicas)
        {
            var m = padrao.Match(mensagem);
            if (!m.Success)
                continue;
            var texto = dica;
            for (var i = m.Groups.Count - 1; i >= 1; i--)
                texto = texto.Replace("$" + i, m.Groups[i].Value);
            return texto;
        }
        return null;
    }
}
