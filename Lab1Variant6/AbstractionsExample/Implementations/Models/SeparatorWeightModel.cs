namespace Lab1Variant6.AbstractionsExample.Implementations.Models;

public class SeparatorWeightModel : WeightedModel<string>
{
    public SeparatorWeightModel(int weight, string value, bool isEndingSentence)
    {
        Weight = weight;
        Value = value;
        IsEndingSentence = isEndingSentence;
    }
    
    public bool IsEndingSentence { get; set; }
}