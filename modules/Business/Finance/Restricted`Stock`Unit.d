Restricted`Stock`Unit protocol {
  * forfit : forfitted 

  stock    -> Stock
  holder   -> Entity
  maturity -> DateTime
  terms    -> [ Legal::Term ]

}

R`S`U = Restricted`Stock`Unit