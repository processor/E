Vector3<T: ℝ & Blittable = f64> struct { 
  x: T = 0
  y: T = 0
  z: T = 0

  [ index: Byte ] => match index { 
    0 => x
    1 => y
    2 => z
  }

  direction => this / length
  length => √(dot(this))
  lengthSquared => dot(this)

  * (other: T)    => Vector3(x: x * other.x, y: y * other.y, z: z * other.z);
  / (other: T)    => Vector3(x: x / other.x, y: y / other.y, z: z / other.z);
  + (other: Self) => Vector3(x: x + other.x, y: y + other.y, z: z + other.z);
  - (other: Self) => Vector3(x: x - other.x, y: y - other.y, z: z - other.z);
  
  negate () => Vector3(x: -x, y: -y, z: -z);

  cross (other: Self) => Vector3(
    x: y * other.z - z * other.y, 
    y: z * other.x - x * other.z, 
    z: x * other.y - y * other.x
  )

  transform (matrix: Matrix4x4<T>) => Vector3(
    x: x * matrix.m11 + y * matrix.m21 + z * matrix.m31 + matrix.m41,
    y: x * matrix.m12 + y * matrix.m22 + z * matrix.m32 + matrix.m42,
    z: x * matrix.m13 + y * matrix.m23 + z * matrix.m33 + matrix.m43
  )

  clamp (min: Self, max: Self) => Vector3(
    x: max(min.x, min(max.x, this.x)),
    y: max(min.y, min(max.y, this.y)),
    z: max(min.z, min(max.z, this.z))
  )

  reflect (normal: Self) => this - (normal * dot(normal)) * 2

  floor   () => Vector3(floor(x), floor(y), floor(z))
  ceiling () => Vector3(ceiling(x), ceiling(y), ceiling(z))
  round   () => Vector3(round(x), round(y), round(z))

  lerp (v: Self, alpha: T = default) => Vector3(
		x: x + (v.x - this.x) * alpha
		y: y + (v.y - this.y) * alpha
		z: z + (v.z - this.z) * alpha
  )

  dot (v: Self) => x * v.x + y * v.y + z * v.z

	angle`To (other: Self) {
		let theta = dot(other) / √(lengthSquared * other.lengthSquared)

		return acos(clamp(theta, -1, 1))
	}

  distance`To (other: Self) {
    let x = this.x - other.x
    let y = this.y - other.y
    let z = this.z - other.z

    return √((x * x) + (y * y) + (z * z))
  }

  clone () => Vector3<T>(x, y, z)

  to => String($"{x},{y},{z}")

  from (text: String) -> Self {
    let parts = split(text, ',')

    return Vector3(
      x: T.parse(parts[0])
      y: T.parse(parts[1])
      z: parts.count == 3 ? T.parse(parts[2]) : 0
    )
  }

  from (value: T) {
    x = value
    y = value
    z = value
  }

}


// sum       (Vector)
// 
// 
// length      (Vector, Vector)
// distance    (Vector, Vector)
// dot         (Vector, Vector)
// cross       (Vector, Vector)      // Cross product between two vectors
// normalize   (Vector, Vector)
// reflect     (Vector, Vector)
// refract     (Vector, Vector)
// faceforward (Vector, Vector)

// Geometric Functions

// <
// <=
// >
// >=
// ==
// != 

// matrixCompMult


// add
// subtract
// clone
// sum (of all elements)

// radians from degrees ()
// degrees from radians ()
