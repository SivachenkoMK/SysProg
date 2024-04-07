namespace Lab1Variant6.AbstractionsExample.Implementations.Models;

public class WeightedModel<T>
{
    public int Weight { get; set; }

    public T Value { get; set; } = default!;
}