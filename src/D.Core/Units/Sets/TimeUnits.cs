namespace E.Units;

using static ConversionFactorFlags;
using static UnitFlags;

public static class TimeUnits
{
    // 5.39 x 10−44 s

    public static readonly UnitInfo Second  = new(UnitType.Second,  Dimension.Time, "s",   flags: SI | Base);  // s

    public static readonly UnitInfo Minute  = new(UnitType.Minute,  "min", new(60,     Second, Exact), []);
    public static readonly UnitInfo Hour    = new(UnitType.Hour,    "h",   new(3600,   Second, Exact), []);
    public static readonly UnitInfo Day     = new(UnitType.Day,     "h",   new(86400,  Second, Exact), []);
    public static readonly UnitInfo Week    = new(UnitType.Week,    "wk",  new(604800, Second, Exact), []);
}