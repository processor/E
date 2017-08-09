Payment `Card protocol { 
  issuer   : Entity
  network  : Payment `Network
  chip?    : Card `Chip
  issued   : DateTime
}


Payment `Card record : Payment `Method {

}

  Charge  `Card
, Prepaid `Card 
, Debit   `Card 
, Credit  `Card record : Payment`Card

VISA            `Card,
MasterCard			 
Disover         `Card,
AmericanExpress `Card : Payment `Card