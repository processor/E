namespace D.Units
{
    public enum Dimension
    {
        None                     = 0,  // aka dimensionless

        // Primary dimensions ----------------------------- 2.1.1.x
        Length                   = 1,   // metre       m         .1
        Mass                     = 2,   // kilogram    kg        .2
        Time                     = 3,   // second      s         .3
        ElectricCurrent          = 4,   // ampere      A         .4
        ThermodynamicTemperature = 5,   // kelvin      K         .5
        AmountOfSubstance        = 6,   // mole        mol       .6
        LuminousIntensity        = 7,   // candela     cd        .7

        // Secondary (derived) dimensions -------------------------
        Acceleration             = 20,  //              a       m/s²
        PlaneAngle               = 21,  // radian       rad
        Frequency                = 22,  // hertz        Hz
        Force                    = 23,  // newton       N       kg/m/s²
        Pressure                 = 24,  // pascal       Pa      force / area

        SolidAngle               = 25,  // steradian    sr

        // Electric
        Energy,                         // joule        J
        ElectricCharge,                 // coulomb      C
        ElectricPotentialDifference,    // volt         V
        ElectricResistance,             // ohm          Ω
        ElectricConductance,            // siemens      S
        Inductance,                     // henry        H
        Capacitance,                    // farad        F
        MagneticFlux,                   // weber        Wb
        MagneticFluxDensity,            // tesla        T
        Power,                          // watt         W       energy/time

        Illuminance,                    // lux          lx          (= lm/m²)
        LuminousFlux,                   // lumen        lm

        CatalyticActivity,              // katal        kat

        Wavenumber,                     //              σ

        // CSS
        Resolution,

        // Information
        AmountOfInformation
    }

    /*

    // Secondary Units
    a   = Acceleration,
    L   = Volume,
   
    j   = ElectricCurrentDensity

    */
}


// Implemented from NIST SPECIAL PUBLICATION 330 
// http://physics.nist.gov/Pubs/SP330/sp330.pdf


// Area             |  SI: square metre                   m^2
// DynamicViscosity |  pascal second                      Pa·s
// EnergyDensity    |  joule per cubic meter
// Density          |  SI: kilogram per cubic metre       kg/m^3  (AKA Mass Density)
// Momentum         |  mass distance/second
// Speed            |  metre per second
// Tension          |  newton meter
// Volume           |  cubic metre	                    liter