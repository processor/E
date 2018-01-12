Offer protocol {
  * cancel                  : Canceled
  * execute (buyer: Entity) : Executed
}

Offer actor {
  selling  : Asset
  buying   : Asset
  quantity : decimal
  price    : Decimal
  expires  : DateTime?
}

// An offer to buy or sell an asset