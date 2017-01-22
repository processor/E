  Deposit
, Charge
, Refund
: Monetary_Transaction record {
  from  _account : Account
  to    _account	: Account
  amount		      : Money
} 

Monetary_Transaction protocal {
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

  Account_Closed
  Authorization_Expired
  Unauthorized
, Insufficient_Funds 
: Refusal_Reason term


// Events ------------------------------------------

  Settlement
, Cancelation
, Refusal
, Reversal
: event {
  transaction: Transaction
}
