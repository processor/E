Dataset record {
  name  : String,
  kind  : Kind
}

Dataset<T> protocol {
  create      ()                    -> * Created
  get         static (Identity<T>)  -> * Dataset<T>
  lookup      static (name: String) -> * Record`Locator<T>

  shards      -> [ ] Shard<T>
  maintainers -> [ ] Entity
}

// A registry is a subset of a dataset 

Registry<T> protocol {
  
}


Dataset<T> protocol {
  get    (Identity<T>)				      -> * T
  insert (T)						            -> * Transaction
  update (Identity<T>, [ ] Change)	-> * Transaction
  delete (Identity<T>)				      -> * Transaction
  query  (Query`Expression)  	      -> * T ↺     | End ∎
}

