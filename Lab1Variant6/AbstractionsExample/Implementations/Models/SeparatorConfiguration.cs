namespace Lab1Variant6.AbstractionsExample.Implementations.Models;

public class SeparatorConfiguration
{
    public readonly List<SeparatorWeightModel> SeparatorWeight = new()
    {
        new SeparatorWeightModel(70, " ", false),
        new SeparatorWeightModel(10, ", ", false),
        new SeparatorWeightModel(4, " - ", false),
        new SeparatorWeightModel(4, ": ", false),
        new SeparatorWeightModel(7, ". ", true),
        new SeparatorWeightModel(2, "! ", true),
        new SeparatorWeightModel(2, "? ", true),
        new SeparatorWeightModel(1, "... ", true),
    };
}