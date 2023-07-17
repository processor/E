namespace E.Transformations;

public readonly struct Rotate<T>(
    T x,
    T y,
    T z,
    Number angle) : ITransform
{
    public T X { get; } = x;

    public T Y { get; } = y;

    public T Z { get; } = z;

    public Number Angle { get; } = angle;
}
