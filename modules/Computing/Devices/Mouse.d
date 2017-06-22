Mouse protocol {
  * attach   : watching
  * | move   : moving  
    | click  : clicking  
    ↺
  * detach ∎ : detached

  // what element is below?
  
  move  (Position) -> Mouse `Moved
  press (Button)   -> Mouse `Pressed 
}

  Left   `Button,
, Middle `Button,
, Right  `Button :
: Mouse  `Button type {

}

Mouse`Clicked event {
  // start
  // end
  button   : Mouse'Button
  position : Point
}


Mouse `Wheel event {
  delta: (x, y, z: Number)
}