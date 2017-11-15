Connection protocol : Channel {
  * connect   : connecting   // send packet 
  * ack       : connected    // acknowledged. GTG 
  * send    
  * receive
  * close   ∎ : closed

  open   () -> * Connected    | Error
  close  () -> * Disconnected | Error
}


Connection actor {
  
}