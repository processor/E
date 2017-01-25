using commerce

Finance 'Instrument record { 
  holder: Entity
}

// should allow 
// Stockholders of Entity / owner of shares




CreditLine record {
  balance : Money
  limit   : Money
  issuer  : Entity
  owner   : Entity
}




