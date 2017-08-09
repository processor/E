Policy protocol {
  allow ( 
    entity   :   Entity, 
    actions  : [ Verb ], 
    resource :   Resource,
    when     :   Predicate
  ) -> * Rule 

  forbid (
    entity   :   Entity, 
    actions  : [ Verb ], 
    resource :   Resource,
    when     :   Predicate
  ) -> * Rule
}

/*
allow (
  entity   : Carbonmade, 
  action   : open | link, 
  resource : Blob#1000,
  where    : time in "jp/toyko" > 5pm
}

// get, read, write: verb

Permissions enum = read | write

// Time as a Channel
*/