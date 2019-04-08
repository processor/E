using D.Units;

namespace D.Transformations
{
    public readonly struct SkewY : ITransform
    {
        public SkewY(UnitValue<double> ay)
        {
            Ay = ay;
        }

        public UnitValue<double> Ay { get; }
    }
}