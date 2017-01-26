Blob record { 
  size: i64 > 0
}

Blob protocal {
  store  ()                                -> * Storage_Transaction
  open   ()                                -> * byte ↺     | Error    ∎ | End ∎
  slice  (offset: Integer, count: Integer) -> * byte ↺     | Error    ∎ | End ∎
  link   (record: Record)                  -> * Linked   ∎ | Failed   ∎
  unlink (record: Record)                  -> * Unlinked ∎ | Failed   ∎

  locate static (Hash)                     -> * Blob_Location
  get    static (Identity)                 -> * Blob | Error

                                               // sql -----------------------------------------------------
  blocks      -> [ ] Blob_Block                // select block     from Blob'blocks        where blob = $0
  hashes      -> [ ] Hash
  attributes  -> [ ] Attribute
}