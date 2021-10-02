using System;
using System.Globalization;

namespace E;

public readonly struct Number : INumeric<double>
{
    public Number(double value)
    {
        Value = value;
    }

    public double Value { get; }

    public static Number Parse(string text)
    {
        return new Number(double.Parse(text, CultureInfo.InvariantCulture));
    }

    #region INumeric

    readonly ObjectType IObject.Kind => ObjectType.Number;

    readonly double INumber.Real => Value;

    #endregion

    #region Casts

    readonly T INumber.As<T>() => (T)Convert.ChangeType(Value, typeof(T));

    public static implicit operator double(Number d) => d.Value;

    public static implicit operator int(Number d) => (int)d.Value;

    public static implicit operator long(Number d) => (long)d.Value;

    #endregion

    #region Arithmetic

    public static Number operator +(Number x, Number y)
        => new Number(x.Value + y.Value);

    public static Number operator -(Number x, Number y)
        => new Number(x.Value - y);

    public static Number operator *(Number x, Number y)
        => new Number(x.Value * y.Value);

    public static Number operator /(Number x, Number y)
        => new Number(x.Value / y.Value);

    public static Number operator %(Number x, Number y)
        => new Number(x.Value % y.Value);

    #endregion

    public readonly override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}