module "Finance"

from Commerce import Currency

Instrument record { 
  holder: Entity
}

// should allow 
// Stockholders of Entity / owner of shares

CreditLine record {
  balance : Currency
  limit   : Currency
  issuer  : Entity
  owner   : Entity
}