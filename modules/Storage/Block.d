Block record {
  index  : Int64 ≥ 0
  offset : Int64 ≥ 0
  length : Int64 ≥ 0
  sha256 : [ Byte ]
}

// Blocks are 4MB chucks
// The final block may be less than 4MB