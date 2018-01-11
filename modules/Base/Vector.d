Vector<T> struct {
  contains(value: T)      -> Boolean
  index`Of(value: T)      -> Int64?
  last`Index`Of(value: T) -> Int64?

  slice(start: Int64, count: Int64) -> Vector<T>
}