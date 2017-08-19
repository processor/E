  Income      `Tax	 
, Payroll     `Tax 
, Property    `Tax 
, Unemployment`Tax 
, Sales       `Tax    
: Tax record {
  currency : Currency
  amount   : Decimal
}

Tax `Deduction record {
   rules: [ Rule ]
}

// Juristriction?