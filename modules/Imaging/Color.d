module Color { 
  RGB   type { r, g,  b  : byte }
  YCbCr type { y, cB, cR : byte }

  RGB impl { 
    to YCbCr { 
      let r = r as f32;
      let g = g as f32;
      let b = b as f32;

      let y  =  0.299f32  * r + 0.587f32  * g + 0.114f32  * b
      let cb = -0.1687f32 * r - 0.3313f32 * g + 0.5f32    * b + 128f32
      let cr =  0.5f32    * r - 0.4187f32 * g - 0.0813f32 * b + 128f32

      return YCbCr(y as u8, cb as u8, cr as u8)
    }
  }
}