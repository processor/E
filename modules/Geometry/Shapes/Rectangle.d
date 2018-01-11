Rectangle<T: Numeric & Blittable = Float64> struct {
  height : T
  width  : T
}

Rectangle<T> protocol {
  height: T
  width: T
}

// implements Geometry