  Deposit
, Charge
, Refund
: Monetary `Transaction record {
  from  account : Account
  to    account	: Account
  amount		      : Money
} 

Monetary `Transaction protocal {
  * initiate    : pending
  * ? authorize : authorized 
  * | settle    : settled
    | cancel    : canceled
	  | refuse	  : refused
  * ? dispute   : disputed   
  * | lock    ∎ : locked
    | reverse ∎ : reversed
  
  
  initiate () -> Transaction
  settle   () -> Settlement
  cancel   (reason?: Cancelation_Reason) -> Cancelation
  refuse   (reason?: Refusal_Reason)     -> Refusal
  lock     () -> Lock                                         // 180 days for credit card transactions or after an unsucessful dispute
  reverse  () -> Reversal
}

  Account `Closed
  Authorization `Expired
  Unauthorized
, Insufficient `Funds 
: Refusal `Reason term


// Events ------------------------------------------

  Settlement
, Cancelation
, Refusal
, Reversal
: event {
  transaction: Transaction
}
