using System.Text.RegularExpressions;

namespace Lab3Variant6;

public class Program
{
    private const string Whitespace = "skip";

    public static string LexemAnalyzer(string jsCode)
    {
        var stringResult = "";

        var WHITESPACE = new Regex("(\\s+)");
        var KEYWORD =
            new Regex(
                @"\b(if|else|for|while|function|return|var|const|let|break|continue|switch|case|default|do|while|try|catch|finally|throw|new|delete|typeof|instanceof|void|in|with|yield)\b");
        var IDENTIFIER = new Regex(@"\b([a-zA-Z_][a-zA-Z0-9_]*)\b");
        var DOT = new Regex("\\.");
        var NUMERIC = new Regex(@"(\b\d+\.?\d*([eE][-+]?\d+)?\b|\b0x[0-9a-fA-F]+\b)");
        var STRING = new Regex("(\"(.*?)\")");
        var CHAR = new Regex("'.'");
        var PREPROCESSOR_COMMAND = new Regex("#\\w+");
        var SINGLE_LINE_COMMENT = new Regex(@"\/\/");
        var MULTIPLE_LINE_COMMENT = new Regex(@"\/\*[\s\S]*?\*\/");
        var OPERATOR = new Regex(@"(\?|\:|>|<|==|!=|<=|>=|&&|\|\||\+\+|--|\+|-|\*|/|%|&|\||\^|~|<<|>>|!|=)");
        var PUNCTUATION = new Regex(@"(\{|\}|\(|\)|\[|\]|;|,)");

        var remainingFragment = jsCode;

        while (!string.IsNullOrEmpty(remainingFragment))
        {
            var found = false;

            var lexems = new List<Lexem>
            {
                new(WHITESPACE, Whitespace),
                new(KEYWORD, nameof(KEYWORD)),
                new(IDENTIFIER, nameof(IDENTIFIER)),
                new(NUMERIC, nameof(NUMERIC)),
                new(STRING, nameof(STRING)),
                new(CHAR, nameof(CHAR)),
                new(PREPROCESSOR_COMMAND, nameof(PREPROCESSOR_COMMAND)),
                new(SINGLE_LINE_COMMENT, nameof(SINGLE_LINE_COMMENT)),
                new(MULTIPLE_LINE_COMMENT, nameof(MULTIPLE_LINE_COMMENT)),
                new(OPERATOR, nameof(OPERATOR)),
                new(PUNCTUATION, nameof(PUNCTUATION)),
                new(DOT, nameof(DOT))
            };

            foreach (var lexem in lexems)
            {
                var match = lexem.Expression.Match(remainingFragment);

                if (match is not { Success: true, Index: 0 })
                    continue; // continue to the next possible regex

                var matchValue = match.Value;

                if (lexem.Name != Whitespace)
                {
                    if (lexem.Name == nameof(SINGLE_LINE_COMMENT))
                    {
                        var endOfLineMatch = Regex.Match(remainingFragment, @"^(.*?)(\r\n|\r|\n|$)");
                        if (endOfLineMatch.Success)
                        {
                            matchValue += endOfLineMatch.Value[matchValue.Length..];
                        }
                    }
                    
                    var lexemMessage = $"<{matchValue} ; {lexem.Name}>";
                    stringResult += lexemMessage + "\n";
                    Console.WriteLine(lexemMessage);
                }

                remainingFragment = remainingFragment[matchValue.Length..];
                found = true;
                break;
            }

            if (found) 
                continue;
            
            var notFoundError = $"Failed to recognize syntax after this point: {remainingFragment}";
            stringResult += notFoundError;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(notFoundError);
        }

        return stringResult;
    }

    private static void Main()
    {
        const string exampleJs =
            @"// This is a single-line comment

/*
This is a multi-line comment.
It can span across multiple lines.
*/

// Variables
var x = 10; // This is a variable assignment
var y = 20; // Another variable

// Function definition
function addNumbers(a, b) {
    /*
    This function adds two numbers.
    */
    return a + b;
}

// Function call
var result = addNumbers(x, y); // Calling the function

console.log(result); // Output: 30
";

        LexemAnalyzer(exampleJs);
    }
}