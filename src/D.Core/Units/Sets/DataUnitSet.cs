namespace E.Units;

using static DataUnits;

public sealed class DataUnitSet : UnitSet
{
    public DataUnitSet()
    {
        Add("B",  Byte);
        Add("kB", Kilobyte);
        Add("MB", Metabyte);
        Add("GB", Gigabyte);
    }
}