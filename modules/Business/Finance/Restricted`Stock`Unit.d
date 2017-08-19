Restricted`Stock`Unit protocol {
  * forfit : forfitted 

  stock    -> Stock
  holder   -> Entity
  maturity -> DateTime
  terms    -> [ Legal::Term ]

}

R`S`U alias of Restricted`Stock`Unit