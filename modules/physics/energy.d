let ElementaryCharge  = 1.60217653e-19          // C

let ElectronMass      = 9.1093826e31 kg     
let SpeedOfLight      = 2.99792458e8 m/s
let ProtonMassUnit    = 1.67262171e-27     
let AtomicMassUnit    = 1.66053886e-27

// 1 Joule
// ~ lift 100g 1 meter vertically from earth
// ~ energy released when it falls to ground
// ~ accelatte 1kg mass 1 m/s though 1m of space
// ~ electricity required to light 1 watt LED for 1s
      
Joule unit : Energy { 
  symbol : "J"
  value  : 1 
}

Calorie unit : Energy {


}

ThermochemicalCalorie unit : Energy { value: 4.184 J }
TableCalorie          unit : Energy { value: 4.1868 J }

// 100,000 BTU 
BTU unit {

}

Therm unit { 


}
//  unit of energy equal to approximately 160 zeptojoules
// 1 volt (1 joule per coulomb, 1 J/C) * e (elementry charge)

ElectronVolt unit @symbol("eV") = ?J


meV,
keV,

MeV,
GeV,
TeV,
PeV,
EeV