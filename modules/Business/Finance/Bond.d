Bond protocol {
  owner  -> Entity
  amount -> price * quantity
}

Bond : finance:Instrument {
  quantity  : Decimal
  price	    : Money.
  interest  : Percent,
  maturity  : DateTime?  
}

Conversion { 
   price      : Money,
   ratio      : Rational,
   cap        : Money,          //  actual ration based based on cap
   discount   : Money,
   expiration : DateTime?
}