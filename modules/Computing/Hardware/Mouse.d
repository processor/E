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

  Left   `Button
, Middle `Button :
, Right  `Button :
: Mouse  `Button : Mouse `Button class { }

Mouse`Clicked event {
  // start
  // end
  button   : Mouse `Button
  position : Vector3<Number>
}

Mouse `Wheel event {
  delta: Vector3<Number>
}