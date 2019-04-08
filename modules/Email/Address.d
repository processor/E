Email `Address struct : Networking::Address { 
  name     : String,
  host     : String
  username : String

  to String => $"<{name}> {username}@{host}"
}