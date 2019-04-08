Canvas protocol {
  draw (buffer : Image, position: Vector2) -> *Done
  draw (shape  : Drawable)
}

Canvas class {
  width  :   Int32 > 0
  height :   Int32 > 0
  pixels : [ Pixel ]
}

Drawable impl for Circle {
  draw(canvas: Canvas) {
    // TODO
  }
}

Drawable impl for Rectangle {
  draw(canvas: Canvas) {
    // TODO
  }
}


// Layers?