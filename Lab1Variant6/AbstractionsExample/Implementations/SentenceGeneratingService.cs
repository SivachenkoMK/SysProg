using System.Text;

namespace Lab1Variant6.AbstractionsExample.Implementations;

public class SentenceGeneratingService : ISentenceGeneratingService
{
    private readonly IWordGeneratingService _wordGeneratingService;
    private readonly ISeparatorGeneratingService _separatorGeneratingService;
    
    public SentenceGeneratingService(IWordGeneratingService wordGeneratingService, ISeparatorGeneratingService separatorGeneratingService)
    {
        _wordGeneratingService = wordGeneratingService;
        _separatorGeneratingService = separatorGeneratingService;
    }
    
    public string GenerateSentence()
    {
        var sb = new StringBuilder();
        
        var startOfSentence = true;
        
        for (var i = 0; i < 1 | !startOfSentence; i++)
        {
            var word = _wordGeneratingService.GenerateWord(startOfSentence);
            startOfSentence = false;
            
            var separator = _separatorGeneratingService.GenerateSeparator(out var isEndingSentence);

            if (isEndingSentence)
                startOfSentence = true;
            
            sb.Append(word + separator);
        }

        return sb.ToString();
    }
}