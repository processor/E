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