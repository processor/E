namespace D.Units
{
    public struct UnitConverter : IConverter<double, double>
    {
        public static readonly UnitConverter None = new UnitConverter(1); 

        // do the oposite of the action to find the value?

        public UnitConverter(double multiplier)
        {
            Multiplier = multiplier;
        }

        public double Multiplier { get; }

        public double Convert(double source)
            => source * Multiplier;
    }
}
