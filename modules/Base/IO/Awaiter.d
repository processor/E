Awaiter<T> protocol { 
  reason : Empty | Throttled
  result : T
  ready  : event
}