Offer protocol {
  * cancel : Canceled
  * execute : Executed
}

Offer actor {
  asset    : Asset
  quantity : decimal
  price    : Decimal
  expires  : DateTime?
}

// An offer to buy or sell an asset