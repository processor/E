Blob protocol {
  open   ()                        -> * Byte ↺     | Error    ∎ | End ∎
  slice  (offset: i64, count: i64) -> * Byte ↺     | Error    ∎ | End ∎
  link   ($0: Record)              -> * Linked   ∎ | Failed   ∎
  unlink ($0: Record)              -> * Unlinked ∎ | Failed   ∎

                                   // sql -----------------------------------------------------
  blocks     -> [] Blob::Block     // select block     from Blob'blocks        where blob = $0
  hashes     -> [] Hash
  attributes -> [] Attribute
}

Blob record { 
  size: i64 > 0
}