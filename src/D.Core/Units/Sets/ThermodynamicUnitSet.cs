namespace E.Units;

using static ThermodynamicUnits;

public sealed class ThermodynamicUnitSet : UnitSet
{
    public ThermodynamicUnitSet()
    {
        Add(Kelvin);
        Add(Celsius);
    }
}