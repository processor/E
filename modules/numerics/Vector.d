Vector<T> protocol { 
  count -> i64
  
  [index: i64] -> T

  clone -> Vector<T>
}