import [ Resolution, Officer ] from Governance
import [ Stock, Stockholder ] from Finance
import [ Place ] from Geography

Corporation protocol {
  * | resolve
    | issue `Stock

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