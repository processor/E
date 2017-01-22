Sale event {
  seller    : Entity 
  buyer     : Entity
  thing     : Product | Service | Asset
  price		  : Money
  quantity  : Decimal
}

Sale protocal {
  * | tax               
  * | complete ∎ : completed

  amount = price * quantity

  tax () -> Sales `Tax       // called by tax authority at the point of sale
}

Purchase := Sale					       // reverse of a sale is a purchase
Receipt  := Invoice	when closed  // friendly name for a paid invoice

// general instance

UPC type


// specific instance
Product record {
  name: String
  code: UPC
}