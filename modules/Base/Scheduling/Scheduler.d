Scheduler protocol { 
  schedule($0: Action)           -> Task
  schedule($0: Action, $1: Time) -> Task
}