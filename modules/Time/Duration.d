Duration type {
  from (Milliseconds) {

  }

  from (Seconds) {
    
  }
}

Millisecond unit
Second unit

Minute unit {
  to Day => value / 1440
}

Year unit {

}

s  postfix (value: Integer) => Second
ms postfix (value: Integer) => Millisecond(value)