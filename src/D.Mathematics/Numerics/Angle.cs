namespace E.Mathematics;

public readonly struct Angle<T> 
    where T : ISpanFormattable
{
    public Angle(T value)
    {
        Value = value;
    }

    public T Value { get; }

    // ToDegrees
    // FromRadians
}