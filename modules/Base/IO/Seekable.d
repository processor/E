Seekable protocol {
  position : Int64 ≥ 0
  length   : Int64 ≥ 0

  seek (position: Int64 ≥ 0) -> OK | OutsideRange	
}