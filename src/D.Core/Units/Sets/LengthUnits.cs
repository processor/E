namespace E.Units;

using static ConversionFactorFlags;
using static UnitFlags;

public static class LengthUnits
{
    public static readonly UnitInfo Parsec           = new(UnitType.Parsec, Dimension.Length, "parsec", flags: Base);
    public static readonly UnitInfo AstronomicalUnit = new(UnitType.AstronomicalUnit, Dimension.Length, "au"); // 10,000,000,000,000
    // public static readonly UnitInfo Ångström         = new(UnitType.ångström, Dimension.Length, "Å", new(0.0000000001m, Meter), []);

    public static readonly UnitInfo Meter     = new(UnitType.Meter,      Dimension.Length, "m",  flags: SI | Base);  // m
    public static readonly UnitInfo Mm        = new(UnitType.Millimeter, "mm", new(MetricPrefix.m, Meter), [], flags: Metric);
    public static readonly UnitInfo Cm        = new(UnitType.Centimeter, "cm", new(MetricPrefix.c, Meter), [], flags: Metric);
    public static readonly UnitInfo Kilometre = new(UnitType.Kilometre,  "km", new(MetricPrefix.k, Meter), [ new(0.000000000000032408m, Parsec)], flags: Metric);

    public static readonly UnitInfo Inch  = new(UnitType.Inch, "in", new(0.0254m, Meter), [], flags: Imperial);
    public static readonly UnitInfo Foot  = new(UnitType.Foot, "ft", new(0.3048m, Meter), [new(12, Inch, Exact)]);
    public static readonly UnitInfo Yard  = new(UnitType.Yard, "yd", new(0.9144m, Meter), [new(3,  Foot, Exact)]);
}
