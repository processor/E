using System.Numerics;

namespace E.Transformations;

public readonly struct Position<T>(
    T x,
    T y,
    T z) where T: unmanaged, INumberBase<T>
{
    public T X { get; } = x;

    public T Y { get; } = y;

    public T Z { get; } = z;
}