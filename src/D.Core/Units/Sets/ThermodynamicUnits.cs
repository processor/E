
using static D.Units.Dimension;
using static D.Units.UnitFlags;

namespace D.Units
{
    public static class ThermodynamicUnits
    {
        public static readonly UnitInfo Kelvin = new UnitInfo("K", ThermodynamicTemperature, SI | Base);
        public static readonly UnitInfo Celsius = new UnitInfo("°C", ThermodynamicTemperature, SI | Base); // + x
    }
}