import Currency from Commerce 

Account protocol {
  * open           : opened
  * | deposit
    | dispute
    ↺
  * close ∎ : closed

  open    ()       -> Account
  close   (Reason) -> Account `Closure

  credit ( ) -> Transaction
  debit  ( ) -> Transaction
  
  deposit (check: Check)                     -> Deposit
  dispute (transaction: Transaction, Reason) -> Dispute    // if ruled in your favor, results in a reversal

  balance      ->   Money
  transactions -> [ Transaction ]
  signers      -> [ Signer ]
  trustee      -> [ Trustee ]
}

Account record {
  provider : Entity
  creation : Timestamp 
}


// beneficiaries