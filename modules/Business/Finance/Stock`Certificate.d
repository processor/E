Stock `Certificate : record {
  stock  : Stock,
  issued : DateTime
}

Stock `Certificate protocol { 
  * aquire    : held
    | sell  ∎ : sold

  purchase() -> Stock`Purchase
  sell()     -> Stock`Sale
}