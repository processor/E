Volume record {
  size      i64 of byte ≥ 0,
  available i64 of byte ≥ 0 | ∞
}

Volume protocol {
  scan   (prefix : String)                  -> * File ↺     | Error ∎ | End ∎ 
  get    (name   : String)                  -> * File     ∎ | Error ∎
  put    (file   : File)                    -> * Put      ∎ | Error ∎
  unlink (name   : String)                  -> * Unlinked ∎ | Error ∎
  link   (source : File, destination: File) -> * Linked   ∎ | Error ∎

  files  -> [ Files ]
  drives -> [ Drives ]
}
