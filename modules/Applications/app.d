using diagnostics

App protocal { 
  network     : Network
  environment : ?
  diagnostics : [ ] Diagnostic
  services    : [ ] Service
  ui          : UI
  screen      : Canvas
  reactor     : Reactor
  user        : User
  devices     : [ ] Devices       // e.g. keyboard, mouse, pen
}

Network protocal {
  requests: [ ] Requests  // inflight
}

UI protocal { 
  views: [ ] View
}