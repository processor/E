Asset protocol {
  * purchase    : owned
  * writedown ↺ 
  * | sell    ∎ : sold
    | dispose ∎ : disposed

  purchase(
    seller  :   Entity, 
    price   :   (Currency, Decimal),
    quanity :   Decimal = 1, 
    terms   : [ Terms ]
  ) -> Purchase        
  
  sell(
    buyer    : Entity,  
    currency : Currency,
    price    : Decimal,
    quantity : Decimal, 
    terms? : [ Terms ]
  ) -> Sale

  writedown (amount: Decimal) -> Writedown

  dispose () -> Asset `Disposal
  
  book `Value	-> (Currency, Decimal)

  writedowns -> [ Writedown ]

  price => purchase.price
}

Asset record {
  purchase	             : Purchase
  depreciation `Schedule : Depreciation `Schedule	// An asset is written_down in steps according to it's schedule
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
