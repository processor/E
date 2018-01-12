Frame<T: Color> class {
  height  : Int32 > 0
  width   : Int32 > 0
  planes  : [ Plane ]
}

// Planar Formats...

Plane struct {
  pixels  : [ Color ]
  stride  : Int32
}