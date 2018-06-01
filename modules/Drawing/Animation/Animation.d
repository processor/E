Animation protocol { 
  * started
  * ended ∎ : canceled

  start() : Started
}

Animation class {
  delay        :   Duration
  duration     :   Duration
  from         :   Float64 | Color
  to           :   Float64 | Color
  properties   : [ Tweenable ]
  direction    :   Forwards | Backwards
  interpolator :   Interpolator
}

Animation `Sequence { 
   animations: [ Animation ]
}

// transition x from 0 to 1 over 1s using algorithm while condition