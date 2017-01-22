Payment `Method record {
  owner: Entity
}

Payment `Card record : Payment`Method {  

}

  Charge  `Card
, Prepaid `Card 
, Debit   `Card 
, Credit  `Card record : Payment`Card

VISA           `Card,
MasterCard			 
Disover        `Card,
AmericanExpress`Card : Payment_Card

Check : Payment_Method

Payment_Card protocal { 
  issuer   : Entity
  network  : Payment'Network
  chip?    :  Card`Chip
  issued   : Moment
}


Check protocal {
  signature -> Signature
  deposit   -> Deposit
}

// Automatated Clearing House

ACH_Transfer record : Payment`Method { 
  transaction: Transaction
}

Wire_Transfer record : Payment`Method {
  transaction: Transaction
}

// AHC, Swift, Visa, Mastercard, ...
Payment `Network   record 
Payment `Processor record

// may be credit or debit depending on whether the card has a line of credit

Authorization event { 
  amount    : Money
  signature : Digital`Signature | Drawn`Signature
  expires   : Moment
}

Digital`Signature record {
  data   : Blob
  device : Payment`Device
}

Drawn`Signature record {
  image: Image
}

Card `Chip record : Signing`Device {

};