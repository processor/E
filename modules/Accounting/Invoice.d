Line = Sale | Adjustment | Fee | Service

Receipt := Invoice when closed  // friendly name for a paid invoice

Invoice protocol {
  * create      : created
  * | bill      : billed
    | pay       : paying 
    ↺
  * | close   ∎ : closed
    | abandon ∎ : abandoned

  create  ()                                                -> Invoice
  pay     (instrument: Payment`Instrument, amount: Decimal) -> Payment
  bill    (recipient: Entity)                               -> Bill
  close   ()
  abandon ()

  bills    -> [ Bill ]           // an invoice may be billed mutiple times 
  payments -> [ Payment ] 
}

Invoice actor {
  amount :   Decimal
  issuer :   Entity
  terms  : [ Invoice`Term ]
  lines  : [ Line ]
}

Bill event {
  invoice   : Invoice
  recipient : Person      // e.g. Sue in accounting @ Google
}

// todo: Acceptance

Invoice`Adjustment struct {
  description : String
  amount      : Decimal
}