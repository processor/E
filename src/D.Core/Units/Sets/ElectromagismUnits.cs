namespace E.Units;

using static Dimension;
using static UnitFlags;

public static class ElectromagismUnits
{
    public static readonly UnitInfo Ampere    = new(25_272, "A",  ElectricCurrent, SI | Base);

    // Derived units --------------------------------------------------------------------------------------------------
    public static readonly UnitInfo Coulomb   = new(25_406,  "C",  ElectricCharge);                   // electrical charge
    public static readonly UnitInfo Ohm       = new(47_083,  "Ω",  ElectricResistance);               // electrical resistance
    public static readonly UnitInfo Farad     = new(131_255, "F",  Capacitance, SI);                  // electrical capacitance
    public static readonly UnitInfo Henry     = new(163_354, "H",  Inductance, SI);                   // electrical inductance
    public static readonly UnitInfo Joule     = new(25_269,  "J",  Energy);
    public static readonly UnitInfo Siemens   = new(169_893, "S",  ElectricConductance, SI);          // electrical conductance
    public static readonly UnitInfo Tesla     = new(163_343, "T",  MagneticFluxDensity);              // magnetic flux density
    public static readonly UnitInfo Volt      = new(25_250,  "V",  ElectricPotentialDifference, SI);  // electrical potential
    public static readonly UnitInfo Watt      = new(25_236,  "W",  Power);
    public static readonly UnitInfo Weber     = new(170_804, "Wb", MagneticFlux);                     // magnetic flux

    // Electromagnetic radiation

    public static readonly UnitInfo Sievert   = new(103_246, "Sv", Energy, SI); // Radioactivity
    public static readonly UnitInfo Gray      = new(190_095, "Gy", Energy, SI);
    public static readonly UnitInfo Becquerel = new(102_573, "Bq", Energy, SI); // Absorbed dose

    // Light
    public static readonly UnitInfo Illuminance  = new(194_411, "lx", Dimension.Illuminance,   SI);
    public static readonly UnitInfo LuminousFlux = new(107_780, "lm", Dimension.LuminousFlux, SI);


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
}