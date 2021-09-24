namespace E.Transformations;

public readonly struct Rotate : ITransform
{
    public Rotate(INumeric<double> x, INumeric<double> y, INumeric<double> z, Number angle)
    {
        X = x;
        Y = y;
        Z = z;
        Angle = angle;
    }

    public INumeric<double> X { get; }

    public INumeric<double> Y { get; }

    public INumeric<double> Z { get; }

    public Number Angle { get; }
}
