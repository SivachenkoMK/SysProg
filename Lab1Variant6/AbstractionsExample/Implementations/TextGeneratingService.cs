using System.Text;

namespace Lab1Variant6.AbstractionsExample.Implementations;

public class TextGeneratingService : ITextGeneratorService
{
    private readonly ISentenceGeneratingService _sentenceGeneratingService;
    
    public TextGeneratingService(ISentenceGeneratingService sentenceGeneratingService)
    {
        _sentenceGeneratingService = sentenceGeneratingService;
    }
    
    public string GenerateText(int sentencesAmount)
    {
        var sb = new StringBuilder();

        for (var i = 0; i < sentencesAmount; i++)
        {
            sb.Append(_sentenceGeneratingService.GenerateSentence());
            
            if (IsNewlineRequired()) sb.Append('\n');
        }

        return sb.ToString();
    }

    private static bool IsNewlineRequired()
    {
        var random = new Random().Next(0, 10);
        return random > 7;
    }
}