Certificate protocol {
  * issue  : issued
  * revoke : revoked

  issue()  : void
  revoke() : Revocation
}

Certificate actor {
  issued  : DateTime
  revoked : DateTime

  private`Key : Protected<[byte]>

}

// OCSPStaple
// x509>