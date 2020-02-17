Loan protocol {
  * create    : active
  * pay ↺
  * forgive ∎ : forgiven                // aka Writeoff
  * close   ∎ : closed

  asset       ->    Asset              // e.g. USD, Gold
  balance     ->    Decimal
  payments    -> [] Loan`Payment
  signers     -> [] Signer
  collatoral  -> [] Collatoral
  underwriter ->    Entity
  processor   ->    Entity

  // - Events
  Default event { }
  Forgiven event { }
}

Loan process : Instrument { 
  owner	   :    Entity
  issued   :    DateTime
  issuer   :    Entity
  payments : [] Loan::Payment

  Payment record {
    interest  : Decimal
    principle : Decimal
  }

  Collatoral record {
    property    : Property
    quantity    : Decimal
    preference  : i64 ≥ 0
  }
}


Amortizable impl for Loan {

  
}