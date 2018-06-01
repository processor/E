Tax record {
  currency : Currency
  amount   : Decimal
}

Income       `Tax record : Tax { }	 
Payroll      `Tax record : Tax { } 
Property     `Tax record : Tax { } 
Unemployment `Tax record : Tax { } 
Sales        `Tax record : Tax { }   

Tax `Deduction record {
  rules: [ Rule ]
}

// Juristriction?