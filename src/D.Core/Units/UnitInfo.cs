using System;
using System.Text;

using D.Expressions;

namespace D.Units
{
    using static Dimension;
    using static Expression;
    using static UnitFlags;

    public class UnitInfo : IEquatable<UnitInfo>, IObject
    {
        public static readonly UnitInfo None = new UnitInfo("unit", Dimension.None);

        #region Angles (Plane & Solid)

        private static readonly Symbol π = Symbol.Variable("π");

        public static readonly UnitInfo Radian    = new UnitInfo("rad", PlaneAngle, Base);
        public static readonly UnitInfo Steradian = new UnitInfo("sr",  SolidAngle, Base);

        public static readonly UnitInfo Degree    = new UnitInfo("deg",  PlaneAngle, 1,     Divide(π, UnitValue.Create(180, Radian))); // π / 180 rad
        public static readonly UnitInfo Gradian   = new UnitInfo("grad", PlaneAngle, 0.9,   Degree); // 400 per circle
        public static readonly UnitInfo Turn      = new UnitInfo("turn", PlaneAngle, 360,   Degree); // 1 per circle


        #endregion

        #region Electromagism / Energy

        public static readonly UnitInfo Ampere  = new UnitInfo("A",  ElectricCurrent, SI | Base);
        public static readonly UnitInfo Coulomb = new UnitInfo("C",  ElectricCharge);
        public static readonly UnitInfo Ohm     = new UnitInfo("Ω",  ElectricResistance);
        public static readonly UnitInfo Farad   = new UnitInfo("F",  Capacitance);
        public static readonly UnitInfo Henry   = new UnitInfo("H",  Inductance);
        public static readonly UnitInfo Joule   = new UnitInfo("J",  Energy);
        public static readonly UnitInfo Siemens = new UnitInfo("S",  ElectricConductance);
        public static readonly UnitInfo Tesla   = new UnitInfo("T",  MagneticFluxDensity);
        public static readonly UnitInfo Volt    = new UnitInfo("V",  ElectricPotentialDifference);
        public static readonly UnitInfo Watt    = new UnitInfo("W", Dimension.Power);
        public static readonly UnitInfo Weber   = new UnitInfo("Wb", MagneticFlux);

        // Electric Properties
        // ElectricChargeDensity       = 51, // C/m3 
        // ElectricCurrentDensity      = 52,
        // ElectricConductance         = 54,

        // ElectricFieldStrength       = 57,
        // ElectricFluxDensity         = 58,
        // Permittivity                = 59, // farad/m
        // Permeability                = 60, // henry/m

        // Resistivity, // SI    ohm/metre

        // ElectricFlux,

        // Irradiance = 1501,     // watts per square meter (recieved)
        // Radiosity = 1502,      // watts per square meter (transmited)

        #endregion

        #region Force

        public static readonly UnitInfo Newton = new UnitInfo("N", Force); // kg⋅m/s²

        #endregion

        #region Frequency

        public static readonly UnitInfo Hertz = new UnitInfo("Hz", Frequency);
        public static readonly UnitInfo kHz   = Hertz.WithPrefix(SIPrefix.k); // kHz

        // rpm

        #endregion

        #region Length

        public static readonly UnitInfo Meter = new UnitInfo("m", Length, SI | Base);  // m
        public static readonly UnitInfo Cm    = Meter.WithPrefix(SIPrefix.c);          // cm

        public static readonly UnitInfo Inch  = new UnitInfo("in", Length, Imperial);

        #endregion

        #region Mass

        public static readonly UnitInfo Gram  = new UnitInfo("g", Mass); 

        // Standard is KG

        public static readonly UnitInfo Pound = new UnitInfo("lb", Mass, 453.592d); // lb = 453.592g

        #endregion

        public static readonly UnitInfo Mole = new UnitInfo("mol", AmountOfSubstance, SI | Base);

        #region Luminocity

        public static readonly UnitInfo Candela = new UnitInfo("cd", LuminousIntensity, SI | Base);

        #endregion

        #region Temperature (Thermodynamic)

        public static readonly UnitInfo Kelvin = new UnitInfo("K", ThermodynamicTemperature, SI | Base);

        // Add −273.15
        public static readonly UnitInfo Celsius = new UnitInfo("°C", ThermodynamicTemperature, SI | Base); // + x

        #endregion

        #region Time

        // 5.39 x 10−44 s

