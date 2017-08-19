Bond protocol : Asset {
  * issue

  interest -> Percent/Period,
  maturity -> DateTime?  
  quantity -> Decimal
}

Bond actor : Asset {
  asset    : Asset,  // e.g. USD, Gold
  quantity : Decimal
  price	   : Decimal.
  interest : Percent/Period,
  maturity : DateTime?  
}