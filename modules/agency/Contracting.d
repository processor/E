Contract record {
  contractee: Entity
  contractor: Entity
)

Contract protocol { 
  terms    -> [ ] law::Legal`Term  
  invoices -> [ ] Invoice 
}

Service `Contract : Contract {
   interval: Interval
}