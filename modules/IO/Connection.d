Connection protocal : Channel { 
  * connect   : connecting   // send packet 
  * ack       : connected    // acknowledged. GTG 
  * send      : sending
  * receive   : receiving
  * close   ∎ : closed

  open   () -> * Connected    | Error
  close  () -> * Disconnected | Error
}