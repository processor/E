namespace E.Units;

using static CssUnits;

public sealed class CssUnitSet : UnitSet
{
    public CssUnitSet()
    {
        // Resolutions (CSS)
        Add("dpi",  Dpi);
        Add("dpcm", Dpcm);
        Add("dppx", Dppx);

        // Lengths (Typography)
        Add("px",   Px);
        Add("pc",   Pc);
        Add("pt",   Pt);
        Add("em",   Em);
        Add("ex",   Ex);
        Add("rem",  Rem);
        Add("ch",   Ch);
        Add("vw",   Vw);
        Add("vh",   Vh);
        Add("vmin", Vmin);
        Add("vmax", Vmax);
        Add("in",   LengthUnits.Inch);
        Add("cm",   LengthUnits.Cm);
    }
}