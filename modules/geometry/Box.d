Box<T: ℝ & Blittable = Float64> struct { 
  width  : T
  height : T
  length : T 

  from (width: T, height: T, length: T) => Box(width, height, length)
}

Geometry impl for Box { 
  faces { 

  }

  vertices {
   
  }
}