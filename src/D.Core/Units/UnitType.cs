using System;
using System.Text;

namespace D.Units
{
    using static UnitId;
    using static UnitFlags;

    using Expressions;

    public class UnitType
    {
        public static readonly UnitType None = new UnitType("unit", Unknown);

        #region Angles

        public static readonly UnitType Radian    = new UnitType("rad", Angle);
        public static readonly UnitType Steradian = new UnitType("㏛", SolidAngle);

        public static readonly UnitType Degree    = new UnitType("deg", Angle);  // 1 degree = π/180 radians
        public static readonly UnitType Gradian   = new UnitType("grad", Angle); // 400 per circle
        public static readonly UnitType Turn      = new UnitType("turn", Angle); // 1 per circle

        #endregion

        #region Electromagism / Energy

        public static readonly UnitType Ampere  = new UnitType("A",  ElectricCurrent, SI | Base);
        public static readonly UnitType Coulomb = new UnitType("C",  ElectricCharge);
        public static readonly UnitType Ohm     = new UnitType("Ω",  ElectricResistance);
        public static readonly UnitType Farad   = new UnitType("F",  Capacitance);
        public static readonly UnitType Henry   = new UnitType("H",  Inductance);
        public static readonly UnitType Joule   = new UnitType("J",  Energy);
        public static readonly UnitType Siemens = new UnitType("S",  ElectricConductance);
        public static readonly UnitType Tesla   = new UnitType("T",  MagneticFluxDensity);
        public static readonly UnitType Volt    = new UnitType("V",  ElectricPotentialDifference);
        public static readonly UnitType Watt    = new UnitType("W",  Power);
        public static readonly UnitType Weber   = new UnitType("Wb", MagneticFlux);

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

        public static readonly UnitType Newton = new UnitType("N", Force); // kg⋅m/s²

        #endregion

        #region Frequency

        public static readonly UnitType Hertz = new UnitType("Hz", Frequency);

        // rpm

        #endregion

        #region Length

        public static readonly UnitType Meter = new UnitType("m", Length, SI | Base);  // m

        public static readonly UnitType Inch  = new UnitType("in", Length, Imperial);

        #endregion

        #region Mass

        public static readonly UnitType Gram  = new UnitType("g", Mass); 

        // Standard is KG

        public static readonly UnitType Pound = new UnitType("lb", Mass, 453.592d); // lb = 453.592g

        #endregion

        public static readonly UnitType Mole = new UnitType("mol", AmountOfSubstance, SI | Base);

        #region Luminocity

        public static readonly UnitType Candela = new UnitType("cd", LuminousIntensity, SI | Base);

        #endregion

        #region Temperature (Thermodynamic)

        public static readonly UnitType Kelvin = new UnitType("K", ThermodynamicTemperature, SI | Base);

        // Add −273.15
        public static readonly UnitType Celsius = new UnitType("°C", ThermodynamicTemperature, SI | Base); // + x

        #endregion

        #region Time

        public static readonly UnitType Second      = new UnitType("s", Time, SI | Base);  // s

        // 60 seconds in minute
        public static readonly UnitType Minute      = new UnitType("min" , Time, 60d);
        public static readonly UnitType Hour        = new UnitType("h"   , Time, 60d * 60d);

        #endregion

        #region Pressure

        public static readonly UnitType Pascal = new UnitType("Pa", Pressure);

        #endregion

        #region Radiation

        public static readonly UnitType Sievert   = new UnitType("Sv", DoseEquivlant);
        public static readonly UnitType Gray      = new UnitType("Gy", AbsorbedDose);
        public static readonly UnitType Becquerel = new UnitType("Bq", Radioactivity);

        #endregion

        #region Volume

        public static UnitType Liter = new UnitType("L", Length); //  1,000 cubic centimeters

        #endregion

        public static readonly UnitType Katal = new UnitType("kat", CatalyticActivity);

        // public static readonly UnitType Area = new UnitType(Length, "m²");
        
