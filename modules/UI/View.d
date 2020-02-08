from Geometry import Rectangle

View protocol { 
  width  : i32
  height : i32

  screenPosition   : Rectangle<i32>
  relativePosition : Rectangle<i32>

  // realativeTo
}