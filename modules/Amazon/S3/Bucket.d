Bucket protocol {
  list(prefix: String) -> [ Object ]
}

Bucket record {
   account : Amazon `Account
   grants  : [ Grant ]
}