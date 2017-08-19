Motion protocal {
 * vote : Vote

  decision : Decision

  votes: [ Vote ]

  vote(actor: Actor) -> Vote
}

// cast
// quorum
// proxy


Motion actor { 
  vote(Entity) { } 

  votes : [ Vote ] 
}

// for | against