Span<T:Blittable> struct {
  contains(value: T)      -> Boolean
  index_Of(value: T)      -> i64?
  last_index_of(value: T) -> i64?

  slice(start: i64, count: i64) -> Span<T>
}