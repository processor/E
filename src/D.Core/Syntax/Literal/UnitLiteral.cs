using D.Units;

namespace D.Syntax
{
    public class UnitLiteral : ISyntax
    {
        public UnitLiteral(Unit<double> unit)
        {
            Value = unit;
        }

        public Unit<double> Value { get; }

        Kind IObject.Kind => Kind.Unit;

        public static implicit operator Unit<double>(UnitLiteral value)
            => value.Value;

        public override string ToString()
            => Value.ToString();
    }
}
