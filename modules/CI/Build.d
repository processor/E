Build protocol {
  * initial  : pending
  * start    : building
  * fail     : failed
  * complete : completed
}

Build process {
  artifacts: [] Artifact

  // - Events
  Failed    event { }
  Completed event { }

  // ---
  Artifact record {
    path: Path
  }
}