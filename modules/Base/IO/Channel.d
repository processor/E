// Channels replace sequences, streams,
// * byte.. :≡ Readable`Channel of zero or more bytes

Channel<T> protocol { 
  * open     : active
  * complete : completed

  status -> Channel`Status
  unread -> i64 ≥ 0

  read () -> 
    | T
	  | Awaiter
	  | ∎

  write (T) async -> 
    | Commited 
	  | Awaiter 
	  | NotConnectedError

}

Seekable`Channel = Channel & Seekable



// Create a multicast wrapper around a channel