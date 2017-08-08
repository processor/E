Store<T> : protocal {
  put    (object: T)     -> * Transaction
  locate (id : Identity) -> * URL
  get    (id : Identity) -> * T | Error
}