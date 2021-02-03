namespace E.Transformations
{
    public readonly struct Translate : ITransform
    {
        public Translate(INumeric<double> x, INumeric<double> y, INumeric<double> z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // 10%
        // 100vh
        // 100%

        public INumeric<double> X { get; }

        public INumeric<double> Y { get; }

        public INumeric<double> Z { get; }
    }
}

// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-function/translate