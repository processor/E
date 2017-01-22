using commerce, finance

Bank record {
  entity : Entity,
  code   : String
}

Bank protocal { 
  * | open      `Account       
    | close     `Account     
    | settle    `Transaction
    | refuse    `Transaction 
    | underwrite`Loan        
    | process   `Transaction 
    ↺            : acting     
  * dissolve ∎   : dissolved

  open       `Account     (Account)     -> Account
  close      `Account     (Account)     -> Account     `Closure
  settle     `Transaction (Transaction) -> Transaction `Settlement
  refuse     `Transaction (Transaction) -> Transaction `Refusal
  reverse    `Transaction (Transaction) -> Transaction `Reversed
  underwrite `Loan        (Loan)        -> Transaction `Underwriting
}

Asset record {
  purchase	             : Purchase
  depreciation'Schedule  : Depreciation`Schedule	// An asset is written_down in steps according to it's schedule
}

Asset protocal {
  * purchase    : owned
  * writedown ↺ 
  * | sell    ∎ : sold
    | dispose ∎ : disposed

  purchase(
    seller : Entity, 
    amount : Money, 
    terms  : [ ] Terms
  ) -> Purchase        
  
  writedown (amount: Money) -> Writedown
  
  sell(
    buyer  : Entity,  
    amount : Money, 
    terms? : [ ] Terms
  ) -> Sale

  dispose () -> Disposal
  
  book'value	-> Money	
  writedowns  -> [ ] Writedown

  price       => purchase.price
}

Asset `Writedown event {
  asset  : Asset
  amount : Money
}

Depreciation `Schedule record {
  interval : Interval				
  callback : (Asset) -> Asset'Writedown		// percentage: 10%, fixed: $100
}

Estate protocal { 
  accounts      -> [ ] Account      // Estate'Accounts -- CREATE TABLE k100452345 (m1 key long, m2 key long)
  assets        -> [ ] Asset
  beneficiaries -> [ ] Entity
}
