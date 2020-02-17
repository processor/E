Stock protocol : Asset {
  * issue
  * split

  entity : Entity
  name   : String
  series : String
}


Stock actor : Asset {
  entity    :    Entity
  name      :    String
  series    :    String
  shares    :    Decimal      // Outstanding
  issued    :    Decimal
  quantity  :    Decimal  
  rights    : [] Right
  splits    : [] Split
  exchanges : [] Exchange<Stock, Self>
  
  // take a single note, and split it into two
  Split event { 
 
  }
}

// Clarify: shares { issued, oustanding, authorized }

// A stock may be created by a corporation
// A stock may be traded on an exchange

AMZN: Stock { }



// `class           `series
// Prefered Shares A 1

// e.g. Google `Stock `Preferred `A` `1`



// A stock may be listed on mutiple exchanges under a "Ticker"

// A corporation may:
// - issue 1 or more classes of stock;
// - issue 1 or more series within each class of stock; 
// - each class and series of Stock may be provided with different rights including:
// / voting
// / preferences
// / limitations

// A stock may split


// a financial security