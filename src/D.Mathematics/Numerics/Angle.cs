using System.Numerics;

namespace E.Mathematics;

public readonly struct Angle<T> 
    where T : INumberBase<T>
{
    public Angle(T value)
    {
        Value = value;
    }

    public T Value { get; }

    // ToDegrees
    // FromRadians
}