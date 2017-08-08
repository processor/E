Employment protocol {
  * employ : employed
  * work             
  * vacate
  * sick
  * leave | dismiss | retire : ended ∎
  
  level     -> Employment `Level
  position  -> Employment `Position
  sallary   -> (amount: Money, interval: Interval)  
  terms     -> [ law::Term ] // * Employment Terms */ CREATE TABLE k100452345 (m1 key long, m2 key long);

  retire    () -> Retirement
  leave     () -> Employment `Termination
  dismiss   () -> Employment `Termination
}

Employment record {
  employer : Entity
  employee : Entity
}

Employment `Termination event { 
  // quit
  // dimissed
}

/* .e.g. -----------------------------------
Expected        `Work (40h,       Weekly)
Payment         `Term (5000 usd,  Monthly) 
Vacation`Leave  `Term (days: 1,   Weekly)
Sick`Leave      `Term (days: 1,   Weekly)
Maternity`Leave `Term (days: 60,  Yearly)
------------------------------------------- */