using financing

Stock `Acquisition event {

}

Stock `Sale event {

}

  Google_Stock_Preferred_A_1
, Google_Stock_Common
: Stock { 
   exchange : NASDAQ     // may change
   symbol   = "GOOGL"    // may change
} 



Shareholder protocol { 
  stock    : Stock;
  quantity : decimal;
}


// take a single note, and split it into two
Stock `Split event { 

}

Stock `Exchange record { 

}