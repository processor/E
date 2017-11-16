Array<T> protocol {
  count    : i64
  capacity : i64
  contains (element: T)          -> Boolean
  remove   (at: i64)             -> Removed | Out`Of`Range
  insert   (element: T, at: i64)
  append   (element: T)
}