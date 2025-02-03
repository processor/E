namespace E.Units;

public static class USVolumeUnits
{                                                                                                            
    public static readonly UnitInfo Teaspoon      = new(UnitType.US_Teaspoon,     "tsp",   new(0.00000492892m,       VolumeUnits.CubicMetre), []);
    public static readonly UnitInfo Tablespoon    = new(UnitType.US_Tablespoon,   "tbsp",  new(0.00001478676478125m, VolumeUnits.CubicMetre), [new(3,   Teaspoon)]);
    public static readonly UnitInfo Cup           = new(UnitType.US_LiquidCup,    "c",     new(0.000236588m,         VolumeUnits.CubicMetre), [new(16,  Tablespoon)]);                                                                                                                                
    public static readonly UnitInfo FluidOunce    = new(UnitType.US_FluidOunce,   "fl oz", new(0.00002957353m,       VolumeUnits.CubicMetre), [new(2,   Tablespoon)]);
    public static readonly UnitInfo FluidPint     = new(UnitType.US_LiquidPint,   "pt",    new(0.0004731765m,        VolumeUnits.CubicMetre), [new(16,  FluidOunce)]);
    public static readonly UnitInfo FluidQuart    = new(UnitType.US_LiquidQuart,  "qt",    new(0.0009463529m,        VolumeUnits.CubicMetre), [new(32,  FluidOunce)]);
    public static readonly UnitInfo FluidGallon   = new(UnitType.US_LiquidGallon, "gal",   new(0.003785412m,         VolumeUnits.CubicMetre), [new(128, FluidOunce)]); // 231 cubic inch
}

public sealed class USfVolumeUnitSet : UnitSet
{
    public USfVolumeUnitSet()
    {
        Add("tsp",   USVolumeUnits.Teaspoon);
        Add("tbsp",  USVolumeUnits.Tablespoon);
        Add("c",     USVolumeUnits.Cup);
        Add("fl oz", USVolumeUnits.FluidOunce);
        Add("pt",    USVolumeUnits.FluidPint);
        Add("qt",    USVolumeUnits.FluidQuart);
        Add("gal",   USVolumeUnits.FluidGallon);
    }
}