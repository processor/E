Container protocol {
  scan   (prefix : String) -> * File ↺  | Error ∎ | End ∎ 
  get    (name   : String) -> * File    | Error ∎ 
  open   (name   : String) -> * byte ↺  | Error ∎ 
  put    (file   : File)	 -> * Put     | Error ∎ 
  delete (name   : String) -> * Deleted | Error ∎ 

  name   -> String
  policy -> Policy
}