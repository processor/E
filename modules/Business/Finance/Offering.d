Offering protocol { 
  asset    ->   Finance::Instrument
  quantity ->   Decimal
  terms    -> [ Legal::Term ]
}

// Terms include 
// - Price
// - ...

Offering record { 
  series: string // Seed, A, B, ...
}