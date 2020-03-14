Policy protocol {
  rules -> [] Rule
}

Policy record {
  evaulate(context: Context) -> Allow | Forbid | Indeterminate

  rules: [] Rule

  Rule record {
    principals : [] Principal,
    actions    : [] Action,
    resources  : [] Resource,
    predicate  :    Predicate
    effect     :    Allow | Forbid
  }
}

// entity    : US-CA:corporations/Carbonmade, 
// action    : Blob::open | Blob::link, 
// resource  : Carbonmade::blobs/100
// predicate : time in "jp/toyko" > 5pm
// effect    : Allow