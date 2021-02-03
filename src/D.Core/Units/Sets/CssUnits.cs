namespace E.Units
{
    using static UnitFlags;
    using static Dimension;

    public static class CssUnits
    {
        // <resolution>
        public static readonly UnitInfo Dpi  = new UnitInfo("dpi",  Resolution); // dots per inch
        public static readonly UnitInfo Dpcm = new UnitInfo("dpcm", Resolution); // dots per cm
        public static readonly UnitInfo Dppx = new UnitInfo("dppx", Resolution); // dots per px
        public static readonly UnitInfo X    = new UnitInfo("x",    Resolution);
        
        // Absolute lengths
        public static readonly UnitInfo Q    = new UnitInfo("q",  Length); // quarter-millimeters
        public static readonly UnitInfo Pt   = new UnitInfo("pt", Length); // Point
        public static readonly UnitInfo Pc   = new UnitInfo("pc", Length); // picas | A pica is a hair less than 1/6 inch, and contains 12 points.
        public static readonly UnitInfo Px   = new UnitInfo("px", Length); // Pixel

        // Relative lengths
        public static readonly UnitInfo Em   = new UnitInfo("em",   Length, Relative);
        public static readonly UnitInfo Ex   = new UnitInfo("ex",   Length, Relative);
        public static readonly UnitInfo Ch   = new UnitInfo("ch",   Length, Relative);
        public static readonly UnitInfo Rem  = new UnitInfo("rem",  Length, Relative);
        public static readonly UnitInfo Vw   = new UnitInfo("vw",   Length, Relative); // 1% of viewport’s width
        public static readonly UnitInfo Vh   = new UnitInfo("vh",   Length, Relative); // 1% of viewport’s height
        public static readonly UnitInfo Vi   = new UnitInfo("vi",   Length, Relative);
        public static readonly UnitInfo Vb   = new UnitInfo("vb",   Length, Relative);
        public static readonly UnitInfo Vmin = new UnitInfo("vmin", Length, Relative); // 1% of viewport’s smaller dimension
        public static readonly UnitInfo Vmax = new UnitInfo("vmax", Length, Relative); // 1% of viewport’s larger dimension
        public static readonly UnitInfo Ic   = new UnitInfo("ic",   Length, Relative);
        public static readonly UnitInfo Lh   = new UnitInfo("lh",   Length, Relative);
        public static readonly UnitInfo Rlh  = new UnitInfo("rlh",  Length, Relative);

        // <flex>
        public static readonly UnitInfo Fr = new UnitInfo("fr", Length, Relative); //
    }
}