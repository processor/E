Keyboard protocol {
  * attach    : attached
  * | press  
    | release 
    ↺
  * detach ∎  : detached
   
  press   (key: Keyboard::Key) emits Key::Press
  release (key: Keyboard::Key) emits Key::Release

  depressed -> [] Keyboard::Key
  capturing ->     Element
}

Keyboard actor {

  Key struct { 
    code: i32

    Down event { }
    Up event { }
  } 


}

