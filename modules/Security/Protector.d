Protector<T> protocol {
  protect(unprotected: T)            -> Protected<T>
  unprotect(protected: Protected<T>) -> T
}