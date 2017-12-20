Set<T> protocol { 
  count     -> i64
  insert(T) -> Boolean

  intersection (Set<T>) -> Set<T>   // ∩
  union        (Set<T>) -> Set<T>   // ∪

  static ∅ { } // empty set
}

_ ⊆ _ axiom { } // Subset         
_ ⊂ _ axiom { } // ProperSubset   
_ ⊄ _ axiom { } // NotSubset      
_ ⊇ _ axiom { } // Superset       
_ ⊃ _ axiom { } // ProperSuperset 
_ ⊅ _ axiom { } // NotSuperset    