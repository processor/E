Block record {
  index  : i64 ≥ 0
  offset : i64 ≥ 0
  length : i64 ≥ 0
  sha256 : [ Byte ]
}

// Blocks are 4MB chucks
// The final block may be less than 4MB