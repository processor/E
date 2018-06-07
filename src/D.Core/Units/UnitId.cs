namespace D.Units
{
    public enum UnitId
    {
        //                                                2.1.1.x
        Unknown                  = 0,   // future SI type (new dimension?)
        Length                   = 1,   // metre       m         .1
        Mass                     = 2,   // kilogram    kg        .2
        Time                     = 3,   // second      s         .3
        ElectricCurrent          = 4,   // ampere      A         .4
        ThermodynamicTemperature = 5,   // kelvin      K         .5
        AmountOfSubstance        = 6,   // mole        mol       .6
        LuminousIntensity        = 7,   // candela     cd        .7

        Angle                    = 20,  // radian       rad
        Frequency                = 21,  // hertz        Hz
        Force                    = 22,  // newton       N       kg/m/s^2
        Pressure                 = 23,  // pascal       Pa      force / area

        SolidAngle,                     // steradian    sr

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

        Illuminance,                    // lux          lx          (= lm/m2)
        LuminousFlux,                   // lumen        lm
        
        Radioactivity,                  // becquerel    Bq
        DoseEquivlant,                  // sievert      Sv
        AbsorbedDose,                   // gray         Gy

        CatalyticActivity,              // katal        kat

        Wavenumber,                     //              σ

        // CSS
        Resolution
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