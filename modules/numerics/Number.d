Number protocol : Arithmetic<Self> { 
  floor   () -> Self
  ceiling () -> Self
  clamp   (min: Self, max: Self) -> Self
  clamp   (range: Range<Self>)   -> Self
}