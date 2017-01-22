Bezier type { 
  c1: Vector3 // anchor point coordinates
  c2: Vector3 // first control point
  c3: Vector3 // second control point
  c4: Vector3 // second anchor point 
}

Curve impl for Bezier {
  getPoint (t: Float) => Vector2 (
    x: c1.x * b1(t) + c2.x * b2(t) + c3.x * b3(t) + c4.x * c4(t),
    y: c1.y * b1(t) + c2.y * b2(t) + c3.y * b3(t) + c4.y * b4(t)
  )

  b1 (t: Float) => t * t * t
  b2 (t: Float) => 3 * t * t * (1-t)
  b3 (t: Float) => 3 * t * (1-t) * (1-t)
  b4 (t: Float) => (1 - t) * (1 - t) * (1 - t)
}

// Usage

canvas |> draw Bezier { 
  c1: Vector3(50, 50),
  c2: Vector3(300, 50),
  c3: Vector3(50, 300),
  c4: Vector3(300, 300)
}