using System;

namespace D.Units
{
    public static class UnitValue
    {
        #region CSS

        public static Number            Number(double value)  => new Number(value);
        public static UnitValue<double> Percent(double value) => new UnitValue<double>(value, UnitInfo.Percent);

        // <length>
        public static UnitValue<double> Em(double value)   => new UnitValue<double>(value, CssUnits.Em);
        public static UnitValue<double> Ex(double value)   => new UnitValue<double>(value, CssUnits.Ex);
        public static UnitValue<double> Ch(double value)   => new UnitValue<double>(value, CssUnits.Ch);
        public static UnitValue<double> Ic(double value)   => new UnitValue<double>(value, CssUnits.Ic);
        public static UnitValue<double> Rem(double value)  => new UnitValue<double>(value, CssUnits.Rem);
        public static UnitValue<double> Lh(double value)   => new UnitValue<double>(value, CssUnits.Lh);
        public static UnitValue<double> Rlh(double value)  => new UnitValue<double>(value, CssUnits.Rlh);
        public static UnitValue<double> Vw(double value)   => new UnitValue<double>(value, CssUnits.Vw);
        public static UnitValue<double> Vh(double value)   => new UnitValue<double>(value, CssUnits.Vh);
        public static UnitValue<double> Vi(double value)   => new UnitValue<double>(value, CssUnits.Vi);
        public static UnitValue<double> Vb(double value)   => new UnitValue<double>(value, CssUnits.Vb);
        public static UnitValue<double> Vmin(double value) => new UnitValue<double>(value, CssUnits.Vmin);
        public static UnitValue<double> Vmax(double value) => new UnitValue<double>(value, CssUnits.Vmax);
        public static UnitValue<double> Cm(double value)   => new UnitValue<double>(value, CssUnits.Px);
        public static UnitValue<double> Mm(double value)   => new UnitValue<double>(value, CssUnits.Px);
        public static UnitValue<double> Q(double value)    => new UnitValue<double>(value, CssUnits.Q);
        public static UnitValue<double> In(double value)   => new UnitValue<double>(value, UnitInfo.Inch);
        public static UnitValue<double> Pt(double value)   => new UnitValue<double>(value, CssUnits.Pt);
        public static UnitValue<double> Pc(double value)   => new UnitValue<double>(value, CssUnits.Pc);
        public static UnitValue<double> Px(double value)   => new UnitValue<double>(value, CssUnits.Px);
        
        // <angle>
        public static UnitValue<double> Deg(double value) => new UnitValue<double>(value, UnitInfo.Degree);
        public static UnitValue<double> Grad(double value) => new UnitValue<double>(value, UnitInfo.Gradian);
        public static UnitValue<double> Rad(double value) => new UnitValue<double>(value, UnitInfo.Radian);
        public static UnitValue<double> Turn(double value) => new UnitValue<double>(value, UnitInfo.Turn);

        // <time>
        public static UnitValue<double> S(double value) => new UnitValue<double>(value, CssUnits.Px);
        public static UnitValue<double> Ms(double value) => new UnitValue<double>(value, CssUnits.Px);


        // <frequency>
        public static UnitValue<double> Hz(double value) => new UnitValue<double>(value, UnitInfo.Hertz);
        public static UnitValue<double> KHz(double value) => new UnitValue<double>(value, UnitInfo.kHz);

        // <resolution>
        public static UnitValue<double> Dpi(double value) => new UnitValue<double>(value, CssUnits.Dpi);
        public static UnitValue<double> Dpcm(double value) => new UnitValue<double>(value, CssUnits.Dpcm);
        public static UnitValue<double> Dppx(double value) => new UnitValue<double>(value, CssUnits.Dppx);

        // <flex>
        public static UnitValue<double> Fr(double value) => new UnitValue<double>(value, CssUnits.Fr);

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