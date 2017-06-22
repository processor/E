using diagnostics

App protocol { 
  network     : Network
  environment : ?
  diagnostics : [ ] Diagnostic
  services    : [ ] Service
  ui          : UI
  screen      : Screen
  reactor     : Reactor
  user        : User
  devices     : [ ] Devices       // e.g. keyboard, mouse, pen
}

Network protocol {
  requests: [ ] Requests  // inflight
}

UI protocol { 
  views: [ ] View
}