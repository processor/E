Bond protocol {
  * issue

  owner  -> Entity
  amount -> price * quantity
}

Bond : Instrument {
  quantity  : Decimal
  price	    : Currency.
  interest  : Percent/Period,
  maturity  : DateTime?  
}