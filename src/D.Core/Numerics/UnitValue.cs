using System;

namespace E.Units
{
    public static class UnitValue
    {
        public static Number Number(double value) => new Number(value);

        #region CSS

        public static UnitValue<double> Percent(double value) => new UnitValue<double>(value, UnitInfo.Percentage);

        // <length>
        public static UnitValue<double> Em(double value)   => new (value, CssUnits.Em);
        public static UnitValue<double> Ex(double value)   => new (value, CssUnits.Ex);
        public static UnitValue<double> Ch(double value)   => new (value, CssUnits.Ch);
        public static UnitValue<double> Ic(double value)   => new (value, CssUnits.Ic);
        public static UnitValue<double> Rem(double value)  => new (value, CssUnits.Rem);
        public static UnitValue<double> Lh(double value)   => new (value, CssUnits.Lh);
        public static UnitValue<double> Rlh(double value)  => new (value, CssUnits.Rlh);
        public static UnitValue<double> Vw(double value)   => new (value, CssUnits.Vw);
        public static UnitValue<double> Vh(double value)   => new (value, CssUnits.Vh);
        public static UnitValue<double> Vi(double value)   => new (value, CssUnits.Vi);
        public static UnitValue<double> Vb(double value)   => new (value, CssUnits.Vb);
        public static UnitValue<double> Vmin(double value) => new (value, CssUnits.Vmin);
        public static UnitValue<double> Vmax(double value) => new (value, CssUnits.Vmax);
        public static UnitValue<double> Cm(double value)   => new (value, CssUnits.Px);
        public static UnitValue<double> Mm(double value)   => new (value, CssUnits.Px);
        public static UnitValue<double> Q(double value)    => new (value, CssUnits.Q);
        public static UnitValue<double> In(double value)   => new (value, UnitInfo.Inch);
        public static UnitValue<double> Pt(double value)   => new (value, CssUnits.Pt);
        public static UnitValue<double> Pc(double value)   => new (value, CssUnits.Pc);
        public static UnitValue<double> Px(double value)   => new (value, CssUnits.Px);
        
        // <angle>
        public static UnitValue<double> Deg(double value)   => new (value, UnitInfo.Degree);
        public static UnitValue<double> Grad(double value)  => new (value, UnitInfo.Gradian);
        public static UnitValue<double> Rad(double value)   => new (value, UnitInfo.Radian);
        public static UnitValue<double> Turn(double value)  => new (value, UnitInfo.Turn);

        // <time>
        public static UnitValue<double> S(double value)     => new (value, CssUnits.Px);
        public static UnitValue<double> Ms(double value)    => new (value, CssUnits.Px);


        // <frequency>
        public static UnitValue<double> Hz(double value)    => new (value, UnitInfo.Hertz);
        public static UnitValue<double> KHz(double value)   => new (value, UnitInfo.kHz);

        // <resolution>
        public static UnitValue<double> Dpi(double value)   => new (value, CssUnits.Dpi);
        public static UnitValue<double> Dpcm(double value)  => new (value, CssUnits.Dpcm);
        public static UnitValue<double> Dppx(double value)  => new (value, CssUnits.Dppx);

        // <flex>
        public static UnitValue<double> Fr(double value)    => new (value, CssUnits.Fr);

        #endregion

        public static UnitValue<T> Create<T>(T value, UnitInfo type)
            where T : unmanaged, IComparable<T>, IEquatable<T>, IFormattable
        {
            return new UnitValue<T>(value, type);
        }

        public static UnitValue<T> Create<T>(UnitInfo type)
            where T : unmanaged, IComparable<T>, IEquatable<T>, IFormattable
        {
            return new UnitValue<T>(default, type);
        }

        public static UnitValue<double> Parse(string text) => UnitValue<double>.Parse(text);
    }
}