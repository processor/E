Animation protocol { 
  * started
  * ended | canceled ∎
}

Animation class {
  delay        :   Duration
  duration     :   Duration
  from         :   Number | Color
  to           :   Number | Color
  properties   : [ Tweenable ]
  direction    :   Forwards | Backwards
  interpolator :   Interpolator
}


Animation `Sequence { 
   animations: [ Animation ]
}

// transition x from 0 to 1 over 1s using algorithm while condition