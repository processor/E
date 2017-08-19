cubicIn  ƒ(x: Number) => x * x * x
cubicOut ƒ(x: Number) => ((x - 1) ** 3) + 1
linear   ƒ(x: Number) => x
sinIn    ƒ(x: Number) => sin(x * π * 0.5)
sinOut   ƒ(x: Number) => - (cos(π * x) / 2) + 0.5

Interpolator protocol = (x: Number) -> Number

// TimeInterpolator