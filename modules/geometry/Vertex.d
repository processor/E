Vertex type { 
  position : Vector3
  normal   : Vector3    // i.e. direction
  // color    : Color
}

Vertex impl {
  from (position: Vector3)                  => Vertex { position: normal: Vector3(0) }
  from (position: Vector3, normal: Vector3) => Vertex { position, normal }

  interpolate(other: Vertex, t: Float) => Vertex {
    position : position.lerp(other.position, t),
    normal   : normal.lerp(other.normal, t)
  }
}