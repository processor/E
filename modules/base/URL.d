URL type {
  protocal : String
  host     : String
  port     : Int16
  path     : String
}

URL impl { 
  to String => $"{protocal}://{host}/{path}"
}