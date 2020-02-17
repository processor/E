Big`Integer protocol {
  bit `Count -> i32
  
  to i32  -> i32  | Overflow
  to i64  -> i64  | Overflow
  to i128 -> i128 | Overflow
  
  to String;
}

// An arbitrary length integer