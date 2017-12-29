Block record {
  index  : i32 >= 0
  offset : i32 >= 0
  length : i32 >= 0
  sha256 : [ byte ]
}

// Blocks are 4MB chucks
// The final block may be less than 4MB