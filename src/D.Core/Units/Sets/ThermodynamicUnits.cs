using static E.Units.Dimension;
using static E.Units.UnitFlags;

namespace E.Units;

// Temperature (Thermodynamics) 
public static class ThermodynamicUnits
{
    public static readonly UnitInfo Kelvin  = new(UnitType.Kelvin,  "K",  ThermodynamicTemperature, SI | Base);
    public static readonly UnitInfo Celsius = new(UnitType.Celsius, "°C", ThermodynamicTemperature, SI | Base); // + x
}