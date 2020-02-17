Host protocol {
  * run(Process)
  * terminate ∎ : terminated

  processors : [] Processor
  memory     : [] Memory
  interfaces : [] Networking::Interface
  volumes    : [] Storage::Volume
  processes  : [] Process

  // run -> Process
}

Host actor {
  addresses: [] IP::Address

  run(entry_point: Function) -> Process
}


Mount mount {
  path   : String
  host   : Host
  volume : Volume
}

// A volume is "Mounted" to a host to a specific path