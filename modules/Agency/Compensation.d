Compensation protocol { 
  
}

Salary struct : Compensation {
  currency : Currency
  amount   : Decimal
  interval : Interval
}

Fixed : Compensation {
  currency : Currency
  amount   : Decimal
}