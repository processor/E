Vector4 type { 
  x, y, z, w: Number
}

apply `Quaternion (q: Quaternion) {
    let ix =   q.w * x + q.y * z - q.z * this.y
    let iy =   q.w * y + q.z * x - q.x * this.z
    let iz =   q.w * z + q.x * y - q.y * this.x
    let iw = - q.x * x - q.y * y - q.z * this.z

    return Vector3 { 
      x: ix * q.w + iw * - q.x + iy * - q.z - iz * - q.y
      y: iy * q.w + iw * - q.y + iz * - q.x - ix * - q.z
      z: iz * q.w + iw * - q.z + ix * - q.y - iy * - q.x
    }
  }