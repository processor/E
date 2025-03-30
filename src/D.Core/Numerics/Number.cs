using System.Globalization;
using System.Numerics;

namespace E;

public readonly struct Number<T>(T value) : INumeric<T>
    where T: struct, INumber<T>
{
    public T Value { get; } = value;

    public static Number<T> Parse(string text)
    {
        return new Number<T>(T.Parse(text, CultureInfo.InvariantCulture));
    }

    #region IObject

    readonly ObjectType IObject.Kind => ObjectType.Number;

    #endregion

    #region Casts

    readonly T INumberObject.As<T>() => T.CreateChecked(Value);

    public static implicit operator double(Number<T> d) => double.CreateChecked(d.Value);

    public static implicit operator int(Number<T> d) => int.CreateChecked(d.Value);

    public static implicit operator long(Number<T> d) => long.CreateChecked(d.Value);

    #endregion

    #region Arithmetic

    public static Number<T> operator +(Number<T> x, Number<T> y)
        => new (x.Value + y.Value);

    public static Number<T> operator -(Number<T> x, Number<T> y)
        => new (x.Value - y.Value);

    public static Number<T> operator *(Number<T> x, Number<T> y)
        => new (x.Value * y.Value);

    public static Number<T> operator /(Number<T> x, Number<T> y)
        => new (x.Value / y.Value);

    public static Number<T> operator %(Number<T> x, Number<T> y)
        => new (x.Value % y.Value);

    #endregion

    public readonly override string ToString() => Value.ToString(null, CultureInfo.InvariantCulture);
}