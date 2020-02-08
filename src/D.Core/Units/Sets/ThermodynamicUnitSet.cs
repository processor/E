
namespace D.Units
{
    public sealed class ThermodynamicUnitSet : UnitSet
    {
        public ThermodynamicUnitSet()
        {
            Add(ThermodynamicUnits.Kelvin);
            Add(ThermodynamicUnits.Celsius);
        }
    }

}