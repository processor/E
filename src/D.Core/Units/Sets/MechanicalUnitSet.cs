namespace E.Units
{
    public sealed class MechanicalUnitSet : UnitSet
    {
        public MechanicalUnitSet()
        {
            Add("N", MechanicalUnits.Newton);
        }
    }
}