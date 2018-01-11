from Geometry import Rectangle

View protocol { 
  width  : Int32 of Length
  height : Int32 of Length

  screenPosition   : Rectangle
  relativePosition : Rectangle

  // realativeTo
}