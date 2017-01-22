using commerce

Finance 'Instrument record { 
  holder: Entity
}

Bond : Instrument {
  quantity  : Decimal
  price	    : Money.
  interest  : Percent,
  maturity  : Moment?  
}

Bond `Conversion { 
   price      : Money,
   ratio      : Rational,
   cap        : Money,          //  actual ration based based on cap
   discount   : Money,
   expiration : Moment?
}

Bond protocal {
  owner  -> Entity
  amount -> price * quantity
}

NASDAQ_Stock_Exchange,
New_York_Stock Exchange : Stock_Exchange

// should allow 
// Stockholders of Entity / owner of shares


Loan : Finance_Instrument { 
  owner	   : Entity
  issued   : Moment
  issuer   : Entity
}

Loan protocal {
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

Loan `Collatoral record {
  @key loan  : Loan
  @key asset : Asset
  priority  : i64 >=0
}

CreditLine record {
  balance : Money
  limit   : Money
  issuer  : Entity
  owner   : Entity
}

Loan `Application {  }




