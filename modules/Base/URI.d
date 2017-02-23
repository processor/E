// URI: Uniform Resource Identifier
// URL: Uniform Resource Locator

URI type {
  protocal : String
  host     : String
  port     : Int16
  path     : String
}

URI impl { 
  to String => $"{protocal}://{host}/{path}"
}