Sale protocol {
  * | tax               
  * | complete ∎ : completed

  amount = price * quantity

  tax () -> Sales `Tax  // called by tax authority at the point of sale
}

Sale event {
  seller   : Entity 
  buyer    : Entity
  thing    : Product | Service | Asset
  currency : Currency
  price		 : Decimal
  quantity : Decimal
}

Purchase := Sale					       // reverse of a sale is a purchase
