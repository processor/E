Stock`Option protocol {
  * exercise : exercised
  * forfit   : forfit

  holder        : Entity
  stock         : Stock
  strike`Price : Decimal  
  expires       : DateTime?
  maturity      : DateTime? 
}


// ISO  (incentived stock option: qualified)
// NSO  (non-qualified)