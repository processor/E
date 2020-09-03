
Invoice protocol {
  * create      : created
  * | bill      : billed
    | pay       : paying 
    ↺ : processing
  * | close   ∎ : closed
    | forgive ∎ : forgiven
    | abandon ∎ : abandoned

  send    (recipient: Entity)
  close   ()
  forgive ()

  payments -> [] Payment 
}

Invoice process {
  currency :    Currency
  amount   :    Decimal
  issuer   :    Entity
  payments : [] Payment
  terms    : [] Legal::Term
  lines    : [] Sale | Tax | Adjustment

  // Messages
  recieve($0: Payment) -> Accepted | Refused { 

  }

  Line = Sale | Adjustment | Fee | Service
}


// Specifies how to pay the invoice
Payable impl for Invoice {
  

}

// An invoice is paid through a payment processor




// WIKIPEDIA: An invoice, bill or tab is a commercial document issued by a seller to a buyer, 
//            relating to a sale transaction and indicating the products, quantities, and 
//            agreed prices for products or services the seller had provided the buyer.