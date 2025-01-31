using static E.Units.Dimension;
using static E.Units.UnitFlags;

namespace E.Units;

// Temperature (Thermodynamics) 
public static class ThermodynamicUnits
{
    public static readonly UnitInfo Kelvin  = new(UnitType.Kelvin,  ThermodynamicTemperature, "K",  flags: SI | Base);
    public static readonly UnitInfo Celsius = new(UnitType.Celsius, ThermodynamicTemperature, "°C", flags: SI | Base); // + x
}