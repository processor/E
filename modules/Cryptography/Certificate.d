Certificate protocol {
  * issue  : issued
  * revoke : revoked

  issue()  : void
  revoke() : Revocation
}

Certificate class {
  issued  : DateTime
  revoked : DateTime
}