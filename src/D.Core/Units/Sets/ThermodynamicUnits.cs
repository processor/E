﻿using static E.Units.Dimension;
using static E.Units.UnitFlags;

namespace E.Units;

// Temperature (Thermodynamics) 
public static class ThermodynamicUnits
{
    public static readonly UnitInfo Kelvin  = new(11_579, "K", ThermodynamicTemperature,  SI | Base);
    public static readonly UnitInfo Celsius = new(25_267, "°C", ThermodynamicTemperature, SI | Base); // + x
}