Host protocol { 
  processors         -> [ Processor ]
  network`Interfaces -> [ Network `Interface ]
  drives             -> [ Drive ]
  mounts             -> [ Mount ]
  processes          -> [ Process ]

  // run -> Process
}

Host actor {
  addresses: [ IP `Address ]
}

Host `Termination event { 
  host: Host
}