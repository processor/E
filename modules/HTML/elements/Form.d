Form type : Block {
  status : Unknown | Validated | Sending | Sent
  action : Function
  fields : [ Field ]
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
  from (fields: [ Field ]) => Form { children: fields }

  fields (this Form) -> [ Field ] => children
}

Field protocol {
  * | Focused
    | Blured

  select()
  validate() -> Valid | Invalid
}
