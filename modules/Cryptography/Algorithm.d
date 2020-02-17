Algorithm protocol { 
  sign    (data       : [] Byte) -> [] Byte
  verify  (data       : [] Byte) ->    Boolean
  encrypt (plaintext  : [] Byte) -> [] Byte
  decrypt (ciphertext : [] Byte) -> [] Byte

  // derive
}