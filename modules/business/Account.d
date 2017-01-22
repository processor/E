Account record {
  provider	: Entity
  creation	: Moment 
}

Account protocal {
  * open           : opened
  * | deposit
    | dispute
    ↺
  * close ∎ : closed

  open    ()       -> Account
  close   (Reason) -> Closure

  deposit (Check)               -> Deposit
  dispute (Transaction, Reason) -> Dispute    // if ruled in your favor, results in a reversal

  balance      ->     Money
  transactions -> [ ] Transaction
  signers      -> [ ] Signers
  trustee      -> [ ] Trustee
}

// beneficiaries