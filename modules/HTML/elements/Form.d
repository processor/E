Field protocol {
  * | Focused
    | Blured

  select()
  validate() -> Valid | Invalid
}

Field class : Block {
 label : Label 
 name  : String,
 input : Input
}

Label class : Block {
  text => children[0] as Content
}

Input<T> class : Inline {
  value: T
}

Form protocol { 
  * validate : validating
  * send : sending
  * sent
}



Form process : Element {
  action :    Function
  fields : [] Field

  from (fields: [] Field) => Form(children: fields)

  fields (this Form) -> [] Field => children

  // validate
  // send
  
}