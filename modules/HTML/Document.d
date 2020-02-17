Root, Barren term; 

Content class {
  text: String 
}

Parentable<T> protocol {
  children : [] T
}

Document class : Element, Parentable<Element> {
  title: String

  // Events -
  Add event      : Mutation { } 
  Wrap event     : Mutation { }
  Unwap event    : Mutation { }
  Detached event : Mutation { }

}

Node enum = Element | Content;

Document protocol : Interactive {
  add`Child (Node) emits Add
}
