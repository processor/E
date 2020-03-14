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

// acts as Contractor
// acts as Employee
// acts as Director
// acts as Officer
// act in role of Manager
