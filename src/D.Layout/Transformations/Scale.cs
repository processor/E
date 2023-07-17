using System.Numerics;

namespace E.Transformations;

public readonly struct Scale<T>(T x, T y, T z) : ITransform
    where T: unmanaged, INumberBase<T>
{
    public T X { get; } = x;

    public T Y { get; } = y;

    public T Z { get; } = z;
}