Plane struct : Clonable {
  normal : Vector3
  d      : f64      // sometimes known as w

  // from Points
  from (a: Vector3, b: Vector3, c: Vector3) {
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