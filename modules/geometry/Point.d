let Point = Vector3  // type alias

parse ƒ String -> Point { 
  let parts = split($0, ',')

  return Point {
    x: Float.parse(parts[0])
    y: Float.parse(parts[1])
    z: parts.count == 3 ? Float.parse(parts[2]) : 0
  }
}