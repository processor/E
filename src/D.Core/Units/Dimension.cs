namespace E.Units;

public enum Dimension
{
    None                        = 0,  // dimensionless

    // Primary dimensions ----------------------------- 2.1.1.x
    Length                      = 1,   // metre         m         .1
    Mass                        = 2,   // kilogram      kg        .2
    Time                        = 3,   // second        s         .3
    ElectricCurrent             = 4,   // ampere        A         .4
    ThermodynamicTemperature    = 5,   // kelvin        K         .5
    AmountOfSubstance           = 6,   // mole          mol       .6
    LuminousIntensity           = 7,   // candela       cd        .7

    // Secondary (derived) dimensions -------------------------
    Acceleration                = 11376,  //            a       m/s²
    Angle                       = 21,     // radian     rad
    Frequency                   = 11652,  // hertz      Hz
    Force                       = 11402,  // newton     N       kg/m/s²
    Pressure                    = 39552,  // pascal     Pa      force / area
    SolidAngle                  = 208476, // steradian  sr
    Volume                      = 39297,  //            V       m³

    // Electric
    Energy                      = 30,      // joule     zJ
    ElectricCharge              = 1111,    // coulomb   C
    ElectricPotentialDifference = 32,      // volt      V
    ElectricResistance          = 25358,   // ohm       Ω
    ElectricConductance         = 34,      // siemens   S
    Inductance                  = 35,      // henry     H
    Capacitance                 = 36,      // farad     F
    MagneticFlux                = 177831,  // weber     Wb
    MagneticFluxDensity         = 38,      // tesla     T
    Power                       = 39,      // watt      W       energy/time
                                           
    Illuminance                 = 194411,  // lux       lx          (= lm/m²)
    LuminousFlux                = 41,      // lumen     lm
                                         
    CatalyticActivity           = 1735592, // katal     kat

    Wavenumber                  = 192510, //            σ

    // CSS
    Resolution                  = 70,

    // Information
    AmountOfInformation         = 105666562
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

/*

// Secondary Units
a   = Acceleration,
L   = Volume,

j   = ElectricCurrentDensity

*/

// Luminance                = 1068, // candela per square meter  cd/m^2

// RefractiveIndex             = 1073,
// RelactivePermeability       = 1074,

// MagneticFieldStrength = 1065,
//                                       
// MolarEnergy                 = 1066,
// MolarEntropy                = 1067,
//                                     
// HeatCapacity
// ThermalConductivity
// SurfaceChargeDensity
// HeatFluxDensity
// SurfaceTension
// 
// Radiance
// RadiantIntensity