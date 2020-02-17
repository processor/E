Bond protocol : Asset {
  * issue

  interest -> Percent/Period,
  maturity -> DateTime?  
  quantity -> Decimal
}

Bond<TAsset> actor : Asset {
  quantity : Decimal
  price	   : Decimal.
  interest : Percent/Period,
  maturity : DateTime?  
}


// Bond<Gold> { quantity: 500 }