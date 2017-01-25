Stock : Finance_Instrument {
  entity   : Entity
  holder   : Entity
  shares   : Decimal
  issued   : DateTime
}

Stock protocal { 
  * aquire    : held
  * | split ∎ : split
    | sell  ∎ : sold

  // aquire
  // split            split to be able to sell a smaller part
  // sell
}

Stock_Acquisition event {

}

Stock_Sale event {

}

  Google_Stock_Preferred_A_1
, Google_Stock_Common
: Stock { 
   exchange : NASDAQ     // may change
   symbol   = "GOOGL"    // may change
} 



Shareholder protocal { 
    // quanity
    // stock
    // ..
}


// take a single note, and split it into two
Stock_Split event { 

}


Stock.owner := Stockholder

Stock_Exchange record { 

}