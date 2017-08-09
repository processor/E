Organism protocol {
  * born
  * action ↺ : acting
  * die    ∎ : dead

  birth -> Birth
  death -> Death?

  die (reason: Reason) -> Death

  relations -> [ Relation ]
  
  registar => birth.registar

  alive if acting

  on death {
	  // some code that runs at death
  }
}

Organism class : Entity { }

Dog => Canine
