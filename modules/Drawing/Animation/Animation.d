Animation protocol { 
  * started
  * ended ∎ : canceled

  start() : Started
}

Animation process {
  delay        :    Duration
  duration     :    Duration
  from         :    f64 | Color
  to           :    f64 | Color
  properties   : [] Tweenable
  direction    :    Forwards | Backwards
  interpolator :    Interpolator
}


// Sequence<Animation>

// transition x from 0 to 1 over 1s using algorithm while condition