Bucket type {
   account : Amazon `Account
   grants  : [ Grant ]
}

Bucket protocol {
  list(prefix: String) -> [ Object ]
}