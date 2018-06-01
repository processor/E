Connection protocol : Channel {
  * connect   : connecting   // send packet 
  * ack       : connected    // acknowledged. GTG 
  * send    
  * receive
  * close   ∎ : closed

  open   ƒ() -> * Connected    | Error
  close  ƒ() -> * Disconnected | Error
}


Connection actor {
  
}