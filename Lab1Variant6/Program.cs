using Lab1Variant6;

var alphabetService = new AlphabetService();
var consonants = alphabetService.GetConsonants();
var separators = alphabetService.GetSeparators();

var fileReader = new InputReader();
var inputResult = fileReader.TryGetInput(Consts.InputFileName);
if (inputResult.IsFailed)
    return -1;

var input = inputResult.Value;
var words = fileReader.GetWords(input, separators);

var result = new Result();

foreach (var word in words)
{
    var wordInfo = new Word(word);
    foreach (var character in wordInfo.Value)
    {
        if (!consonants.Contains(character))
        {
            wordInfo.CloseWord();
            wordInfo.ResetLocalMaxAmountOfConsonants();
            continue;
        }

        wordInfo.ResetWordIfNew();
        wordInfo.RegisterConsonant();
    }

    wordInfo.EqualizeMaxAmountIfCurrentIsLower();
    result.UpdateResultWithNewWord(wordInfo);
}

result.Display();
return 0;