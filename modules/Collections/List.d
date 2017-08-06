List<T> protocol { 
  count -> Int64
  
  contains (element: T)   -> Boolean
  remove   (at: Int64)  -> Removed | OutOfRange
  insert   (element: T, at: i64);
  append   (element: T);
}