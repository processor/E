Media<T> : Inline { 
  blob: T
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
  
  mute  () -> Muted
  play  () -> Played
  pause () -> Paused
  end   () -> Ended
}

Audio type : Media<OPUS|MP3>                { }
Video type : Media<WEBM | H264 | H265>      { }
Image type : Media<JPEG | GIF | PNG | WEBP> { }