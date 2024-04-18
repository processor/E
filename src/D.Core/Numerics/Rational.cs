using System;
using System.Globalization;

namespace E;

using static Math;

public readonly struct Rational(
    long numerator,
    long denominator) : INumber
{
    public long Numerator { get; } = numerator;

    public long Denominator { get; } = denominator;

    readonly ObjectType IObject.Kind => ObjectType.Rational;

    #region INumeric

    double INumber.Real => (double)Numerator / Denominator;

    T INumber.As<T>() => T.CreateChecked(Numerator) / T.CreateChecked(Denominator);

    #endregion

    #region Helpers

    public readonly Rational Reduce()
    {
        var n = Numerator;
        var d = Denominator;

        if (n == 0)
        {
            d = 1;

            return new Rational(n, d);
        }

        var gcd = Gcd(n, d);

        n /= gcd;
        d /= gcd;

        if (d < 0)
        {
            n *= -1;
            d *= -1;
        }

        return new Rational(n, d);
    }

    // greatest common denominator
    private static long Gcd(long a, long b)
    {
        a = Abs(a);
        b = Abs(b);

        long remainder;

        while (b != 0)
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
