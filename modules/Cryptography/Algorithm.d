Algorithm protocol { 
  sign    (data       : [byte]) -> [ byte ]
  verify  (data       : [byte]) ->   Boolean
  encrypt (plaintext  : [byte]) -> [ byte ]
  decrypt (ciphertext : [byte]) -> [ byte ]

  // derive
}