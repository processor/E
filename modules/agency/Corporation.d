from Governance import Resolution, Officer
from Finance    import Stock, Stockholder 
from Geography  import Place

Corporation protocol {
  * | resolve
    | issue`Stock
    | issue`Bond
  
  resolve (motion: Motion)    : Resolution
  jurisdiction :   Place
  officers     : [ Officer ]
  stockholders : [ Stockholder ]
  holdings     : [ Holding ]
}

Corporation actor : Organization {

  
}

// TODO: Issue / allocate / modify shares

// TODO: Authorized Shares

// Stockholders are provided with 1 vote per share