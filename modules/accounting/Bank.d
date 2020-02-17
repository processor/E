Bank protocol { 
  * form       : active
  * act ↺      : acting
  * dissolve ∎ : dissolved

  // Actions
  open       ($0: Account)
  close      ($0: Account)
  settle     ($0: Charge) 
  refuse     ($0: Charge) 
  reverse    ($0: Charge) 
  underwrite ($0: Loan)      -> Underwriting
  accept     ($0: Deposit)

  transfer (source: Account, destination: Account, amount: Decimal) -> Transfer

  // Messages
  recieve($0: Charge)  -> Accepted | Refused
  recieve($0: Payment) -> Accepted | Refused
}

Bank actor {
  entity : Entity
  code   : String
}


// Central banks

// - May create or destroy money (demoninated in a urrency)

// A bank maintains accounts, underwrites, and services loans
