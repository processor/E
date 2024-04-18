namespace E.Units;

using static UnitFlags;
using static Dimension;

public static class CssUnits
{
    // <resolution>
    public static readonly UnitInfo Dpi  = new(305896,     "dpi",  Resolution); // dots per inch
    public static readonly UnitInfo Dpcm = new(5299480,    "dpcm", Resolution); // dots per cm
    public static readonly UnitInfo Dppx = new(110742_003, "dppx", Resolution); // dots per px
    public static readonly UnitInfo X    = new(             "x",    Resolution);
        
    // Absolute lengths
    public static readonly UnitInfo Q    = new(          "q",  Length); // quarter-millimeters
    public static readonly UnitInfo Pt   = new(156389,   "pt", Length); // Point
    public static readonly UnitInfo Pc   = new(2718_629, "pc", Length); // picas | A pica is a hair less than 1/6 inch, and contains 12 points.
    public static readonly UnitInfo Px   = new(355198,   "px", Length); // Pixel
                                         
    // Relative lengths                  
    public static readonly UnitInfo Em   = new(1334525, "em",   Length, Relative);
    public static readonly UnitInfo Ex   = new("ex",   Length, Relative);
    public static readonly UnitInfo Ch   = new("ch",   Length, Relative);
    public static readonly UnitInfo Rem  = new("rem",  Length, Relative);
    public static readonly UnitInfo Vw   = new("vw",   Length, Relative); // 1% of viewport’s width
    public static readonly UnitInfo Vh   = new("vh",   Length, Relative); // 1% of viewport’s height
    public static readonly UnitInfo Vi   = new("vi",   Length, Relative);
    public static readonly UnitInfo Vb   = new("vb",   Length, Relative);
    public static readonly UnitInfo Vmin = new("vmin", Length, Relative); // 1% of viewport’s smaller dimension
    public static readonly UnitInfo Vmax = new("vmax", Length, Relative); // 1% of viewport’s larger dimension
    public static readonly UnitInfo Ic   = new("ic",   Length, Relative);
    public static readonly UnitInfo Lh   = new("lh",   Length, Relative);
    public static readonly UnitInfo Rlh  = new("rlh",  Length, Relative);

    // <flex>
    public static readonly UnitInfo Fr   = new("fr", Length, Relative); //
}