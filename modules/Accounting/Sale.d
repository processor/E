Sale protocol {
  * | tax              
  * | complete ∎ : completed


  tax () -> [] Sales`Tax  // called by the taxing authorities at the point of sale
}

Sale process {
  seller   :    Entity 
  buyer    :    Entity
  currency :    Currency
  price		 :    Decimal

  // 
  quantity :    Decimal
  location :    Place                // used to determine tax
  terms    : [] Legal::Term
}

// Sale from sellers pespective
// Purchase from buyers perspective

Purchase := Sale					       // reverse of a sale is a purchase

// Subject To
// Excise Tax
// Sales Tax


// A "sale" consists in the passing of title from the seller to the buyer for a price (Section 2-401). 