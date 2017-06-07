Machine : Entity {
  addresses: IP'Address [ ]
}

Machine protocal { 
  processes          -> [ ] Process 
  processors         -> [ ] Processor
  network`Interfaces -> [ ] Network'Interface
  drives             -> [ ] Drive
  mounts             -> [ ] Mount
}

Machine'Termination event { 
  machine: Machine
}