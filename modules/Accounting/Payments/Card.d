Card : Instrument { 
  issuer     : Entity
  network    : Payment::Network
  key?       : Cryptography::Key
  issued     : DateTime
  expiration : Expiration
}