namespace D.Units
{
    public static class DataUnits
    {
        public static readonly UnitInfo Bit      = new ("bit", Dimension.AmountOfInformation);
        public static readonly UnitInfo Baud     = new ("Bd",  Dimension.AmountOfInformation); // 1bit /s

        public static readonly UnitInfo Byte     = new ("B",   Dimension.AmountOfInformation, 8, Bit); // TODO: Metric
        
        public static readonly UnitInfo Kibibyte = new ("KiB", Dimension.AmountOfInformation, 1024,    Byte);
        public static readonly UnitInfo Mebibyte = new ("KiB", Dimension.AmountOfInformation, 1048576, Byte);
        public static readonly UnitInfo Tebibyte = new ("TiB", Dimension.AmountOfInformation, 1099511627776, Byte);

    }
}