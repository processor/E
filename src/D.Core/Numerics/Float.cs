using System;

namespace D
{
    public struct Float : INumeric<double>
    {
        #region Constructors

        public Float(double value)
        {
            Value = value;
        }

        #endregion

        public double Value { get; }

        Kind IObject.Kind => Kind.Number;

        double INumber.Real => Value;

        #region Casts

        T INumber.As<T>() => (T)Convert.ChangeType(Value, typeof(T));

        public static implicit operator double(Float d) => d.Value;

        public static implicit operator int   (Float d) => (int)d.Value;

        public static implicit operator long  (Float d) => (long)d.Value;

        #endregion

        #region Arithmetic
    
        public static Float operator + (Float x, Float y)
            => new Float(x.Value + y.Value);

        public static Float operator - (Float x, Float y)
            => new Float(x.Value - y);

        public static Float operator * (Float x, Float y)
            => new Float(x.Value * y.Value);

        public static Float operator / (Float x, Float y)
            => new Float(x.Value / y.Value);

        public static Float operator % (Float x, Float y)
            => new Float(x.Value % y.Value);

        #endregion

        public override string ToString()
            => Value.ToString();        
    }
}