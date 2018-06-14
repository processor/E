namespace D.Transformations
{
    public readonly struct Scale : ITransform
    {
        public Scale(INumeric<double> x, INumeric<double> y, INumeric<double> z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public INumeric<double> X { get; }

        public INumeric<double> Y { get; }

        public INumeric<double> Z { get; }
    }
}