from Governance import Resolution, Officer
from Finance    import Stock, Stockholder 
from Geography  import Place

Corporation protocol {
  * | resolve
    | issue(Stock)
    | issue(Bond)
  
  resolve ƒ(motion: Motion) : Resolution
}

Corporation actor : Organization {
  jurisdiction  :    Entity           // e.g. US/IL
  qualification : [] Qualification
  officers      : [] Officer
  stockholders  : [] Stockholder
  holdings      : [] Holding
  board         :    Board<Director>
}

// Foreign Qualification

Qualitification { 


}


// The stock (also capital stock) of a corporation is all of the shares into which ownership of the corporation is divided





// TODO: Issue / allocate / modify shares

// TODO: Authorized Shares

// Stockholders are provided with 1 vote per share