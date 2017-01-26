Heading type : Element { 
  level: Number
}

Heading protocal { 
  // level ∈ 1..6 
}

Heading impl {
  from (String) => ...
  
  level => level
}