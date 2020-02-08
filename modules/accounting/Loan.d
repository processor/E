Loan protocol {
  * created
  * pay ↺
  * writeoff 
  * close ∎  : closed

  asset       ->   Asset              // e.g. USD, Gold
  balance     ->   Decimal
  payments    -> [ Loan`Payment ]
  signers     -> [ Signer ]
  collatoral  -> [ Collatoral ]
  underwriter ->   Entity
  processor   ->   Entity
  default     ->   Loan`Default
}

Loan actor : Instrument { 
  owner	   :   Entity
  issued   :   DateTime
  issuer   :   Entity
  payments : [ Loan`Payment ]
}

Loan`Payment : Transaction {
  interest  : Decimal
  principle : Decimal
}

Collatoral record {
  asset       : Asset
  quantity    : Decimal
  preference  : i64 ≥ 0
}

Loan`Application {  }