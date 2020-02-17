Process protocol {
  * run         : running
  | terminate ∎ : terminated
}

Process actor {
  id        :    Identity
  program   :    Program
  metric    : [] Metric

  terminate() { }
}

// metrics include errors, data transfer, etc.

// a series of actions or steps taken in order to achieve a particular end.