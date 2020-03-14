Dataset<T> protocol {
  get    (Identity)				                  -> * T
  insert (T)						                    -> * Transaction
  update (id: Identity, changes: [] Change)	-> * Transaction
  delete (id: Identity)				              -> * Transaction
  query  (Query`Expression)  	              -> * T ↺ | End ∎
}

Dataset struct : record {
  name  : String,
  kind  : Kind
}

Dataset<T> protocol {
  create      ()                    -> * Created
  get         static (id: Identity) -> * Dataset<T>
  lookup      static (name: String) -> * Record`Locator<T>

  shards      -> [] Shard<T>
  maintainers -> [] Entity
}


// Collection | Table ?