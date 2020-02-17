Frame<T: Color> class {
  height :    i32 > 0
  width  :    i32 > 0
  planes : [] Plane
}

// Planar Formats...

Plane struct {
  pixels  : [] Pixel
  stride  :    i32
}


