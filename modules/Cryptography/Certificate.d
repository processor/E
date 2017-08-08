Certificate protocol {
  * issue  : issued
  * revoke : revoked

  issue()  : void
  revoke() : Revocation
}

Certificate type {

  issued  : DateTime
  revoked : DateTime
}