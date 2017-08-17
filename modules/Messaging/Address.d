Address protocol  {

}

Email `Address struct : Address { 
  let name     : String,
  let host     : String
  let username : String
  let protocol : String

  to String => $"<{name}> {username}@{host}"
}