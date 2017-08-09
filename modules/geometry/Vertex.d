Vertex struct {
  position : Vector3
  normal   : Vector3 // aka direction

  from (position: Vector3)                  => Vertex { position: normal: Vector3(0) }
  from (position: Vector3, normal: Vector3) => Vertex { position, normal }

  interpolate(other: Vertex, t: Number) => Vertex {
    position : position.lerp(other.position, t),
    normal   : normal.lerp(other.normal, t)
  }
}