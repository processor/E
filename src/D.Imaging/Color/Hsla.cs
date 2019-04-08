namespace D.Imaging
{
    public readonly struct Hsla : IColor
    {
        public Hsla(double h, double s, double l, double a)
        {
            H = h;
            S = s;
            L = l;
            A = a;
        }

        public double H { get; } // in degrees

        public double S { get; } // 0 - 1

        public double L { get; } // 0 - 1

        public double A { get; } // 0 - 1
    }

    // hsla( <hue>, <percentage>, <percentage>, <alpha-value> )
}
