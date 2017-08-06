module "Taxation"

using commerce

  Income      `Tax	 
, Payroll     `Tax 
, Property    `Tax 
, Unemployment`Tax 
, Sales       `Tax    
: Tax record {
  amount    : Money
}

Rule : record {

}

Tax `Deduction record {
   rules: [ Rule ]
}

Authority record { 

}

// Juristriction?