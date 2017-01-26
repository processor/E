Box type { 
  center : Point
  width  : Number
  height : Number
  length : Number 
}

Box impl { 
  from (width, height, length: Number) => Box { width, height, length }
}

Geometry impl for Box { 
  faces { 

  }

  vertices {
   
  }
}