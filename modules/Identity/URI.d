// URI: Uniform Resource Identifier
// URL: Uniform Resource Locator

URI struct {
  scheme   : Scheme       
  host     : String       // AKA authority
  port     : i16 > 0
  path     : Path
  query    : String
  fragment : String

  to String => $"{scheme}://{host}/{path}"

  Scheme enum { 
    http
    https
    ftp
    mailto
    file
    data
    irc
  }
}


// URI = scheme:[//authority]path[?query][#fragment]




// A Uniform Resource Identifier (URI) is a string of characters that unambiguously identifies a particular resource. 
// A Uniform Resource Name (URN) is a URI that identifies a resource by name in a particular namespace
// A Uniform Resource Locator (URL) is a URI that specifies the means of acting upon or obtaining the representation of a resource, i.e. specifying both its primary access mechanism and network location. 

// isbn:0-486-27557-4