        #region Resolution

        public static readonly UnitType Dpi  = new UnitType("dpi",  Resolution);
        public static readonly UnitType Dpcm = new UnitType("dpcm", Resolution);
        public static readonly UnitType Dppx = new UnitType("dppx", Resolution);

        #endregion

        // Absolute Lengths
        public static readonly UnitType Q    = new UnitType("q",  Length); // quarter-millimeters
        public static readonly UnitType Pt   = new UnitType("pt", Length); // points
        public static readonly UnitType Pica = new UnitType("pc", Length); // picas | A pica is a hair less than 1/6 inch, and contains 12 points.
        public static readonly UnitType Px   = new UnitType("px", Length);

        // Relative Lengths
        public static readonly UnitType Em   = new UnitType("em",   Length, Relative);
        public static readonly UnitType Ex   = new UnitType("ex",   Length, Relative);
        public static readonly UnitType Ch   = new UnitType("ch",   Length, Relative);
        public static readonly UnitType Rem  = new UnitType("rem",  Length, Relative);
        public static readonly UnitType Vw   = new UnitType("vw",   Length, Relative); // 1% of viewport’s width
        public static readonly UnitType Vh   = new UnitType("vh",   Length, Relative); // 1% of viewport’s height
        public static readonly UnitType Vmin = new UnitType("vmin", Length, Relative); // 1% of viewport’s smaller dimension
        public static readonly UnitType Vmax = new UnitType("vmax", Length, Relative); // 1% of viewport’s larger dimension

        public UnitType(string name) // e.g. px
        {
            Name = name;
            BaseUnit = Unknown;
            BaseFactor = 1;
        }

        public UnitType(string name, int exponent) 
        {
            Name = name;
            BaseUnit = Unknown;
            BaseFactor = 1;
            Exponent = exponent;
        }

        public UnitType(string name, UnitId id, UnitFlags flags = UnitFlags.None)
        {
            Name = name;
            BaseUnit = id;
            Flags = flags;
            BaseFactor = 1;
        }

        public UnitType(string name, UnitId id, double baseMultiplier)
        {
            Name = name;
            BaseUnit = id;
            BaseFactor = baseMultiplier;
        }

        public UnitType(SIPrefix prefix, string name, UnitId id, double baseMultiplier, int exponent)
        {
            Prefix     = prefix;
            Name       = name;
            BaseUnit   = id;
            BaseFactor = baseMultiplier;
            Exponent   = exponent;
        }

        public SIPrefix Prefix { get; } = SIPrefix.None;

        public bool IsBaseUnit => Flags.HasFlag(Base);

        public UnitId BaseUnit { get; }

        public double BaseFactor { get; }

        public string Name { get; }

        public int Exponent { get; } = 1;

        public IConverter<double, double> BaseConverter
            => new UnitConverter(BaseFactor);

        private UnitFlags Flags { get; }

        public IExpression Expand()
            => throw new Exception("Not yet implemented");

        public UnitType WithPrefix(SIPrefix prefix)
        {
            return new UnitType(prefix, Name, BaseUnit, BaseFactor, Exponent);
        }

        public UnitType WithExponent(int exponent)
        {
            return new UnitType(Prefix, Name, BaseUnit, BaseFactor, exponent);
        }

        // TODO: Multiply (Add Exponents)

        public static bool TryParse(string name, out UnitType type)
        {
            if (name.Length <= 3 && TryParseSymbol(name, out type))
            {
                return true;
            }
            else if (TryParseName(name, out type))
            {
                return true;
            }
            else if (SIPrefix.TryParseSymbol(name, out SIPrefix prefix))
            {
                var unitName = name.Substring(prefix.Length);

                if (TryParseSymbol(unitName, out var unitType))
                {
                    type = unitType.WithPrefix(prefix);

                    return true;
                }
            }

            type = null;

            return false;
        }

