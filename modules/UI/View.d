from Geometry import Rectangle

View protocol { 
  width  : Int32
  height : Int32

  screenPosition   : Rectangle<Int32>
  relativePosition : Rectangle<Int32>

  // realativeTo
}