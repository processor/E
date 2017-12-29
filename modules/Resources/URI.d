// URI: Uniform Resource Identifier
// URL: Uniform Resource Locator

URI struct {
  protocol : String
  host     : String
  port     : i16 > 0
  path     : String

  to String => $"{protocol}://{host}/{path}"
}