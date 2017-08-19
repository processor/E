Policy protocol {
  allow ( 
    principal  :   Principal, 
    actions    : [ Action ], 
    resource   :   Resource,
    predicate  :   Expression
  ) -> * Rule 

  forbid (
    principal :   Principal, 
    actions   : [ Action ], 
    resource  :   Resource,
    predicate :   Predicate
  ) -> * Rule
}

/*
allow (
  entity    : Corporation(name: Carbonmade), 
  action    : Blob::open | Blob::link, 
  resource  : Blob(id: 1000)
  predicate : time in "jp/toyko" > 5pm
}

// get, read, write: verb

Permissions enum = read | write

// Time as a Channel
*/