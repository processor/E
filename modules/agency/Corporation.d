/ * ----------------------------
- United States ----------------
----------------------------- */
  Alabama          `Corporation
, Alaska           `Corporation
, Arizona	         `Corporation
, Arkansas	       `Corporation
, California       `Corporation
, Colorado         `Corporation
, Connecticut      `Corporation
, Delaware         `Corporation
, Florida          `Corporation
, Georgia          `Corporation
, Hawaii           `Corporation
, Idaho            `Corporation
, Illinois         `Corporation
, Indiana          `Corporation
, Iowa             `Corporation
, Kansas           `Corporation
, Kentucky         `Corporation
, Louisiana        `Corporation
, Maine            `Corporation
, Maryland         `Corporation
, Massachusetts    `Corporation
, Michigan         `Corporation
, Minnesota        `Corporation
, Mississippi      `Corporation
, Missouri         `Corporation
, Montana          `Corporation
, Nebraska         `Corporation
, Nevada           `Corporation
, New `Hampshire   `Corporation
, New `Jersey      `Corporation
, New` Mexico      `Corporation
, New `York        `Corporation
, North `Carolina  `Corporation
, North `Dakota    `Corporation
, Ohio             `Corporation
, Oklahoma         `Corporation
, Oregon           `Corporation
, Pennsylvania     `Corporation
, Rhode `Island     `Corporation
, South `Carolina  `Corporation
, South `Dakota    `Corporation
, Tennessee        `Corporation
, Texas            `Corporation
, Utah             `Corporation
, Vermont          `Corporation
, Virginia         `Corporation
, Washington       `Corporation
, West_Virginia    `Corporation
, Wisconsin        `Corporation
, Wyoming          `Corporation
: Corporation;


Corporation protocal {  
  * | resolve 
    | issue `Stock

  resolve     ()                   -> Corporate `Resolution
  issue`Stock (quantity: Decimal)  -> Stock

  officers -> [ ] Corporate 'Officer
  shares   -> [ ] finance::Share
}

Corporate `Resolution record { 

}




Incorporation event {
  entity	   	  : Organization
  regitar	   	  : Incorporation_Registrar
  jurisitrction : Entity
)

Dissolution event {
  entity    : Organization
  reason    : Reason		// for dissolution
  registrar : Registrar
}