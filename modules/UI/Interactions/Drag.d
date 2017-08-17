Drag struct { 
  element  : Element
  position : Vector2
}

Drag `Start event { 
  element: Element
}

Drag `End event { 

}

Drop event { 
  element  : Element
  position : Vector2
}

Drag protocol { 
   * start
   * drop
}