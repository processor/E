using System;
using System.Text;

using D.Syntax;

namespace D.Units
{
    public readonly struct UnitValue<T> : IUnitValue<T>, IEquatable<UnitValue<T>>
        where T : struct, IComparable<T>, IEquatable<T>, IFormattable
    {
        private static readonly T one = (T)Convert.ChangeType(1, typeof(T));

        public static readonly UnitValue<T> One = new UnitValue<T>(one, UnitInfo.None);
        
        public UnitValue(T value, UnitInfo unit)
        {
            Value = value; // 1
            Unit  = unit;  // g
        }

        public T Value { get; }   

        public UnitInfo Unit { get; }

        #region With

        public UnitValue<T> With(T quantity) => new UnitValue<T>(quantity, Unit);

        public UnitValue<T> With(T quantity, UnitInfo type) => new UnitValue<T>(quantity, type);

        #endregion

        #region Conversions

        public double From(IUnitValue source)
        {
           return Unit.Prefix.Value * source.Unit.Prefix.Value;
        }

        public double To(UnitInfo type)
        {
            var target = UnitValue.Create(1d, type);

            return To(target);
        }

        public double To(IUnitValue target)
        {
            // ensure they have a common base type?

            // kg   = 1000
            // g    = 0
            // mg   = 0.001

            // kg -> mg = 1,000,000     kg.units / mg.units
            // mg -> kg = .0000001      mg.units / kg.units

            // Type Conversions ft -> m, etc 

            double q = Convert.ToDouble(Value);

            return q * (
                (Unit.Prefix.Value * Unit.DefinitionValue) /
                (target.Unit.Prefix.Value * target.Unit.DefinitionValue)
            );
        }

        #endregion

        #region INumber

        Kind IObject.Kind => Kind.UnitValue;

        double INumber.Real
        {

            get
            {
                var result = Convert.ToDouble(Value);

                if (Unit.DefinitionUnit is Number definationUnit)
                {
                    result = result * definationUnit.Value;
                }

                return result;
            }
        }

        T1 INumber.As<T1>() => (T1)Convert.ChangeType(Value, typeof(T1));

        #endregion

        // No space between units...

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.Append(Value);
            
            sb.Append(Unit.ToString());   // e.g. kg
    
            return sb.ToString();
        }

        public static UnitValue<T> Wrap(T value)
        {
            return new UnitValue<T>(value, UnitInfo.None);
        }

        public static bool TryParse(string text, out UnitValue<T> unit)
        {
            if (UnitInfo.TryParse(text, out UnitInfo type))
            {
                unit = new UnitValue<T>(one, type);

                return true; // Simple unit
            }
            else if (SIPrefix.TryParseSymbol(text, out SIPrefix prefix))
            {
                var unitName = text.Substring(prefix.Length);
                
                if (UnitInfo.TryParse(unitName, out type))
                {
                    unit = new UnitValue<T>(one, type.WithPrefix(prefix));

                    return true;
                }
            }

            unit = default;

            return false;
        }

        public static UnitValue<T> Parse(string text)
        {
            if ((char.IsDigit(text[0]) || text[0] == '-') && Parsing.Parser.Parse(text) is UnitValueSyntax unit)
            {
                // 1 g
                // 1g
                // 1.1g
                // 1px

                double quantity = double.Parse((unit.Expression as NumberLiteralSyntax).Text);

                var type = UnitInfo.TryParse(unit.UnitName, out UnitInfo unitType)
                    ? unitType
                    : new UnitInfo(unit.UnitName);

                return new UnitValue<T>((T)Convert.ChangeType(quantity, typeof(T)), type.WithExponent(unit.UnitPower));
            }
            else
            {
                var type = UnitInfo.TryParse(text, out UnitInfo unitType)
                   ? unitType
                   : new UnitInfo(text);

                return new UnitValue<T>(one, type);
            }
        }

        public void Deconstruct(out T value, string unitName)
        {
            (value, unitName) = (Value, Unit.Name);
        }

        public bool Equals(UnitValue<T> other) =>
            Value.Equals(other.Value) &&
            Unit.Equals(other.Unit);
    }
}

// 1s   = (1)(1)second
// m³   = (1) m^3  AREA
// ms   = (1/1000) * (1) s


// A dimension is a measure of a physical variable (without numerical values),
// while a unit is a way to assign a number or measurement to that dimension.