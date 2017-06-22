Heading type : Element { 
  level: Number
}

Heading protocol { 
  // level ∈ 1..6 
}

Heading impl {
  from (String) => ...
  
  level => level
}