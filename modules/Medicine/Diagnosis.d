Diagnosis protocol {
  organism    ->    Protected<Organism> 
  symptoms    -> [] Symptoms
  diagonstics -> [] Diagonstic
  treatments  -> [] Treatment
  procedures  -> [] Procedure
}

Diagnosis record { }

Disease  record : Diagnosis { }
Disorder record : Diagnosis { }
Syndrome record : Diagnosis { }


// protected Organism   // rule for unmasking? i.e. until 50 years after Death
