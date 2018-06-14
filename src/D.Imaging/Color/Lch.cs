using System;

namespace D.Imaging
{
    public readonly struct Lch : IColor
    {
        public Lch(double l, double c, double h, double alpha = 1)
        {
            L = l;
            C = c;
            H = h;
            Alpha = alpha;
        }

        public double L { get; } // 0 - 1

        public double C { get; } // 0 - 1

        public double H { get; } // 0 - 1

        public double Alpha { get; }

        public Lab ToLab()
        {
            return new Lab(L, a: Math.Cos(H), b: Math.Sin(H));
        }
    }

    // ( <number>  <number>  <number> [, <alpha-value>]?)

}