Triangle<T: Numeric & Blittable = Float64> struct {
  a: Vector3<T>
  b: Vector3<T>
  c: Vector3<T>

  area() {
    return 0
  }
}