Machine : Entity {
  addresses: [ IP `Address ]
}

Machine protocol { 
  processes          -> [ Process ]
  processors         -> [ Processor ]
  network`Interfaces -> [ Network `Interface ]
  drives             -> [ Drive ]
  mounts             -> [ Mount ]
}

Machine `Termination event { 
  machine: Machine
}