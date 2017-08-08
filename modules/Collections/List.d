List<T> protocol { 
  count -> i64
  
  contains (element: T)   -> Boolean
  remove   (at: i64)  -> Removed | OutOfRange
  insert   (element: T, at: i64);
  append   (element: T);
}