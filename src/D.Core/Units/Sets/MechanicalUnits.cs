namespace E.Units;

using static UnitFlags;

public static class MechanicalUnits
{
    public static readonly UnitInfo Newton = new(12_438, "N", Dimension.Force, SI); // mechanical force | kg⋅m/s²
}