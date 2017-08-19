Email `Address struct : Networking::Address { 
  let name     : String,
  let host     : String
  let username : String
  let protocol : String

  to String => $"<{name}> {username}@{host}"
}