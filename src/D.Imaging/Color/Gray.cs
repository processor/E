namespace D.Imaging
{
    public readonly struct Gray : IColor
    {
        public Gray(double value, double alpha)
        {
            Value = value;
            Alpha = alpha;
        }

        public double Value { get; }

        public double Alpha { get; }
    }
}