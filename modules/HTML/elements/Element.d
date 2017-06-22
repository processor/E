Element protocol : Interactive { 
  matches     (Selector)           -> Boolean
  closest     (Selector)           -> Node
  adjacent    (Node)               -> Node          // find the node following the current Node -- need previous
                 
  add `Child    (child   : Node)   -> Added
  add `Adjacent (sibling : Node)   -> Added

  wrap       (parent: Node)     -> Wrapped<T>                        // wraps child with parent
  unwrap     ()                 -> Unwrapped | Must'Be'Only'Child  // replaces parent with child
  detach     ()                 -> Detached                        // detaches the node
  
  focus () -> Focused
  blur  () -> Blured

  id     : String
  parent : Element

  let childless => count children == 0
}

Element type { 
  parent   : Node?
  children : Node [ ]
}

Element impl for Node { 

  
}

Element impl for Parentable<Element> { 


}