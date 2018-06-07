using System;
using System.Text;

using D.Syntax;

namespace D.Units
{
    public readonly struct UnitValue<T> : IUnitValue<T>, IEquatable<UnitValue<T>>
        where T : struct, IComparable<T>, IEquatable<T>, IFormattable
    {
        private static readonly T one = (T)Convert.ChangeType(1, typeof(T));

        public static readonly UnitValue<T> One = new UnitValue<T>(one, UnitType.None);
        
        public UnitValue(T quantity, UnitType type)
        {
            Quantity = quantity; // 1
            Type     = type;     // g
        }


        public T Quantity { get; }    // Value

        public UnitType Type { get; } // Unit

        #region With

        public UnitValue<T> With(T quantity) => new UnitValue<T>(quantity, Type);

        public UnitValue<T> With(T quantity, UnitType type) => new UnitValue<T>(quantity, type);

        #endregion

        #region Conversions

        public double From(IUnitValue source)
        {
           return Type.Prefix.Value * source.Type.Prefix.Value;
        }

        public double To(UnitType type)
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

            double q = Convert.ToDouble(Quantity);

            return q * (
                (Type.Prefix.Value * Type.BaseFactor) /
                (target.Type.Prefix.Value * target.Type.BaseFactor)
            );
        }

        #endregion

        #region INumber

        Kind IObject.Kind => Kind.UnitValue;

        double INumber.Real => Convert.ToDouble(Quantity);

        T1 INumber.As<T1>() => (T1)Convert.ChangeType(Quantity, typeof(T1));

        #endregion

        // No space between units...

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Quantity);
            
            sb.Append(Type.ToString());   // e.g. kg
    
            return sb.ToString();
        }

        public static UnitValue<T> Wrap(T value)
        {
            return new UnitValue<T>(value, UnitType.None);
        }

        public static bool TryParse(string text, out UnitValue<T> unit)
        {
            if (UnitType.TryParse(text, out UnitType type))
            {
                unit = new UnitValue<T>(one, type);

                return true; // Simple unit
            }
            else if (SIPrefix.TryParseSymbol(text, out SIPrefix prefix))
            {
                var unitName = text.Substring(prefix.Length);
                
                if (UnitType.TryParse(unitName, out type))
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

                var type = UnitType.TryParse(unit.UnitName, out UnitType unitType)
                    ? unitType
                    : new UnitType(unit.UnitName);

                return new UnitValue<T>((T)Convert.ChangeType(quantity, typeof(T)), type.WithExponent(unit.UnitPower));
            }
            else
            {
                var type = UnitType.TryParse(text, out UnitType unitType)
                   ? unitType
                   : new UnitType(text);

                return new UnitValue<T>(one, type);
            }
        }

        public bool Equals(UnitValue<T> other) =>
            Quantity.Equals(other.Quantity) &&
            Type.Equals(other.Type);
    }
}

// 1s   = (1)(1)second
// m³   = (1) m^3  AREA
// ms   = (1/1000) * (1) s