        private static bool TryParseName(string text, out UnitType type)
        {
            switch(text)
            {
                case "ohm"      : type = Ohm;        break;
                case "radian"   : type = Radian;     break;
                case "pascal"   : type = Pascal;     break;
                case "coulomb"  : type = Coulomb;    break;
                case "newton"   : type = Newton;     break;
                case "gray"     : type = Gray;       break;
                case "hertz"    : type = Hertz;      break;
                case "meter"    : type = Meter;      break;
                case "gram"     : type = Gram;       break;
                case "grad"     : type = Gradian;    break;
                case "second"   : type = Second;     break;
                case "ampere"   : type = Ampere;     break;
                case "kelvin"   : type = Kelvin;     break;
                case "mole"     : type = Mole;       break;
                case "candela"  : type = Candela;    break;
                case "katal"    : type = Katal;      break;
                case "henry"    : type = Henry;      break;
                case "farad"    : type = Farad;      break;
                case "steradian": type = Steradian;  break;
                case "joule"    : type = Joule;      break;
                case "turn"     : type = Turn;       break;
                case "dpcm"     : type = Dpcm;       break;
                case "dppx"     : type = Dppx;       break;
                case "vmin"     : type = Vmin;       break;
                case "vmax"     : type = Vmax;       break;
                default:
                    type = null;

                    return false;
            }

            return true;
        }

        // TODO: Use Trie
        private static bool TryParseSymbol(string text, out UnitType type)
        {
            switch (text)
            {
                case "rad"  : type = Radian;       break;          
                case "Ω"    : type = Ohm;          break;          
                case "m"    : type = Meter;        break;          
                case "A"    : type = Ampere;       break;         
                case "g"    : type = Gram;         break;          
                case "s"    : type = Second;       break;          
                case "Hz"   : type = Hertz;        break;          
                case "Wb"   : type = Weber;        break;          
                case "T"    : type = Tesla;        break;       
                case "Pa"   : type = Pascal;       break;          
                case "㏛"   : type = Steradian;    break;          
                case "K"    : type = Kelvin;       break;         
                case "W"    : type = Watt;         break;          
                case "F"    : type = Farad;        break;          
                case "H"    : type = Henry;        break;          
                case "V"    : type = Volt;         break;         
                case "C"    : type = Coulomb;      break;          
                case "S"    : type = Siemens;      break;          
                case "J"    : type = Joule;        break;          
                case "N"    : type = Newton;       break;
                                                   
                case "Bq"   : type =  Becquerel;    break;          
                case "Sv"   : type =  Sievert;      break;          
                case "Gy"   : type =  Gray;         break;         
                                                   
                case "mol"  : type =  Mole;         break;
                                                  
                case "min"  : type =  Minute;       break;
                case "h"    : type =  Hour;         break;
                    
                // case "lx"   : return Illuminance;         
                // case "lm"   : return LuminousFlux;
                
                case "cd"   : type = Candela; break;

                case "kat"  : type = Katal; break;

                case "L"    : type = Liter; break;

                // Non-SI units
                case "lb"   : type = Pound; break;
                case "in"   : type = Inch; break;
                case "deg"  : type = Degree; break;


                case "px"   : type = Px;  break;

                // Resolutions
                case "dpi"  : type = Dpi;  break;

                case "pc"   : type = Pica; break;
                case "pt"   : type = Pt; break;
                case "em"   : type = Em; break;
                case "ex"   : type = Ex; break;
                case "rem"  : type = Rem; break;
                case "ch"   : type = Ch; break;
                case "vw"   : type = Vw; break;
                case "vh"   : type = Vh; break;
               
                default     :

                    type = null;

                    return false;
            }

            return true;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Prefix.Value != 1)
            {
                sb.Append(Prefix.Name); // e.g. k
            }

            sb.Append(Name);   // e.g. g

            if (Exponent != 1)
            {
                sb.Append(new Superscript(Exponent).ToString());
            }

            return sb.ToString();

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
