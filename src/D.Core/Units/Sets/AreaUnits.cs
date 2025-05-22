namespace E.Units;

public static class AreaUnits
{
    public static readonly UnitInfo SquareMetre = LengthUnits.Meter.WithExponent(2, UnitType.SquareMeter);
    public static readonly UnitInfo SquareFoot  = LengthUnits.Foot.WithExponent(2,  UnitType.SquareFoot);
}