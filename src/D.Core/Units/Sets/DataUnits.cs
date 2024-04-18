namespace E.Units;

public static class DataUnits
{
    public static readonly UnitInfo Bit      = new(UnitType.Bit, "bit", Dimension.AmountOfInformation);
    public static readonly UnitInfo Baud     = new(UnitType.Baud, "Bd",  Dimension.AmountOfInformation); // 1bit /s

    public static readonly UnitInfo Byte     = new("B",   Dimension.AmountOfInformation, 8, Bit); // TODO: Metric
        
    public static readonly UnitInfo Kibibyte = new(UnitType.Kibibyte, "KiB", Dimension.AmountOfInformation, 1_024,            Byte);
    public static readonly UnitInfo Mebibyte = new(UnitType.Mebibyte, "MiB", Dimension.AmountOfInformation, 1_048_576,        Byte);
    public static readonly UnitInfo Tebibyte = new(UnitType.Tebibyte, "TiB", Dimension.AmountOfInformation, 109_951_1627_776, Byte);

}