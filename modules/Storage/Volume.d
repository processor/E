Volume record { 
  total    _bytes : i64 > 0,
  available_bytes : i64 ≥ 0 | ∞
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
