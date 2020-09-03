from Law import Term, Contract 

Person protocol { 
  marry     (person: Person)                                   -> Marriage

  contracts   -> [] Contract   // Contracts(contractee).contractor
  employments -> [] Employment // Employments(employer).employee
}

Person actor { 

  
}

Employable protocol {

  // employee under terms -> Employment

}