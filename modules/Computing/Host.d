Host protocol {
  * terminate : terminated

  processors         : [ Processor ]
  network`Interfaces : [ Network `Interface ]
  mounts             : [ Mount ]
  processes          : [ Process ]


  // run -> Process
}


Host actor {
  addresses: [ IP `Address ]

  run(entryPoint: Function) -> Process
}

Host `Termination event { 
  host: Host
}

Mount mount {
  path  : String
  host  : Host
  drive : Drive
}