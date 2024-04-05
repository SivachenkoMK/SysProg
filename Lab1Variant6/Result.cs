namespace Lab1Variant6;

public class Result
{
    public List<string> WordsWithMostConsonants = new();

    private int _biggestAmountOfConsonantsInTheLeftSide;

    public void UpdateResultWithNewWord(Word wordInfo)
    {
        if (_biggestAmountOfConsonantsInTheLeftSide == wordInfo.AmountOfConsonantsInCurrentWord)
        {
            WordsWithMostConsonants.Add(wordInfo.Value);
        }

        if (_biggestAmountOfConsonantsInTheLeftSide < wordInfo.AmountOfConsonantsInCurrentWord)
        {
            _biggestAmountOfConsonantsInTheLeftSide = wordInfo.AmountOfConsonantsInCurrentWord;
            WordsWithMostConsonants.Clear();
            WordsWithMostConsonants.Add(wordInfo.Value);
        }
    }
}