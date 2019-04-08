namespace D.Imaging
{
    public readonly struct Hsb
    {
        public Hsb(double h, double s, double b)
        {
            H = h;
            S = s;
            B = b;
        }

        public double H { get; } // in degrees

        public double S { get; } // 0 - 1

        public double B { get; } // 0 - 1
    }
}
