namespace E.Units;

public static class DataUnits
{
    public static readonly UnitInfo Bit      = new(8805,  "bit", Dimension.AmountOfInformation);
    public static readonly UnitInfo Baud     = new(192027,"Bd",  Dimension.AmountOfInformation); // 1bit /s

    public static readonly UnitInfo Byte     = new("B",   Dimension.AmountOfInformation, 8, Bit); // TODO: Metric
        
    public static readonly UnitInfo Kibibyte = new(79756, "KiB", Dimension.AmountOfInformation, 1_024,            Byte);
    public static readonly UnitInfo Mebibyte = new(79758, "MiB", Dimension.AmountOfInformation, 1_048_576,        Byte);
    public static readonly UnitInfo Tebibyte = new(79769, "TiB", Dimension.AmountOfInformation, 109_951_1627_776, Byte);

}