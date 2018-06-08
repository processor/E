namespace D.Units
{
    using static Dimension;
    using static UnitFlags;

    public static class ElectromagismUnits
    {
        public static readonly UnitInfo Ampere  = new UnitInfo("A",  ElectricCurrent, SI | Base);

        // Derived units ----------------------------------------------------------------------------------------------------------
        public static readonly UnitInfo Coulomb = new UnitInfo("C",  ElectricCharge);                   // electrical charge
        public static readonly UnitInfo Ohm     = new UnitInfo("Ω",  ElectricResistance);               // electrical resistance
        public static readonly UnitInfo Farad   = new UnitInfo("F",  Capacitance, SI);                  // electrical capacitance
        public static readonly UnitInfo Henry   = new UnitInfo("H",  Inductance, SI);                   // electrical inductance
        public static readonly UnitInfo Joule   = new UnitInfo("J",  Energy);
        public static readonly UnitInfo Siemens = new UnitInfo("S",  ElectricConductance, SI);          // electrical conductance
        public static readonly UnitInfo Tesla   = new UnitInfo("T",  MagneticFluxDensity);              // magnetic flux density
        public static readonly UnitInfo Volt    = new UnitInfo("V",  ElectricPotentialDifference, SI);  // electrical potential
        public static readonly UnitInfo Watt    = new UnitInfo("W",  Power);
        public static readonly UnitInfo Weber   = new UnitInfo("Wb", MagneticFlux);                     // magnetic flux


        // Electromagnetic radiation

        public static readonly UnitInfo Sievert   = new UnitInfo("Sv", Energy, SI); // Radioactivity
        public static readonly UnitInfo Gray      = new UnitInfo("Gy", Energy, SI);
        public static readonly UnitInfo Becquerel = new UnitInfo("Bq", Energy, SI); // Absorbed dose

        // Light
        public static readonly UnitInfo Illuminance  = new UnitInfo("lx", Dimension.Illuminance,   SI);
        public static readonly UnitInfo LuminousFlux = new UnitInfo("lm", Dimension.LuminousFlux, SI);




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