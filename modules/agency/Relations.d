  Parent `Relation
, Child  `Relation
, Friend              // Personal
, Acquaintance        // Business
: Relation record { 
  relator : Entity
  relatee : Entity
}


// All other family relationships (grandfather, mother, cousins, siblings, etc) may be discovered by transversing the graph