using static D.Units.UnitFlags;

namespace D.Units
{
    public static class MechanicalUnits
    {
        public static readonly UnitInfo Newton = new UnitInfo("N", Dimension.Force, SI); // mechanical force | kg⋅m/s²
    }
}