Human type : Organism { 

}

Human protocol { 
  name -> Name  // current legal name
}

Human impl {
  employeers  => ∀ Employments(employee).employer
  contractees => ∀ Entity`Contracts(contractor).contractee
}
