Diagnosis protocol {
  organism    -> Organism | Protected<Organism> 

  symptoms    -> [ Symptoms ]
  diagonstics -> [ Diagonstic ]
  treatments  -> [ Treatment ]
  procedures  -> [ Procedure ]
}

Diagnosis record { }

Disease : Diagnosis { }
Disorder : Diagnosis { }
Syndrome : Diagnosis { }




// protected Organism   // rule for unmasking? i.e. until 50 years after Death
