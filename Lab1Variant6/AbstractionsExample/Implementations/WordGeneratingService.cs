using System.Text;

namespace Lab1Variant6.AbstractionsExample.Implementations;

public class WordGeneratingService : IWordGeneratingService
{
    public string GenerateWord(bool capitalized)
    {
        var sb = new StringBuilder();
        var wordLength = new Random().Next(3, 10);

        for (var i = 0; i < wordLength; i++)
        {
            if (capitalized && i == 0)
                sb.Append(GetUppercase());
            else
                sb.Append(GetLowercase());
        }

        return sb.ToString();
    }

    private char GetUppercase()
    {
        var uppercase = new Random().Next(65, 91);
        return (char)uppercase;
    }

    private char GetLowercase()
    {
        var lowercase = new Random().Next(97, 123);
        return (char)lowercase;
    }
}