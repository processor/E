namespace E.Units;

using static Dimension;
using static UnitFlags;

public static class ElectromagnetismUnits
{
    public static readonly UnitInfo Ampere    = new(UnitType.Ampere, "A",  ElectricCurrent, SI | Base);

    // Derived units -----------------------------------------------------------------------------------------yea---------
    public static readonly UnitInfo Coulomb   = new(UnitType.Coulomb, "C",  ElectricCharge);                   // electrical charge
    public static readonly UnitInfo Ohm       = new(UnitType.Ohm,     "Ω",  ElectricResistance);               // electrical resistance
    public static readonly UnitInfo Farad     = new(UnitType.Farad,   "F",  Capacitance, SI);                  // electrical capacitance
    public static readonly UnitInfo Henry     = new(UnitType.Henry,   "H",  Inductance, SI);                   // electrical inductance
    public static readonly UnitInfo Joule     = new(UnitType.Joule,   "J",  Energy);
    public static readonly UnitInfo Siemens   = new(UnitType.Siemens, "S",  ElectricConductance, SI);          // electrical conductance
    public static readonly UnitInfo Tesla     = new(UnitType.Tesla,   "T",  MagneticFluxDensity);              // magnetic flux density
    public static readonly UnitInfo Volt      = new(UnitType.Volt,    "V",  ElectricPotentialDifference, SI);  // electrical potential
    public static readonly UnitInfo Watt      = new(UnitType.Watt,    "W",  Power);
    public static readonly UnitInfo Weber     = new(UnitType.Weber,   "Wb", MagneticFlux);                     // magnetic flux

    // Electromagnetic radiation

    public static readonly UnitInfo Sievert   = new(UnitType.Sievert,   "Sv", Energy, SI); // Radioactivity
    public static readonly UnitInfo Gray      = new(UnitType.Gray,      "Gy", Energy, SI);
    public static readonly UnitInfo Becquerel = new(UnitType.Becquerel, "Bq", Energy, SI); // Absorbed dose

    // Light
    public static readonly UnitInfo Illuminance  = new(UnitType.Illuminance,  "lx", Dimension.Illuminance,  SI);
    public static readonly UnitInfo LuminousFlux = new(UnitType.LuminousFlux, "lm", Dimension.LuminousFlux, SI);


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

    // Irradiance = 1501,     // watts per square meter (received)
    // Radiosity = 1502,      // watts per square meter (transmitted)
}