Email `Message record {
  to   : [ ] Email` Address
  from :     Email` Address
  cc   : [ ] Email` Address
  body :     Email` Body
}

Email `Message impl { 
  to String => $"<{name}> {username}@{host}"
}