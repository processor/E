Payment::Processor actor : Authority {
  initiate  ($0: Charge) -> Authorization
  authorize ($0: Charge)
  capture   ($0: Charge)
  settle    ($0: Charge)
  cancel    ($0: Charge)

  // Messages
  process   ($0: Chargeback) { 

  }
}

// braintree:charges/11
// stripe:chages/12341

// Charges are deposited in a Merchant account

// The processor works with the Issuing and Aquiring banks to initiate and settle charges made using a Payment::Instrument (check, card, etc.)

// Braintree provides clients with a merchant account and a payment gateway.


// [sender→sender bank → indirect agent of sender bank → direct agent of sender bank → central bank → direct agent of receiver bank → indirect agent of receiver bank → receiver bank → receiver(payee)]

// An acquiring bank (also known simply as an acquirer) is a bank or financial institution that processes credit or debit card payments on behalf of a merchant
// The acquiring bank enters into a contract with a merchant and offers it a merchant account. 

// Payments may be classified by the number of parties involved in a transaction. For example, a pre paid card transaction usually involves four parties (the purchaser, the seller, the issuing bank, and the acquiring bank)


// There are two types of payment methods; exchanging and provisioning. 
// Exchanging involves the use of money, comprising banknotes and coins. 
// Provisioning involves the transfer of money from one account to another, and involves a third party. 
// Credit card, debit card, cheque, money transfers, and recurring cash or ACH (Automated Clearing House) disbursements are all electronic payments methods.