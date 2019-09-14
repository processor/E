
using static D.Units.ThermodynamicUnits;

namespace D.Units
{
    public sealed class ThermodynamicUnitSet : UnitSet
    {
        public ThermodynamicUnitSet()
        {
            Add("K",  Kelvin);
            Add("°C", Celsius);
        }
    }
}