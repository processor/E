Vector<T> protocol { 
  count -> i64
  
  [index: i64] -> T

  clone -> V
}

// A vector can represent any blitable type ...
// Represented as a Span in C#