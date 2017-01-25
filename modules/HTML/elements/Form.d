Form type : Block {
  status : Unknown | Validated | Sending | Sent
  action : Function
  fields : [ ] Fields;
}

Field type : Block {
 label : Label 
 name  : String,
 input : Input
}

Label type : Block { }

Label impl {
  text => children[0] as Content
}

Input<T> type : Inline { 
  value: T
}

Form impl : Element {
  from ([ ] Field fields) => Form { children: fields }

  fields (this Form) -> [ ] Field => children
}

Field protocal {
  * | Focused
    | Blured

  select()
  validate() -> Valid | Invalid
}
