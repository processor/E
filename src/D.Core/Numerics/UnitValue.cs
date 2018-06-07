using System;
using System.Text;

namespace D.Units
{
    public readonly struct UnitValue<T> : IUnitValue // e.g. Unit<int>
        where T : unmanaged, IComparable<T>
    {
        private static readonly T one = (T)Convert.ChangeType(1, typeof(T));

        public static readonly UnitValue<T> One = new UnitValue<T>(one, UnitType.None);

        // 1s   = (1)(1)second
        // m³   = (1) m^3  AREA
        // ms   = (1/1000) * (1) s

        /*
        public static readonly Func<T, INumeric<T>> constructor = Constructor.Compile<Func<T, INumeric<T>>>(typeof(INumeric<T>));
        */

        public UnitValue(T quantity, UnitType type)
        {
            Quantity = quantity; // 1
            Type     = type;     // g
        }

        public T Quantity { get; }

        public UnitType Type { get; }

        #region With

        public UnitValue<T1> With<T1>(T1 quantity) 
            where T1 : unmanaged, IComparable<T1>, IEquatable<T1>
        {
            return new UnitValue<T1>(quantity, Type);
        }

        public UnitValue<T> With<T>(T quantity, UnitType type)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new UnitValue<T>(quantity, type);
        }

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
            // if they're different types, throw? .

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

        #region INumeric

        Kind IObject.Kind => Kind.UnitLiteral;

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
            // todo: strip off any exponent...

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

        public static UnitValue<T1> Parse<T1>(string text)
            where T1 : unmanaged, IComparable<T1>, IEquatable<T1>
        {
            if (UnitType.TryParse(text, out UnitType type))
            {
                return new UnitValue<T1>(UnitValue<T1>.one, type);
            }
            else
            {
                type = new UnitType(text);

                return new UnitValue<T1>(UnitValue<T1>.one, type);
            }
        }      
    }

    public static class UnitValue
    {
        public static UnitValue<T> Create<T>(T value, UnitType type)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new UnitValue<T>(value, type);
        }

        public static UnitValue<T> Create<T>(UnitType type)
            where T : unmanaged, IComparable<T>, IEquatable<T>
        {
            return new UnitValue<T>(default, type);
        }

        public static UnitValue<double> Parse(string text) => UnitValue<double>.Parse<double>(text);
    }
}