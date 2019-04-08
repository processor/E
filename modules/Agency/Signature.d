Signature protocal {
  actor: Actor
}

// A signature may be drawn or provided through a digital device
Signature record {
  
}

Digital `Signature record {
  algorithm :   HMAC256 | HMAC512 // ...
  value     : [ byte ]
  device    :   Payment`Device    // instrument ? 
}


Drawn `Signature record {
  image: Image
}

// # Signature
// A signature may be used to consent or agree to something