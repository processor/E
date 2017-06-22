Loan protocol {
  * created
  * payment ↺
  * paidoff ∎

  balance      -> Money
  payments     -> [ ] Loan_Payments
  signers      -> [ ] Signer
  collatoral   -> [ ] Collatoral
  underwriter  -> Entity
  processor    -> Entity
  default      -> Loan 'Default

  on defaulted {
     // ?
  }
}

Loan `Payment event {
  loan     : Loan
  interest : Money
  principle: Money
}


Loan : Finance `Instrument { 
  owner	   : Entity
  issued   : DateTime
  issuer   : Entity
}


Loan `Collatoral record {
  @key loan  : Loan
  @key asset : Asset
  priority  : i64 >=0
}

Loan `Application {  }