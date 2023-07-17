namespace E.Transformations;

public readonly struct SkewY<T>(T ay) : ITransform
{
    public T Ay { get; } = ay;
}
