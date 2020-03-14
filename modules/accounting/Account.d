Account protocol {
  * open    : active
  * | transfer
    | authorize
    ↺ : acting
  * close ∎ : closed

  asset          : Asset
  balance        : Decimal

  entries        : [] Entry
  signers        : [] Signer                // agents authorized to transact
  authorizations : [] Authorization
  beneficiaries  : [] Beneficiary
  instruments    : [] Payment::Instrument  // An account may be Charged through Instruments
  liabilities    : [] Liability

  // A securities, or brokerage account may hold financial assets (securities) on behalf of the investor

  holdings       : [] Holding

  Entry {
    transaction    : Transaction
    sequenceNumber : i64
    source         : Account
    balance        : Balance
    quantity       : Decimal     // credit | debit
  }

  // - Events
  Closure event {
    reason: Reason
  }
}


// Cash
// Margin

// Transfers are processed by one or more intermediary Banks



// Stellar: Accounts control the access rights to balances.

// A Brockage account may have mutiple holdings

// Account Type
// - Assets
// - Libabilies
// - Income
// - Expenses
// - Capital


// Real accounts consist of all those accounts which are related to assets. 
// Intangible assets are also considered as Real Accounts.


// Nominal accounts consist of all those accounts which are related to expenses, losses, Income and Gains.




// An account may have one or more holdings
// A holding may be any type of Asset, including a currency or title to a Car, house, etc.

// Holding is the process of ownership (aqusition, amortization, disposal, etc)...



// A holding is a record of Account for an Asset
// e.g. 5 shares of AMD, 15 USD, 200 oranges
// The aggergate quantity of a particular asset (stock, bond, token, future, option, etc)

// Questions


// Accounts are created through banks

// Account types ...
// Asset
// Liability
// Income
// Expense
// Equity

Account actor {
  provider : Entity     // who holds the entity...
  balance  : Decimal 
}

// Deposits are made to banks -- which issue a credit to an account

// Are all signers authorized to make transactions?

// May an account hold mutiple assets?

// every account should have a sequenceNumber that's incrimented with each mutation

