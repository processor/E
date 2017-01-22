using System;

namespace D
{
    using static Math;

    public struct Rational : INumber
    {
        private long numerator;
        private long denominator;

        public Rational(long numerator, long denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;

            Reduce();
        }

        public long Numerator => numerator;

        public long Denominator => denominator;

        Kind IObject.Kind => Kind.Rational;

        #region INumeric

        double INumber.Real => (double)numerator / denominator;

        T INumber.As<T>() => (T)Convert.ChangeType((double)numerator / denominator, typeof(T));

        #endregion

        #region Helpers

        private void Reduce()
        {
            if (numerator == 0)
            {
                denominator = 1;

                return;
            }

            var gcd = Gcd(numerator, denominator);

            numerator /= gcd;
            denominator /= gcd;

            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }
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

        public override string ToString()
            => numerator + " / " + denominator;
    }
}
