using System;
using System.Text;

namespace D.Units
{
    // 8.314 m^3 Pa / mol / K
    // 8.314 (m^3 Pa) / (mol K)
    // 8.314 (m^3 * Pa) / (mol * K)

    // Unit 8.314 (m^3 Pa) / (mol K)

    public interface IUnit : INumber
    {
        SIPrefix Prefix { get; }

        UnitType Type { get; }

        int Power { get; }

        double To(IUnit unit);

        double To(UnitType unit);

        Unit<T> With<T>(T quantity)
            where T : struct, IComparable<T>, IEquatable<T>;

        Unit<T> With<T>(T quantity, int power)
          where T : struct, IComparable<T>, IEquatable<T>;
    }

    public static class Unit
    {
        public static Unit<T1> Create<T1>(T1 value, UnitType type)
            where T1 : struct, IComparable<T1>, IEquatable<T1>
        {
            return new Unit<T1>(value, type);
        }

        public static Unit<T1> Create<T1>(SIPrefix prefix, UnitType type)
            where T1 : struct, IComparable<T1>, IEquatable<T1>
        {
            return new Unit<T1>(prefix, type);
        }

        public static Unit<double> Parse(string text)
            => Unit<double>.Parse<double>(text);
    }

    public struct Unit<T> : IUnit // e.g. Unit<int>
        where T : struct, IComparable<T>
    {
        private static readonly T one = (T)Convert.ChangeType(1, typeof(T));

        public static readonly Unit<T> One = new Unit<T>(SIPrefix.None, UnitType.None);

        // 1s   = (1)(1)second
        // m³   = (1) m^3  AREA
        // ms   = (1/1000) * (1) s

        /*
        public static readonly Func<T, INumeric<T>> constructor = Constructor.Compile<Func<T, INumeric<T>>>(typeof(INumeric<T>));
        */


        public Unit(T quantity, UnitType type, int power = 1)
            : this(quantity, SIPrefix.None, type, power) { }

        public Unit(SIPrefix prefix, UnitType type, int power = 1)
            : this(one, prefix, type, power) { }

        public Unit(T quantity, SIPrefix prefix, UnitType type, int power = 1)
        {
            Quantity = quantity; // 1
            Prefix   = prefix;   // k
            Type     = type;     // g
            Power    = power;    // ³
        }

        public SIPrefix Prefix { get; }

        public UnitType Type { get; }

        public T Quantity { get; }

        public int Power { get; }

        #region With

        public Unit<T1> With<T1>(T1 quantity) 
            where T1 : struct, IComparable<T1>, IEquatable<T1>
        {
            return new Unit<T1>(quantity, Prefix, Type, Power);
        }

        public Unit<T1> With<T1>(T1 quantity, int power)
         where T1 : struct, IComparable<T1>, IEquatable<T1>
        {
            return new Unit<T1>(quantity, Prefix, Type, power);
        }

        public Unit<T> With(T quantity, int power)
            => new Unit<T>(quantity, Prefix, Type, power);

        public Unit<T> With(T quantity)
            => new Unit<T>(quantity, Prefix, Type, Power);

        public IUnit WithPower(int power)
            => new Unit<T>(Quantity, Prefix, Type, power);

        #endregion

        #region Conversions

        public Float From(IUnit source)
            => new Float(Prefix.Value * source.Prefix.Value);

        public double To(UnitType type)
        {
            var target = Unit.Create(1d, type);

            return To(target);
        }

        public double To(IUnit target)
        {
            // if they're different types, throw? .

            // kg   = 1000
            // g    = 0
            // mg   = 0.001

            // kg -> mg = 1,000,000     kg.units / mg.units
            // mg -> kg = .0000001      mg.units / kg.units

            // Type Conversions ft -> m, etc 

            var q = Convert.ToDouble(Quantity);

            // throw new Exception(string.Join(",", q, this.Type.Name, Prefix.Value, Type.BaseFactor, target.Prefix.Value, target.Type.BaseFactor));

            return q * (
                (Prefix.Value * Type.BaseFactor) /
                (target.Prefix.Value * target.Type.BaseFactor)
            );
        }

        #endregion

        #region INumeric

        Kind IObject.Kind => Kind.Unit;

        double INumber.Real 
            => Convert.ToDouble(Quantity);

        T1 INumber.As<T1>()
            => (T1)Convert.ChangeType(Quantity, typeof(T1));

        #endregion

        // No space between units...

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Quantity);
            
            if (Prefix.Value != 1)
            {
                sb.Append(Prefix.Name); // e.g. k
            }

            sb.Append(Type.Name);   // e.g. g
    
            if (Power != 1)
            {
                sb.Append(new Superscript(Power).ToString());
            }

            return sb.ToString();
        }

        public static Unit<T> Wrap(T value)
            => new Unit<T>(value, UnitType.None);

        public static bool TryParse<T1>(string text, out Unit<T1> unit)
            where T1 : struct, IComparable<T1>, IEquatable<T1>
        {
            UnitType type;
            SIPrefix prefix;

            // Strip off any exponent...

            if (UnitType.TryParse(text, out type))
            {
                unit = new Unit<T1>(SIPrefix.None, type);

                return true; // Simple unit
            }
            else if (SIPrefix.TryParseSymbol(text, out prefix))
            {
                var unitName = text.Substring(prefix.Length);

                if (UnitType.TryParse(unitName, out type))
                {
                    unit = new Unit<T1>(prefix, type);

                    return true;
                }
            }

            unit = default(Unit<T1>);

            return false;
        }

        public static Unit<T1> Parse<T1>(string text)
            where T1 : struct, IComparable<T1>, IEquatable<T1>
        {
            UnitType type;
            SIPrefix prefix;

            if (UnitType.TryParse(text, out type))
            {
                return new Unit<T1>(SIPrefix.None, type);
            }
            else if (SIPrefix.TryParseSymbol(text, out prefix))
            {
                var unitName = text.Substring(prefix.Length);

                if (!UnitType.TryParse(unitName, out type))
                {
                    type = new UnitType(unitName);
                }

                return new Unit<T1>(prefix, type);
            }
            else
            {
                type = new UnitType(text);

                return new Unit<T1>(SIPrefix.None, type);
            }
        }      
    }
}

// public static readonly UnitType Speed = new UnitType(Length, Second); // "m/s"
// public static readonly UnitType Accelaration = new UnitType(Length, SecondSquare); // "m/s²"


/*
// Acceleration     ,  // metre per second squared	         m/s^2
// Area             ,  // SI: square metre                   m^2
// DynamicViscosity ,  // pascal second                      Pa·s
// EnergyDensity    ,  // joule per cubic meter
// Density          ,  // SI: kilogram per cubic metre       kg/m^3  (AKA Mass Density)
// Momentum         ,  // mass distance/second
// Speed            ,  // metre per second
// Tension          ,  // newton meter
// Volume           ,  // cubic metre	                    liter


// Luminance                = 1068, // candela per square meter  cd/m^2
// LuminiousFlux            = 1069,


// Wavenumber                  = 1070, 

// RefractiveIndex             = 1073,
// RelactivePermeability       = 1074,

MagneticFieldStrength = 1065,
                                      
MolarEnergy                 = 1066,
MolarEntropy                = 1067,
                                      
      
                                      
HeatCapasity          
ThermalConductivity   
SurfaceChargeDensity  
HeatFluxDensity       
SurfaceTension        
                       
Radiance              
RadiantIntensity      

*/
