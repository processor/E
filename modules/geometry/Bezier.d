Bezier<T: ℝ & Blittable = Float64> struct : Curve { 
  c1: (x: T, y: T) // anchor point coordinates
  c2: (x: T, y: T) // first control point
  c3: (x: T, y: T) // second control point
  c4: (x: T, y: T) // second anchor point 

  getPoint (t: T) => (
    x: c1.x * b1(t) + c2.x * b2(t) + c3.x * b3(t) + c4.x * c4(t),
    y: c1.y * b1(t) + c2.y * b2(t) + c3.y * b3(t) + c4.y * b4(t)
  )
  
  private {
    b1 (t: T) => t * t * t
    b2 (t: T) => 3 * t * t * (1-t)
    b3 (t: T) => 3 * t * (1-t) * (1-t)
    b4 (t: T) => (1 - t) * (1 - t) * (1 - t)
  }
}

// # Usage
// canvas |> draw Bezier(
//   c1: (50, 50),
//   c2: (300, 50),
//   c3: (50, 300),
//   c4: (300, 300)
// )