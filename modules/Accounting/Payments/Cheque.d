Cheque protocol : Payment::Instrument {
  amount    -> Decimal
  currency  -> Currency
  signature -> Signature // what are we agreeing too?
  clearing  -> Clearing
}

// We should generalize this to a Promise

// A cheque is a negotiable instrument instructing a financial institution 
// to pay a specific amount of a specific currency from a specified transactional 
// account held in the drawer's name with that institution