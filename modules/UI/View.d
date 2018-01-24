from Geometry import Rectangle

View protocol { 
  width  : Int32 of Length
  height : Int32 of Length

  screenPosition   : Rectangle<Int32>
  relativePosition : Rectangle<Int32>

  // realativeTo
}