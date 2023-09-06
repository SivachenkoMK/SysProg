// Basis alphabet definition
char[] consonants = "BCDFGHJKLMNPQRSTVWXYZbcdfghjklmnpqrstvwxyz".ToCharArray();
char[] separators = "!@#$%^&*()_+-=`~?.,';][/\\ \r\n\t\f\a\b".ToCharArray();

string input;

try
{
    input = File.ReadAllText("input.txt");
}
catch (Exception)
{
    Console.WriteLine("Error during file read occured.");
    return -1;
}

int biggestAmountOfConsonantsInTheLeftSide = 0;

List<string> wordsWithMostConsonants = new List<string>();
var words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

foreach (var word in words)
{
    int localMaxAmountOfConsonants = 0;
    int amountOfConsonantsInCurrentWord = 0;
    bool resetOnNextIteration = false;

    foreach (var character in word)
    {
        if (!consonants.Contains(character))
        {
            if (amountOfConsonantsInCurrentWord < localMaxAmountOfConsonants)
            {
                amountOfConsonantsInCurrentWord = localMaxAmountOfConsonants;
                resetOnNextIteration = true;
            }

            continue;
        }

        if (resetOnNextIteration)
        {
            localMaxAmountOfConsonants = 0;
            resetOnNextIteration = false;
        }

        localMaxAmountOfConsonants++;
    }

    if (biggestAmountOfConsonantsInTheLeftSide == amountOfConsonantsInCurrentWord)
    {
        wordsWithMostConsonants.Add(word);
    }
    else if (biggestAmountOfConsonantsInTheLeftSide < amountOfConsonantsInCurrentWord)
    {
        biggestAmountOfConsonantsInTheLeftSide = amountOfConsonantsInCurrentWord;
        wordsWithMostConsonants.Clear();
        wordsWithMostConsonants.Add(word);
    }
}

Console.WriteLine(string.Join('\n', wordsWithMostConsonants));

return 0;


