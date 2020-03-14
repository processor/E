Certificate protocol {
  * issue()  : issued
  * revoke() : revoked
  
  subjects    : [] Subject
  public_key  :   Key
  private_key :   Protected<Key>
  chain       :   Chain
  
  on issued { 

  }

  Subject choice = Domain | Entity | Message::Address
}

Certificate process {
  issued  : DateTime?
  
  private_key : Protected<Key>

  Revoked event { 
    reason: String
  }
}



// has been revoked



// OCSPStaple
// x509>