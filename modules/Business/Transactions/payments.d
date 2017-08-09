Payment `Method record {
  owner: Entity
}

Check : Payment `Method

Check protocol {
  signature -> Signature
  deposit   -> Deposit
}


// AHC, Swift, Visa, Mastercard, ...
Payment `Network   record 
Payment `Processor record

// may be credit or debit depending on whether the card has a line of credit

Payment `Authorization event { 
  amount    : Money
  signature : Digital`Signature | Drawn`Signature
  expires   : DateTime
}

Digital `Signature record {
  data   : Blob
  device : Payment`Device
}

Drawn `Signature record {
  image: Image
}

Card `Chip record : Signing`Device {

}