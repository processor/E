Asset protocol {
  * purchase    : owned
  * writedown ↺ 
  * | sell    ∎ : sold
    | dispose ∎ : disposed

  buy(
    seller   : Entity, 
    currency : Currency, 
    amount   : Decimal,
    quantity : Decimal = 1, 
    terms    : [ Legal::Term ]
  ) -> Purchase        
  
  sell(
    buyer    : Entity,  
    currency : Currency, 
    amount   : Decimal,
    price    : Decimal,
    quantity : Decimal = 1, 
    terms? : [ Legal::Term ]
  ) -> Sale

  writedown (amount: Decimal) -> Writedown

  dispose () -> Asset`Disposal
  
  book`value -> (Currency, Decimal)

  writedowns -> [ Writedown ]

  price => purchase.price
}

// identities
// issuer
// manufacturer

Asset record {
  
}

// Purchase (price)

// An asset is written down in scheduled steps
Depreciation`Schedule {
  asset: Asset
}

// Move offers to exchange?

// Tangible
// Intangible

// Cash (fiat currency)
// Land & Property

// Finance assets (aka securities) include:
// - Loans
// - Bonds (Corporate, Government, Muncipal)
// - Equity
// - Mutal Fund
// - Stock Option
// - Future Option
