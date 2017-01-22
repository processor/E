URL type {
  protocal : String
  host     : String
  port     : Integer
  path     : String
}

URL impl { 
  to String => $"{protocal}://{host}/{path}"
}