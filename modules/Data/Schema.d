Schema record {
  name         : String
  collections  : [] Schema `Collection
  relationship : [] Schema `Relationship
}

Schema`Collection record {
  name      : String
  type      : Type
  size      : i32? > 0
  precision : i32?
  members   : Schema `Member
}

Schema `Relationship {

}