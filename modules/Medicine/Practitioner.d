Practitioner protocol { 
  exam     (organism: Organism) -> Examimation                // Uncovers symptoms
  diagnose (organism: Organism) -> Diagnosis | Inconclusive

  perscribe (drug: Drug, for: Medical `Diagnosis, recipient: Organism) -> Perscription
  perform   (procedure: Procedure)                                     -> Procedure
  treat     (Organism, Treatment)                                      -> Treatment

  displines -> [] Displine
}


Practitioner role of Person { }

// A practitioner pratices medicine and:
// - diagnoses and treats syptoms, dieases, disorders, ...
