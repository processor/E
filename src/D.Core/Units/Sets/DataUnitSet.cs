namespace E.Units;

using static DataUnits;

public sealed class DataUnitSet : UnitSet
{
    public DataUnitSet()
    {
        Add("B",  Byte);
        Add("kB", Byte.WithPrefix(SIPrefix.k));
        Add("MB", Byte.WithPrefix(SIPrefix.M));
        Add("GB", Byte.WithPrefix(SIPrefix.G));
    }
}