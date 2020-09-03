Bond protocol : Security {
  terms: [ ] Term
  maturity -> DateTime?  
  quantity -> Decimal

  // Terms include

  // Interest (Percent / Period)
}

Bond actor : Asset {
  security : Security.type
  quantity : Decimal
  price	   : Decimal.
  interest : Percent/Period,
  maturity : DateTime?  
}


// Bond<Gold> { quantity: 500 }