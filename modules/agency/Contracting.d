import * from Legal
import [ Invoice ] from Commerce

Contract record {
  contractee: Entity
  contractor: Entity
)

Contract protocol { 
  terms    -> [ Term ]
  invoices -> [ Invoice ] 
}

Service `Contract : Contract {
   interval: Interval
}