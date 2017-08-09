Human protocol { 
  name -> Name  // current legal name
}

Human class : Organism { 
  let name: String
  
  employeers  => ∀ Employments(employee).employer
  contractees => ∀ Entity`Contracts(contractor).contractee
}