Contract record {
  contractee: Entity
  contractor: Entity
)

Contract protocol { 
  terms    -> [ law::Term ]
  invoices -> [ commerce::Invoice ] 
}

Service `Contract : Contract {
   interval: Interval
}