namespace E.Units
{
    using static Dimension;
    using static UnitFlags;

    public static class ElectromagismUnits
    {
        public static readonly UnitInfo Ampere  = new ("A",  ElectricCurrent, SI | Base);

        // Derived units --------------------------------------------------------------------------------------------------
        public static readonly UnitInfo Coulomb   = new ("C",  ElectricCharge);                   // electrical charge
        public static readonly UnitInfo Ohm       = new ("Ω",  ElectricResistance);               // electrical resistance
        public static readonly UnitInfo Farad     = new ("F",  Capacitance, SI);                  // electrical capacitance
        public static readonly UnitInfo Henry     = new ("H",  Inductance, SI);                   // electrical inductance
        public static readonly UnitInfo Joule     = new ("J",  Energy);
        public static readonly UnitInfo Siemens   = new ("S",  ElectricConductance, SI);          // electrical conductance
        public static readonly UnitInfo Tesla     = new ("T",  MagneticFluxDensity);              // magnetic flux density
        public static readonly UnitInfo Volt      = new ("V",  ElectricPotentialDifference, SI);  // electrical potential
        public static readonly UnitInfo Watt      = new ("W",  Power);
        public static readonly UnitInfo Weber     = new ("Wb", MagneticFlux);                     // magnetic flux

        // Electromagnetic radiation

        public static readonly UnitInfo Sievert   = new ("Sv", Energy, SI); // Radioactivity
        public static readonly UnitInfo Gray      = new ("Gy", Energy, SI);
        public static readonly UnitInfo Becquerel = new ("Bq", Energy, SI); // Absorbed dose

        // Light
        public static readonly UnitInfo Illuminance  = new ("lx", Dimension.Illuminance,   SI);
        public static readonly UnitInfo LuminousFlux = new ("lm", Dimension.LuminousFlux, SI);


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
}