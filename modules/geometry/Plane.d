Plane<T: ℝ = Float64> struct : Clonable {
  normal : (x: T, y: T, z: T)
  d      : T              // also known as w | constant

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

  transform (quaterion: Quaternion<T>) {

  }

  project(point: (x: T, y: T, z: T)) {
    
  }

  // Clonable
  clone () => Plane { normal: normal.clone(), d } 
}