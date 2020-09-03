Sale protocol {         
  * | complete ∎ : completed
}

Sale process {
  seller   :    Entity 
  buyer    :    Entity
  currency :    Currency
  price		 :    Decimal

  // 
  quantity      :    Decimal
  jurisdictions : [] Jurisdiction   // used to determine tax
  terms         : [] Legal::Term
}


// Subject To
// Excise Tax
// Sales Tax
// by various taxation authorities (Jurisdictions) at the point of sale

Taxable impl for Sale {
  
}

// Sale from sellers pespective
// Purchase from buyers perspective

// A "sale" consists in the passing of title from the seller to the buyer for a price (Section 2-401). 