using System.Globalization;
using System.Numerics;

namespace E;

public readonly struct Rational<T>(
    T numerator,
    T denominator) : INumberObject
    where T : INumber<T>
{
    public T Numerator { get; } = numerator;

    public T Denominator { get; } = denominator;

    readonly ObjectType IObject.Kind => ObjectType.Rational;

    #region INumeric

    TTarget INumberObject.As<TTarget>() => TTarget.CreateChecked(Numerator) / TTarget.CreateChecked(Denominator);

    #endregion

    #region Helpers

    public readonly Rational<T> Reduce()
    {
        var n = Numerator;
        var d = Denominator;

        if (n == T.Zero)
        {
            d = T.One;

            return new Rational<T>(n, d);
        }

        var gcd = Gcd(n, d);

        n /= gcd;
        d /= gcd;

        if (d < T.Zero)
        {
            n *= -T.One;
            d *= -T.One;
        }

        return new Rational<T>(n, d);
    }

    // greatest common denominator
    private static T Gcd(T a, T b)
    {
        a = T.Abs(a);
        b = T.Abs(b);

        T remainder;

        while (b != T.Zero)
        {
            remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }

    #endregion

    public readonly override string ToString() => string.Create(CultureInfo.InvariantCulture, $"{Numerator} / {Denominator}");
}
