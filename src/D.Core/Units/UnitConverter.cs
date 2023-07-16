namespace E.Units;

public sealed class UnitConverter(double multiplier) : IConverter<double, double>
{
    public static readonly UnitConverter None = new(1);

    public double Multiplier { get; } = multiplier;

    public double Convert(double source) => source * Multiplier;
}

// do the oposite of the action to find the value?
