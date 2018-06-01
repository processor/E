Angle protocol { }

Radian    unit(symbol: "rad",  value: 1)            : Angle { }
Degree    unit(symbol: "deg",  value: (π/180) rad)) : Angle { }
Turn      unit(symbol: "turn". value: 360 deg)      : Angle { }
Radian    unit(symbol: "grad", value: (1/400) turn) : Angle { }
Arcsecond unit(symbol: "arcs", value: (1/3600) deg) : Angle { }
Arcminute unit(symbol: "arcm", value: (1/60) deg)   : Angle { }

// # Examples 
// 1 grad
// 90 deg
// 5 rad
// 90 deg to rad
// 1 turn to deg