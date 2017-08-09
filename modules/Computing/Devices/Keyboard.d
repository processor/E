Keyboard protocol {
  * attach    : attached
  * | press  
    | release 
    ↺
  * detach ∎  : detached
   
  press   (key: Keyboard `Key) -> Key `Press
  release (key: Keyboard `Key) -> Key `Release

  depressed -> [ Keyboard `Key ]
  capturing ->   Element
}

Keyboard `Key struct { 
  code: Int32
}

Key `Down event { 
  key: Key
}

Key `Up event {
  key: Key
}