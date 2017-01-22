Contract record {
  contractee: Entity
  contractor: Entity
)

Contract protocal { 
  terms    -> [ ] law::Legal`Term  
  invoices -> [ ] Invoice 
}

Service `Contract : Contract {
   interval: Interval
}