namespace E.Units;

using static Dimension;
using static UnitFlags;

public static class CssUnits
{
    // <resolution>
    public static readonly UnitInfo Dpi  = new(UnitType.Dpi,     Resolution, "dpi");  // dots per inch
    public static readonly UnitInfo Dpcm = new(UnitType.Dpcm,    Resolution, "dpcm"); // dots per cm
    public static readonly UnitInfo Dppx = new(UnitType.Dppx,    Resolution, "dppx"); // dots per px
    public static readonly UnitInfo X    = new((UnitType)(-100), Resolution, "x");
        
    // Absolute lengths
    public static readonly UnitInfo Q    = new(UnitType.QuaterMillimeters, Length, "Q"); // quarter-millimeters
    public static readonly UnitInfo Pt   = new(UnitType.Pt,                Length, "pt"); // Point
    public static readonly UnitInfo Pc   = new(UnitType.Pica,              Length, "pc"); // picas | A pica is a hair less than 1/6 inch, and contains 12 points.
    public static readonly UnitInfo Px   = new(UnitType.Px,                Length, "px"); // Pixel
                                         
    // Relative lengths                  
    public static readonly UnitInfo Em   = new(UnitType.Em,   Length, "em",   flags: Relative);
    public static readonly UnitInfo Ex   = new((UnitType)500, Length, "ex",   flags: Relative);
    public static readonly UnitInfo Ch   = new((UnitType)501, Length, "ch",   flags: Relative);
    public static readonly UnitInfo Rem  = new((UnitType)502, Length, "rem",  flags: Relative);
    public static readonly UnitInfo Vw   = new((UnitType)503, Length, "vw",   flags: Relative); // 1% of viewport’s width
    public static readonly UnitInfo Vh   = new((UnitType)504, Length, "vh",   flags: Relative); // 1% of viewport’s height
    public static readonly UnitInfo Vi   = new((UnitType)505, Length, "vi",   flags: Relative);
    public static readonly UnitInfo Vb   = new((UnitType)506, Length, "vb",   flags: Relative);
    public static readonly UnitInfo Vmin = new(UnitType.Vmin, Length, "vmin", flags: Relative); // 1% of viewport’s smaller dimension
    public static readonly UnitInfo Vmax = new((UnitType)507, Length, "vmax", flags: Relative); // 1% of viewport’s larger dimension
    public static readonly UnitInfo Ic   = new((UnitType)508, Length, "ic",   flags: Relative);
    public static readonly UnitInfo Lh   = new((UnitType)509, Length, "lh",   flags: Relative);
    public static readonly UnitInfo Rlh  = new((UnitType)510, Length, "rlh",  flags: Relative);

    // <flex>
    public static readonly UnitInfo Fr   = new(UnitType.Fr, Length, "fr", flags: Relative);
}