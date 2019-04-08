namespace D.Units
{
    public static class DataUnits
    {
        public static readonly UnitInfo Bit  = new UnitInfo("bit", Dimension.AmountOfInformation);
        public static readonly UnitInfo Baud = new UnitInfo("Bd",  Dimension.AmountOfInformation); // 1bit /s

        public static readonly UnitInfo Byte     = new UnitInfo("B",   Dimension.AmountOfInformation, 8, Bit); // TODO: Metric
        
        public static readonly UnitInfo Kibibyte = new UnitInfo("KiB", Dimension.AmountOfInformation, 1024,    Byte);
        public static readonly UnitInfo Mebibyte = new UnitInfo("KiB", Dimension.AmountOfInformation, 1048576, Byte);
        public static readonly UnitInfo Tebibyte = new UnitInfo("TiB", Dimension.AmountOfInformation, 1099511627776, Byte);

    }
}