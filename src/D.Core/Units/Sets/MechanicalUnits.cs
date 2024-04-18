namespace E.Units;

using static UnitFlags;

public static class MechanicalUnits
{
    public static readonly UnitInfo Newton = new(UnitType.Newton, "N", Dimension.Force, SI); // mechanical force | kg⋅m/s²
}