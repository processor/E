Mouse protocol {
  * attach   : watching
  * | move   : moving  
    | click  : clicking  
    ↺
  * detach ∎ : detached

  // what element is below?
  
  move  (Position) emits Wheel
  press (Button) emits Pressed

  // Events 

  Click event {
    // start
    // end
    button   : Mouse `Button
    position : vec2
  }

  Wheel event {
    delta: vec3
  }
}

  Left   `Button
, Middle `Button :
, Right  `Button :
: Mouse  `Button : Mouse `Button class { }