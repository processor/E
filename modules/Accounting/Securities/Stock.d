Stock protocol : Asset {
  entity : Entity
  name   : String
  series : Stock::Series
}

Stock type : Asset {
  entity       :    Entity
  name         :    String
  series       :    Series
  share_count  :    Decimal      // Outstanding
  issued       :    Decimal
  quantity     :    Decimal  
  rights       : [] Right
  splits       : [] Split
  exchanges    : [] Exchange<Stock, Self>
  
  Series type { 

  }

  // take a single note, and split it into two
  Split event { 
 
  }
}

// A corporation may:
// - issue 1 or more classes of stock;
// - issue 1 or more series within each class of stock; 
// - each class and series of Stock may be provided with different rights including:
// / voting
// / preferences
// / limitations


// Clarify: shares { issued, oustanding, authorized }

// A stock may be created by a corporation
// A stock may be traded on an exchange

AMZN: Stock { }

// `class           `series
// Prefered Shares A 1

// e.g. Google `Stock `Preferred `A` `1`



// A stock may be listed on mutiple exchanges under a "Ticker"


// A stock may split


// a financial security


// Common/ordinary shares
// Preferred/preference shares
// Common/ordinary convertible shares
// Preferred/preference convertible shares
// Limited partnership units
// Depositary receipts on equities
// Structured instruments (participation)
// Others (miscellaneous)