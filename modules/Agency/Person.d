Person protocol { 
  contract  (contractor : Person,                          terms  : [ ] terms) -> Contract       
  employee  (employee   : Human,                           terms  : [ ] terms) -> Employment 
  purchase  (asset      : Asset | Product,                 terms  : [ ] terms) -> Purchase 
  bill      (entity     : Person, lines: [ ] Invoice`Line, terms? : [ ] terms) -> Invoice

  contracts    -> [ ] Contract      // Contracts(contractee).contractor
  employments  -> [ ] Employment    // Employments(employer).employee
}
