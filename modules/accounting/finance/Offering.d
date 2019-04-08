Offering protocol { 
  asset    :   Finance::Instrument
  quantity :   Decimal
  terms    : [ Legal::Term ]
}

// Terms include 
// - price
// - ...

Offering record { 
  series: String // Seed, A, B, ...
}
