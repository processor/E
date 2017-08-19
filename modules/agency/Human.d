Human protocol { 
  name  -> Name  // current legal name
  names -> [ Names ] // aliases and former names

  // employed   as employee   through employments
  // contracted as contracter through contracts
}

Human actor : Organism { 
  name: String
  
  employments -> [ Employment ]
  contracts   -> [ Contract ]
  holdings    -> [ Holding ]

  employers   => from employments select employeer
  contractees => from contracts select contratee
}