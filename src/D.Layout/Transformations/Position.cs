namespace E.Transformations;

public readonly struct Position
{
    public Position(INumeric<double> x, INumeric<double> y, INumeric<double> z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public INumeric<double> X { get; }

    public INumeric<double> Y { get; }

    public INumeric<double> Z { get; }
}
