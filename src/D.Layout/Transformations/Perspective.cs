namespace E.Transformations;

public readonly struct Perspective : ITransform
{
    public Perspective(INumeric<double> length)
    {
        Length = length;
    }

    public INumeric<double> Length { get; }
}
