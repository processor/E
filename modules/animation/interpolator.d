cubicIn    ƒ(x: Float) => x * x * x
cubicOut   ƒ(x: Float) => ((x - 1) ** 3) + 1
linear     ƒ(x: Float) => x
sinIn      ƒ(x: Float) => sin(x * π * 0.5)
sinOut     ƒ(x: Float) => - (cos(π * x) / 2) + 0.5

Interpolator protocal = (x: Float) -> Float

// TimeInterpolator