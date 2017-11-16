from Governance import Resolution, Officer
from Finance    import Stock, Stockholder 
from Geography  import Place

Corporation protocol {
  * | resolve
    | issue `Stock
    | issue `Bond

  resolve      (motion: Motion)    : Resolution
  issue `Stock (quantity: Decimal) : Stock

  jurisdiction :   Place
  officers     : [ Officer ]
  stockholders : [ Stockholder ]
  holdings     : [ Holding ]
}

Corporation actor : Organization {

  
}

// Stockholders are provided with 1 vote per share