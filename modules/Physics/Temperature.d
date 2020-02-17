Temperature protocol { }

Kelvin     unit(symbol: "K")                                         : Temperature { }
Celsius    unit(symbol: "°C",  defination: ($0 + 273.15) K)          : Temperature { }
Delisle    unit(symbol: "°De", defination: (373.15 − °De * (2⁄3) K)) : Temperature { }

Fahrenheit unit(symbol: "°F")  : Temperature { }
Rankine    unit(symbol: "°R")  : Temperature { }
