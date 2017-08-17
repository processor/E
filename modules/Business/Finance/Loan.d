Loan protocol {
  * created
  * payment ↺
  * writeoff 
  * close ∎  : closed

  balance     ->   Money
  payments    -> [ Loan `Payment ]
  signers     -> [ Signer ]
  collatoral  -> [ Collatoral ]
  underwriter ->   Entity
  processor   ->   Entity
  default     ->   Loan `Default

  on defaulted {
     // ?
  }
}

Loan : Instrument { 
  owner	   : Entity
  issued   : DateTime
  issuer   : Entity
}

Loan `Payment : Transaction {
  loan     : Loan
  interest : Money
  principle: Money
}


Loan `Collatoral record {
  loan  @key : Loan
  asset @key : Asset
  priority   : i64 >=0
}

Loan `Application {  }