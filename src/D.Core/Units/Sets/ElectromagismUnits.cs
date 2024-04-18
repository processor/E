namespace E.Units;

using static Dimension;
using static UnitFlags;

public static class ElectromagismUnits
{
    public static readonly UnitInfo Ampere    = new(25272, "A",  ElectricCurrent, SI | Base);

    // Derived units --------------------------------------------------------------------------------------------------
    public static readonly UnitInfo Coulomb   = new(25406,  "C",  ElectricCharge);                   // electrical charge
    public static readonly UnitInfo Ohm       = new(47083,  "Ω",  ElectricResistance);               // electrical resistance
    public static readonly UnitInfo Farad     = new(131255, "F",  Capacitance, SI);                  // electrical capacitance
    public static readonly UnitInfo Henry     = new(163354, "H",  Inductance, SI);                   // electrical inductance
    public static readonly UnitInfo Joule     = new(25269,  "J",  Energy);
    public static readonly UnitInfo Siemens   = new(169893, "S",  ElectricConductance, SI);          // electrical conductance
    public static readonly UnitInfo Tesla     = new(163343, "T",  MagneticFluxDensity);              // magnetic flux density
    public static readonly UnitInfo Volt      = new(25250,  "V",  ElectricPotentialDifference, SI);  // electrical potential
    public static readonly UnitInfo Watt      = new(25236,  "W",  Power);
    public static readonly UnitInfo Weber     = new(170804, "Wb", MagneticFlux);                     // magnetic flux

    // Electromagnetic radiation

    public static readonly UnitInfo Sievert   = new(103246, "Sv", Energy, SI); // Radioactivity
    public static readonly UnitInfo Gray      = new(190095, "Gy", Energy, SI);
    public static readonly UnitInfo Becquerel = new(102573, "Bq", Energy, SI); // Absorbed dose

    // Light
    public static readonly UnitInfo Illuminance  = new(194411, "lx", Dimension.Illuminance,  SI);
    public static readonly UnitInfo LuminousFlux = new(107780, "lm", Dimension.LuminousFlux, SI);


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