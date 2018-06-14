namespace D.Imaging
{
    public readonly struct Rgba : IColor
    {
        public Rgba(double r, double g, double b, double a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public double R { get; }

        public double G { get; }

        public double B { get; }

        public double A { get; }
    }
}
