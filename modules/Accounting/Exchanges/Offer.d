Offer protocol {
 * created      : open
 * cancel     ∎ : canceled
 * execute () ∎ : executed
}

Offer<TBuying, TSelling> process {
  seller     : Actor?
  buyer      : Actor?
  buying     : TBuying  // B
  selling    : TSelling // A
  quantity   : Decimal  // of TBuying
  price      : Decial   // in TSelling
  terms      : [] Legal::Term
  expiration : DateTime?

  execute ƒ(buyer: Entity, seller: Entity, clearinghouse: Clearinghouse) { }
}

Orderbook repository {

}


// Execution is the transaction whereby the seller agrees to sell and the buyer agrees to buy a security in a legally enforceable transaction.
//  Thereafter, all the processes that lead up to settlement is referred to as clearing, such as recording the transaction. Settlement is the actual exchange of money, or some other value, for the securities.

// Any transfer of financial instruments, such as stocks, in the primary or secondary markets involves 3 processes:

// execution
// clearing
// settlement


// An order is an instruction to buy or sell on a trading venue such 
// as a stock market, bond market, commodity market, financial derivative market or cryptocurrency exchange. 


// An offer may be "accepted" / or "excecuted"


// An offer to buy or sell an asset
// wikipedia: A proposal to sell or buy a specific product or service under specific conditions

// an offer price, or ask price, the price a seller is willing to accept for a particular good


// In law:
// Offer and acceptance, elements of a contract



// Also know as a "bid" order on an orderbook


// An order is an instruction to buy or sell on a trading venue such as a stock market, bond market, commodity market, 
// financial derivative market or cryptocurrency exchange. These instructions can be simple or complicated, and can be sent to either a broker or directly to a trading venue via direct market access. T
