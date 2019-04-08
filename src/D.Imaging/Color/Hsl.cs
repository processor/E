namespace D.Imaging
{
    public readonly struct Hsl : IColor
    {
        // (120, 100%, 50%)
        // hsl( <hue>, <percentage>, <percentage> )    

        public Hsl(double h, double s, double l)
        {
            H = h;
            S = s;
            L = l;
        }

        public double H { get; } // in degrees (0-360)

        public double S { get; } // 0 - 1

        public double L { get; } // 0 - 1


        public static Hsl From(Argument[] args)
        {
            return new Hsl((args[0].Value as INumber).Real, (args[1].Value as INumber).Real, (args[2].Value as INumber).Real);
        }
    }
}
