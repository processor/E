Bucket protocol {
  list(prefix: String) -> [ Object ]
}

Bucket actor {
   account : Amazon `Account
   grants  : [ Grant ]
}
