BigInteger protocol {
  bit `Count -> i32
  
  to Int32  -> Int32  | Overflow
  to Int64  -> Int64  | Overflow
  to Int128 -> Int128 | Overflow
  
  to String;
}

// An arbitrary length integer