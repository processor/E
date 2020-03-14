Authority actor { 
  issue(request: Certificate::Request) -> Certificate
  revoke(certificate: Certificate)
}

// Roots

C`A := Certificate::Authority

// Certificate authority

// In the X.509 trust model, a certificate authority (CA) is responsible for signing certificates. 

// Certificate authorities are also responsible for maintaining up-to-date revocation information 
// about certificates they have issued, indicating whether certificates are still valid. 
