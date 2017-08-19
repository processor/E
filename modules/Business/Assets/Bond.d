Bond protocol : Asset {
  * issue

  interest -> Percent/Period,
  maturity -> DateTime?  
  quantity -> Decimal
}

Bond actor : Asset {
  currency : Currency,
  quantity : Decimal
  price	   : Decimal.
  interest : Percent/Period,
  maturity : DateTime?  
}