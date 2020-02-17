Mass protocol { }

// # Metric
Milligram unit(symbol: "mg", definition: 0.0001 g) : Mass { }
Centigram unit(symbol: "cg", definition: 0.001 g)  : Mass { }
Gram      unit(symbol: "g",  definition: 1 )       : Mass { }
Kilogram  unit(symbol: "kg", definition: 1000 g)   : Mass { } // SI base...

// # Imperial
Pound     unit(symbol: "lb", definition: 0.453592 kg) : Mass { } 
Stone     unit(symbol: "st", definition: 14 lb)       : Mass { } 
Short`Ton unit(              definition: 2000 lb)     : Mass { }
Long`Ton  unit(              definition: 2240 lb)     : Mass { }

// - Examples
// Kilogram(5) 
// 5 kg
// 15 lb + 5 kg