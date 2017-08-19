import Currency from Commerce 

Account protocol {
  * open    : open
  * | transfer
    | authorize
    ↺
  * close ∎ : closed

  open    ()               -> Account
  close   (reason: Reason) -> Account `Closure

  transfer (amount: Decimal, target : Account) -> Transaction

  asset          ->   Asset           // e.g USD, Gold
  balance        ->   Decimal
  entries        -> [ Entry ]
  signers        -> [ Signer ]        // agents authorized to transact
  trustee        -> [ Trustee ]
  authorizations -> [ Authorization ]
}

Account actor {
  provider : Entity     // who holds the entity...
  balance  : Decimal 
}

// Deposits are made to banks -- which issue a credit to an account

// Are all signers authorized to make transactions?

// May an account hold mutiple assets?

// beneficiaries