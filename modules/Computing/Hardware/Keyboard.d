Keyboard protocol {
  * attach    : attached
  * | press  
    | release 
    ↺
  * detach ∎  : detached
   
  press   ($0: Keyboard::Key) emits Key::Press
  release ($0: Keyboard::Key) emits Key::Release

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

