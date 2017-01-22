Heading type : Element { 
  level: Float
}

Heading protocal { 
  // level ∈ 1..6 
}

Heading impl {
  from (String) => ...
  
  level => level
}