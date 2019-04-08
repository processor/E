Rectangle<T: ℝ = Float64> struct {
  width, height: T
}

Rectangle<T> protocol {
  height: T
  width: T
}

// implements Geometry