using commerce

  Income      `Tax	 
, Payroll     `Tax 
, Property    `Tax 
, Unemployment`Tax 
, Sales       `Tax    
: Tax record {
  amount    : Money
}

Tax `Payment record { 
  authority : Entity
  amount    : Money
}


Tax `Rule {

}


Tax `Exemption record {

}


Tax `Deduction record {
   rules: [ ] Tax'Rule
}

// Juristriction?

Tax `Authority record { 

}