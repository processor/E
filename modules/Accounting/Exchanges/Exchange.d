Exchange<TSource: Asset, TTarget: Asset> protocol { 
  orders -> [] Order
}

Exchange<TSource, TTarget> actor {
  clearinghouse : Clearinghouse
  orders        : [] Order

  place ƒ(order: Order) { }
}

Quote<TSource, TTarget> {
  ask: Decimal
  bid: Decimal
}

// The international standard ISO 10383 (Market Identifier Code – MIC) specifies a universal method of identifying exchanges,
// trading platforms and regulated or non-regulated markets as sources of prices and related information in order to facilitate automated processing.

// Codes for exchanges and market identification (MIC)



// Stock exchanges, such as the New York Stock Exchange (NYSE) and the NASDAQ, use clearing firms