Payment`Card protocol : Payment`Instrument  { 
  issuer   : Entity
  network  : Payment `Network
  chip?    : Card `Chip
  issued   : DateTime
}

Payment`Card protocol : Payment`Instrument  {
  network     : Payment`Network
  transaction : [ Transaction ]
}

Charge  `Card actor : Payment`Card
Prepaid `Card actor : Payment`Card
Debit   `Card actor : Payment`Card 
Credit  `Card actor : Payment`Card