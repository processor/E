// URI: Uniform Resource Identifier
// URL: Uniform Resource Locator

URI struct {
  protocol : String
  host     : String
  port     : Int16
  path     : String

  to String => $"{protocol}://{host}/{path}"
}