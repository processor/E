using static E.Units.UnitFlags;

namespace E.Units
{
    public static class MechanicalUnits
    {
        public static readonly UnitInfo Newton = new ("N", Dimension.Force, SI); // mechanical force | kg⋅m/s²
    }
}