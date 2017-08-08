Address type {
  name     : String,
  host     : String
  username : String
  protocol : String
}

Email `Address : Address { 
  
}

Email `Address impl {
  to String => $"<{name}> {username}@{host}"
}