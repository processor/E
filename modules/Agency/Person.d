from Law import Term, Contract 

Person protocol { 
  contract  (contractor : Person,                          terms  : [ Term ]) -> Contract       
  employee  (employee   : Human,                           terms  : [ Term ]) -> Employment 
  purchase  (asset      : Asset | Product,                 terms  : [ Term ]) -> Purchase 
  bill      (entity     : Person, lines: [ Invoice`Line ], terms  : [ Term ]) -> Invoice

  contracts   -> [ Contract]    // Contracts(contractee).contractor
  employments -> [ Employment ] // Employments(employer).employee
}