Drag struct { 
  element  : Element
  position : Vector2
}

Drag`Start event { 
  element: Element
}

Drag`End event { 

}

Drag protocol { 
   * start
   * drop
}