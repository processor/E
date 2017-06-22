Bond protocol {
  owner  -> Entity
  amount -> price * quantity
}

Bond : Finance` Instrument {
  quantity  : Decimal
  price	    : Money.
  interest  : Percent,
  maturity  : DateTime?  
}

Bond `Conversion { 
   price      : Money,
   ratio      : Rational,
   cap        : Money,          //  actual ration based based on cap
   discount   : Money,
   expiration : DateTime?
}
