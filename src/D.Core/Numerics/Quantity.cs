using System.Numerics;

namespace E.Units;

public static class Quantity
{
    // public static Number Number(double value) => new (value);

    #region CSS

    public static Quantity<double> Percent(double value) => new(value, UnitInfo.Percent);

    // <length>
    public static Quantity<double> Em(double value)   => new(value, CssUnits.Em);
    public static Quantity<double> Ex(double value)   => new(value, CssUnits.Ex);
    public static Quantity<double> Ch(double value)   => new(value, CssUnits.Ch);
    public static Quantity<double> Ic(double value)   => new(value, CssUnits.Ic);
    public static Quantity<double> Rem(double value)  => new(value, CssUnits.Rem);
    public static Quantity<double> Lh(double value)   => new(value, CssUnits.Lh);
    public static Quantity<double> Rlh(double value)  => new(value, CssUnits.Rlh);
    public static Quantity<double> Vw(double value)   => new(value, CssUnits.Vw);
    public static Quantity<double> Vh(double value)   => new(value, CssUnits.Vh);
    public static Quantity<double> Vi(double value)   => new(value, CssUnits.Vi);
    public static Quantity<double> Vb(double value)   => new(value, CssUnits.Vb);
    public static Quantity<double> Vmin(double value) => new(value, CssUnits.Vmin);
    public static Quantity<double> Vmax(double value) => new(value, CssUnits.Vmax);
    public static Quantity<double> Cm(double value)   => new(value, CssUnits.Px);
    public static Quantity<double> Mm(double value)   => new(value, CssUnits.Px);
    public static Quantity<double> Q(double value)    => new(value, CssUnits.Q);
    public static Quantity<double> In(double value)   => new(value, UnitInfo.Inch);
    public static Quantity<double> Pt(double value)   => new(value, CssUnits.Pt);
    public static Quantity<double> Pc(double value)   => new(value, CssUnits.Pc);
    public static Quantity<double> Px(double value)   => new(value, CssUnits.Px);
        
    // <angle>
    public static Quantity<double> Deg(double value)  => new(value, UnitInfo.Degree);
    public static Quantity<double> Grad(double value) => new(value, UnitInfo.Gradian);
    public static Quantity<double> Rad(double value)  => new(value, UnitInfo.Radian);
    public static Quantity<double> Turn(double value) => new(value, UnitInfo.Turn);

    // <time>
    public static Quantity<double> S(double value)    => new(value, CssUnits.Px);
    public static Quantity<double> Ms(double value)   => new(value, CssUnits.Px);


    // <frequency>
    public static Quantity<double> Hz(double value)   => new(value, UnitInfo.Hertz);
    public static Quantity<double> KHz(double value)  => new(value, UnitInfo.kHz);

    // <resolution>
    public static Quantity<double> Dpi(double value)  => new(value, CssUnits.Dpi);
    public static Quantity<double> Dpcm(double value) => new(value, CssUnits.Dpcm);
    public static Quantity<double> Dppx(double value) => new(value, CssUnits.Dppx);

    // <flex>
    public static Quantity<double> Fr(double value)   => new(value, CssUnits.Fr);

    // Volume
    public static Quantity<double> CubicMetre(double value) => new(value, UnitInfo.CubicMetre);

    #endregion

    public static Quantity<T> Create<T>(T value, UnitInfo type)
        where T : unmanaged, INumberBase<T>
    {
        return new Quantity<T>(value, type);
    }

    public static Quantity<T> Create<T>(UnitInfo type)
        where T : unmanaged, INumberBase<T>
    {
        return new Quantity<T>(default, type);
    }

    public static Quantity<double> Parse(string text) => Quantity<double>.Parse(text);
}


// Mass, time, distance, heat, and angle are among the familiar examples of quantitative properties.