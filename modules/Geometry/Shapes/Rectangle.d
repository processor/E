Rectangle<T: ℝ = f64> struct {
  width, height: T
}

Rectangle<T> protocol {
  height: T
  width: T
}

// implements Geometry