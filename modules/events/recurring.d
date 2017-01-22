Trigger type { 
  interval   : Interval       // P1M, P1Y, ...
  start      : DateTime
  end        : DateTime
}

Trigger protocal {
  fire () -> Moment
}


// Fires
