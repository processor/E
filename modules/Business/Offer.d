Offer protocol {
  * cancel                  : canceled
  * execute (buyer: Entity) : executed
}



Offer actor {
  selling  : Asset
  buying   : Asset
  quantity : Decimal
  price    : Decimal
  expires  : DateTime?
}

// An offer to buy or sell an asset