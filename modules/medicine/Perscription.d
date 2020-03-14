Perscription protocol {
  * perscribe : prescribed
  * dispense  : dispenced
   
}
Perscription process {
  perscriber   :    Practitioner
  organism     :    Protected<Organism> 
  drug         :    Drug,
  instructions : [] Instruction

  // prescribed (by a doctor)
  // dispenced via a Pharmacy
}


// an instruction written by a medical practitioner that authorizes a patient to be provided a medicine or treatment.
