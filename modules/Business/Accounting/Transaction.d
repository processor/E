Transaction protocol {
  * initiate    : pending
  * ? authorize : authorized 
  * | settle    : settled
    | cancel    : canceled
	  | refuse	  : refused
  * ? dispute   : disputed   
  * | lock    ∎ : locked
    | reverse ∎ : reversed
  
  initiate    () -> Transaction
  settle      () -> Settlement
  cancel      (reason?: Cancelation `Reason) -> Cancelation
  refuse      (reason?: Refusal `Reason)     -> Refusal
  lock        () -> Lock                                         // 180 days for credit card transactions or after an unsucessful dispute
  reverse     () -> Reversal

  source     -> Account
  
  signatures -> [ Signatures ]
  events     -> [ Events ] 
}

// A transaction atomically mutates a pair of Account ledgers (Credit | Debit)

  Account `Closed
  Authorization `Expired
  Unauthorized
, Insufficient `Funds 
: Refusal `Reason term

  Deposit
, Charge
, Refund
: Transaction record {
  from    : Account
  to      : Account
  amount	: Decimal
} 

// Transaction Operations ...

Transaction actor {

}

// Events ------------------------------------------

Settlement  event { }
Cancelation event { }
Refusal     event { }
Reversal    event { }