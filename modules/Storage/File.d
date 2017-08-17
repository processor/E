File : Blob {
  volume : Volume
  name   : String
}

File protocol {
  delete  () -> Deleted
  mutate  () -> Channel `Writer of byte
  
  key ≡ (volume: Volume, name: String)
}