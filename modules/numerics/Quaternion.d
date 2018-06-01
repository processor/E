Quaternion<T: ℝ & Blittable = Float64> struct {
  w, x, y, z: T

  dot (v: Quaternion<T>) => x * v.x + y * v.y + z * v.z + w * v.w
  
  length => sqrt(x * x + y * y + z * z + w * w)

  lengthSquared => x * x + y * y + z * z + w * w

  normalize () {
    let ls = x * x + y * y + z * z + w * w

    let inN = 1.0 / sqrt(ls)

    return Quaternion(
      x: x * inN
      y: y * inN
      z: z * inN
      w: w * inN
    )
  }

  dot (rhs: Quaternion<T> rhs) => 
    this.x * rhs.x +
    this.y * rhs.y +
    this.z * rhs.z +
    this.w * rhs.w

  inverse() {
    let ls = x * x + y * y + z * z + w * w
    let invNorm = 1.0 / ls

    return Quaternion(
      x: -x * invNorm;
      y: -y * invNorm;
      z: -z * invNorm;
      w: w * invNorm;
    )
  }

  conjugate => Quaternion(
    x: -x
    y: -y
    z: -z
    w: w
  )

  // *
  multiplyBy (b: Quaternion<T>) => Quaternion(
    x: this.x * b.w + this.w * b.x + this.y * b.z - this.z * b.y,
		y: this.y * b.w + this.w * b.y + this.z * b.x - this.x * b.z,
		z: this.z * b.w + this.w * b.z + this.x * b.y - this.y * b.x,
		w: this.w * b.w - this.x * b.x - this.y * b.y - this.z * b.z
  )

  // spherical linear interpolation
  // https://en.wikipedia.org/wiki/Slerp
  slerp (rhs: Quaternion, amount: T) {
    let t = amount

    let cosOmega = this.x * rhs.x + this.y * rhs.y +
                   this.z * rhs.z + this.w * rhs.w

    let flip = cosOmega < 0

    if flip {
      cosOmega = -cosOmega;
    }

    let s1, s2: T

    if cosOmega > 1.0 - T.epsilon {
        // Too close, do straight linear interpolation.
        s1 = 1.0 - t;
        s2 = flip ? - t : t;
    }
    else {
      let omega = acos(cosOmega);
      let invSinOmega = (1 / sin(omega));

      s1 = sin((1.0 - t) * omega) * invSinOmega;
      s2 = flip
        ? - sin(t * omega) * invSinOmega
        :   sin(t * omega) * invSinOmega
    }

    return Quaternion(
      x: s1 * this.x + s2 * rhs.x
      y: s1 * this.y + s2 * rhs.y
      z: s1 * this.z + s2 * rhs.z
      w: s1 * this.w + s2 * rhs.w
    )
  }
}