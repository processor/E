using System;

namespace D.Units
{
    using static UnitId;
    using static UnitFlags;

    using Expressions;

    public class UnitType
    {
        public static readonly UnitType None = new UnitType("unit", Unknown);

        #region Angles

        public static readonly UnitType Radian      = new UnitType("rad", Angle);
        public static readonly UnitType Steradian   = new UnitType("㏛", SolidAngle);

        public static readonly UnitType Degree      = new UnitType("deg", Angle); //  1 degree = π/180 radians

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

        public static readonly UnitType Meter     = new UnitType("m", Length, SI | Base);  // m

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

        #region Others

        public static readonly UnitType Pixel = new UnitType("px", Length);

        #endregion

  
        public UnitType(string name) // e.g. px
        {
            Name = name;
            BaseUnit = Unknown;
            BaseFactor = 1;
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

        public bool IsBaseUnit => Flags.HasFlag(Base);

        public UnitId BaseUnit { get; }

        public double BaseFactor { get; }

        public string Name { get; }

        public IConverter<double, double> BaseConverter
            => new UnitConverter(BaseFactor);

        private UnitFlags Flags { get; }

        public IExpression Expand()
        {
            throw new Exception("Not yet implemented");
        }

        public override string ToString() => Name;

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
            else
            {
                type = null;

                return false;
            }
        }

        private static bool TryParseName(string text, out UnitType type)
        {
            switch(text)
            {
                case "ohm"      : type = Ohm; break;
                case "radian"   : type = Radian;     break;
                case "pascal"   : type = Pascal;     break;
                case "coulomb"  : type = Coulomb;    break;
                case "newton"   : type = Newton;     break;
                case "gray"     : type = Gray;       break;
                case "hertz"    : type = Hertz;      break;
                case "meter"    : type = Meter;      break;
                case "gram"     : type = Gram;       break;
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

                case "deg"  : type = Degree; break;
                case "px"   : type = Pixel; break;

                default     :

                    type = null;

                    return false;
            }

            return true;
        }
    }
}