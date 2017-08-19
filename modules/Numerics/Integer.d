Integer protocol {
  bitCount -> i32
  
  to i32 -> i32 | overflow
  to i64 -> i36 | overflow
  
  to String;
}

// An arbitrary length integer