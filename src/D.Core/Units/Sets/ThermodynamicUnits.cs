using static E.Units.Dimension;
using static E.Units.UnitFlags;

namespace E.Units;

// Temperature (Thermodynamics) 
public static class ThermodynamicUnits
{
    public static readonly UnitInfo Kelvin  = new ("K", ThermodynamicTemperature, SI | Base);
    public static readonly UnitInfo Celsius = new ("°C", ThermodynamicTemperature, SI | Base); // + x
}