Offering protocol { 
  asset    :    Asset
  quantity :    Decimal
  terms    : [] Legal::Term
}

// Terms include 
// - price
// - ...

Offering process { 
  series: String // Seed, A, B, ...
}
