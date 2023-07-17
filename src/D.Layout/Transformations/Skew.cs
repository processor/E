using System.Numerics;

namespace E.Transformations;

public readonly struct Skew<T>(T ax, T ay) : ITransform
    where T: unmanaged, INumberBase<T>
{
    public T Ax { get; } = ax;

    public T Ay { get; } = ay;
}