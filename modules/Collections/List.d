List<T> protocal { 
  count -> Int64
  
  contains (element: T)   -> Boolean
  remove   (at: Int64)  -> Removed | OutOfRange
  insert   (element: T, at: Int64);
  append   (element: T);
}