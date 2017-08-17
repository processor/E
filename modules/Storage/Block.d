Block record {
  index  : i32
  offset : i32
  length : i32
  sha256 : [ byte ]
}

// Blocks are 4MB chucks
// The final block may be less than 4MB