Vertex struct {
  position : Vector3(0)
  normal   : Vector3(0) // aka direction

  from (position: Vector3) => Vertex(position, Vector(0))
  
  interpolate (other: Vertex, t: Number) => Vertex {
    position : position.lerp(other.position, t),
    normal   : normal.lerp(other.normal, t)
  }
}