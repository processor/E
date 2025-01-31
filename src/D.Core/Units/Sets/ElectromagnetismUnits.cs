namespace E.Units;

using static Dimension;
using static UnitFlags;

public static class ElectromagnetismUnits
{
    public static readonly UnitInfo Ampere    = new(UnitType.Ampere, ElectricCurrent, "A", flags: SI | Base);

    // Derived units -----------------------------------------------------------------------------------------
    public static readonly UnitInfo Coulomb   = new(UnitType.Coulomb, ElectricCharge,              "C");  // electrical charge
    public static readonly UnitInfo Ohm       = new(UnitType.Ohm,     ElectricResistance,          "Ω");  // electrical resistance
    public static readonly UnitInfo Farad     = new(UnitType.Farad,   Capacitance,                 "F");  // electrical capacitance
    public static readonly UnitInfo Henry     = new(UnitType.Henry,   Inductance,                  "H");  // electrical inductance
    public static readonly UnitInfo Joule     = new(UnitType.Joule,   Energy,                      "J");  
    public static readonly UnitInfo Siemens   = new(UnitType.Siemens, ElectricConductance,         "S");  // electrical conductance
    public static readonly UnitInfo Tesla     = new(UnitType.Tesla,   MagneticFluxDensity,         "T");  // magnetic flux density
    public static readonly UnitInfo Volt      = new(UnitType.Volt,    ElectricPotentialDifference, "V");  // electrical potential
    public static readonly UnitInfo Watt      = new(UnitType.Watt,    Power,                       "W");  
    public static readonly UnitInfo Weber     = new(UnitType.Weber,   MagneticFlux,                "Wb"); // magnetic flux

    // Electromagnetic radiation

    public static readonly UnitInfo Sievert   = new(UnitType.Sievert,   Energy, "Sv", flags: SI); // Radioactivity
    public static readonly UnitInfo Gray      = new(UnitType.Gray,      Energy, "Gy", flags: SI);
    public static readonly UnitInfo Becquerel = new(UnitType.Becquerel, Energy, "Bq", flags: SI); // Absorbed dose

    // Light
    public static readonly UnitInfo Illuminance  = new(UnitType.Illuminance,  Dimension.Illuminance, "lx",  flags: SI);
    public static readonly UnitInfo LuminousFlux = new(UnitType.LuminousFlux, Dimension.LuminousFlux, "lm", flags: SI);


    // Electric Properties
    // ElectricChargeDensity       // C/m3 
    // ElectricCurrentDensity      
    // ElectricConductance         

    // ElectricFieldStrength       
    // ElectricFluxDensity         
    // Permittivity                // farad/m
    // Permeability                // henry/m

    // Resistivity, // SI    ohm/metre

    // ElectricFlux,

    // Irradiance = 1501,     // watts per square meter (received)
    // Radiosity = 1502,      // watts per square meter (transmitted)
}