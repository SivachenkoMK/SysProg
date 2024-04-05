namespace Lab1Variant6;

public class Word
{
    public int LocalMaxAmountOfConsonants;
    
    public int AmountOfConsonantsInCurrentWord;
    
    public bool ResetOnNextIteration;
    
    public readonly string Value;

    public Word(string word)
    {
        Value = string.IsNullOrWhiteSpace(word) ? string.Empty : word;
    }
    
    public void CloseWord()
    {
        if (AmountOfConsonantsInCurrentWord < LocalMaxAmountOfConsonants)
        {
            AmountOfConsonantsInCurrentWord = LocalMaxAmountOfConsonants;
            ResetOnNextIteration = true;
        }
    }

    public void ResetLocalMaxAmountOfConsonants()
    {
        LocalMaxAmountOfConsonants = 0;
    }

    public void ResetWordIfNew()
    {
        if (ResetOnNextIteration)
        {
            LocalMaxAmountOfConsonants = 0;
            ResetOnNextIteration = false;
        }
    }

    public void RegisterConsonant()
    {
        LocalMaxAmountOfConsonants++;
    }

    public void EqualizeMaxAmountIfCurrentIsLower()
    {
        if (LocalMaxAmountOfConsonants >= AmountOfConsonantsInCurrentWord)
            AmountOfConsonantsInCurrentWord = LocalMaxAmountOfConsonants;
    }
}