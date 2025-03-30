using System.Numerics;

namespace E.Transformations;

public readonly struct Rotate<T>(
    T x,
    T y,
    T z,
    Number<T> angle) : ITransform
    where T: unmanaged, INumber<T>
{
    public T X { get; } = x;

    public T Y { get; } = y;

    public T Z { get; } = z;

    public Number<T> Angle { get; } = angle;
}
