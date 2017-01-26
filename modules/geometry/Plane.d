Plane type {
  normal : Vector3
  d      : Number  // sometimes known as w
}

// : Clonable
Plane impl {
  from(normal: Vector3, d: Number) => Plane { normal, d }

  // from Points
  from (Vector3, Vector3, Vector3) {
    let n = (b - a).cross(c - a).direction

    return Plane { 
      normal : n, 
      d      : n.dot(a)
    }
  }

  normalize() {

  }

  transform (Matrix4) {

  }

  transform (Quaternion) {

  }

  project(point: Vector3) {
    
  }

  // Clonable
  clone () => Plane { normal: normal.clone(), d } 
}