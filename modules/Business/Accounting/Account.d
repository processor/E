import Currency from Commerce 

Account protocol {
  * open           : opened
  * | deposit
    | dispute
    ↺
  * close ∎ : closed

  open    ()               -> Account
  close   (reason: Reason) -> Account `Closure

  deposit  (check: Check)                     -> Deposit
  dispute  (transaction: Transaction, Reason) -> Dispute    // if ruled in your favor, results in a reversal
  transfer (target: Account)                 -> Transaction

  asset        ->   Asset     // The asset held in the account (.e.g USD, Oranges, etc.)
  balance      ->   Decimal
  entries      -> [ Entry ]
  signers      -> [ Signer ]        // agents authorized to transact
  trustee      -> [ Trustee ]
  transactions -> [ Transaction ]
}

Account actor {
  provider : Entity
  balance  : Decimal 
}




// Are all signers authorized to make transactions?

// May an account hold mutiple assets?

// beneficiaries