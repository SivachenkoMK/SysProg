using System.Text.RegularExpressions;

namespace Lab3Variant6;

public class Lexem
{
    public Lexem(Regex expression, string name)
    {
        Expression = expression;
        Name = name;
    }

    public Regex Expression { get; set; }
    
    public string Name { get; set; }
}