using FluentResults;

namespace Lab1Variant6;

public class InputReader
{
    public Result<string> TryGetInput(string fileName)
    {
        try
        {
            var input = File.ReadAllText(fileName);
            return input;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during file read occured: {ex.Message}");
            return FluentResults.Result.Fail("Error during file read occured.");
        }
    }

    public string[] GetWords(string input, char[] separators)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<string>();
        
        var words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        return words;
    }
}