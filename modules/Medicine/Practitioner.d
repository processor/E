Practitioner protocol { 
  exam     (Human)          -> Examimation                // Uncovers symptoms
  diagnose (Human)          -> Diagnosis | Inconclusive

  perscribe (Human, drug: Drug, for: Medical `Diagnosis) -> Perscription
  perform   (Procedure)                                  -> Procedure
  treat     (Organism, Treatment)                        -> Treatment

  displines -> [ Displine ]
}

Practitioner record { 
  person: Entity
}

Practitioner protocol {
  
}
