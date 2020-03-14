Organism protocol {
  * born
  * act ↺ : acting
  * die ∎ : dead

  birth : Birth
  death : Death?

  die ($0: Reason) : Death

  relations : [] Relation
  
  registar => birth.registar

  on death {
	  // some code that runs at death
  }
}

// states ---
Organism is alive when acting


Organism actor : Entity { 

}

Dog actor : Canine
