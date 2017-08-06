Node<T> type {
  id       : Identity
  value    : T
  incoming : [ Node ]
  outgoing : [ Node ]
}

Edge<T> type {
  id     : Identity
  source : T
  target : T
}