Animation type {
  delay         : Duration
  duration      : Duration
  from          : Float | Color
  to            : Float | Color
  properties    : [ ] Tweenable
  direction     : Forwards | Backwards
  interpolator  : Interpolator
}

Animation protocal { 
  * started
  * ended | canceled

}
Animation `Sequence { 
   animations: [ ] Animation
}

transition x from 0 to 1 over 1s using algorithm while condition

Elastic { }