Protected<T> protocol {
  unprotect() -> T
}

// An encrypted record...

Protected<T> struct {
  value  : T 
  policy : Policy
}