namespace E.Units
{
    public sealed class DataUnitSet : UnitSet
    {
        public DataUnitSet()
        {
            Add("B",  DataUnits.Byte);
            Add("kB", DataUnits.Byte.WithPrefix(SIPrefix.k));
            Add("MB", DataUnits.Byte.WithPrefix(SIPrefix.M));
            Add("GB", DataUnits.Byte.WithPrefix(SIPrefix.G));
        }
    }
}