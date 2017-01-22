Box type { 
  center : Point
  width  : Float
  height : Float
  length : Float 
}

Box impl { 
  from (width, height, length: Float) => Box { width, height, length }
}

Geometry impl for Box { 
  faces { 

  }

  vertices {
   
  }
}