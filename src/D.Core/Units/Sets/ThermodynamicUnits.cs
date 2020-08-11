
using static D.Units.Dimension;
using static D.Units.UnitFlags;

namespace D.Units
{
    // Temperature (Thermodynamics) 
    public static class ThermodynamicUnits
    {
        public static readonly UnitInfo Kelvin  = new ("K", ThermodynamicTemperature, SI | Base);
        public static readonly UnitInfo Celsius = new ("°C", ThermodynamicTemperature, SI | Base); // + x
    }
}