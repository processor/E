Seekable protocol { 
  position : Int64 ≥ 0
  length   : Int64 > 0

  seek (Int64 ≥ 0) -> 
   | Seeked
   | OutsideRange	
}