Vector3 struct { 
  x, y, z: Number

  from (x, y, z: Number) => Vector3(x, y, z);
  from (x, y: Number)    => Vector3(x, y, z: 0);
  from (value: T)        => Vector3(x: value, y: value, z: value);

  [ index: i64 ] => match index { 
    0 => x
    1 => y
    2 => z
  }

  direction          => this / this.length       // aka unit vector : a vector of length 1
  length             => sqrt(this.dot(this))
  lengthSquared      => dot(this);

  * (value: Number)  => Vector3(x: x * value,   y: y * value,   z: z * value  );
  / (value: Number)  => Vector3(x: x / value,   y: y / value,   z: z / value  );
  + (value: Vector3) => Vector3(x: x + value.x, y: y + value.y, z: z + value.z);
  - (value: Vector3) => Vector3(x: x - value.x, y: y - value.y, z: z - value.z);
  
  negate ()          => Vector3(x: -x, y: -y, z: -z);

  cross (v: Vector3) => Vector3(x: y * v.z - z * v.y, y: z * v.x - x * v.z, z: x * v.y - y * v.x)

  transform (Matrix4x4) => Vector3(
    x: x * $0.m11 + y * $0.m21 + z * $0.m31 + $0.m41,
    y: x * $0.m12 + y * $0.m22 + z * $0.m32 + $0.m42,
    z: x * $0.m13 + y * $0.m23 + z * $0.m33 + $0.m43
  )

  clamp (min: Vector3, max: Vector3) => Vector3(
    x: max(min.x, min(max.x, this.x)),
    y: max(min.y, min(max.y, this.y)),
    z: max(min.z, min(max.z, this.z))
  )

  reflect (normal: Vector3) => this - (normal * this.dot(normal)) * 2

  floor () => Vector3(
    x: floor(x),
    y: floor(y),
    z: floor(z)
  )

  ceiling () => Vector3(
    x: ceiling(x),
    y: ceiling(y),
    z: ceiling(z)
  )

  round () => Vector3(
    x: round(x),
    y: round(y),
    z: round(z)
  )

  lerp (v: Vector3, alpha = 0) => Vector3(
		x: x + (v.x - this.x) * alpha
		y: y + (v.y - this.y) * alpha
		z: z + (v.z - this.z) * alpha
  )

  dot (v: Vector3) => this.x * v.x + this.y * v.y + this.z * v.z

	angle`To (point: Vector3) {
		let theta = this.dot(point) / sqrt(this.lengthSquared * point.lengthSquared)

		return acos(clamp(theta, -1, 1))
	}

  distance`To (point: Vector3) {
    let x = this.x - point.x
    let y = this.y - point.y
    let z = this.z - point.z

    return sqrt((x * x) + (y * y) + (z * z))
  }

  clone () => Vector3(x, y, z)

  to String => $"{x},{y},{z}";

  from String -> Vector3 {
    let parts = split(this, ',')

    return Vector3(
      x: Number.parse(parts[0])
      y: Number.parse(parts[1])
      z: parts.count == 3 ? Number.parse(parts[2]) : 0
    )
  }

}