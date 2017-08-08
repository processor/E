
Bond protocol {
  owner  -> Entity
  amount -> price * quantity
}

Bond : Instrument {
  quantity  : Decimal
  price	    : Money.
  interest  : Percent,
  maturity  : DateTime?  
}