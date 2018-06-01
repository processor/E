Length protocol { } 

// # Metric units
Centimeter unit(symbol: "cm",    value: 0.001 m) : Length { }
Meter      unit(symbol: "m",     value: 1)       : Length { }
Kilometer  unit(symbol: "km",    value: 1000 m)  : Length { }

// # Imperial units
Inch       unit(symbol: "inch", value: 0.0254 m) : Length { }
Foot       unit(symbol: "ft",   value: 12 inch)  : Length { }
Yard       unit(symbol: "yd",   value: 3 ft)     : Length { }
Mile       unit(symbol: "mi")   value: 5280 ft)  : Length { }