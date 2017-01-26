Root, Barren term; 

Content type { 
  text: String 
}

Parentable type of T  { 
  children : [ ] T
}

Document type : Parentable<Element> {
  title: String
}

Node = Element | Content;


  Added<T>
, Wrapped<T>
, Unwrapped<T>
, Detached<T> 
: Document'Mutation event { }

Document protocal : Interactive { 
  add`Child (Node) -> Added
}
