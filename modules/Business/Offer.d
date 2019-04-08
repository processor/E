from Legal import Term

Offer protocol {
  * cancel                  : canceled
  * execute (buyer: Entity) : executed
}

Offer actor {
  selling  : Asset
  buying   : Asset
  quantity : Decimal
  price    : Decimal
  terms    : [ Term ]
  expires  : DateTime?
}

// An offer to buy or sell an asset
// wikipedia: A proposal to sell or buy a specific product or service under specific conditions

// an offer price, or ask price, the price a seller is willing to accept for a particular good


// In law:
// Offer and acceptance, elements of a contract