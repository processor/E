module "Finance"

import Money from Commerce

Instrument record { 
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