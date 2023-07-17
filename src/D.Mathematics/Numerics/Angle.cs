using System.Numerics;

namespace E.Mathematics;

public readonly struct Angle<T>(T value)
    where T : INumberBase<T>
{
    public T Value { get; } = value;

    // ToDegrees
    // FromRadians
}