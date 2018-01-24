Line<T: ℝ & Blittable = Float64> struct : Blittable, Equatable<Self> { 
  start : Vector3<T>
  end   : Vector3<T>

  // TODO: 3d...
  // sqrt((x2 - x1)^2 + (y2 - y1)^2)
  length => sqrt(pow((end.Y - start.y), 2) + pow((end.X - start.x), 2));

  == ƒ(other: Self) => 
    start == other.line &&
    end   == other.end;
}