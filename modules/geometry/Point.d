let Point = Vector3  // type alias

parse ƒ String -> Point { 
  let parts = split(this, ',')

  return Point {
    x: Number.parse(parts[0])
    y: Number.parse(parts[1])
    z: parts.count == 3 ? Number.parse(parts[2]) : 0
  }
}