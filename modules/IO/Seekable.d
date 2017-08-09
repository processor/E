Seekable protocol {
  position : i64 >= 0
  length   : i64 >= 0

  seek (position: i64 >= 0) -> 
   | Seeked
   | OutsideRange	
}