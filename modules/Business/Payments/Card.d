Payment `Card protocol { 
  issuer   : Entity
  network  : Payment `Network
  chip?    : Card `Chip
  issued   : DateTime
}


Payment `Card actor : Payment `Method {
  transaction : [ Transaction ]
}

  Charge  `Card
, Prepaid `Card 
, Debit   `Card 
, Credit  `Card record : Payment`Card

VISA            `Card,
MasterCard			 
Disover         `Card,
AmericanExpress `Card : Payment `Card