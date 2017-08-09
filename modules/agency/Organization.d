import [ Bylaw ] from Legal

Organization protocol {
  * incorporate : incorporated
  * action    ↺ : acting 
  * bankrupt    ↑ acting        // backto acting
  * merge     ∎ : merged
  * disolve   ∎ : dissolved
 
  incorporate   ()                     -> Incorporation   // established
  dissolve      ()                     -> Dissolution
  merge         (target: Organization) -> Merger

  directors    -> [ Director ]   // including managing & officers
  jurisdiction ->   Jurisdiction // via Incorporation.jurisdiction

  bylaws       -> [ Bylaw ]

  when dissolved {
     // logic that triggers when dissolved
  }
}

Partnership class : Organization   
Corporation class : Organization     // e.g. Microsoft, Inc
Institution class : Organization