 RGB<T> struct {
    r, g, b : T

    to YCbCr<T> {
      let r :f32 = r;
      let g :f32 = g;
      let b :f32 = b;

      let y  =  0.299f32  * r + 0.587f32  * g + 0.114f32  * b
      let cb = -0.1687f32 * r - 0.3313f32 * g + 0.5f32    * b + 128f32
      let cr =  0.5f32    * r - 0.4187f32 * g - 0.0813f32 * b + 128f32

      return YCbCr<T>(y, cb, cr)
    }
  }