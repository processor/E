Authority actor {
  calculate($0: Taxable) -> Tax::Liability | Inapplicable
  
  // Messages
  recieve(payment: Payment) { }
}




// US`Treasury


