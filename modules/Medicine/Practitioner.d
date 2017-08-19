Practitioner protocol { 
  exam     (organism: Organism) -> Examimation                // Uncovers symptoms
  diagnose (organism: Organism) -> Diagnosis | Inconclusive

  perscribe (organism: Organism, drug: Drug, for: Medical `Diagnosis) -> Perscription
  perform   (procedure: Procedure)                                  -> Procedure
  treat     (Organism, Treatment)                        -> Treatment

  displines -> [ Displine ]
}

Practitioner actor { 
  person: Entity
}

// A practitioner pratices medicine and:
// - diagnoses and treats syptoms, dieases, disorders, ...