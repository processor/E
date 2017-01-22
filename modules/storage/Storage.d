File : Blob {
  volume : Volume
  name   : String
}

File protocal {
  delete  () -> Deleted
  mutate  () -> Channel'Writer of byte
  
  mutations = File'Mutations

  key ≡ (volume: Volume, name: String)

  // last mutator
  // last mutation
}

File'Mutation event { 
  file    : File,
  mutator : Entity
}

Bucket protocal {
  scan   (prefix : String) -> * File ↺  | Error ∎ | End ∎ 
  get    (name   : String) -> * File    | Error ∎ 
  open   (name   : String) -> * byte ↺  | Error ∎ 
  put    (file   : File)	 -> * Put     | Error ∎ 
  delete (name   : String) -> * Deleted | Error ∎ 
}

Storage'Transaction protocal  {
  * Accepted   ∎ | Error ∎            // Transfered, may still be lost if the node fails before commit
  * Committed  ∎ | Error ∎            // Durably stored
  * Propagated ∎                      // Globally propogated / accessable
}


Record protocal {
  store () -> * Transaction;
}

names {
  lookup(String) : * Identity | NotFound
}

Bucket descriptor {
   name: String
}

Blob'Block record {
  blob   : Blob
  block  : Block
  index  : i32
  offset : i32
  length : i32
}

Volume record { 
  total    _bytes : i64 > 0,
  available_bytes : i64 ≥ 0 | ∞
}

Volume protocal {
  scan   (prefix : String)           -> * File ↺     | Error ∎ | End ∎ 
  get    (name   : String)           -> * File     ∎ | Error ∎
  put    (file   : File)             -> * Put      ∎ | Error ∎
  unlink (name   : String)           -> * Unlinked ∎ | Error ∎
  link   (name   : String, to: File) -> * Linked   ∎ | Error ∎

  files  -> [ ] Files
  drives -> [ ] Drives
}

Locator type {
   shard : Shard'id,
   type  : Kind'id,
   id    : Identity
}

Identity `Sequence protocal : Readable'Channel {
  * Identity ↺ | 
  * End ∎
  
  seed   : i64
  max    : i64 > 0
} 










