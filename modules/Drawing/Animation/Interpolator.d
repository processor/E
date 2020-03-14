Interpolator protocol = (x: T) -> T



cubicIn  ƒ(x: ℝ) => x * x * x
cubicOut ƒ(x: ℝ) => ((x - 1) ** 3) + 1
linear   ƒ(x: ℝ) => x
sinIn    ƒ(x: ℝ) => sin(x * π * 0.5)
sinOut   ƒ(x: ℝ) => - (cos(π * x) / 2) + 0.5
