Charge protocol {
  * initiate     : initiated
  * ? authorize  : authorized 
  * | settle   ∎ : settled
    | cancel   ∎ : canceled
	  | refuse	 ∎ : refused

  currency      : Currency
  amount        : Decimal

  authorization :    Payment::Authorization
  source        :    Payment::Instrument | Account
  processor     :    Payment::Processor

  instrument    :    Payment::Instrument
  destination   :    Account
  events        : [] Event
  refunds       : [] Refund

  // Events -

  Settled  event { }
  Canceled event { }
  Refused  event { }
}


// Charges may fail
// Charges may be disputed

// Charges are deposited into the Merchant's Account


 // Credit card charges may be disputed for 180 days

// A payment instrument may be "charged"