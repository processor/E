Employment protocol {
  * commence    : employed
  * act ↺       : acting
  * terminate ∎ : terminated // may be mutal or one sided 
  
  roles         -> [] Role
  absences      -> [] Absence
  compensations -> [] Compensation
  terms         -> [] Legal::Term

  // either party may terminate the employment under it's terms
  terminate() { }
}

Absence record { 
  paid: boolean
  reason: Reason
   
  Reason enum { Vacation, Sick }
}

Employment process {
  commence : DateTime
  employer : Entity
  employee : Entity

  // events
  Terminated event {
    reason: Reason
    
    Reason enum { Quit, Dismissed, Retired }
  }
}


Wages record : Compensation { 
  payments: [] Payment
}

Payable impl for Wages { } 



// notes: 
// An employment has state
// - work & compensation takes place through an employment

/* .e.g. -----------------------------------
Expected        `Work (40 hours,  Weekly)
Payment         `Term (5000 usd,  Monthly) 
Vacation`Leave  `Term (1  day,    Weekly)
Sick`Leave      `Term (1  day,    Weekly)
Maternity`Leave `Term (60 days,   Yearly)
------------------------------------------- */


// Employee of X
// Member of