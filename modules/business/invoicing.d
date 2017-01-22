using commerce

type Line = Sale | Adjustment | Fee | Service

Invoice record {
  amount : Money
  issuer : Entity
  terms  : [ ] Invoice`Term
  lines  : [ ] Line
}

Invoice protocal {
  * created     : created
  * | bill      : billed
    | pay       : paying 
    ↺
  * | close   ∎ : closed
    | abandon ∎ : abandoned

  create  ()                              -> Invoice
  pay     (Payment'Method, amount: Money) -> Payment
  bill    (recipient: Entity)             -> Bill
  close   ()                              -> Closure
  abandon ()                              -> Abandonment

  bills    -> [ ] Bill          // an invoice may be billed mutiple times 
  payments -> [ ] Transaction 
}

Bill event { 
  invoice   : Invoice
  recipient : Person      // e.g. Sue in accounting @ Google
}

// todo: Acceptance

Invoice `Closure event { } 
Invoice `Abandonment event { } 

Invoice `Adjustment type {
  description : String
  amount      : Money
}