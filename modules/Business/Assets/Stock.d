Stock protocol {
  * issue
  * split

  entity : Entity
  name   : String
  series : String

  offers : [ Offer ]    // buy & sell offers
}

Stock actor : Asset {
  entity   :   Entity
  name     :   String
  series   :   String
  quantity :   Decimal
  rights   : [ Right ]
}

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