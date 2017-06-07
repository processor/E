Employment protocal {
  * employ            : employed
  * work             
  * vacate
  * sick
  * leave | dismiss | retire : ended end
  
  level     -> Employment `Level
  position  -> Employment `Position
  sallary   -> (Money, Interval)  
  terms     -> [ ] law::Legal`Terms              // * Employment'Terms */ CREATE TABLE k100452345 (m1 key long, m2 key long);

  retire    () -> Retirement
  leave     () -> Employment `Termination
  dismiss   () -> Employment `Termination
}

Employment record {
  employer : Entity
  employee : Human
}


Employment `Termination event { 
  // quit
  // dimissed
}

/* .e.g. -----------------------------------
Expected        `Work (40h,       Weekly)
Payment         `Term (USD(5000), Monthly) 
Vacation`Leave  `Term (days: 1,   Weekly)
Sick`Leave      `Term (days: 1,   Weekly)
Maternity`Leave `Term (days: 60,  Yearly)
------------------------------------------- */