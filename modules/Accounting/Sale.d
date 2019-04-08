Sale protocol {
  * | tax               
  * | complete ∎ : completed

  amount = price * quantity

  tax () -> Sales `Tax  // called by tax authority at the point of sale
}

Sale event {
  seller   : Entity 
  buyer    : Entity
  object   : Product | Asset
  currency : Currency
  price		 : Decimal
  quantity : Decimal
}

// Sale from sellers pespective
// Purchase from buyers perspective

Purchase := Sale					       // reverse of a sale is a purchase
