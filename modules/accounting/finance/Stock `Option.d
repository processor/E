Stock`Option protocol {
  * exercise : exercised
  * forfit   : forfit

  holder   : Entity
  stock    : Stock
  price    : Decimal  // Strike Price
  expires  : DateTime?
  maturity : DateTime? 
}

// ISO  (incentived stock option: qualified)
// NSO  (non-qualified)