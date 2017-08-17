Asset protocol {
  * purchase    : owned
  * writedown ↺ 
  * | sell    ∎ : sold
    | dispose ∎ : disposed

  purchase(
    seller :   Entity, 
    amount :   Money, 
    terms  : [ Terms ]
  ) -> Purchase        
  
  writedown (amount: Money) -> Writedown
  
  sell(
    buyer  :   Entity,  
    amount :   Money, 
    terms? : [ Terms ]
  ) -> Sale

  dispose () -> Asset `Disposal
  
  book `value	->   Money	
  writedowns  -> [ Writedown ]

  price => purchase.price
}

Asset record {
  purchase	             : Purchase
  depreciation `Schedule : Depreciation `Schedule	// An asset is written_down in steps according to it's schedule
}

Depreciation `Schedule record {
  interval : Interval				
  callback : (asset: Asset) -> Writedown		// percentage: 10%, fixed: $100
}