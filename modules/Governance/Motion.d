Motion protocal {
 * vote : Vote

  decision : Decision

  // qortum
  
  votes: [ Vote ]

  vote(actor: Actor) -> Vote
}

Motion actor { 
  vote(Entity) { } 

  votes : [ Vote ] 
}

// for | against