Policy protocol { 
  allow ( 
    entity   : Entity, 
    actions  : [ ] Verb, 
    resource : Resource,
    when     : Predicate
  ) -> * Rule 

  forbid (
    entity   : Entity, 
    actions  : [ ] Verb, 
    resource : Resource,
    when     : Predicate
  ) -> * Rule
}

/*
allow (
  entity   : Carbonmade, 
  action   : open | link, 
  resource : Blob#1000,
  where    : time in Toyko > 5pm
}

// get, read, write:  verb;

type Permissions = read | write

// Time as a Channel
*/