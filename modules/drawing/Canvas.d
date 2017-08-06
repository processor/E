Canvas type { 
  width  :   Number > 0
  height :   Number > 0
  pixels : [ Pixel ]
}

Canvas protocol { 
  draw (buffer: Image, position: Vector2) -> *Done
  draw (Circle,    at: Vector3)
  draw (Rectangle, at: Vector3)
  draw (Sphere,    at: Vector3)
  draw (Arc,       at: Vector3)
  draw (text: String, font: Font, size: i64, at: Vector3)
}