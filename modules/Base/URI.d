// URI: Uniform Resource Identifier
// URL: Uniform Resource Locator

URI type {
  protocol : String
  host     : String
  port     : Int16
  path     : String
}

URI impl { 
  to String => $"{protocol}://{host}/{path}"
}