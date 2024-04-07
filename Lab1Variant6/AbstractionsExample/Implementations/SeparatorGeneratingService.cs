using Lab1Variant6.AbstractionsExample.Implementations.Models;

namespace Lab1Variant6.AbstractionsExample.Implementations;

public class SeparatorGeneratingService : ISeparatorGeneratingService
{
    private readonly SeparatorConfiguration _separatorConfiguration;
    
    public SeparatorGeneratingService(SeparatorConfiguration separatorConfiguration)
    {
        _separatorConfiguration = separatorConfiguration;
    }
    
    public string GenerateSeparator(out bool isEndingSentence)
    {
        var currentSeparator = new Random().Next(0, _separatorConfiguration.SeparatorWeight.Sum(x => x.Weight) + 1);

        foreach (var item in _separatorConfiguration.SeparatorWeight)
        {
            if (item.Weight < currentSeparator)
            {
                currentSeparator -= item.Weight;
            }
            else
            {
                isEndingSentence = item.IsEndingSentence;
                return item.Value;
            }
        }

        throw new NotImplementedException("This state shouldn't be reached due to weights resolving mechanism.");
    }
}