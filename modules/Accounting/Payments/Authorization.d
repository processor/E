Authorization protocol {
  * revoke   : revoked
  * cancel   : canceled
  * complete : 

  terms     : [] Term
  account   : Account 
  signature : Signature  // A signature from an account holder
}

Authorization record {
  account   : Account
  entity    : Entity
  amount    : Decimal
  signature : Signature
  expires   : DateTime?
}

// Authorizes and entity to debit a specific amount from an account


// This should be done through a contract term (amount / frequency)