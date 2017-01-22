Human type : Organism { 

}

Human protocal { 
  name -> Name  // current legal name
}

Human impl {
  employeers  => ∀ Employments(employee).employer
  contractees => ∀ Entity`Contracts(contractor).contractee
}
