Processor protocol {
  authorize (account: Account) -> Authorization
  charge    (account: Account)
  dispute   (charge : Charge, reason: Reason) -> Dispute
}