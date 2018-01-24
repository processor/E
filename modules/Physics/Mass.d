Mass protocol { }

// # Metric
Milligram unit(symbol: "mg", value: 0.0001 g) : Mass { }
Centigram unit(symbol: "cg", value: 0.001 g)  : Mass { }
Gram      unit(symbol: "g",  value: 1 )       : Mass { }
Kilogram  unit(symbol: "kg", value: 1000 g)   : Mass { } // SI base...

// # Imperial
Pound     unit(symbol: "lb", value: 0.453592 kg) : Mass { } 
Stone     unit(symbol: "st", value: 14 lb)       : Mass { } 
Short`Ton unit(value: 2000 lb) : Mass { }
Long`Ton  unit(value: 2240 lb) : Mass { }


// - Examples
// Kilogram(5) 
// 5 kg
// 15 lb + 5 kg