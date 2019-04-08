Organism protocol {
  * born
  * act ↺ : acting
  * die ∎ : dead

  birth : Birth
  death : Death?

  die (reason: Reason) : Death

  relations : [ Relation ]
  
  registar => birth.registar

  alive if acting

  on death {
	  // some code that runs at death
  }
}

Organism actor : Entity { 

}

Dog actor : Canine
