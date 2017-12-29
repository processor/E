Media<T> : Inline {
  stream: T
}

Media<T: Audio | Video> protocol {
  * | load & loaded  : loading
    | buffer         : buffering  
    | play           : playing
    | seek & seeked  : seeking
    | volume `Change : adjusting `Volume
    | mute
    | pause          : paused
    ↺ 
  | stall            : stalled
  | end ∎            : ended

  blob   -> T
  volume -> Percent
  
  mute  () -> * Ok | Error
  play  () -> * Ok | Error
  pause () -> * Ok | Error
  end   () -> * Ok | Error
}

Audio class : Media<T: OPUS | MP3>              { }
Video class : Media<T: WEBM | H264 | H265>      { }
Image class : Media<T: JPEG | GIF | PNG | WEBP> { }