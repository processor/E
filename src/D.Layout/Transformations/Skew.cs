namespace E.Transformations
{
    public readonly struct Skew : ITransform
    {
        public Skew(INumeric<double> ax, INumeric<double> ay)
        {
            Ax = ax;
            Ay = ay;
        }

        public INumeric<double> Ax { get; }

        public INumeric<double> Ay { get;  }
    }
}