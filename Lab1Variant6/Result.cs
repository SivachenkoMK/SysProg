namespace Lab1Variant6;

public class Result
{
    private readonly List<string> _wordsWithMostConsonants = new();

    private int _biggestAmountOfConsonantsInTheLeftSide;

    public void UpdateResultWithNewWord(Word wordInfo)
    {
        if (_biggestAmountOfConsonantsInTheLeftSide == wordInfo.AmountOfConsonantsInCurrentWord)
        {
            _wordsWithMostConsonants.Add(wordInfo.Value);
        }

        if (_biggestAmountOfConsonantsInTheLeftSide < wordInfo.AmountOfConsonantsInCurrentWord)
        {
            _biggestAmountOfConsonantsInTheLeftSide = wordInfo.AmountOfConsonantsInCurrentWord;
            _wordsWithMostConsonants.Clear();
            _wordsWithMostConsonants.Add(wordInfo.Value);
        }
    }

    public string Display()
    {
        var result = string.Join('\n', _wordsWithMostConsonants);
        Console.WriteLine(result);

        return result;
    }
}