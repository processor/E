using System;

namespace D
{
    using static Math;

    public readonly struct Rational : INumber
    {
        public Rational(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public long Numerator { get; }

        public long Denominator { get; }

        ObjectType IObject.Kind => ObjectType.Rational;

        #region INumeric

        double INumber.Real => (double)Numerator / Denominator;

        T INumber.As<T>() => (T)Convert.ChangeType((double)Numerator / Denominator, typeof(T));

        #endregion

        #region Helpers

        public Rational Reduce()
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

        // greatest common denomiator
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

        public override string ToString() => Numerator + " / " + Denominator;
    }
}
