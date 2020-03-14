module Payment {
  Authorization process { }
}


Payment record {
  payer       :   Entity
  payee       :   Entity
  amount      :   Decimal
  currency    :   Currency
  fees        : [ Payment::Fee ]
  refunds     : [ Refund ]
  processor   :   Payment::Processor
  instrument  :   Payment::Instrument          // The source
  destination :   Account
  for         :   Invoice | Security`Deposit
  charge      :   Charge?  // to the instrument
}



// Used to indicate the person or thing that something is sent or given to.


// A payment is created once a charge is completed

// A payment is the trade of value from one party (such as a person or company) to another for goods, or services, or to fulfill a legal obligation.

// Payment can take a large variety of forms. 
// Barter, the exchange of one good or service for another, is a form of payment.


// The most common means of payment involve use of money, cheque, or debit, credit or bank transfers. 
// Payments may also take complicated forms, such as stock issues or the transfer of anything of value or benefit to the parties.

// In US law, the payer is the party making a payment while the payee is the party receiving the payment. 
// In trade, payments are frequently preceded by an invoice

// Payments are made using an Instrument

// The payee may compromise on a debt, i.e., accept a part payment in full settlement of a debtor's obligation, or may offer a discount, 

// E.G: For payment in cash, or for prompt payment, etc. 
// On the other hand, the payee may impose a surcharge, for example, as a late payment fee, or for use of a certain credit card, etc.
