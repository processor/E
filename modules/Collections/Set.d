Set<T> protocol { 
  count     -> Count
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