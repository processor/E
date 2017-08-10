Address protocol  {

}

Email `Address struct : Address { 
  name     : String,
  host     : String
  username : String
  protocol : String

  to String => $"<{name}> {username}@{host}"
}