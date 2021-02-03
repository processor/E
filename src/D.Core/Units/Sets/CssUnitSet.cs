using static E.Units.CssUnits;

namespace E.Units
{
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
            Add("in",   UnitInfo.Inch);
            Add("cm",   UnitInfo.Cm);
        }
    }
}