Bezier<T: Numeric & Blittable = Float64> struct : Curve { 
  c1: Vector2<T> // anchor point coordinates
  c2: Vector2<T> // first control point
  c3: Vector2<T> // second control point
  c4: Vector2<T> // second anchor point 

  private b1 (t: Number) => t * t * t
  private b2 (t: Number) => 3 * t * t * (1-t)
  private b3 (t: Number) => 3 * t * (1-t) * (1-t)
  private b4 (t: Number) => (1 - t) * (1 - t) * (1 - t)

  getPoint (t: Number) => Vector2 (
    x: c1.x * b1(t) + c2.x * b2(t) + c3.x * b3(t) + c4.x * c4(t),
    y: c1.y * b1(t) + c2.y * b2(t) + c3.y * b3(t) + c4.y * b4(t)
  )
}

// Usage
// canvas |> draw Bezier { 
//   c1: Vector3(50, 50),
//   c2: Vector3(300, 50),
//   c3: Vector3(50, 300),
//   c4: Vector3(300, 300)
// }