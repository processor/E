Refund protocol {
  currency : Currency
  amount   : Decimal
}

Refund process { 
  
}

Refund protocol {
  * initiate    : pending
  * ? authorize : authorized 
  * | settle    : settled
    | cancel    : canceled
	  | refuse	  : refused
  * ? dispute   : disputed   
  * | lock    ∎ : locked
    | reverse ∎ : reversed

  currency    : Currency.type
  transaction : Transaction
  amount      : Decimal
}

Refund process {


}
