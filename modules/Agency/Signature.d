Signature protocal {
  actor: Actor
}


// A signature may be drawn 
// or be made through a digital device
Signature record {
  
}


Digital `Signature record {
  algorithm :   HMAC256 | HMAC512 // ...
  value     : [ byte ]
  device    :   Payment`Device
}