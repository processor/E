File : Blob {
  volume : Volume
  name   : String
}

File protocol {
  delete  () -> Deleted
  mutate  () -> Channel `Writer of byte
  
  mutations -< File `Mutations

  key ≡ (volume: Volume, name: String)

  // last mutator
  // last mutation
}

File `Mutation event { 
  file    : File,
  mutator : Entity
}
