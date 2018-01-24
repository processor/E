Ellipsoid<T: ℝ & Blittable = Float64> struct {
  majorAxis    : T
  minorAxis    : T
  eccentricity : T

  volume => 0
}