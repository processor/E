Option record {
  security   : Security.type
  quantity   : Decimal
  price      : Decimal // Strike Price
  expiration : DateTime
  terms      : [] Term
}

// Of a Stock
// Of a Future

// Payment
// Ratio
// Cap
// Discount
// Expiration


Exercisable impl for Option {
  exercise ƒ() { }
}

Expirable impl for Option {
  
}

// ISO  (incentived stock option: qualified)
// NSO  (non-qualified)
