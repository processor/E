// Channels replace sequences, streams,
// * byte.. :≡ Readable`Channel of zero or more bytes

Channel<T> protocol { 
  * open     : active
  * complete : completed

  status -> Status
  unread -> i64 ≥ 0

  read () -> 
    | T
	  | Awaiter
	  | ∎

  write (T) -> 
    | Commited 
	  | Awaiter
	  | NotConnectedError

  Status enum { Open, Closed }

}

Seekable`Channel = Channel & Seekable



// Create a multicast wrapper around a channel