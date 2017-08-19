Line struct @primitive { 
  start : Vector3
  end   : Vector3

  // TODO: 3d...
  // sqrt((x2 - x1)^2 + (y2 - y1)^2)
  length => sqrt(pow((end.Y - start.y), 2) + pow((end.X - start.x), 2));

}

Equatable impl for Line {
  == ƒ(lhs: Line, rhs: Line) => 
    lhs.start == rhs.line &&
    lhs.end   == rhs.end
}