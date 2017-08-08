Bucket protocol {
  scan   (prefix : String) -> * File ↺  | Error ∎ | End ∎ 
  get    (name   : String) -> * File    | Error ∎ 
  open   (name   : String) -> * byte ↺  | Error ∎ 
  put    (file   : File)	 -> * Put     | Error ∎ 
  delete (name   : String) -> * Deleted | Error ∎ 
}

Storage `Transaction protocol  {
  * Accepted   ∎ | Error ∎   // Transfered, may still be lost if the node fails before commit
  * Committed  ∎ | Error ∎   // Durably stored
  * Propagated ∎             // Globally propogated / accessable
}

Record protocol {
  store () -> * Transaction;
}

Bucket descriptor {
   name: String
}