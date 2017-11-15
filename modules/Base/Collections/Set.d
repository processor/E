Set<T> protocol { 
  count     -> i64
  insert(T) -> Boolean

  intersection (Set<T>) -> Set<T>   // ∩
  union        (Set<T>) -> Set<T>   // ∪
}

Subset         , // ⊆
ProperSubset   , // ⊂
NotSubset      , // ⊄"
Superset       , // ⊇
ProperSuperset , // ⊃
NotSuperset      // ⊅