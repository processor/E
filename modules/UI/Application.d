from diagnostics import Diagnostic

Application protocol { 
  network     :   Network
  environment :   Environment
  services    : [ Service ]
  screen      :   Screen
  reactor     :   Reactor
  user        :   User
  devices     : [ Device ] // e.g. keyboard, mouse, pen
}

Network protocol {
  requests: [ Requests ]  // inflight
}

UI protocol { 
  views: [ View ]
}