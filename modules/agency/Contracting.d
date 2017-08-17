Contract record {
  contractee: Entity
  contractor: Entity
)

Contract protocol { 
  terms    -> [ Law::Term ]
  invoices -> [ Commerce::Invoice ] 
}

Service `Contract : Contract {
   interval: Interval
}