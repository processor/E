namespace E.Units;

public static class VolumeUnits
{
    public static readonly UnitInfo CubicMetre = new(UnitType.CubicMetre, Dimension.Volume, "m", exponent: 3, flags: UnitFlags.Derived | UnitFlags.Metric | UnitFlags.Base);


    public static readonly UnitInfo Litre      = new(UnitType.Litre,      "L",  new(0.001m,    CubicMetre), [], UnitFlags.SI | UnitFlags.Metric); //  1,000 cubic centimeters | 0.001 cubic metre
    public static readonly UnitInfo Millilitre = new(UnitType.Millilitre, "ml", new(0.000001m, CubicMetre), []);

    public static readonly UnitInfo MetricTeaspoon   = new(UnitType.MetricTeaspoon,   "tsp",               new(0.000005m,  CubicMetre), [], UnitFlags.Metric);
    public static readonly UnitInfo MetricTablespoon = new(UnitType.MetricTablespoon, "Metric tablespoon", new(0.000015m,  CubicMetre), [], UnitFlags.Metric); // 15 millilitre
    public static readonly UnitInfo MetricCup        = new(UnitType.MetricCup,        "Metric Cup",        new(0.00025m,   CubicMetre), []); // 0.25 litre
}