namespace E.Units;

using static UnitFlags;

public static class MechanicalUnits
{
    public static readonly UnitInfo Newton = new(UnitType.Newton, Dimension.Force, "N", flags:SI); // mechanical force | kg⋅m/s²
}