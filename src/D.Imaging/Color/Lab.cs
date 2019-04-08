using System;

namespace D.Imaging
{
    public readonly struct Lab : IColor
    {
        public Lab(double l, double a, double b)
        {
            L = l;
            A = a;
            B = b;
        }

        public double L { get; } // 0 - 1

        public double A { get; } // 0 - 1

        public double B { get; } // 0 - 230

        public Lch ToLch()
        {
            return new Lch(
                l: L,
                c: Math.Sqrt(A * A + B * B),
                h: Math.Atan2(B, A)
            );
        }
    }

}

// lab( <number>  <number>  <number>  [, <alpha-value>]?)

