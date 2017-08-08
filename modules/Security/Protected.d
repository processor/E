Protected<T> protocol {
  unprotect() -> T
}

// An encrypted record...

Protected<T> type {
  value  : T 
  policy : Policy
}