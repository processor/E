Sale protocol {         
  * | complete ∎ : completed
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


// Subject To
// Excise Tax
// Sales Tax
// by various taxation uthorities at the point of sale

Taxable impl for Sale {
  
}

// Sale from sellers pespective
// Purchase from buyers perspective

// A "sale" consists in the passing of title from the seller to the buyer for a price (Section 2-401). 