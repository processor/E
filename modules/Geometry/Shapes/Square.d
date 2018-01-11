Square<T: Numeric & Blittable = Float64> struct {
  length: T

  height => length
  width  => length
  area   => width * height
}

// implements Geometry