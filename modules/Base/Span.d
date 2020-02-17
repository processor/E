Span<T:Blittable> struct {
  contains(value: T)      -> Boolean
  index`Of(value: T)      -> i64?
  last`Index`Of(value: T) -> i64?

  slice(start: i64, count: i64) -> Span<T>
}