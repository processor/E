Stock : Finance `Instrument {
  entity   : Entity
  holder   : Entity
  shares   : Decimal
  issued   : DateTime
}

Stock protocol { 
  * aquire    : held
  * | split ∎ : split
    | sell  ∎ : sold

  // aquire
  // split            split to be able to sell a smaller part
  // sell
}
