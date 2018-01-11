Ellipsoid<T: Numeric & Blittable = Float64> protocol {
  majorAxis    : T
  minorAxis    : T
  eccentricity : T
}

Ellipsoid<T: Numeric & Blittable = Float64> struct {
  majorAxis    : T
  minorAxis    : T
  eccentricity : T

  volume => 0
}