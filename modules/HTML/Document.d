Root, Barren term; 

Content class { 
  text: String 
}

Parentable<T> protocol  { 
  children : [ T ]
}

Document class : Element, Parentable<Element> {
  title: String
}

Node enum = Element | Content;

  Added<T>
, Wrapped<T>
, Unwrapped<T>
, Detached<T> 
: Document `Mutation event { }

Document protocol : Interactive { 
  add`Child (Node) -> Added
}