        public static readonly UnitInfo Second  = new UnitInfo("s",    Time, SI | Base);  // s
        public static readonly UnitInfo Minute  = new UnitInfo("min",  Time, 60d);
        public static readonly UnitInfo Hour    = new UnitInfo("h",    Time, 60d * 60d);
        public static readonly UnitInfo Week    = new UnitInfo("wk",   Time, 60d * 60d * 24 * 7);

        #endregion

        #region Pressure

        public static readonly UnitInfo Pascal = new UnitInfo("Pa", Pressure);

        #endregion

        #region Radiation

        public static readonly UnitInfo Sievert   = new UnitInfo("Sv"); // Radioactivity
        public static readonly UnitInfo Gray      = new UnitInfo("Gy");
        public static readonly UnitInfo Becquerel = new UnitInfo("Bq"); // Absorbed dose

        #endregion

        #region Volume

        public static UnitInfo Liter = new UnitInfo("L", Length); //  1,000 cubic centimeters

        #endregion

        public static readonly UnitInfo Katal = new UnitInfo("kat", CatalyticActivity);

        public static readonly UnitInfo SquareMeters = new UnitInfo("m", Length).WithExponent(2);
        
        #region Information

        public static readonly UnitInfo Byte = new UnitInfo("B", AmountOfInformation, Base);

        #endregion

        // Dimensionless

        public static readonly UnitInfo Percent = new UnitInfo("%", Dimension.None, 1, new Number(0.01)); // 1/100

        public UnitInfo(string name) // e.g. px
        {
            Name = name;
            Dimension = Dimension.None;
            DefinitionValue = 1;
        }

        public UnitInfo(string symbol, int exponent) 
        {
            Name      = symbol;
            Dimension   = Dimension.None;
            DefinitionValue = 1;
            Power    = exponent;
        }

        public UnitInfo(string name, Dimension id, UnitFlags flags = UnitFlags.None)
        {
            Name = name;
            Dimension = id;
            Flags = flags;
            DefinitionValue = 1;
        }

        public UnitInfo(string symbol, Dimension dimension, double definitionValue)
        {
            Name       = symbol;
            Dimension  = dimension;
            DefinitionValue = definitionValue;
        }

        public UnitInfo(SIPrefix prefix, string name, Dimension id, double definitionValue, int power)
        {
            Prefix          = prefix;
            Name            = name;
            Dimension       = id;
            DefinitionValue = definitionValue;
            Power           = power;
        }

        public UnitInfo(string name, Dimension id, double definitionValue, IObject definationUnit)
        {
            Name            = name;
            Dimension       = id;
            DefinitionValue = definitionValue;
            DefinitionUnit  = definationUnit;
        }

        public UnitInfo(string name, Dimension id, IObject definationUnit)
        {
            Name = name;
            Dimension = id;
            DefinitionValue = 1;
            DefinitionUnit = definationUnit;
        }

        public SIPrefix Prefix { get; } = SIPrefix.None;

        public bool IsBaseUnit => Flags.HasFlag(Base);

        public Dimension Dimension { get; }

        public string Name { get; }

        public int Power { get; } = 1;
                
        public double DefinitionValue { get; }

        public IObject DefinitionUnit { get; }

        public IConverter<double, double> BaseConverter => new UnitConverter(DefinitionValue);

        private UnitFlags Flags { get; }

        Kind IObject.Kind => Kind.Unit;

        public UnitInfo WithPrefix(SIPrefix prefix)
        {
            return new UnitInfo(prefix, Name, Dimension, DefinitionValue, Power);
        }

        public UnitInfo WithExponent(int exponent)
        {
            if (this.Power == exponent) return this;

            return new UnitInfo(Prefix, Name, Dimension, DefinitionValue, exponent);
        }

        public static bool TryParse(string name, out UnitInfo type)
        {
            if (UnitSet.Default.TryGet(name, out type))
            {
                return true;
            }
            else if (SIPrefix.TryParseSymbol(name, out SIPrefix prefix))
            {
                var unitName = name.Substring(prefix.Length);

                if (UnitSet.Default.TryGet(unitName, out var unitType))
                {
                    type = unitType.WithPrefix(prefix);

                    return true;
                }
            }

            type = null;

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Prefix.Value != 1)
            {
                sb.Append(Prefix.Name); // e.g. k
            }

            sb.Append(Name);   // e.g. g

            if (Power != 1)
            {
                sb.Append(new Superscript(Power).ToString());
            }

            return sb.ToString();
        }

        public bool Equals(UnitInfo other) =>
            Prefix.Equals(other.Prefix) &&
            Name == other.Name &&
            Power == other.Power;

    }
}

// Note: Only SI units may be prefixed.

/*



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
