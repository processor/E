Motion protocal {

  * ammend
  * withdrawl : withdrawn
  * vote      
  * resolve   : resolved

  votes    : [] Vote
  decision :    Decision

  vote(actor: Actor) -> Vote
  
}


// Motions are a statement of a proposal for an action.


// quorum
// proxy


Motion process { 
  document: Document

  cast(vote: Vote)   { } 
  ammend(Ammendment) { }
  withdraw()         { }

  votes : [] Vote 

  Amendment record { }
}

// A document may have one or more articles


// A motion that isn't seconded dies without further consideration

// for | against