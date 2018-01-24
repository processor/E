Matrix4x4<T: ℝ & Blittable> struct {
  elements: [ T ] // 4x4 = 16 elements

  [ index: i64 ] => elements[index]
  [ row: i64, column: i64 ] => elements[((row - 1) * 4) + (index - 1)]

  m11	=> this[0]  // 1, 1
  m12	=> this[4]  // 1, 2
  m13	=> this[8]  // 1, 3
  m14	=> this[12] // 1, 4
  m21	=> this[1]  // 2, 1
  m22	=> this[5]  // 2, 2
  m23	=> this[9]  // 2, 3
  m24	=> this[13] // 2, 4
  m31	=> this[2]  // 3, 1
  m32	=> this[6]  // 3, 2
  m33	=> this[10] // 3, 3
  m34	=> this[14] // 3, 4
  m41	=> this[3]  // 4, 1
  m42	=> this[7]  // 4, 2
  m43	=> this[11] // 4, 3
  m44	=> this[15] // 4, 4
  
  // based on http://www.euclideanspace.com/maths/algebra/matrix/functions/inverse/fourD/index.htm

  inverse ƒ() {
    let t11 = m23 * m34 * m42 - m24 * m33 * m42 + m24 * m32 * m43 - m22 * m34 * m43 - m23 * m32 * m44 + m22 * m33 * m44
    let t12 = m14 * m33 * m42 - m13 * m34 * m42 - m14 * m32 * m43 + m12 * m34 * m43 + m13 * m32 * m44 - m12 * m33 * m44
    let t13 = m13 * m24 * m42 - m14 * m23 * m42 + m14 * m22 * m43 - m12 * m24 * m43 - m13 * m22 * m44 + m12 * m23 * m44
    let t14 = m14 * m23 * m32 - m13 * m24 * m32 - m14 * m22 * m33 + m12 * m24 * m33 + m13 * m22 * m34 - m12 * m23 * m34

    let determinant = m11 * t11 + m21 * t12 + m31 * t13 + m41 * t14

    let detInv = 1 / determinant

    let els = T[16]

    els[0] = t11 * detInv;
    els[1] = (m24 * m33 * m41 - m23 * m34 * m41 - m24 * m31 * m43 + m21 * m34 * m43 + m23 * m31 * m44 - m21 * m33 * m44) * detInv
    els[2] = (m22 * m34 * m41 - m24 * m32 * m41 + m24 * m31 * m42 - m21 * m34 * m42 - m22 * m31 * m44 + m21 * m32 * m44) * detInv
    els[3] = (m23 * m32 * m41 - m22 * m33 * m41 - m23 * m31 * m42 + m21 * m33 * m42 + m22 * m31 * m43 - m21 * m32 * m43) * detInv

    els[4] = t12 * detInv;
    els[5] = (m13 * m34 * m41 - m14 * m33 * m41 + m14 * m31 * m43 - m11 * m34 * m43 - m13 * m31 * m44 + m11 * m33 * m44) * detInv
    els[6] = (m14 * m32 * m41 - m12 * m34 * m41 - m14 * m31 * m42 + m11 * m34 * m42 + m12 * m31 * m44 - m11 * m32 * m44) * detInv
    els[7] = (m12 * m33 * m41 - m13 * m32 * m41 + m13 * m31 * m42 - m11 * m33 * m42 - m12 * m31 * m43 + m11 * m32 * m43) * detInv

    els[8]  = t13 * detInv;
    els[9]  = (m14 * m23 * m41 - m13 * m24 * m41 - m14 * m21 * m43 + m11 * m24 * m43 + m13 * m21 * m44 - m11 * m23 * m44) * detInv
    els[10] = (m12 * m24 * m41 - m14 * m22 * m41 + m14 * m21 * m42 - m11 * m24 * m42 - m12 * m21 * m44 + m11 * m22 * m44) * detInv
    els[11] = (m13 * m22 * m41 - m12 * m23 * m41 - m13 * m21 * m42 + m11 * m23 * m42 + m12 * m21 * m43 - m11 * m22 * m43) * detInv

    els[12] = t14 * detInv;
    els[13] = (m13 * m24 * m31 - m14 * m23 * m31 + m14 * m21 * m33 - m11 * m24 * m33 - m13 * m21 * m34 + m11 * m23 * m34) * detInv
    els[14] = (m14 * m22 * m31 - m12 * m24 * m31 - m14 * m21 * m32 + m11 * m24 * m32 + m12 * m21 * m34 - m11 * m22 * m34) * detInv
    els[15] = (m12 * m23 * m31 - m13 * m22 * m31 + m13 * m21 * m32 - m11 * m23 * m32 - m12 * m21 * m33 + m11 * m22 * m33) * detInv

    return Matrix4<T>(elements: els)
  }

  fromScale (x: T, y: T, z: T) => Matrix4<T> {
    elements: [ 
      x, 0, 0, 0,
      0, y, 0, 0,
      0, 0, z, 0,
      0, 0, 0, 1 
    ] 
  }

  fromRotationX (theta: T) {
    let c = cos(theta)
    let s = sin(theta)

    return Matrix4<T> {
      elements: [ 
        1, 0,  0, 0,
        0, c, - s, 0,
        0, s,  c, 0,
        0, 0,  0, 1 
      ]
    }		
  }

  fromTranslation (x: T, y: T, z: T) => Matrix4 {
    elements: [
      1, 0, 0, x,
      0, 1, 0, y,
      0, 0, 1, z,
      0, 0, 0, 1
    ]
  }

  // inplace
  scale (v: Vector3) {
    var els = Array<T>.fill(16, 0)
    
		els[0] *= v.x; els[4] *= v.y; els[8]  *= v.z;
		els[1] *= v.x; els[5] *= v.y; els[9]  *= v.z;
		els[2] *= v.x; els[6] *= v.y; els[10] *= v.z;
		els[3] *= v.x; els[7] *= v.y; els[11] *= v.z;

    return Matrix4 { elements: els }
	}
}