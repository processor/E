namespace E.Transformations;

public readonly struct Perspective<T>(T length) : ITransform
{
    public T Length { get; } = length;
}
