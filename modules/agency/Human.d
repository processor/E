Human protocol { 
  name -> Name  // current legal name

  // employed   as employee   through employments
  // contracted as contracter through contracts
}

Human class : Organism { 
  name: String
  
  employments -> [ Employment ]
  contracts   -> [ Contract ]

  employers   => from employments select employeer
  contractees => from contracts select contratee
}