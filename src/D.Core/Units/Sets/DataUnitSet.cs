namespace D.Units
{
    public class DataUnitSet : UnitSet
    {
        public DataUnitSet()
        {
            Add("B", DataUnits.Byte);
            Add("kB", DataUnits.Byte.WithPrefix(SIPrefix.k));
            Add("MB", DataUnits.Byte.WithPrefix(SIPrefix.M));
        }
    }
}