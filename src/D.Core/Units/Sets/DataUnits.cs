namespace E.Units;

public static class DataUnits
{
    public static readonly UnitInfo Bit      = new(8_805,  "bit", Dimension.AmountOfInformation);
    public static readonly UnitInfo Baud     = new(192_027,"Bd",  Dimension.AmountOfInformation); // 1bit /s

    public static readonly UnitInfo Byte     = new("B",   Dimension.AmountOfInformation, 8, Bit); // TODO: Metric
        
    public static readonly UnitInfo Kibibyte = new("KiB", Dimension.AmountOfInformation, 1_024,            Byte);
    public static readonly UnitInfo Mebibyte = new("KiB", Dimension.AmountOfInformation, 1_048_576,        Byte);
    public static readonly UnitInfo Tebibyte = new("TiB", Dimension.AmountOfInformation, 109_951_1627_776, Byte);

}