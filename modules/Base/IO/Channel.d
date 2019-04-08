// Channels replace sequences, streams,
// * byte.. :≡ Readable`Channel of zero or more bytes

Channel protocol { 
  status -> Channel`Status
  unread -> Int64 ≥ 0

  read () -> 
    | * 
	  | Awaiter
	  | ∎

  write async -> 
    | Commited 
	  | Awaiter 
	  | NotConnectedError
}

Seekable`Channel = Channel & Seekable

Channel`Status enum {
  Closed
  Connected
  Terminated
}

Readable`Channel protocol {
  available : Int64 ≥ 0
  read ƒ    -> Message | Backpressure
}

Writeable`Channel protocol {
  async write ƒ(message: Message) -> OK | Awaiter | Closed
}

Channel`Awaiter<T> protocol { 
  reason : NoMessages | Throttled
  result : T
  ready  : event
}

// Create a multicast wrapper around a channel