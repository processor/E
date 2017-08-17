from Governance import Resolution, Officer
from Finance    import Stock, Stockholder 
from Geography  import Place

Corporation protocol {
  * | resolve
    | issue `Stock
    | issue `Bond

  resolve      (motion: Motion)    -> Resolution
  issue `Stock (quantity: Decimal) -> Stock

  jurisdiction ->   Place
  officers     -> [ Officer ]
  stockholders -> [ Stockholder ]
}

Incorporation event {
  entity	   	  : Organization
  regitar	   	  : Registrar
  jurisitrction : Entity
)

Dissolution event {
  entity    : Organization
  reason    : Reason		// for dissolution
  registrar : Registrar
}