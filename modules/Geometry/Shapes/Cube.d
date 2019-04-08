Cube<T: ℝ = Float64> struct { 
  width  : T = 1
  height : T = 1
  length : T = 1 // depth?

  from (width: T, height: T, length: T) => Box(width, height, length)
}

// surfaceArea

Geometry impl for Cube { 
  faces { 

  }

  vertices {
   
  }
}


// Citations

// Definition
// In geometry, a cube[1] is a three-dimensional solid object bounded by six square faces, facets or sides, with three meeting at each vertex.
// All sides of a cube are of equal length but cuboid may have sides of different lengths.