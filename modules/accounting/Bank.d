Bank protocol { 
  * | open       `Account       
    | close      `Account     
    | settle     `Transaction
    | refuse     `Transaction 
    | underwrite `Loan        
    | process    `Transaction 
    ↺            : acting     
  * dissolve ∎   : dissolved

  open       `Account     (Account)     -> Account
  close      `Account     (Account)     -> Account`Closure
  settle     `Transaction (Transaction) -> Transaction`Settlement
  refuse     `Transaction (Transaction) -> Transaction`Refusal
  reverse    `Transaction (Transaction) -> Transaction`Reversed
  underwrite `Loan        (Loan)        -> Transaction`Underwriting

  deposit() -> Deposit

}

Bank actor {
  entity : Entity,
  code   : String
}

// A bank maintains accounts, underwrites, and services loans
// A payment processor may also act as an intermedary in a dispute


  Account `Closed
  Authorization `Expired
  Unauthorized
, Insufficient `Funds 
: Refusal `Reason term
