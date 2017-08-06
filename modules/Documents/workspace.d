Project impl { 
   add (Document)  -> Workspace `Object
   add (Section)   -> Workspace `Section
   add (Artboard)  -> Workspace `Object
   add (Object)    -> Workspace `Object
   add (Dataset)   -> Workspace `Object
}

Artboard type { 
   width     : Length
   height    : Length
   
   position  : Position, 
   objects   : [ (Position, Object) ]
}