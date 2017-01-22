List protocal <T> { 
  count -> Integer
  
  contains (element: T)   -> Boolean
  remove   (at: Integer)  -> Removed | OutOfRange
  insert   (element: T, at: Integer);
  append   (element: T);
}