Corporation protocol {  
  * | resolve 
    | issue `Stock

  resolve     ()                   -> Corporate `Resolution
  issue`Stock (quantity: Decimal)  -> Stock

  jurisdiction ->   Place
  officers     -> [ Corporate `Officer ]
  shares       -> [ finance::Share ]
}

Corporate `Resolution record { 

}


Incorporation event {
  entity	   	  : Organization
  regitar	   	  : Incorporation `Registrar
  jurisitrction : Entity
)

Dissolution event {
  entity    : Organization
  reason    : Reason		// for dissolution
  registrar : Registrar
}